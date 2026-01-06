namespace Silksprite.AvatarTinkerVista.Common.Wear
{
    public static class WearUtil
    {
        public static string Normalize(string s)
        {
            return s.ToLowerInvariant();
        }

        public static int NameDistance(string targetName, string baseName)
        {
            return targetName.Contains(baseName) ? targetName.Length : 10000;
        }
    }
}