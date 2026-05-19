using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Target NDMF Platform")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_target_platform")]
    public class AtivTargetNdmfPlatform : AtivTargetPlatformBase
    {
        public bool useOutputPlatform = true;
        public override bool UseOutputPlatform => useOutputPlatform;

        public static AllPlatformsInjectedHandler? AllPlatformsInjected;
        public delegate IEnumerable<AtivPlatformHandle> AllPlatformsInjectedHandler();

        public static CurrentAmbientPlatformIdInjectedHandler? CurrentAmbientPlatformIdInjected;
        public delegate string CurrentAmbientPlatformIdInjectedHandler();

        public static SelectedPlatformIdInjectedHandler? SelectedPlatformIdInjected;
        public delegate string? SelectedPlatformIdInjectedHandler(GameObject gameObject);

        public override IEnumerable<AtivPlatformHandle> AllPlatforms()
        {
            return (AllPlatformsInjected ?? EmptyPlatforms).Invoke()
                .OrderBy(platform => platform.DisplayName);
        }

        static IEnumerable<AtivPlatformHandle> EmptyPlatforms()
        {
            yield break;
        }

        public override string? SelectedPlatformId()
        {
            return useOutputPlatform
                ? CurrentAmbientPlatformIdInjected?.Invoke()
                : SelectedPlatformIdInjected?.Invoke(gameObject);
        }
    }
}
