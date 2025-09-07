using System;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using Silksprite.AvatarTinkerVista.Utils;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Generate VRM0+1 SpringBone Collider")]
    public class AtivGenerateVrmSpringBoneCollider : AtivGeneratingComponent
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
                    gizmos.Color = Color.magenta;
                    gizmos.DrawWireSphereLocal(transform, offset, radius);
                    break;
                case ColliderTypes.Capsule:
                    gizmos.Color = Color.magenta;
                    gizmos.DrawWireCapsuleLocal(transform, offset, tail, radius);
                    break;
                case ColliderTypes.Plane:
                    gizmos.Color = Color.magenta;
                    gizmos.DrawPlaneLocal(transform, offset, normal, 0.05f);
                    break;
                case ColliderTypes.SphereInside:
                    gizmos.Color = new Color(0.5f, 0, 1.0f);
                    gizmos.DrawWireSphereLocal(transform, offset, radius);
                    break;
                case ColliderTypes.CapsuleInside:
                    gizmos.Color = new Color(0.5f, 0, 1.0f);
                    gizmos.DrawWireCapsuleLocal(transform, offset, tail, radius);
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
