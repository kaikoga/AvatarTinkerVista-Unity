using Anatawa12.AvatarOptimizer.PrefabSafeSet;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Aao
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV AAO Merge Other Skinned Mesh")]
    public class AtivAaoMergeOtherSkinnedMesh : AtivGeneratingComponent
    {
        public SkinnedMeshRendererSet excludeRenderersSet;
        public MeshRendererSet excludeStaticRenderersSet;
    }
}
