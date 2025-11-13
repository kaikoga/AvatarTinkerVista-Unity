using nadena.dev.ndmf;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1.Passes
{
    class DefaultVRM1FirstPersonPass : Pass<DefaultVRM1FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootTransform.TryGetComponent<Vrm10Instance>(out var vrmInstance))
            {
                DefaultVRM1FirstPersonProcessor.Process(vrmInstance);
            }
        }

    }
}
