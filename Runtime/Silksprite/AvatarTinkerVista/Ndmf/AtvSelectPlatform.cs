using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATV Select Platform")]
    public class AtvSelectPlatform : AtvDeleteComponentsBase
    {
        public override IEnumerable<string> ComponentTypeNamePrefixes
        {
            get
            {
                if (platform != AtvPlatform.VRCSDK3_AVATARS) yield return "VRC.SDK3.";
                if (platform != AtvPlatform.VRM0) yield return "VRM.";
                if (platform != AtvPlatform.VRM1) yield return "UniVRM10.";
            }
        }

        public AtvPlatform platform;

        public enum AtvPlatform
        {
            VRCSDK3_AVATARS,
            VRM0,
            VRM1
        }
    }
}
