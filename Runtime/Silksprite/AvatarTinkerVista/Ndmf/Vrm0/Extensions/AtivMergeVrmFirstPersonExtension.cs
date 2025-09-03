using System;
using VRM;
using static Silksprite.AvatarTinkerVista.Ndmf.AtivMergeVrmFirstPerson;

namespace Silksprite.AvatarTinkerVista.Ndmf.Vrm0.Extensions
{
    public static class AtivMergeVrmFirstPersonExtension
    {
        public static FirstPersonFlag Vrm0FirstPersonFlag(this RendererFirstPersonFlags rendererFirstPersonFlags)
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
