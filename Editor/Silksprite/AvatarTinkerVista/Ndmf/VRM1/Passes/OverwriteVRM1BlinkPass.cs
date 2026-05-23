using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM1.Converters;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1.Passes
{
    class OverwriteVRM1BlinkPass : Pass<OverwriteVRM1BlinkPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootTransform.TryGetComponent<Vrm10Instance>(out var vrm10Instance))
            {
                foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivOverwriteBlink>())
                {
                    new BlinkConverterSingleForVRM1().ToPlatform(ativ, vrm10Instance);
                    new BlinkConverterSeparateForVRM1().ToPlatform(ativ, vrm10Instance);
                }
            }
        }
    }
}
