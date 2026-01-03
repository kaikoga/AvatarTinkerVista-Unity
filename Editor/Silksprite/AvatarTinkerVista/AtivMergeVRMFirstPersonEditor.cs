using UnityEditor;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivMergeVRMFirstPerson))]
    [CanEditMultipleObjects]
    class AtivMergeVRMFirstPersonEditor : Editor
    {
        SerializedProperty _propRenderers;

        void OnEnable()
        {
            _propRenderers = serializedObject.FindProperty(nameof(AtivMergeVRMFirstPerson.renderers));
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_propRenderers);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
