using System.Linq;
using Silksprite.AvatarTinkerVista.Common;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateDynamics))]
    [CanEditMultipleObjects]
    class AtivGenerateVRMSpringBonesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            AtivGUILayout.GizmosDarkModeToggle();
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            foreach (var ativ in targets.OfType<AtivGenerateDynamics>())
            {
                if (serializedObject.isEditingMultipleObjects)
                {
                    EditorGUILayout.ObjectField(ativ, typeof(AtivGenerateDynamics), true);
                }
                AtivGUILayout.Header("Joints");
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
