using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace Silksprite.AvatarTinkerVista.ProjectSettings
{
    static class UniVRMDetector
    {
        const string DetectedVRM0 = "ATIV_DETECTED_VRM0";
        const string DetectedVRM1 = "ATIV_DETECTED_VRM1";

        [InitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            var isUniVRMAvailable = IsUniVRMAvailable();

            foreach (var target in ActiveBuildTargets())
            {
                var namedTarget = NamedBuildTarget.FromBuildTargetGroup(target);
                PlayerSettings.GetScriptingDefineSymbols(namedTarget, out var defines);

                var newDefines = BuildDefines(defines.Where(x => !string.IsNullOrEmpty(x)), isUniVRMAvailable).ToArray();
                if (!defines.SequenceEqual(newDefines))
                {
                    PlayerSettings.SetScriptingDefineSymbols(namedTarget, newDefines);
                }
            }
        }

        static (bool vrm0, bool vrm1) IsUniVRMAvailable()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return (
                assemblies.Any(x => x.GetName().Name == "VRM"),
                assemblies.Any(x => x.GetName().Name == "VRM10"));
        }

        static IEnumerable<BuildTargetGroup> ActiveBuildTargets()
        {
            yield return BuildTargetGroup.Standalone;
            yield return BuildTargetGroup.Android;
            yield return BuildTargetGroup.iOS;
        }

        static IEnumerable<string> BuildDefines(IEnumerable<string> defines, (bool vrm0, bool vrm1) isUniVRMAvailable)
        {
            foreach (var define in defines)
            {
                switch (define)
                {
                    case DetectedVRM0:
                        if (!isUniVRMAvailable.vrm0) continue;
                        break;
                    case DetectedVRM1:
                        if (!isUniVRMAvailable.vrm1) continue;
                        break;
                }
                yield return define;
            }
            if (isUniVRMAvailable.vrm0) yield return DetectedVRM0;
            if (isUniVRMAvailable.vrm1) yield return DetectedVRM1;
        }
    }
}
