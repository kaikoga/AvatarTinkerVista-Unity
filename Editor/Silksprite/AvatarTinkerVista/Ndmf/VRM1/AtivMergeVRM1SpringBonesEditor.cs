using UnityEditor;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1
{
    [CustomEditor(typeof(AtivMergeVRM1SpringBones))]
    class AtivMergeVRM1SpringBonesEditor : Editor
    {
        SerializedProperty _propColliderGroups;
        SerializedProperty _propSprings;

        void OnEnable()
        {
            _propColliderGroups = serializedObject.FindProperty(nameof(AtivMergeVRM1SpringBones.colliderGroups));
            _propSprings = serializedObject.FindProperty(nameof(AtivMergeVRM1SpringBones.springs));
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_propColliderGroups);
            EditorGUILayout.PropertyField(_propSprings);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
