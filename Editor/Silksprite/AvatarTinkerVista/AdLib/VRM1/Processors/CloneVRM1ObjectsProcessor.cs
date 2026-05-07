using Silksprite.AdLib.Utils.VRM1;
using UnityEditor;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.AdLib.VRM1.Processors
{
    public static class CloneVRM1ObjectsProcessor
    {
        public static void Process(Vrm10Instance vrmInstance)
        {
            if (vrmInstance.Vrm is not { } vrm)
            {
                return;
            }
            if (EditorUtility.IsPersistent(vrm))
            {
                return;
            }
            
            var newVrm = new CustomCloneVRM10Object().Clone(vrmInstance.Vrm).mainAsset;
            vrmInstance.Vrm = newVrm;
        }
    }
}