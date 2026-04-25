using System.Collections.Generic;
using System.Linq;
using Ablet.Building;
using Ablet.Registries;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.AvatarTinkerVista.Common.Utils;
using UnityEngine;

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
            return PlatformRegistry.Instance.All()
                .Select(platform => new AtivPlatformHandle(platform.Id, platform.DisplayName));
        }

        public override string SelectedPlatformId()
        {
            if (useOutputPlatform && BuildContext.TryGetCurrentBuildArgument(out var argument))
            {
                return argument.TargetPlatform.Id;
            }
            return PlatformRegistry.Instance.TryGuessPlatform(AtivRuntimeUtil.FindAvatarInParents(transform).gameObject, out var platform)
                ? platform.Id
                : null;
        }
    }
}
