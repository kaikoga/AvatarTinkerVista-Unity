using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM0.Converters;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    class OverwriteVRM0ViewPositionPass : Pass<OverwriteVRM0ViewPositionPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootTransform.TryGetComponent<VRMMeta>(out var vrmMeta))
            {
                foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivOverwriteViewPosition>())
                {
                    new ViewPositionConverterForVRM0().ToPlatform(ativ, vrmMeta);
                }
            }
        }
    }
}
