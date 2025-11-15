using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converter;
using Silksprite.AvatarTinkerVista.Common.Utils;
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
                case VRCAimConstraintBase:
                case VRCLookAtConstraintBase:
                case VRCRotationConstraintBase:
                case VRCParentConstraintBase:
                case VRCScaleConstraintBase:
                case VRCPositionConstraintBase:
                    break;
                default:
                    // unknown
                    return;
            }
            var secondary = context.transform.FindOrCreateSecondary(constraintFrom.gameObject.name);
            var ativConstraint = secondary.gameObject.AddComponent<AtivGenerateConstraint>();
            switch (constraintFrom)
            {
                case VRCAimConstraintBase vrcAim:
                    ativConstraint.kind = AtivGenerateConstraint.ConstraintKind.Aim;
                    ativConstraint.aimAxis = GuessAimAxis(vrcAim.AimAxis);
                    break;
                case VRCLookAtConstraintBase:
                    ativConstraint.kind = AtivGenerateConstraint.ConstraintKind.Aim;
                    break;
                case VRCRotationConstraintBase vrcRotation:
                    (ativConstraint.kind, ativConstraint.rollAxis) = GuessRollAxisOrRotation(vrcRotation);
                    break;
                case VRCParentConstraintBase:
                    ativConstraint.kind = AtivGenerateConstraint.ConstraintKind.Rotation;
                    break;
                case VRCScaleConstraintBase:
                case VRCPositionConstraintBase:
                    // not supported
                    break;
            }
            foreach (var vrcSource in constraintFrom.Sources.Take(1))
            {
                ativConstraint.source = vrcSource.SourceTransform;
                ativConstraint.target = constraintFrom.GetEffectiveTargetTransform();
                ativConstraint.weight = constraintFrom.GlobalWeight * vrcSource.Weight;
            }
        }
        
        static AtivGenerateConstraint.AimAxis GuessAimAxis(Vector3 vrcAimAxis)
        {
            return new(float abs, AtivGenerateConstraint.AimAxis value)[]
            {
                (vrcAimAxis.x, AtivGenerateConstraint.AimAxis.PositiveX),
                (-vrcAimAxis.x, AtivGenerateConstraint.AimAxis.NegativeX),
                (vrcAimAxis.y, AtivGenerateConstraint.AimAxis.PositiveY),
                (-vrcAimAxis.y, AtivGenerateConstraint.AimAxis.NegativeY),
                (vrcAimAxis.z, AtivGenerateConstraint.AimAxis.PositiveZ),
                (-vrcAimAxis.z, AtivGenerateConstraint.AimAxis.NegativeZ)
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
