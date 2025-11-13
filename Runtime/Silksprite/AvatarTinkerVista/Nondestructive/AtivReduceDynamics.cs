using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Nondestructive
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Reduce VRC PhysBones")]
    [HelpURL("https://docs.kaikoga.net/ativ/ndmf_components/ativ_reduce_vrc_physbones")]
    public class AtivReduceDynamics : AtivTransformingComponent
    {
        public bool reduceOnPC = false;
        public bool reduceOnMobile = true;

#if UNITY_STANDALONE
        public bool ReduceOnPlatform => reduceOnPC;
#else
        public bool ReduceOnPlatform => reduceOnMobile;
#endif

        public Transform[] keepBoneRoots = { };
    }
}
