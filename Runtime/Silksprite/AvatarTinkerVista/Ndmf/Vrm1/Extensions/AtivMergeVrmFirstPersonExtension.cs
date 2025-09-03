using System;
using UniGLTF.Extensions.VRMC_vrm;

using static Silksprite.AvatarTinkerVista.Ndmf.AtivMergeVrmFirstPerson;

namespace Silksprite.AvatarTinkerVista.Ndmf.Vrm1.Extensions
{
    public static class AtivMergeVrmFirstPersonExtension
    {
        public static FirstPersonType Vrm1FirstPersonType(this RendererFirstPersonFlags rendererFirstPersonFlags)
        {
            switch (rendererFirstPersonFlags.firstPersonFlag)
            {
                case AtivFirstPersonFlag.Auto: return FirstPersonType.auto;
                case AtivFirstPersonFlag.Both: return FirstPersonType.both;
                case AtivFirstPersonFlag.ThirdPersonOnly: return FirstPersonType.thirdPersonOnly;
                case AtivFirstPersonFlag.FirstPersonOnly: return FirstPersonType.firstPersonOnly;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
