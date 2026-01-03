using System.Linq;
using Silksprite.AvatarTinkerVista.VRM0.Extensions;
using VRM;

namespace Silksprite.AvatarTinkerVista.VRM0.Processors
{
    public static class MergeVRM0FirstPersonProcessor
    {
        public static void Process(VRMFirstPerson vrmFirstPerson)
        {
            var sources = vrmFirstPerson.GetComponentsInChildren<AtivMergeVRMFirstPerson>(); 

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