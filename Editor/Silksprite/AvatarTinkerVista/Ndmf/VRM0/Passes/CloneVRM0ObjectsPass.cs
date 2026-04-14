using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.AdLib.VRM0.Processors;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    class CloneVRM0ObjectsPass : Pass<CloneVRM0ObjectsPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootTransform.TryGetComponent<VRMMeta>(out var vrmMeta))
            {
                CloneVRM0ObjectsProcessor.Process(vrmMeta);
            }
        }
    }
}
