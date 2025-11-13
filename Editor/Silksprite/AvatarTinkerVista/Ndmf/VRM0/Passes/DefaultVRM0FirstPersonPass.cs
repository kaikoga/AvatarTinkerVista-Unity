using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM0.Processors;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    class DefaultVRM0FirstPersonPass : Pass<DefaultVRM0FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootTransform.TryGetComponent<VRMFirstPerson>(out var vrmFirstPerson))
            {
                DefaultVRM0FirstPersonProcessor.Process(vrmFirstPerson);
            }
        }
    }
}
