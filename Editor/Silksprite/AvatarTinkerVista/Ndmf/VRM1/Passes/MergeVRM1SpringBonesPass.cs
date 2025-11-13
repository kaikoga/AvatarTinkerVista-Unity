using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM1.Processors;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1.Passes
{
    class MergeVRM1SpringBonesPass : Pass<MergeVRM1SpringBonesPass>
    {
        protected override void Execute(BuildContext context)
        {
            if (context.AvatarRootObject.TryGetComponent<Vrm10Instance>(out var vrmInstance))
            {
                MergeVRM1SpringBonesProcessor.Process(vrmInstance);
            }
        }
    }
}
