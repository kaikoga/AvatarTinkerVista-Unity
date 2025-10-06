using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Reduce VRC PhysBones")]
    [HelpURL("https://docs.kaikoga.net/ativ/ndmf_components/ativ_reduce_vrc_physbones")]
    public class AtivReduceDynamics : AtivTransformingComponent
    {
        public Transform[] keepBoneRoots = { };
    }
}
