using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Nondestructive.Base;
using UnityEngine;
#if UNITY_EDITOR && ATIV_NDMF && false
using nadena.dev.ndmf;
using nadena.dev.ndmf.runtime;
using nadena.dev.ndmf.platform;
#endif

namespace Silksprite.AvatarTinkerVista.Nondestructive
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Delete Other Platform Components")]
    [HelpURL("https://docs.kaikoga.net/ativ/ndmf_components/ativ_delete_other_platform_components")]
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

        public bool ndmfDetectPlatform = true;
        public AtivPlatform platform;

        public AtivPlatform ActualPlatform()
        {
#if UNITY_EDITOR && ATIV_NDMF && false
            if (ndmfDetectPlatform
                && RuntimeUtil.FindAvatarInParents(transform) is {} avatarRoot
                && PlatformRegistry.GetPrimaryPlatformForAvatar(avatarRoot.gameObject) is {} avatarPlatform)
            {
                return avatarPlatform.QualifiedName switch
                {
                    WellKnownPlatforms.VRChatAvatar30 => AtivPlatform.VRCSDK3_Avatars,
                    "net.kaikoga.ativ.univrm.vrm0" => AtivPlatform.VRM0,
                    "net.kaikoga.ativ.univrm.vrm1" => AtivPlatform.VRM1,
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
