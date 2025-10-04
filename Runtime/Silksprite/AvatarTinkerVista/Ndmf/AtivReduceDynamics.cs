using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Reduce VRC PhysBones")]
    public class AtivReduceDynamics : AtivTransformingComponent
    {
        public Transform[] keepBoneRoots = { };
    }
}
