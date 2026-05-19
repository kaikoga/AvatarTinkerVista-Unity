using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.AvatarTinkerVista.Common.Registries;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Target Selected Dynamics Platform")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_target_platform")]
    public class AtivTargetSelectedDynamicsPlatform : AtivTargetPlatformBase
    {
        public override bool UseOutputPlatform => false;

        public override IEnumerable<AtivPlatformHandle> AllPlatforms()
        {
            return DynamicsRegistry.All()
                .Select(dynamics => new AtivPlatformHandle(dynamics.Id, dynamics.DisplayName))
                .OrderBy(platform => platform.DisplayName);
        }

        public override string? SelectedPlatformId() =>
            AtivSelectDynamics.GetDynamicsIdOf(transform) switch
            {
                DynamicsRegistry.Auto => DynamicsMarkerRegistry.GuessDynamicsIdOf(transform),
                var dynamicsId => dynamicsId
            };
    }
}
