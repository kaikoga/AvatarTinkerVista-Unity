namespace Silksprite.AvatarTinkerVista.Common.DataObjects
{
    public readonly struct AtivPlatformHandle
    {
        public readonly string Id;
        public readonly string DisplayName;
        public readonly int Order;

        public AtivPlatformHandle(string id, string displayName, int order = 0)
        {
            Id = id;
            DisplayName = displayName;
            Order = order;
        }
    }
}
