using nadena.dev.ndmf;
using nadena.dev.ndmf.vrchat;
using Silksprite.AvatarTinkerVista.VRChat.Processors;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRChat.Passes
{
    class ReduceVRCPhysBonesPass : Pass<ReduceVRCPhysBonesPass>
    {
        protected override void Execute(BuildContext context)
        {
            var avatarDescriptor = context.VRChatAvatarDescriptor();
            if (avatarDescriptor)
            {
                ReduceVRCPhysBonesProcessor.Process(avatarDescriptor);
            }
        }
    }
}
