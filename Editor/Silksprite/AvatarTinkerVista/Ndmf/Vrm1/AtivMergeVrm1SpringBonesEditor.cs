using UnityEditor;

namespace Silksprite.AvatarTinkerVista.Ndmf.Vrm1
{
    [CustomEditor(typeof(AtivMergeVrm1SpringBones))]
    class AtivMergeVrm1SpringBonesEditor : Editor
    {
        SerializedProperty _propColliderGroups;
        SerializedProperty _propSprings;

        void OnEnable()
        {
            _propColliderGroups = serializedObject.FindProperty(nameof(AtivMergeVrm1SpringBones.colliderGroups));
            _propSprings = serializedObject.FindProperty(nameof(AtivMergeVrm1SpringBones.springs));
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_propColliderGroups);
            EditorGUILayout.PropertyField(_propSprings);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
