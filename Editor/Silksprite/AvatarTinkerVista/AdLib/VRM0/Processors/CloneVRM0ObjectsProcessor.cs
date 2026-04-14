using Silksprite.AdLib.Utils.VRM0;
using UnityEditor;
using VRM;

namespace Silksprite.AvatarTinkerVista.AdLib.VRM0.Processors
{
    public static class CloneVRM0ObjectsProcessor
    {
        public static void Process(VRMMeta vrmMeta)
        {
            if (!(vrmMeta.Meta is { } meta))
            {
                return;
            }
            if (EditorUtility.IsPersistent(meta))
            {
                return;
            }
            
            var newMeta = new CustomCloneVRMMetaObject().Clone(vrmMeta.Meta).mainAsset;
            vrmMeta.Meta = newMeta;
        }
    }
}