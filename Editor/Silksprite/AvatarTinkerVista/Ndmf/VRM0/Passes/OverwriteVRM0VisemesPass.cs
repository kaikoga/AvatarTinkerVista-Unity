using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM0.Converters;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    class OverwriteVRM0VisemesPass : Pass<OverwriteVRM0VisemesPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootTransform.TryGetComponent<VRMBlendShapeProxy>(out var vrmBlendShapeProxy))
            {
                foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivOverwriteVisemes>())
                {
                    new VisemesConverterForVRM0().ToPlatform(ativ, vrmBlendShapeProxy);
                }
            }
        }
    }
}
