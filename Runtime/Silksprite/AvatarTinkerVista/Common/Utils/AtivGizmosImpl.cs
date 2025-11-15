using System;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Utils
{
    class AtivGizmosImpl : IDisposable
    {
        readonly Matrix4x4 _lastMatrix = Gizmos.matrix;

        public Color Color
        {
            get => Gizmos.color;
            set => Gizmos.color = value;
        }

        public void DrawLineGlobal(Vector3 from, Vector3 to)
        {
            Gizmos.matrix = Matrix4x4.identity;
            Gizmos.DrawLine(from, to);
        }

        public void DrawSphereGlobal(Vector3 offset, float radius)
        {
            Gizmos.matrix = Matrix4x4.identity;
            Gizmos.DrawSphere(offset, radius);
        }

        public void DrawWireSphereGlobal(Vector3 offset, float radius)
        {
            Gizmos.matrix = Matrix4x4.identity;
            Gizmos.DrawWireSphere(offset, radius);
        }

        public void DrawWireCapsuleGlobal(Vector3 from, Vector3 to, float radius)
        {
            var d = to - from;
            Gizmos.matrix = Matrix4x4.TRS(from, Quaternion.FromToRotation(Vector3.forward, d), Vector3.one);
            var end = Vector3.forward * d.magnitude;
            Gizmos.DrawWireSphere(Vector3.zero, radius);
            Gizmos.DrawWireSphere(end, radius);
            var t1 = Vector3.right * radius;
            var t2 = Vector3.up * radius;
            Gizmos.DrawLine(t1, end + t1);
            Gizmos.DrawLine(t2, end + t2);
            Gizmos.DrawLine(-t1, end - t1);
            Gizmos.DrawLine(-t2, end - t2);
        }

        public void DrawPlaneGlobal(Vector3 offset, Vector3 normal, float radius)
        {
            DrawLineGlobal(offset, offset + normal * radius);
            var rotation = Quaternion.FromToRotation(Vector3.forward, normal);
            var t1 = rotation * Vector3.right;
            var t2 = rotation * Vector3.up;
            t1 *= radius / t1.magnitude;
            t2 *= radius / t2.magnitude;

            for (var i = -1f; i <= 1f; i++)
            {
                DrawLineGlobal(offset + t1 * i - t2, offset + t1 * i + t2);
                DrawLineGlobal(offset + t2 * i - t1, offset + t2 * i + t1);
            }
        }
        public void Dispose()
        {
            Gizmos.matrix = _lastMatrix;
        }
    }
}
