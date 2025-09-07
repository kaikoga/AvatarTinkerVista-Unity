using System.Collections.Generic;
using System.Linq;
using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Utils;
using UnityEngine;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class GenerateVrm0SpringBonesPass : Pass<GenerateVrm0SpringBonesPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmMeta = context.AvatarRootTransform.GetComponent<VRMMeta>();
            if (!vrmMeta) return;

            var vrm0ColliderGroups = new Dictionary<AtivGenerateVrmSpringBoneColliderGroup, VRMSpringBoneColliderGroup[]>();
            foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivGenerateVrmSpringBoneColliderGroup>())
            {
                var vrmColliderGroup = GenerateSpringBoneColliderGroup(context.AvatarRootTransform, ativ);
                if (vrmColliderGroup != null)
                {
                    vrm0ColliderGroups.Add(ativ, vrmColliderGroup);
                }
            }
            foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivGenerateVrmSpringBones>())
            {
                GenerateSpringBones(context.AvatarRootTransform, ativ, vrm0ColliderGroups);
            }
        }

        VRMSpringBoneColliderGroup[] GenerateSpringBoneColliderGroup(Transform avatarRootTransform, AtivGenerateVrmSpringBoneColliderGroup ativ)
        {
            var ativColliders = ativ.colliders
                .Where(collider => collider)
                .GroupBy(collider => collider.ActualRootBone)
                .ToArray();
            if (ativColliders.Any()) return null;

            return ativColliders
                .Select(g =>
                {
                    var vrmColliderGroup = ativ.gameObject.AddComponent<VRMSpringBoneColliderGroup>();
                    vrmColliderGroup.Colliders = g
                        .Where(ativCollider => ativCollider.colliderType == AtivGenerateVrmSpringBoneCollider.ColliderTypes.Sphere)
                        .Select(ativCollider => new VRMSpringBoneColliderGroup.SphereCollider
                        {
                            Offset = ativCollider.offset,
                            Radius = ativCollider.radius
                        }).ToArray();
                    return vrmColliderGroup;
                }).ToArray();
        }

        void GenerateSpringBones(Transform avatarRootTransform, AtivGenerateVrmSpringBones ativ, Dictionary<AtivGenerateVrmSpringBoneColliderGroup, VRMSpringBoneColliderGroup[]> vrmColliderGroups)
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
