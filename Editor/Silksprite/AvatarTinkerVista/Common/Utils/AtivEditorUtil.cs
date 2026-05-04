using System.Diagnostics;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.Loch;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Utils
{
    public static class AtivEditorUtil
    {
        public static void OpenInExplorer(string directoryPath)
        {
#if UNITY_EDITOR_WIN
            Process.Start("explorer.exe", directoryPath);
#else
            Process.Start("open", directoryPath);
#endif
        }

        const string NameOfHasValue = "hasValue";
        const string NameOfRelativePath = "relativePath";
        public static SkinnedMeshRenderer? ResolveAvatarRelativeSkinnedMeshRenderer(LocalizedProperty lop)
        {
            var transform = (lop.Property.serializedObject.targetObject as Component)?.transform;
            var hasValue = lop.Property.FindPropertyRelative(NameOfHasValue).boolValue;
            var relativePath = lop.Property.FindPropertyRelative(NameOfRelativePath).stringValue;
            return hasValue ? AvatarRelativeReference.ResolveNow<SkinnedMeshRenderer>(transform, relativePath) : null;
        }

    }
}
