using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Common.DataObjects;

namespace Silksprite.AvatarTinkerVista.Common.Registries
{
    public static class DynamicsRegistry
    {
        const string Discard = "Discard";
        public const string Auto = "Auto";

        static readonly List<AtivDynamicsHandle> Dynamics = new List<AtivDynamicsHandle>
        {
            new AtivDynamicsHandle(Discard, "Discard", -1),
            new AtivDynamicsHandle(Auto, "Auto", -1)
        };

        public static IEnumerable<AtivDynamicsHandle> All() => Dynamics;
        public static void Register(AtivDynamicsHandle id) => Dynamics.Add(id);
    }
}