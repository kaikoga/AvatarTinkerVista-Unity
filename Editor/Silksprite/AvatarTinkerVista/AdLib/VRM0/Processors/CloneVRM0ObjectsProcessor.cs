using Silksprite.AdLib.Utils.VRM0;
using Silksprite.AvatarTinkerVista.Common.Utils;
using VRM;

namespace Silksprite.AvatarTinkerVista.AdLib.VRM0.Processors
{
    public static class CloneVRM0ObjectsProcessor
    {
        public static void Process(VRMMeta vrmMeta)
        {
            if (vrmMeta.Meta is not { } meta)
            {
                return;
            }
            
            vrmMeta.Meta = AtivEditorUtil.ToEphemeralClone(meta, m => new CustomCloneVRMMetaObject().Clone(m).mainAsset);
        }
    }
}