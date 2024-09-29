#if ATIV_VRM0

using System.Linq;
using nadena.dev.ndmf;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class MergeVrm0FirstPersonPass : Pass<MergeVrm0FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            var rootTransform = context.AvatarRootObject;
            var vrmFirstPerson = rootTransform.GetComponent<VRMFirstPerson>();
            if (!vrmFirstPerson) return;

            var sources = rootTransform.GetComponentsInChildren<AtivMergeVrmFirstPerson>(); 

            vrmFirstPerson.Renderers.AddRange(sources.SelectMany(source => source.renderers)
                .Select(renderer => new VRMFirstPerson.RendererFirstPersonFlags
                {
                    Renderer = renderer.renderer,
                    FirstPersonFlag = renderer.Vrm0FirstPersonFlag
                }));

            foreach (var source in sources)
            {
                UnityEngine.Object.DestroyImmediate(source);
            }
        }
    }
}

#endif
