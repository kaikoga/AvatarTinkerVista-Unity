using System;
using Silksprite.AvatarTinkerVista.Converter;
using UniGLTF.Extensions.VRMC_node_constraint;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Converter
{
    public class ConstraintsConverterToVRM1Constraints : ConstraintsConverterBase<Transform, AtivGenerateVRM1Constraint>
    {
        protected override void ConvertConstraint(Transform context, AtivGenerateVRM1Constraint constraintFrom)
        {
            switch (constraintFrom.kind)
            {
                case AtivGenerateVRM1Constraint.ConstraintKind.Aim:
                    var vrmAim = constraintFrom.ActualTarget.gameObject.AddComponent<Vrm10AimConstraint>();
                    vrmAim.Weight = constraintFrom.weight;
                    vrmAim.Source = constraintFrom.source;
                    vrmAim.AimAxis = AimAxis.NegativeX;
                    break;
                case AtivGenerateVRM1Constraint.ConstraintKind.Roll:
                    var vrmRoll = constraintFrom.ActualTarget.gameObject.AddComponent<Vrm10RollConstraint>();
                    vrmRoll.Weight = constraintFrom.weight;
                    vrmRoll.Source = constraintFrom.source;
                    vrmRoll.RollAxis = constraintFrom.rollAxis switch
                    {
                        AtivGenerateVRM1Constraint.RollAxis.X => RollAxis.X,
                        AtivGenerateVRM1Constraint.RollAxis.Y => RollAxis.Y,
                        AtivGenerateVRM1Constraint.RollAxis.Z => RollAxis.Z,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                case AtivGenerateVRM1Constraint.ConstraintKind.Rotation:
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
