using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM1.Converters;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1.Passes
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
