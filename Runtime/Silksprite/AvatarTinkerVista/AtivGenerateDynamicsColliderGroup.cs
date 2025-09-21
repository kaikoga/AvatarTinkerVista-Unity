using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Generate VRM0+1 SpringBone Collider Group")]
    public class AtivGenerateDynamicsColliderGroup : AtivGeneratingComponent
    {
        public List<AtivGenerateDynamicsCollider> colliders = new List<AtivGenerateDynamicsCollider>();
        
        void OnDrawGizmosSelected()
        {
            foreach (var c in colliders.Where(c => c))
            {
                c.DrawGizmos();
            }
        }
    }
}
