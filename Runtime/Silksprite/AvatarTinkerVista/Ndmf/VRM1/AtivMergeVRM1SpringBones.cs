using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Merge VRM1 SpringBones")]
    [DisallowMultipleComponent]
    [HelpURL("https://docs.kaikoga.net/ativ/ndmf_components/ativ_merge_vrm1_springbones")]
    public class AtivMergeVRM1SpringBones : AtivTransformingComponent
    {
        public List<VRM10SpringBoneColliderGroup> colliderGroups = new List<VRM10SpringBoneColliderGroup>();
        public List<Vrm10InstanceSpringBone.Spring> springs = new List<Vrm10InstanceSpringBone.Spring>();

        #region copy of Vrm10Instance.OnDrawGizmosSelected

        void OnDrawGizmosSelected()
        {
            static Color JointColor(VRM10SpringBoneJoint joint)
            {
                return Color.green;
            }
        
            foreach (var spring in springs)
            {
                var joints = spring.Joints;
                if (joints.Count > 0)
                {
                    var backup = Gizmos.matrix;
                    Gizmos.matrix = Matrix4x4.identity;
                    VRM10SpringBoneJoint lastJoint = joints[0];
                    for (int i = 1; i < joints.Count; ++i)
                    {
                        var joint = joints[i];
                        Gizmos.color = JointColor(lastJoint);
                        if (joint != null && lastJoint != null)
                        {
                            Gizmos.DrawLine(lastJoint.transform.position, joint.transform.position);
                        }
                        lastJoint = joint;
                    }
                    Gizmos.matrix = backup;
                }
            }
        }

        #endregion

    }

}
