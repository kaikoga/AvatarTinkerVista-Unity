#if ATIV_VRM0 || ATIV_VRM1

using UnityEditor;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [CustomEditor(typeof(AtivMergeVrmFirstPerson))]
    class AtivMergeVrmFirstPersonEditor : Editor
    {
        SerializedProperty _propRenderers;

        void OnEnable()
        {
            _propRenderers = serializedObject.FindProperty(nameof(AtivMergeVrmFirstPerson.renderers));
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_propRenderers);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif