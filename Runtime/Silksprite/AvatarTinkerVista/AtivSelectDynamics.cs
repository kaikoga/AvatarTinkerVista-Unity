using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Select Dynamics")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_select_dynamics")]
    public class AtivSelectDynamics : AtivGeneratingComponent
    {
        const string Discard = "Discard";
        public const string Auto = "Auto";

        static readonly List<string> DynamicIds = new List<string>
        {
            Discard,
            Auto
        };

        public static IEnumerable<string> AllDynamicIds() => DynamicIds; 

        public static void RegisterDynamicsId(string id) => DynamicIds.Add(id);

        public List<DynamicsOption> options = new List<DynamicsOption>
        {
            new DynamicsOption
            {
                dynamicsId = Auto
            }
        };

        public static string GetDynamicsIdOf(Transform avatarRoot) =>
            avatarRoot.GetComponentsInChildren<AtivSelectDynamics>()
                .SelectMany(c => c.options)
                .FirstOrDefault()?.dynamicsId ?? Auto;

        [Serializable] 
        public class DynamicsOption
        {
            public string dynamicsId = Auto;
        }
    }
}
