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
        public static readonly AtivPlatformHandle Windows = new AtivPlatformHandle("Windows", "Windows");
        public static readonly AtivPlatformHandle Macos = new AtivPlatformHandle("Macos", "macOS");
        public static readonly AtivPlatformHandle Linux = new AtivPlatformHandle("Linux", "Linux");
        public static readonly AtivPlatformHandle Android = new AtivPlatformHandle("Android", "Android");
        public static readonly AtivPlatformHandle Ios = new AtivPlatformHandle("Ios", "iOS");

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
        static string UnityPlatformId => Android.Id;
#elif UNITY_IOS
        static string UnityPlatformId => Ios.Id;
#endif

        public override string SelectedPlatformId() => UnityPlatformId;
    }
}
