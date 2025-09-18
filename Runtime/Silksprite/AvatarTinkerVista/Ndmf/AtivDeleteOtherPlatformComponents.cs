using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Delete Other Platform Components")]
    public class AtivDeleteOtherPlatformComponents : AtivDeleteComponentsBase
    {
        public override IEnumerable<string> ComponentTypeNamePrefixes
        {
            get
            {
                if (platform != AtivPlatform.VRCSDK3_AVATARS) yield return "VRC.SDK3.";
                if (platform != AtivPlatform.VRM0) yield return "VRM.";
                if (platform != AtivPlatform.VRM1) yield return "UniVRM10.";
                if (platform != AtivPlatform.VRM1) yield return "UniHumanoid.";
            }
        }

        public AtivPlatform platform;

        public enum AtivPlatform
        {
            VRCSDK3_AVATARS,
            VRM0,
            VRM1
        }
    }
}
