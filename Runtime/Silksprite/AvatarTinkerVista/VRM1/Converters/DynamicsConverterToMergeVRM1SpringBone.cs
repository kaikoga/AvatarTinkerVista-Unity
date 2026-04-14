using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converters;
using Silksprite.AvatarTinkerVista.Common.Utils;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Converters
{
    public class DynamicsConverterToMergeVRM1SpringBone : DynamicsConverterBase<
        Transform, AtivGenerateDynamics, AtivGenerateDynamicsColliderGroup, VRM10SpringBoneColliderGroup
    >
    {
        protected override bool TryConvertCollider(Transform context, AtivGenerateDynamicsColliderGroup ativ, out VRM10SpringBoneColliderGroup result)
        {
            var ativColliders = ativ.colliders.Where(collider => collider).ToArray();
            if (ativColliders.Length == 0)
            {
                result = null;
                return false;
            }

            result = ativ.gameObject.AddComponent<VRM10SpringBoneColliderGroup>();
            result.Name = ativ.gameObject.name;
            result.Colliders = ativColliders.Select(ativCollider => 
            {
                var vrm10Collider = ativCollider.ActualRootBone.gameObject.AddComponent<VRM10SpringBoneCollider>();
                switch (ativCollider.colliderType)
                {
                    case AtivGenerateDynamicsCollider.ColliderTypes.Sphere:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.Sphere;
                        vrm10Collider.Radius = ativCollider.radius;
                        break;
                    case AtivGenerateDynamicsCollider.ColliderTypes.Capsule:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.Capsule;
                        vrm10Collider.Radius = ativCollider.radius;
                        vrm10Collider.Tail = ativCollider.tail;
                        break;
                    case AtivGenerateDynamicsCollider.ColliderTypes.Plane:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.Plane;
                        vrm10Collider.Radius = ativCollider.radius;
                        vrm10Collider.Normal = ativCollider.normal;
                        break;
                    case AtivGenerateDynamicsCollider.ColliderTypes.SphereInside:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.SphereInside;
                        vrm10Collider.Radius = ativCollider.radius;
                        break;
                    case AtivGenerateDynamicsCollider.ColliderTypes.CapsuleInside:
                        vrm10Collider.ColliderType = VRM10SpringBoneColliderTypes.CapsuleInside;
                        vrm10Collider.Radius = ativCollider.radius;
                        vrm10Collider.Tail = ativCollider.tail;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return vrm10Collider;
            }).ToList();
            return true;
        }

        protected override void ConvertDynamics(Transform context, AtivGenerateDynamics ativ, Dictionary<AtivGenerateDynamicsColliderGroup, VRM10SpringBoneColliderGroup> vrm10ColliderGroups)
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
            var vrm10Spring = new Vrm10InstanceSpringBone.Spring(ativ.gameObject.name)
            {
                ColliderGroups = ativ.colliderGroups
                    .Select(vrm10ColliderGroups.GetValueOrDefault)
                    .Where(colliderGroup => colliderGroup)
                    .ToList(),
                Joints = vrm10SpringJoints,
                Center = ativ.center
            };

            var mergeSpringBone = context.FindOrCreateSecondary(ativ.gameObject.name).gameObject.AddComponent<AtivMergeVRM1SpringBones>(); 
            mergeSpringBone.springs.Add(vrm10Spring);
            mergeSpringBone.colliderGroups.AddRange(vrm10ColliderGroups.Values);
        }
    }
}
