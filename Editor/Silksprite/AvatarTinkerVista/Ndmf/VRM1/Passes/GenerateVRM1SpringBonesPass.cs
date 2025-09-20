using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM1.Converter;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class GenerateVRM1SpringBonesPass : Pass<GenerateVRM1SpringBonesPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootTransform.TryGetComponent<Vrm10Instance>(out var vrm10Instance))
            {
                new DynamicsConverterToVRM1SpringBone().Convert(vrm10Instance, true);
            }
        }
    }
}
