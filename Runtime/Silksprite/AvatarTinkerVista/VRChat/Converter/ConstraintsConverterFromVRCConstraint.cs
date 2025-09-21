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
                    var ativAim = secondary.gameObject.AddComponent<AtivGenerateVRMConstraint>();
                    ativAim.kind = AtivGenerateVRMConstraint.ConstraintKind.Aim;
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
                    var ativLookAt = secondary.gameObject.AddComponent<AtivGenerateVRMConstraint>();
                    ativLookAt.kind = AtivGenerateVRMConstraint.ConstraintKind.Aim;
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
                    var ativRotation = secondary.gameObject.AddComponent<AtivGenerateVRMConstraint>();
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
                    var ativRotation = secondary.gameObject.AddComponent<AtivGenerateVRMConstraint>();
                    ativRotation.kind = AtivGenerateVRMConstraint.ConstraintKind.Rotation;
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
        
        static AtivGenerateVRMConstraint.AimVector GuessAimAxis(Vector3 vrcAimAxis)
        {
            return new(float abs, AtivGenerateVRMConstraint.AimVector value)[]
            {
                (vrcAimAxis.x, AtivGenerateVRMConstraint.AimVector.PositiveX),
                (-vrcAimAxis.x, AtivGenerateVRMConstraint.AimVector.NegativeX),
                (vrcAimAxis.y, AtivGenerateVRMConstraint.AimVector.PositiveY),
                (-vrcAimAxis.y, AtivGenerateVRMConstraint.AimVector.NegativeY),
                (vrcAimAxis.z, AtivGenerateVRMConstraint.AimVector.PositiveZ),
                (-vrcAimAxis.z, AtivGenerateVRMConstraint.AimVector.NegativeZ)
            }.OrderByDescending(r => r.abs).Select(r => r.value).First();
        }

        static (AtivGenerateVRMConstraint.ConstraintKind constraintKind, AtivGenerateVRMConstraint.RollAxis rollAxis) GuessRollAxisOrRotation(VRCRotationConstraintBase vrcRotation)
        {
            return (vrcRotation.AffectsRotationX, vrcRotation.AffectsRotationY, vrcRotation.AffectsRotationZ) switch
            {
                (true, false, false) => (AtivGenerateVRMConstraint.ConstraintKind.Roll, AtivGenerateVRMConstraint.RollAxis.X),
                (false, true, false) => (AtivGenerateVRMConstraint.ConstraintKind.Roll, AtivGenerateVRMConstraint.RollAxis.Y),
                (false, false, true) => (AtivGenerateVRMConstraint.ConstraintKind.Roll, AtivGenerateVRMConstraint.RollAxis.Z),
                _ => (AtivGenerateVRMConstraint.ConstraintKind.Rotation, default)
            };
        }
    }
}
