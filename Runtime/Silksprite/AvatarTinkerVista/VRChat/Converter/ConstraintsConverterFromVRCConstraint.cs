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
                    var ativAim = secondary.gameObject.AddComponent<AtivGenerateVRM1Constraint>();
                    ativAim.kind = AtivGenerateVRM1Constraint.ConstraintKind.Aim;
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
                    var ativLookAt = secondary.gameObject.AddComponent<AtivGenerateVRM1Constraint>();
                    ativLookAt.kind = AtivGenerateVRM1Constraint.ConstraintKind.Aim;
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
                    var ativRotation = secondary.gameObject.AddComponent<AtivGenerateVRM1Constraint>();
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
                    var ativRotation = secondary.gameObject.AddComponent<AtivGenerateVRM1Constraint>();
                    ativRotation.kind = AtivGenerateVRM1Constraint.ConstraintKind.Rotation;
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
        
        static AtivGenerateVRM1Constraint.AimVector GuessAimAxis(Vector3 vrcAimAxis)
        {
            return new(float abs, AtivGenerateVRM1Constraint.AimVector value)[]
            {
                (vrcAimAxis.x, AtivGenerateVRM1Constraint.AimVector.PositiveX),
                (-vrcAimAxis.x, AtivGenerateVRM1Constraint.AimVector.NegativeX),
                (vrcAimAxis.y, AtivGenerateVRM1Constraint.AimVector.PositiveY),
                (-vrcAimAxis.y, AtivGenerateVRM1Constraint.AimVector.NegativeY),
                (vrcAimAxis.z, AtivGenerateVRM1Constraint.AimVector.PositiveZ),
                (-vrcAimAxis.z, AtivGenerateVRM1Constraint.AimVector.NegativeZ)
            }.OrderByDescending(r => r.abs).Select(r => r.value).First();
        }

        static (AtivGenerateVRM1Constraint.ConstraintKind constraintKind, AtivGenerateVRM1Constraint.RollAxis rollAxis) GuessRollAxisOrRotation(VRCRotationConstraintBase vrcRotation)
        {
            return (vrcRotation.AffectsRotationX, vrcRotation.AffectsRotationY, vrcRotation.AffectsRotationZ) switch
            {
                (true, false, false) => (AtivGenerateVRM1Constraint.ConstraintKind.Roll, AtivGenerateVRM1Constraint.RollAxis.X),
                (false, true, false) => (AtivGenerateVRM1Constraint.ConstraintKind.Roll, AtivGenerateVRM1Constraint.RollAxis.Y),
                (false, false, true) => (AtivGenerateVRM1Constraint.ConstraintKind.Roll, AtivGenerateVRM1Constraint.RollAxis.Z),
                _ => (AtivGenerateVRM1Constraint.ConstraintKind.Rotation, default)
            };
        }
    }
}
