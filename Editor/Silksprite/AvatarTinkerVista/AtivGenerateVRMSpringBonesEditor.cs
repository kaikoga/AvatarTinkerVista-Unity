using System.Linq;
using Silksprite.AvatarTinkerVista.Common;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateDynamics))]
    [CanEditMultipleObjects]
    class AtivGenerateVRMSpringBonesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            LEditorGUILayout.LocaleSelector();
            AtivGUILayout.GizmosDarkModeToggle();
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            foreach (var ativ in targets.OfType<AtivGenerateDynamics>())
            {
                if (serializedObject.isEditingMultipleObjects)
                {
                    EditorGUILayout.ObjectField(ativ, typeof(AtivGenerateDynamics), true);
                }
                LGUILayout.Heading(Loc("AtivGenerateDynamics::Joints"));
                using (new EditorGUI.IndentLevelScope(1))
                using (new EditorGUI.DisabledScope(true))
                {
                    foreach (var joint in ativ.GuessJoints())
                    {
                        EditorGUILayout.ObjectField(joint, typeof(Transform), true);
                    }
                }
            }
        }
    }
}
