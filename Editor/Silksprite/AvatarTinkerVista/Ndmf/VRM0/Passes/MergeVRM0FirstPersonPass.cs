using System.Linq;
using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Ndmf.VRM0.Extensions;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    class MergeVRM0FirstPersonPass : Pass<MergeVRM0FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            var rootTransform = context.AvatarRootObject;
            var vrmFirstPerson = rootTransform.GetComponent<VRMFirstPerson>();
            if (!vrmFirstPerson) return;

            var sources = rootTransform.GetComponentsInChildren<AtivMergeVRMFirstPerson>(); 

            vrmFirstPerson.Renderers.AddRange(sources.SelectMany(source => source.renderers)
                .Select(renderer => new VRMFirstPerson.RendererFirstPersonFlags
                {
                    Renderer = renderer.renderer,
                    FirstPersonFlag = renderer.VRM0FirstPersonFlag()
                }));

            foreach (var source in sources)
            {
                UnityEngine.Object.DestroyImmediate(source);
            }
        }
    }
}
