using System;
using System.Collections.Generic;
using System.Linq;
using nadena.dev.ndmf;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class GenerateVrm1SpringBonesPass : Pass<GenerateVrm1SpringBonesPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrm10Instance = context.AvatarRootTransform.GetComponent<Vrm10Instance>();
            if (!vrm10Instance) return;

            var vrm10ColliderGroups = new Dictionary<AtivGenerateVrmSpringBoneColliderGroup, VRM10SpringBoneColliderGroup>();
            foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivGenerateVrmSpringBoneColliderGroup>())
            {
                var vrm10ColliderGroup = GenerateSpringBoneColliderGroup(vrm10Instance, ativ);
                if (vrm10ColliderGroup)
                {
                    vrm10ColliderGroups.Add(ativ, vrm10ColliderGroup);
                }
            }
            foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivGenerateVrmSpringBones>())
            {
                GenerateSpringBones(vrm10Instance, ativ, vrm10ColliderGroups);
            }
        }

        VRM10SpringBoneColliderGroup GenerateSpringBoneColliderGroup(Vrm10Instance vrm10Instance, AtivGenerateVrmSpringBoneColliderGroup ativ)
        {
            var ativColliders = ativ.colliders.Where(collider => collider).ToArray();
            if (ativColliders.Length == 0) return null;

            var vrm10ColliderGroup = vrm10Instance.gameObject.AddComponent<VRM10SpringBoneColliderGroup>();
            vrm10ColliderGroup.Colliders = ativColliders.Select(ativCollider => 
            {
                var vrm10Collider = ativCollider.ActualRootBone.gameObject.AddComponent<VRM10SpringBoneCollider>();
                switch (ativCollider.colliderType)
                {
                    case AtivGenerateVrmSpringBoneCollider.ColliderTypes.Sphere:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.Sphere;
                        vrm10Collider.Radius = ativCollider.radius;
                        break;
                    case AtivGenerateVrmSpringBoneCollider.ColliderTypes.Capsule:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.Capsule;
                        vrm10Collider.Radius = ativCollider.radius;
                        vrm10Collider.Tail = ativCollider.tail;
                        break;
                    case AtivGenerateVrmSpringBoneCollider.ColliderTypes.Plane:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.Plane;
                        vrm10Collider.Radius = ativCollider.radius;
                        vrm10Collider.Normal = ativCollider.normal;
                        break;
                    case AtivGenerateVrmSpringBoneCollider.ColliderTypes.SphereInside:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.SphereInside;
                        vrm10Collider.Radius = ativCollider.radius;
                        break;
                    case AtivGenerateVrmSpringBoneCollider.ColliderTypes.CapsuleInside:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.CapsuleInside;
                        vrm10Collider.Radius = ativCollider.radius;
                        vrm10Collider.Tail = ativCollider.tail;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return vrm10Collider;
            }).ToList();
            return vrm10ColliderGroup;
        }

        void GenerateSpringBones(Vrm10Instance vrm10Instance, AtivGenerateVrmSpringBones ativ, Dictionary<AtivGenerateVrmSpringBoneColliderGroup, VRM10SpringBoneColliderGroup> vrm10ColliderGroups)
        {
            var joints = ativ.GuessJoints().ToArray();
            if (joints.Any(joint => joint.TryGetComponent<VRM10SpringBoneJoint>(out _)))
            {
                return;
            }
            var vrm10SpringJoints = joints.Select(joint =>
            {
                var springJoint = joint.gameObject.AddComponent<VRM10SpringBoneJoint>();
                springJoint.m_stiffnessForce = ativ.stiffness;
                springJoint.m_gravityPower = ativ.gravityPower;
                springJoint.m_gravityDir = ativ.gravityDir;
                springJoint.m_dragForce = ativ.dragForce;
                springJoint.m_jointRadius = ativ.radius;
                return springJoint;
            }).ToList();
            var vrm10Spring = new Vrm10InstanceSpringBone.Spring(ativ.name)
            {
                ColliderGroups = ativ.colliderGroups
                    .Select(vrm10ColliderGroups.GetValueOrDefault)
                    .Where(colliderGroup => colliderGroup)
                    .ToList(),
                Joints = vrm10SpringJoints,
                Center = ativ.center
            };
            vrm10Instance.SpringBone.Springs.Add(vrm10Spring);
            vrm10Instance.SpringBone.ColliderGroups.AddRange(vrm10ColliderGroups.Values);
        }
    }
}
