using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM0.Processors;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    class OverwriteVRM0MetaPass : Pass<OverwriteVRM0MetaPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootTransform.TryGetComponent<VRMMeta>(out var vrmMeta))
            {
                OverwriteVRM0MetaProcessor.Process(vrmMeta);
            }
        }
    }
}
