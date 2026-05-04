using System.Collections.Generic;
using System.Linq;
using nadena.dev.ndmf.platform;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    static class AtivTargetNdmfPlatformEditor
    {
        [InitializeOnLoadMethod]
        public static void InitializeOnLoad()
        {
            AtivTargetNdmfPlatform.AllPlatformsInjected = AllPlatforms;
            AtivTargetNdmfPlatform.SelectedPlatformIdInjected = SelectedPlatformId;
            AtivTargetNdmfPlatform.CurrentAmbientPlatformIdInjected = CurrentAmbientPlatformId;
        }
        static IEnumerable<AtivPlatformHandle> AllPlatforms()
        {
            return PlatformRegistry.PlatformProviders.Values
                .Select(platform => new AtivPlatformHandle(platform.QualifiedName, platform.DisplayName));
        }
        
        static string? SelectedPlatformId(GameObject gameObject)
        {
            return PlatformRegistry.GetPrimaryPlatformForAvatar(gameObject)?.QualifiedName;
        }

        static string CurrentAmbientPlatformId()
        {
            return AmbientPlatform.CurrentPlatform.QualifiedName;
        }
    }
}
