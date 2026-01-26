using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;

#if ATIV_ABLET
using Ablet;
using Ablet.Builtin;
#endif

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Delete Other Platform Components")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_delete_other_platform_components")]
    public class AtivDeleteOtherPlatformComponents : AtivDeleteComponentsBase
    {
        public override IEnumerable<string> ComponentTypeNamePrefixes
        {
            get
            {
                var actualPlatform = ActualPlatform();
                if (actualPlatform != AtivPlatform.VRCSDK3_Avatars) yield return "VRC.SDK3.";
                if (actualPlatform != AtivPlatform.VRM0) yield return "VRM.";
                if (actualPlatform != AtivPlatform.VRM1) yield return "UniVRM10.";
                if (actualPlatform != AtivPlatform.VRM1) yield return "UniHumanoid.";
            }
        }

        public bool abletDetectPlatform = true;
        public AtivPlatform platform;

        public AtivPlatform ActualPlatform()
        {
#if ATIV_ABLET
            if (abletDetectPlatform)
            {
                return AbletFacade.GetEntrypointFor(transform.gameObject).platform.Id switch
                {
                    BuiltinPlatformIds.VRChatAvatarSDK3 => AtivPlatform.VRCSDK3_Avatars,
                    BuiltinPlatformIds.UniVRM => AtivPlatform.VRM0,
                    BuiltinPlatformIds.UniVRM10 => AtivPlatform.VRM1,
                    _ => platform
                };
            }
#endif
            return platform;
        }

        public enum AtivPlatform
        {
            VRCSDK3_Avatars,
            VRM0,
            VRM1
        }
    }
}
