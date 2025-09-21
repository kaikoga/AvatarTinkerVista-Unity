using System;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateVRMSpringBoneCollider))]
    [CanEditMultipleObjects]
    class AtivGenerateVRMSpringBoneColliderEditor : Editor
    {
        SerializedProperty _propRenderers;
        SerializedProperty _propRootBone;
        SerializedProperty _propOffset;
        SerializedProperty _propRadius;
        SerializedProperty _propTail;
        SerializedProperty _propNormal;

        void OnEnable()
        {
            _propRenderers = serializedObject.FindProperty(nameof(AtivGenerateVRMSpringBoneCollider.colliderType));
            _propRootBone = serializedObject.FindProperty(nameof(AtivGenerateVRMSpringBoneCollider.rootBone));
            _propOffset = serializedObject.FindProperty(nameof(AtivGenerateVRMSpringBoneCollider.offset));
            _propRadius = serializedObject.FindProperty(nameof(AtivGenerateVRMSpringBoneCollider.radius));
            _propTail = serializedObject.FindProperty(nameof(AtivGenerateVRMSpringBoneCollider.tail));
            _propNormal = serializedObject.FindProperty(nameof(AtivGenerateVRMSpringBoneCollider.normal));
        }
        
        public override void OnInspectorGUI()
        {
            AtivGUILayout.GizmosDarkModeToggle();
            EditorGUILayout.PropertyField(_propRenderers);
            EditorGUILayout.PropertyField(_propRootBone);
            EditorGUILayout.PropertyField(_propOffset);
            switch ((AtivGenerateVRMSpringBoneCollider.ColliderTypes)_propRenderers.intValue)
            {
                case AtivGenerateVRMSpringBoneCollider.ColliderTypes.Sphere:
                    EditorGUILayout.PropertyField(_propRadius);
                    break;
                case AtivGenerateVRMSpringBoneCollider.ColliderTypes.Capsule:
                    EditorGUILayout.PropertyField(_propRadius);
                    EditorGUILayout.PropertyField(_propTail);
                    break;
                case AtivGenerateVRMSpringBoneCollider.ColliderTypes.Plane:
                    EditorGUILayout.PropertyField(_propNormal);
                    break;
                case AtivGenerateVRMSpringBoneCollider.ColliderTypes.SphereInside:
                    EditorGUILayout.PropertyField(_propRadius);
                    break;
                case AtivGenerateVRMSpringBoneCollider.ColliderTypes.CapsuleInside:
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
