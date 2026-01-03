using System;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using VRM;
using static Silksprite.AvatarTinkerVista.Common.DataObjects.AtivRendererFirstPersonFlags;

namespace Silksprite.AvatarTinkerVista.VRM0.Extensions
{
    public static class AtivMergeVRMFirstPersonExtension
    {
        public static FirstPersonFlag VRM0FirstPersonFlag(this AtivRendererFirstPersonFlags rendererFirstPersonFlags)
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
