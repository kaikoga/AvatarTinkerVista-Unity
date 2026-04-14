using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM1.Converters;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1.Passes
{
    class OverwriteVRM1ViewPositionPass : Pass<OverwriteVRM1ViewPositionPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmInstance = context.AvatarRootTransform.GetComponent<Vrm10Instance>();
            if (vrmInstance)
            {
                foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivOverwriteViewPosition>())
                {
                    new ViewPositionConverterForVRM1().ToPlatform(ativ, vrmInstance);
                }
            }
        }
    }
}
