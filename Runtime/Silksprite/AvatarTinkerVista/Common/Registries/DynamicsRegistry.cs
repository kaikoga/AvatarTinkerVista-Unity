using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Registries
{
    public static class DynamicsRegistry
    {
        const string Discard = "Discard";
        public const string Auto = "Auto";

        static readonly List<AtivDynamicsHandle> Dynamics = new List<AtivDynamicsHandle>
        {
            new AtivDynamicsHandle(Discard, "Discard", null, -1),
            new AtivDynamicsHandle(Auto, "Auto", null, -1)
        };

        public static IEnumerable<AtivDynamicsHandle> All() => Dynamics;
        public static void Register(AtivDynamicsHandle id) => Dynamics.Add(id);

        public static string GuessDynamicsIdOf(Transform transform)
        {
            return All()
                .Where(dynamics => dynamics.MarkerComponentType != null)
                .FirstOrDefault(dynamics => transform.GetComponentsInChildren(dynamics.MarkerComponentType).Length > 0)
                ?.Id;
        }
    }
}