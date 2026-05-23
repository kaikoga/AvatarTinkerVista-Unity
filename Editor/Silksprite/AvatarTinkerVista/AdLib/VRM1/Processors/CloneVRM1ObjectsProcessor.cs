using Silksprite.AdLib.Utils.VRM1;
using Silksprite.AvatarTinkerVista.Common.Utils;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.AdLib.VRM1.Processors
{
    public static class CloneVRM1ObjectsProcessor
    {
        public static void Process(Vrm10Instance vrmInstance)
        {
            vrmInstance.Vrm = AtivEditorUtil.ToEphemeralClone(
                vrmInstance.Vrm,
                vrm => new CustomCloneVRM10Object().Clone(vrm).mainAsset);
        }
    }
}