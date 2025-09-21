using Silksprite.AvatarTinkerVista.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
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
        
        static readonly GUIStyle HeaderStyle = new GUIStyle
        {
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleLeft,
            padding = new RectOffset(-4, 0, 4, 0)
        };

        public static void Header(string content) => EditorGUILayout.LabelField(content, HeaderStyle);

    }
}
