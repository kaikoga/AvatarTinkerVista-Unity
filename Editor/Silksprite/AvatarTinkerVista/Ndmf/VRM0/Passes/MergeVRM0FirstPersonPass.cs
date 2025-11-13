using nadena.dev.ndmf;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    class MergeVRM0FirstPersonPass : Pass<MergeVRM0FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootObject.TryGetComponent<VRMFirstPerson>(out var vrmFirstPerson))
            {
                MergeVRM0FirstPersonProcessor.Process(vrmFirstPerson);
            }
        }
    }
}
