namespace Silksprite.AvatarTinkerVista.Common.DataObjects
{
    public class AtivDynamicsHandle
    {
        public readonly string Id;
        public readonly string DisplayName;
        public readonly int Order;

        public AtivDynamicsHandle(string id, string displayName, int order = 0)
        {
            Id = id;
            DisplayName = displayName;
            Order = order;
        }
    }
}
