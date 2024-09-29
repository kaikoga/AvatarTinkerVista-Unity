#if ATIV_VRM1

using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Merge VRM1 SpringBones")]
    [DisallowMultipleComponent]
    public class AtivMergeVrm1SpringBones : AtivTransformingComponent
    {

        public List<VRM10SpringBoneColliderGroup> colliderGroups = new List<VRM10SpringBoneColliderGroup>();
        public List<Vrm10InstanceSpringBone.Spring> springs = new List<Vrm10InstanceSpringBone.Spring>();

        #region copy of Vrm10Instance.OnDrawGizmosSelected

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            foreach (var spring in springs)
            {
                foreach (var (head, tail) in spring.EnumHeadTail())
                {
                    Gizmos.DrawLine(head.transform.position, tail.transform.position);
                    Gizmos.DrawWireSphere(tail.transform.position, head.m_jointRadius);
                }
            }
        }

        #endregion

    }

}

#endif
