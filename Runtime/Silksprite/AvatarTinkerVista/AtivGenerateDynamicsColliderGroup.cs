using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Generate Dynamics Collider Group")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_generate_dynamics_collider_group")]
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
