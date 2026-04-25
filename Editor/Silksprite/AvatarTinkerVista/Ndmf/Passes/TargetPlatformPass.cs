using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Common.Base;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class TargetPlatformPass : Pass<TargetPlatformPass>
    {
        protected override void Execute(BuildContext context)
        {
            AtivTargetPlatformBase.ApplyToAvatarRoot(context.AvatarRootTransform, false);
        }
    }
}
