using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM0.Converter;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    class GenerateVRM0SpringBonesPass : Pass<GenerateVRM0SpringBonesPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootTransform.TryGetComponent<VRMMeta>(out _))
            {
                new DynamicsConverterToVRM0SpringBone().Convert(context.AvatarRootTransform, true);
            }
        }
    }
}
