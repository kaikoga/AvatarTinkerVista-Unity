#if ATIV_VRM1

using System.Linq;
using nadena.dev.ndmf;
using nadena.dev.ndmf.runtime;
using Silksprite.AdLib.Utils.VRM1;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class MergeVrm1FirstPersonPass : Pass<MergeVrm1FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            var rootTransform = context.AvatarRootObject;
            var vrmInstance = rootTransform.GetComponent<Vrm10Instance>();
            if (!vrmInstance) return;

            var sources = rootTransform.GetComponentsInChildren<AtivMergeVrmFirstPerson>();

            vrmInstance.Vrm = new CustomCloneVRM10Object().Clone(vrmInstance.Vrm).mainAsset;
            vrmInstance.Vrm.FirstPerson.Renderers.AddRange(sources.SelectMany(source => source.renderers)
                .Select(renderer => new RendererFirstPersonFlags
                {
                    Renderer = RuntimeUtil.RelativePath(context.AvatarRootObject, renderer.renderer.gameObject),
                    FirstPersonFlag = renderer.Vrm1FirstPersonType
                }));

            foreach (var source in sources)
            {
                UnityEngine.Object.DestroyImmediate(source);
            }
        }
    }
}

#endif
