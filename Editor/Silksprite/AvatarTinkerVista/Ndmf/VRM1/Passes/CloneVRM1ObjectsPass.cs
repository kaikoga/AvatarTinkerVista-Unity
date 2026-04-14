using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.AdLib.VRM1.Processors;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1.Passes
{
    class CloneVRM1ObjectsPass : Pass<CloneVRM1ObjectsPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmInstance = context.AvatarRootTransform.GetComponent<Vrm10Instance>();
            if (!vrmInstance) return;

            CloneVRM1ObjectsProcessor.Process(vrmInstance);
        }

    }
}
