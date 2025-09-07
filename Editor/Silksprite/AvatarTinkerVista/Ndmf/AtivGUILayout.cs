using Silksprite.AvatarTinkerVista.Utils;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    public static class AtivGUILayout
    {
        public static void GizmosDarkModeToggle()
        {
            using var change = new EditorGUI.ChangeCheckScope();
            var isDarkMode = EditorGUILayout.Toggle("Gizmos Dark Mode", AtivGizmoStyle.IsDarkMode);
            if (change.changed)
            {
                AtivGizmoStyle.IsDarkMode = isDarkMode;
                SceneView.RepaintAll();
            }
        }
    }
}
