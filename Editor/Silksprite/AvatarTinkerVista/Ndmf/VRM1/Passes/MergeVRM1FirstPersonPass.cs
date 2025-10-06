using System.Linq;
using nadena.dev.ndmf;
using nadena.dev.ndmf.runtime;
using Silksprite.AdLib.Utils.VRM1;
using Silksprite.AvatarTinkerVista.Ndmf.VRM1.Extensions;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1.Passes
{
    class MergeVRM1FirstPersonPass : Pass<MergeVRM1FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            var rootTransform = context.AvatarRootObject;
            var vrmInstance = rootTransform.GetComponent<Vrm10Instance>();
            if (!vrmInstance) return;

            var sources = rootTransform.GetComponentsInChildren<AtivMergeVRMFirstPerson>();

            vrmInstance.Vrm = new CustomCloneVRM10Object().Clone(vrmInstance.Vrm).mainAsset;
            vrmInstance.Vrm.FirstPerson.Renderers.AddRange(sources.SelectMany(source => source.renderers)
                .Select(renderer => new RendererFirstPersonFlags
                {
                    Renderer = RuntimeUtil.RelativePath(context.AvatarRootObject, renderer.renderer.gameObject),
                    FirstPersonFlag = renderer.VRM1FirstPersonType()
                }));

            foreach (var source in sources)
            {
                UnityEngine.Object.DestroyImmediate(source);
            }
        }
    }
}
