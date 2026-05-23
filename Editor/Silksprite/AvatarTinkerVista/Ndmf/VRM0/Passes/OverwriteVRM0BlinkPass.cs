using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM0.Converters;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    class OverwriteVRM0BlinkPass : Pass<OverwriteVRM0BlinkPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootTransform.TryGetComponent<VRMBlendShapeProxy>(out var vrmBlendShapeProxy))
            {
                foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivOverwriteBlink>())
                {
                    new BlinkConverterSingleForVRM0().ToPlatform(ativ, vrmBlendShapeProxy);
                    new BlinkConverterSeparateForVRM0().ToPlatform(ativ, vrmBlendShapeProxy);
                }
            }
        }
    }
}
