using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateVRMSpringBones))]
    [CanEditMultipleObjects]
    class AtivGenerateVRMSpringBonesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            AtivGUILayout.GizmosDarkModeToggle();
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            foreach (var ativ in targets.OfType<AtivGenerateVRMSpringBones>())
            {
                if (serializedObject.isEditingMultipleObjects)
                {
                    EditorGUILayout.ObjectField(ativ, typeof(AtivGenerateVRMSpringBones), true);
                }
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
