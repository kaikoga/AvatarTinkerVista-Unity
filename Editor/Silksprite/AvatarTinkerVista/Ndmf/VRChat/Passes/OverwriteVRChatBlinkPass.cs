using nadena.dev.ndmf;
using nadena.dev.ndmf.vrchat;
using Silksprite.AvatarTinkerVista.VRChat.Converters;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRChat.Passes
{
    class OverwriteVRChatBlinkPass : Pass<OverwriteVRChatBlinkPass>
    {
        protected override void Execute(BuildContext context)
        {
            var avatarDescriptor = context.VRChatAvatarDescriptor();
            if (avatarDescriptor)
            {
                foreach (var ativ in context.AvatarRootTransform.GetComponentsInChildren<AtivOverwriteBlink>())
                {
                    new BlinkConverterForVRChat().ToPlatform(ativ, avatarDescriptor);
                }
            }
        }
    }
}
