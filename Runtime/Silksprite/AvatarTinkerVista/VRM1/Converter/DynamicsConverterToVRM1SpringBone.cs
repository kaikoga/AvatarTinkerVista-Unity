using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Converter;
using Silksprite.AvatarTinkerVista.Ndmf;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Converter
{
    public class DynamicsConverterToVRM1SpringBone : DynamicsConverterBase<
        Vrm10Instance, AtivGenerateVRMSpringBones, AtivGenerateVRMSpringBoneColliderGroup, VRM10SpringBoneColliderGroup
    >
    {
        protected override bool TryConvertCollider(Vrm10Instance vrm10Instance, AtivGenerateVRMSpringBoneColliderGroup ativ, out VRM10SpringBoneColliderGroup result)
        {
            var ativColliders = ativ.colliders.Where(collider => collider).ToArray();
            if (ativColliders.Length == 0)
            {
                result = null;
                return false;
            }

            result = vrm10Instance.gameObject.AddComponent<VRM10SpringBoneColliderGroup>();
            result.Colliders = ativColliders.Select(ativCollider => 
            {
                var vrm10Collider = ativCollider.ActualRootBone.gameObject.AddComponent<VRM10SpringBoneCollider>();
                switch (ativCollider.colliderType)
                {
                    case AtivGenerateVRMSpringBoneCollider.ColliderTypes.Sphere:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.Sphere;
                        vrm10Collider.Radius = ativCollider.radius;
                        break;
                    case AtivGenerateVRMSpringBoneCollider.ColliderTypes.Capsule:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.Capsule;
                        vrm10Collider.Radius = ativCollider.radius;
                        vrm10Collider.Tail = ativCollider.tail;
                        break;
                    case AtivGenerateVRMSpringBoneCollider.ColliderTypes.Plane:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.Plane;
                        vrm10Collider.Radius = ativCollider.radius;
                        vrm10Collider.Normal = ativCollider.normal;
                        break;
                    case AtivGenerateVRMSpringBoneCollider.ColliderTypes.SphereInside:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.SphereInside;
                        vrm10Collider.Radius = ativCollider.radius;
                        break;
                    case AtivGenerateVRMSpringBoneCollider.ColliderTypes.CapsuleInside:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.CapsuleInside;
                        vrm10Collider.Radius = ativCollider.radius;
                        vrm10Collider.Tail = ativCollider.tail;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return vrm10Collider;
            }).ToList();
            return result;
        }

        protected override void ConvertDynamics(Vrm10Instance vrm10Instance, AtivGenerateVRMSpringBones ativ, Dictionary<AtivGenerateVRMSpringBoneColliderGroup, VRM10SpringBoneColliderGroup> vrm10ColliderGroups)
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
