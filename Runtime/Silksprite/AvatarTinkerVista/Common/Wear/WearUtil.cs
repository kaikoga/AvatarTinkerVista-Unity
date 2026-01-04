using System;

namespace Silksprite.AvatarTinkerVista.Common.Wear
{
    public static class WearUtil
    {
        public static int NameDistance(string s, string t)
        {
            return ModifiedLevenshteinDistanceScore(s, t);
        }

        static int ModifiedLevenshteinDistanceScore(string s, string t)
        {
            // returns (Levenshtein distance) - (matched characters count) 
            var m = s.Length;
            var n = t.Length;
            
            var d = new int[m + 1, n + 1];
            for (var i = 0; i <= m; i++)
            {
                d[i, 0] = i;
            }
            for (var j = 0; j <= n; j++)
            {
                d[0, j] = j;
            }
            for (var j = 1; j <= n; j++)
            {
                for (var i = 1; i <= m; i++)
                {
                    var cost = s[i - 1] == t[j - 1] ? /* 0 */ -1 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(
                            d[i - 1, j] + 1,
                            d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[m, n];
        }
    }
}