using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.AvatarTinkerVista.Common.Utils;
using UnityEngine;

#if ATIV_ABLET
using Ablet.Building;
using Ablet.Registries;
#endif

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Target Ablet Platform")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_target_platform")]
    public class AtivTargetAbletPlatform : AtivTargetPlatformBase
    {
        public bool useOutputPlatform = true;
        public override bool UseOutputPlatform => useOutputPlatform;

        public override IEnumerable<AtivPlatformHandle> AllPlatforms()
        {
#if ATIV_ABLET
            return PlatformRegistry.Instance.All()
                .Select(platform => new AtivPlatformHandle(platform.Id, platform.DisplayName));
#else
            return Enumerable.Empty<AtivPlatformHandle>();
#endif
        }

        public override string? SelectedPlatformId()
        {
#if ATIV_ABLET
            if (useOutputPlatform && BuildContext.TryGetCurrentBuildArgument(out var argument))
            {
                return argument.TargetPlatform.Id;
            }
            if (AtivRuntimeUtil.FindAvatarInParents(transform)?.gameObject is { } avatarRootObject
                && PlatformRegistry.Instance.TryGuessPlatform(avatarRootObject, out var platform))
            {
                return platform.Id;
            }
#endif
            return null;
        }
    }
}
