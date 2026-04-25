using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.Registries;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Select Dynamics")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_select_dynamics")]
    public class AtivSelectDynamics : AtivGeneratingComponent
    {
        public List<DynamicsOption> options = new List<DynamicsOption>
        {
            new DynamicsOption
            {
                dynamicsId = DynamicsRegistry.Auto
            }
        };

        [Serializable]
        public class DynamicsOption
        {
            public string dynamicsId = DynamicsRegistry.Auto;
        }

        public static string GetDynamicsIdOf(Transform avatarRoot) =>
            avatarRoot.GetComponentsInChildren<AtivSelectDynamics>()
                .SelectMany(c => c.options)
                .FirstOrDefault()?.dynamicsId ?? DynamicsRegistry.Auto;
    }
}
