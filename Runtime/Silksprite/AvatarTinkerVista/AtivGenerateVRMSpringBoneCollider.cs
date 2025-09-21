using System;
using Silksprite.AvatarTinkerVista.Base;
using Silksprite.AvatarTinkerVista.Utils;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Generate VRM0+1 SpringBone Collider")]
    public class AtivGenerateVRMSpringBoneCollider : AtivGeneratingComponent
    {
        public ColliderTypes colliderType;

        public Transform rootBone;
        public Vector3 offset;
        [Range(0, 1.0f)]
        public float radius;
        public Vector3 tail;
        public Vector3 normal = Vector3.up;

        public Transform ActualRootBone => rootBone ? rootBone : transform;

        public void OnValidate()
        {
            normal = normal.normalized;
        }

        void OnDrawGizmosSelected()
        {
            DrawGizmos();
        }

        public void DrawGizmos()
        {
            using var gizmos = new AtivGizmos();
            switch (colliderType)
            {
                case ColliderTypes.Sphere:
                    gizmos.Color = AtivGizmoStyle.Current.Collider;
                    gizmos.DrawWireSphereLocal(ActualRootBone, offset, radius);
                    break;
                case ColliderTypes.Capsule:
                    gizmos.Color = AtivGizmoStyle.Current.Collider;
                    gizmos.DrawWireCapsuleLocal(ActualRootBone, offset, tail, radius);
                    break;
                case ColliderTypes.Plane:
                    gizmos.Color = AtivGizmoStyle.Current.Collider;
                    gizmos.DrawPlaneLocal(ActualRootBone, offset, normal, 0.05f);
                    break;
                case ColliderTypes.SphereInside:
                    gizmos.Color = AtivGizmoStyle.Current.InnerCollider;
                    gizmos.DrawWireSphereLocal(ActualRootBone, offset, radius);
                    break;
                case ColliderTypes.CapsuleInside:
                    gizmos.Color = AtivGizmoStyle.Current.InnerCollider;
                    gizmos.DrawWireCapsuleLocal(ActualRootBone, offset, tail, radius);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public enum ColliderTypes
        {
            Sphere,
            Capsule,
            Plane,
            SphereInside,
            CapsuleInside,
        }
    }
}
