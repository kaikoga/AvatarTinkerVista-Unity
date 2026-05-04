using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Common.Base;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class TargetInputPlatformPass : Pass<TargetInputPlatformPass>
    {
        protected override void Execute(BuildContext context)
        {
            AtivTargetPlatformBase.ApplyToAvatarRoot(context.AvatarRootTransform, true);
        }
    }
}
