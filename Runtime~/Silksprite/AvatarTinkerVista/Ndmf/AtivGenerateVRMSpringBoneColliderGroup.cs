using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Generate VRM0+1 SpringBone Collider Group")]
    public class AtivGenerateVRMSpringBoneColliderGroup : AtivGeneratingComponent
    {
        public List<AtivGenerateVRMSpringBoneCollider> colliders = new List<AtivGenerateVRMSpringBoneCollider>();
        
        void OnDrawGizmosSelected()
        {
            foreach (var c in colliders.Where(c => c))
            {
                c.DrawGizmos();
            }
        }
    }
}
