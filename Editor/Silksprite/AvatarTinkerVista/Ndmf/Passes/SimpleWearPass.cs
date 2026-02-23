using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Common.Wear;
using Silksprite.AvatarTinkerVista.Utils;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class SimpleWearPass : Pass<SimpleWearPass>
    {
        protected override void Execute(BuildContext context)
        {
            foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivSimpleWear>(true))
            {
                SimpleWearSetup.SetupAvatarIfNeeded(ativ);
                var sourceTree = ativ.ResolveModule();
                var targetTree = ativ.ResolveAvatar();
                WearProcessor.Wear(sourceTree, targetTree);
            }
        }
    }
}
