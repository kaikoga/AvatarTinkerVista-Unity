using Silksprite.AdLib.Utils.VRM0;
using Silksprite.AvatarTinkerVista.Common.Utils;
using VRM;

namespace Silksprite.AvatarTinkerVista.AdLib.VRM0.Processors
{
    public static class CloneVRM0ObjectsProcessor
    {
        public static void Process(VRMMeta vrmMeta)
        {
            vrmMeta.Meta = AtivEditorUtil.ToEphemeralClone(
                vrmMeta.Meta,
                meta => new CustomCloneVRMMetaObject().Clone(meta).mainAsset);

            if (vrmMeta.TryGetComponent<VRMBlendShapeProxy>(out var blendShapeProxy))
            {
                blendShapeProxy.BlendShapeAvatar = AtivEditorUtil.ToEphemeralClone(
                    blendShapeProxy.BlendShapeAvatar,
                    blendShapeAvatar => new CustomCloneBlendShapeAvatar().Clone(blendShapeAvatar).mainAsset); 
            }
        }
    }
}