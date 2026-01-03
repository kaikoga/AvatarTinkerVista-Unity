using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Common.Wear;
using Silksprite.AvatarTinkerVista.Processors;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class DefaultRendererSettingsPass : Pass<DefaultRendererSettingsPass>
    {
        protected override void Execute(BuildContext context)
        {
            DefaultRendererSettingsProcessor.Process(context.AvatarRootTransform);
        }
    }
}
