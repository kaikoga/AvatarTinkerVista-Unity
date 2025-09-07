using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using Silksprite.AvatarTinkerVista.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Generate VRM0+1 SpringBones")]
    public class AtivGenerateVrmSpringBones : AtivGeneratingComponent
    {
        public float stiffness = 1.0f;
        public float gravityPower;
        public Vector3 gravityDir = Vector3.down;
        [Range(0, 1)]
        public float dragForce = 0.4f;
        public float radius = 0.02f;

        public Transform rootBone;
        public Transform center;

        public Transform ActualRootBone => rootBone ? rootBone : transform;

        public List<AtivGenerateVrmSpringBoneColliderGroup> colliderGroups = new List<AtivGenerateVrmSpringBoneColliderGroup>();

        public IEnumerable<Transform> GuessJoints()
        {
            var joint = ActualRootBone;
            while (joint)
            {
                yield return joint;
                if (joint.childCount == 0) break;
                joint = joint.OfType<Transform>().OrderBy(c => c.GetComponents<Component>().Length).First();
            }
        }

        void OnDrawGizmosSelected()
        {
            static Color JointColor(Transform joint)
            {
                return Color.yellow;
            }

            var joints = GuessJoints().ToArray();
            if (joints.Length > 0)
            {
                using var gizmos = new AtivGizmos();
                var lastJoint = joints[0];
                gizmos.Color = new Color(1, 0.75f, 0f);
                gizmos.DrawWireSphereLocal(lastJoint.transform, Vector3.zero, 0.01f);
                for (var i = 1; i < joints.Length; ++i)
                {
                    var joint = joints[i];
                    gizmos.Color = JointColor(lastJoint);
                    if (joint != null && lastJoint != null)
                    {
                        gizmos.DrawLineGlobal(lastJoint.transform.position, joint.transform.position);
                        gizmos.Color = Color.yellow;
                        gizmos.DrawWireSphereLocal(joint.transform, Vector3.zero, radius);
                    }
                    lastJoint = joint;
                }
            }
        }
    }

}
