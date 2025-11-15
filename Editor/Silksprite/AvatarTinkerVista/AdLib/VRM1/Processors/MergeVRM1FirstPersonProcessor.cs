using System.Linq;
using Silksprite.AdLib.Utils.VRM1;
using Silksprite.AvatarTinkerVista.Common.Utils;
using Silksprite.AvatarTinkerVista.Nondestructive;
using Silksprite.AvatarTinkerVista.VRM1.Extensions;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Processors
{
    public static class MergeVRM1FirstPersonProcessor
    {
        public static void Process(Vrm10Instance vrmInstance)
        {
            var sources = vrmInstance.GetComponentsInChildren<AtivMergeVRMFirstPerson>();

            vrmInstance.Vrm = new CustomCloneVRM10Object().Clone(vrmInstance.Vrm).mainAsset;
            vrmInstance.Vrm.FirstPerson.Renderers.AddRange(sources.SelectMany(source => source.renderers)
                .Select(renderer => new RendererFirstPersonFlags
                {
                    Renderer = AtivRuntimeUtil.RelativePath(vrmInstance.transform, renderer.renderer.transform),
                    FirstPersonFlag = renderer.VRM1FirstPersonType()
                }));

            foreach (var source in sources)
            {
                UnityEngine.Object.DestroyImmediate(source);
            }
        }
    }
}