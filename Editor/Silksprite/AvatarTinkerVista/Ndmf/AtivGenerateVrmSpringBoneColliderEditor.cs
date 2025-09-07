using System;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [CustomEditor(typeof(AtivGenerateVrmSpringBoneCollider))]
    [CanEditMultipleObjects]
    class AtivGenerateVrmSpringBoneColliderEditor : Editor
    {
        SerializedProperty _propRenderers;
        SerializedProperty _propRootBone;
        SerializedProperty _propOffset;
        SerializedProperty _propRadius;
        SerializedProperty _propTail;
        SerializedProperty _propNormal;

        void OnEnable()
        {
            _propRenderers = serializedObject.FindProperty(nameof(AtivGenerateVrmSpringBoneCollider.colliderType));
            _propRootBone = serializedObject.FindProperty(nameof(AtivGenerateVrmSpringBoneCollider.rootBone));
            _propOffset = serializedObject.FindProperty(nameof(AtivGenerateVrmSpringBoneCollider.offset));
            _propRadius = serializedObject.FindProperty(nameof(AtivGenerateVrmSpringBoneCollider.radius));
            _propTail = serializedObject.FindProperty(nameof(AtivGenerateVrmSpringBoneCollider.tail));
            _propNormal = serializedObject.FindProperty(nameof(AtivGenerateVrmSpringBoneCollider.normal));
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_propRenderers);
            EditorGUILayout.PropertyField(_propRootBone);
            EditorGUILayout.PropertyField(_propOffset);
            switch ((AtivGenerateVrmSpringBoneCollider.ColliderTypes)_propRenderers.intValue)
            {
                case AtivGenerateVrmSpringBoneCollider.ColliderTypes.Sphere:
                    EditorGUILayout.PropertyField(_propRadius);
                    break;
                case AtivGenerateVrmSpringBoneCollider.ColliderTypes.Capsule:
                    EditorGUILayout.PropertyField(_propRadius);
                    EditorGUILayout.PropertyField(_propTail);
                    break;
                case AtivGenerateVrmSpringBoneCollider.ColliderTypes.Plane:
                    EditorGUILayout.PropertyField(_propNormal);
                    break;
                case AtivGenerateVrmSpringBoneCollider.ColliderTypes.SphereInside:
                    EditorGUILayout.PropertyField(_propRadius);
                    break;
                case AtivGenerateVrmSpringBoneCollider.ColliderTypes.CapsuleInside:
                    EditorGUILayout.PropertyField(_propRadius);
                    EditorGUILayout.PropertyField(_propTail);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
