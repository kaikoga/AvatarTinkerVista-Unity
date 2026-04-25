using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Target Unity Platform")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_target_platform")]
    public class AtivTargetUnityPlatform : AtivTargetPlatformBase
    {
        public static AtivPlatformHandle Windows = new AtivPlatformHandle("Windows", "Windows", 0);
        public static AtivPlatformHandle Macos = new AtivPlatformHandle("Macos", "macOS", 1);
        public static AtivPlatformHandle Linux = new AtivPlatformHandle("Linux", "Linux", 2);
        public static AtivPlatformHandle Android = new AtivPlatformHandle("Android", "Android", 3);
        public static AtivPlatformHandle Ios = new AtivPlatformHandle("Ios", "iOS", 4);

        public override IEnumerable<AtivPlatformHandle> AllPlatforms()
        {
            yield return Windows;
            yield return Macos;
            yield return Linux;
            yield return Android;
            yield return Ios;
        }

#if UNITY_STANDALONE_WIN
        static string UnityPlatformId => Windows.Id;
#elif UNITY_STANDALONE_OSX
        static string UnityPlatformId => Macos.Id;
#elif UNITY_STANDALONE_LINUX
        static string UnityPlatformId => Linux.Id;
#elif UNITY_ANDROID
        static string AndroidPlatformId => Android.Id;
#elif UNITY_IOS
        static string iOSPlatformId => iOS.Id;
#endif

        public override string SelectedPlatformId() => UnityPlatformId;
    }
}
