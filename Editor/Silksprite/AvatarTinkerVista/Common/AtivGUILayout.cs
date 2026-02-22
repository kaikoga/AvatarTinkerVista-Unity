using Silksprite.AvatarTinkerVista.Common.Utils;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista.Common
{
    public static class AtivGUILayout
    {
        public static void GizmosDarkModeToggle()
        {
            using var change = new EditorGUI.ChangeCheckScope();
            var isDarkMode = LEditorGUILayout.Toggle(Loc("ativ::GizmosDarkMode"), AtivGizmoStyle.IsDarkMode);
            if (change.changed)
            {
                AtivGizmoStyle.IsDarkMode = isDarkMode;
                SceneView.RepaintAll();
            }
        }

    }
}
