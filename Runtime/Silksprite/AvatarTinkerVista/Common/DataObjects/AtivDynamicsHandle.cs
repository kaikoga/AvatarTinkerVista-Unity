using System;

namespace Silksprite.AvatarTinkerVista.Common.DataObjects
{
    public class AtivDynamicsHandle
    {
        public readonly string Id;
        public readonly string DisplayName;
        public readonly Type? MarkerComponentType;
        public readonly int Order;

        public AtivDynamicsHandle(string id, string displayName, Type? markerComponentType, int order = 0)
        {
            Id = id;
            DisplayName = displayName;
            MarkerComponentType = markerComponentType;
            Order = order;
        }
    }
}
