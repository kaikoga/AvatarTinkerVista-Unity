using System;
using Silksprite.AvatarTinkerVista.Converter;
using Silksprite.AvatarTinkerVista.Ndmf;
using UniGLTF.Extensions.VRMC_node_constraint;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Converter
{
    public class ConstraintsConverterToVRM1Constraint : ConstraintsConverterBase<Transform, AtivGenerateVRMConstraint>
    {
        protected override void ConvertConstraint(Transform context, AtivGenerateVRMConstraint constraintFrom)
        {
            switch (constraintFrom.kind)
            {
                case AtivGenerateVRMConstraint.ConstraintKind.Aim:
                    var vrmAim = constraintFrom.ActualTarget.gameObject.AddComponent<Vrm10AimConstraint>();
                    vrmAim.Weight = constraintFrom.weight;
                    vrmAim.Source = constraintFrom.source;
                    vrmAim.AimAxis = AimAxis.NegativeX;
                    break;
                case AtivGenerateVRMConstraint.ConstraintKind.Roll:
                    var vrmRoll = constraintFrom.ActualTarget.gameObject.AddComponent<Vrm10RollConstraint>();
                    vrmRoll.Weight = constraintFrom.weight;
                    vrmRoll.Source = constraintFrom.source;
                    vrmRoll.RollAxis = constraintFrom.rollAxis switch
                    {
                        AtivGenerateVRMConstraint.RollAxis.X => RollAxis.X,
                        AtivGenerateVRMConstraint.RollAxis.Y => RollAxis.Y,
                        AtivGenerateVRMConstraint.RollAxis.Z => RollAxis.Z,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                case AtivGenerateVRMConstraint.ConstraintKind.Rotation:
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
