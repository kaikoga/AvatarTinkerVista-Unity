using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM1.Processors;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1.Passes
{
    class OverwriteVRM1MetaPass : Pass<OverwriteVRM1MetaPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmInstance = context.AvatarRootTransform.GetComponent<Vrm10Instance>();
            if (!vrmInstance) return;

            OverwriteVRM1MetaProcessor.Process(vrmInstance);
        }

    }
}
