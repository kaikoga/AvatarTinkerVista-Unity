using System.Linq;
using Silksprite.AvatarTinkerVista.Converter;
using Silksprite.AvatarTinkerVista.Utils;
using UnityEngine;
using VRC.Dynamics;
using VRC.Dynamics.ManagedTypes;

namespace Silksprite.AvatarTinkerVista.VRChat.Converter
{
    public class ConstraintsConverterFromVRCConstraint : ConstraintsConverterBase<Transform, VRCConstraintBase>
    {
        protected override void ConvertConstraint(Transform context, VRCConstraintBase constraintFrom)
        {
            switch (constraintFrom)
            {
                case VRCAimConstraintBase vrcAim:
                {
                    var secondary = context.transform.FindOrCreateSecondary(vrcAim.gameObject.name);
                    var ativAim = secondary.gameObject.AddComponent<AtivGenerateConstraint>();
                    ativAim.kind = AtivGenerateConstraint.ConstraintKind.Aim;
                    ativAim.aimAxis = GuessAimAxis(vrcAim.AimAxis);
                    foreach (var vrcSource in vrcAim.Sources.Take(1))
                    {
                        ativAim.source = vrcSource.SourceTransform;
                        ativAim.target = vrcAim.GetEffectiveTargetTransform();
                        ativAim.weight = vrcAim.GlobalWeight * vrcSource.Weight;
                    }
                    break;
                }
                case VRCLookAtConstraintBase vrcLookAt:
                {
                    var secondary = context.transform.FindOrCreateSecondary(vrcLookAt.gameObject.name);
                    var ativLookAt = secondary.gameObject.AddComponent<AtivGenerateConstraint>();
                    ativLookAt.kind = AtivGenerateConstraint.ConstraintKind.Aim;
                    foreach (var vrcSource in vrcLookAt.Sources.Take(1))
                    {
                        ativLookAt.source = vrcSource.SourceTransform;
                        ativLookAt.target = vrcLookAt.GetEffectiveTargetTransform();
                        ativLookAt.weight = vrcLookAt.GlobalWeight * vrcSource.Weight;
                    }
                    break;
                }
                case VRCRotationConstraintBase vrcRotation:
                {
                    var secondary = context.transform.FindOrCreateSecondary(vrcRotation.gameObject.name);
                    var ativRotation = secondary.gameObject.AddComponent<AtivGenerateConstraint>();
                    (ativRotation.kind, ativRotation.rollAxis) = GuessRollAxisOrRotation(vrcRotation);
                    foreach (var vrcSource in vrcRotation.Sources.Take(1))
                    {
                        ativRotation.source = vrcSource.SourceTransform;
                        ativRotation.target = vrcRotation.GetEffectiveTargetTransform();
                        ativRotation.weight = vrcRotation.GlobalWeight * vrcSource.Weight;
                    }
                    break;
                }
                case VRCParentConstraintBase vrcParent:
                {
                    var secondary = context.transform.FindOrCreateSecondary(vrcParent.gameObject.name);
                    var ativRotation = secondary.gameObject.AddComponent<AtivGenerateConstraint>();
                    ativRotation.kind = AtivGenerateConstraint.ConstraintKind.Rotation;
                    foreach (var vrcSource in vrcParent.Sources.Take(1))
                    {
                        ativRotation.source = vrcSource.SourceTransform;
                        ativRotation.target = vrcParent.GetEffectiveTargetTransform();
                        ativRotation.weight = vrcParent.GlobalWeight * vrcSource.Weight;
                    }
                    break;
                }
                case VRCScaleConstraintBase:
                case VRCPositionConstraintBase:
                    // not supported
                    break;
            }
        }
        
        static AtivGenerateConstraint.AimVector GuessAimAxis(Vector3 vrcAimAxis)
        {
            return new(float abs, AtivGenerateConstraint.AimVector value)[]
            {
                (vrcAimAxis.x, AtivGenerateConstraint.AimVector.PositiveX),
                (-vrcAimAxis.x, AtivGenerateConstraint.AimVector.NegativeX),
                (vrcAimAxis.y, AtivGenerateConstraint.AimVector.PositiveY),
                (-vrcAimAxis.y, AtivGenerateConstraint.AimVector.NegativeY),
                (vrcAimAxis.z, AtivGenerateConstraint.AimVector.PositiveZ),
                (-vrcAimAxis.z, AtivGenerateConstraint.AimVector.NegativeZ)
            }.OrderByDescending(r => r.abs).Select(r => r.value).First();
        }

        static (AtivGenerateConstraint.ConstraintKind constraintKind, AtivGenerateConstraint.RollAxis rollAxis) GuessRollAxisOrRotation(VRCRotationConstraintBase vrcRotation)
        {
            return (vrcRotation.AffectsRotationX, vrcRotation.AffectsRotationY, vrcRotation.AffectsRotationZ) switch
            {
                (true, false, false) => (AtivGenerateConstraint.ConstraintKind.Roll, AtivGenerateConstraint.RollAxis.X),
                (false, true, false) => (AtivGenerateConstraint.ConstraintKind.Roll, AtivGenerateConstraint.RollAxis.Y),
                (false, false, true) => (AtivGenerateConstraint.ConstraintKind.Roll, AtivGenerateConstraint.RollAxis.Z),
                _ => (AtivGenerateConstraint.ConstraintKind.Rotation, default)
            };
        }
    }
}
