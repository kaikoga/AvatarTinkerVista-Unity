using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Converter;
using Silksprite.AvatarTinkerVista.Ndmf;
using Silksprite.AvatarTinkerVista.Utils;
using UnityEngine;
using VRM;

namespace Silksprite.AvatarTinkerVista.VRM0.Converter
{
    public class DynamicsConverterToVRM0SpringBone : DynamicsConverterBase<
        Transform, AtivGenerateDynamics, AtivGenerateDynamicsColliderGroup, VRMSpringBoneColliderGroup[]
    >
    {
        protected override bool TryConvertCollider(Transform avatarRootTransform, AtivGenerateDynamicsColliderGroup ativ, out VRMSpringBoneColliderGroup[] result)
        {
            var ativColliders = ativ.colliders
                .Where(collider => collider)
                .GroupBy(collider => collider.ActualRootBone)
                .ToArray();
            if (ativColliders.Any())
            {
                result = null;
                return false;
            }

            result = ativColliders
                .Select(g =>
                {
                    var vrmColliderGroup = ativ.gameObject.AddComponent<VRMSpringBoneColliderGroup>();
                    vrmColliderGroup.Colliders = g
                        .Where(ativCollider => ativCollider.colliderType == AtivGenerateDynamicsCollider.ColliderTypes.Sphere)
                        .Select(ativCollider => new VRMSpringBoneColliderGroup.SphereCollider
                        {
                            Offset = ativCollider.offset,
                            Radius = ativCollider.radius
                        }).ToArray();
                    return vrmColliderGroup;
                }).ToArray();
            return true;
        }

        protected override void ConvertDynamics(Transform avatarRootTransform, AtivGenerateDynamics ativ, Dictionary<AtivGenerateDynamicsColliderGroup, VRMSpringBoneColliderGroup[]> vrmColliderGroups)
        {
            var secondary = avatarRootTransform.FindOrCreateSecondary(ativ.gameObject.name);
            var vrmSpringBone = secondary.gameObject.AddComponent<VRMSpringBone>();
            vrmSpringBone.m_stiffnessForce = ativ.stiffness;
            vrmSpringBone.m_gravityPower = ativ.gravityPower;
            vrmSpringBone.m_gravityDir = ativ.gravityDir;
            vrmSpringBone.m_dragForce = ativ.dragForce;
            vrmSpringBone.m_hitRadius = ativ.radius;
            vrmSpringBone.RootBones = new List<Transform>
            {
                ativ.ActualRootBone
            };
            vrmSpringBone.m_center = ativ.center;
            vrmSpringBone.ColliderGroups = vrmColliderGroups.Values
                .SelectMany(values => values)
                .Distinct().ToArray();
        }
    }
}
