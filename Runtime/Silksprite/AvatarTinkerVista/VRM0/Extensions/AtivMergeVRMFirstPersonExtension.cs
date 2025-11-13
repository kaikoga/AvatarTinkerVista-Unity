using System;
using VRM;
using static Silksprite.AvatarTinkerVista.Nondestructive.AtivMergeVRMFirstPerson;

namespace Silksprite.AvatarTinkerVista.VRM0.Extensions
{
    public static class AtivMergeVRMFirstPersonExtension
    {
        public static FirstPersonFlag VRM0FirstPersonFlag(this RendererFirstPersonFlags rendererFirstPersonFlags)
        {
            switch (rendererFirstPersonFlags.firstPersonFlag)
            {
                case AtivFirstPersonFlag.Auto: return FirstPersonFlag.Auto;
                case AtivFirstPersonFlag.Both: return FirstPersonFlag.Both;
                case AtivFirstPersonFlag.ThirdPersonOnly: return FirstPersonFlag.ThirdPersonOnly;
                case AtivFirstPersonFlag.FirstPersonOnly: return FirstPersonFlag.FirstPersonOnly;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
