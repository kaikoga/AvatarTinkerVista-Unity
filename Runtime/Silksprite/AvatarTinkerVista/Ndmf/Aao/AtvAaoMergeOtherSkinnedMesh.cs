using Anatawa12.AvatarOptimizer.PrefabSafeSet;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Aao
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATV AAO Merge Other Skinned Mesh")]
    public class AtvAaoMergeOtherSkinnedMesh : AtvGeneratingComponent
    {
        public SkinnedMeshRendererSet excludeRenderersSet;
        public MeshRendererSet excludeStaticRenderersSet;
    }
}
