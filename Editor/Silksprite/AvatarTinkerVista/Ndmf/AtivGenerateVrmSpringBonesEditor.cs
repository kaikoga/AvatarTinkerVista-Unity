using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [CustomEditor(typeof(AtivGenerateVrmSpringBones))]
    [CanEditMultipleObjects]
    class AtivGenerateVrmSpringBonesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            foreach (var ativ in targets.OfType<AtivGenerateVrmSpringBones>())
            {
                if (serializedObject.isEditingMultipleObjects)
                {
                    EditorGUILayout.ObjectField(ativ, typeof(AtivGenerateVrmSpringBones), true);
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
