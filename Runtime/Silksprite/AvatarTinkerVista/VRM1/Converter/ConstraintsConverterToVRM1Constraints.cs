using System;
using Silksprite.AvatarTinkerVista.Common.Converter;
using UniGLTF.Extensions.VRMC_node_constraint;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Converter
{
    public class ConstraintsConverterToVRM1Constraints : ConstraintsConverterBase<Transform, AtivGenerateConstraint>
    {
        protected override void ConvertConstraint(Transform context, AtivGenerateConstraint constraintFrom)
        {
            switch (constraintFrom.kind)
            {
                case AtivGenerateConstraint.ConstraintKind.None:
                    break;
                case AtivGenerateConstraint.ConstraintKind.Aim:
                    var vrmAim = constraintFrom.ActualTarget.gameObject.AddComponent<Vrm10AimConstraint>();
                    vrmAim.Weight = constraintFrom.weight;
                    vrmAim.Source = constraintFrom.source;
                    vrmAim.AimAxis = constraintFrom.aimAxis switch
                    {
                        AtivGenerateConstraint.AimAxis.PositiveX => AimAxis.PositiveX,
                        AtivGenerateConstraint.AimAxis.NegativeX => AimAxis.NegativeX,
                        AtivGenerateConstraint.AimAxis.PositiveY => AimAxis.PositiveY,
                        AtivGenerateConstraint.AimAxis.NegativeY => AimAxis.NegativeY,
                        AtivGenerateConstraint.AimAxis.PositiveZ => AimAxis.PositiveZ,
                        AtivGenerateConstraint.AimAxis.NegativeZ => AimAxis.NegativeZ,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                case AtivGenerateConstraint.ConstraintKind.Roll:
                    var vrmRoll = constraintFrom.ActualTarget.gameObject.AddComponent<Vrm10RollConstraint>();
                    vrmRoll.Weight = constraintFrom.weight;
                    vrmRoll.Source = constraintFrom.source;
                    vrmRoll.RollAxis = constraintFrom.rollAxis switch
                    {
                        AtivGenerateConstraint.RollAxis.X => RollAxis.X,
                        AtivGenerateConstraint.RollAxis.Y => RollAxis.Y,
                        AtivGenerateConstraint.RollAxis.Z => RollAxis.Z,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                case AtivGenerateConstraint.ConstraintKind.Rotation:
                    var vrmRotation = constraintFrom.ActualTarget.gameObject.AddComponent<Vrm10RotationConstraint>();
                    vrmRotation.Weight = constraintFrom.weight;
                    vrmRotation.Source = constraintFrom.source;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
