using nadena.dev.ndmf;
using nadena.dev.ndmf.vrchat;
using Silksprite.AvatarTinkerVista.VRChat.Converter;
using Silksprite.AvatarTinkerVista.VRChat.Converters;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRChat.Passes
{
    class OverwriteVRChatViewPositionPass : Pass<OverwriteVRChatViewPositionPass>
    {
        protected override void Execute(BuildContext context)
        {
            var avatarDescriptor = context.VRChatAvatarDescriptor();
            if (avatarDescriptor)
            {
                foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivOverwriteViewPosition>())
                {
                    new ViewPositionConverterForVRChat().ToPlatform(ativ, avatarDescriptor);
                }
            }
        }
    }
}
