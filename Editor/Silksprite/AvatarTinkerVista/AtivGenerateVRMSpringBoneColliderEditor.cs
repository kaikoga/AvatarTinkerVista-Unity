using System;
using Silksprite.AvatarTinkerVista.Common;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateDynamicsCollider))]
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
            _propRenderers = serializedObject.FindProperty(nameof(AtivGenerateDynamicsCollider.colliderType));
            _propRootBone = serializedObject.FindProperty(nameof(AtivGenerateDynamicsCollider.rootBone));
            _propOffset = serializedObject.FindProperty(nameof(AtivGenerateDynamicsCollider.offset));
            _propRadius = serializedObject.FindProperty(nameof(AtivGenerateDynamicsCollider.radius));
            _propTail = serializedObject.FindProperty(nameof(AtivGenerateDynamicsCollider.tail));
            _propNormal = serializedObject.FindProperty(nameof(AtivGenerateDynamicsCollider.normal));
        }
        
        public override void OnInspectorGUI()
        {
            AtivGUILayout.GizmosDarkModeToggle();
            EditorGUILayout.PropertyField(_propRenderers);
            EditorGUILayout.PropertyField(_propRootBone);
            EditorGUILayout.PropertyField(_propOffset);
            switch ((AtivGenerateDynamicsCollider.ColliderTypes)_propRenderers.intValue)
            {
                case AtivGenerateDynamicsCollider.ColliderTypes.Sphere:
                    EditorGUILayout.PropertyField(_propRadius);
                    break;
                case AtivGenerateDynamicsCollider.ColliderTypes.Capsule:
                    EditorGUILayout.PropertyField(_propRadius);
                    EditorGUILayout.PropertyField(_propTail);
                    break;
                case AtivGenerateDynamicsCollider.ColliderTypes.Plane:
                    EditorGUILayout.PropertyField(_propNormal);
                    break;
                case AtivGenerateDynamicsCollider.ColliderTypes.SphereInside:
                    EditorGUILayout.PropertyField(_propRadius);
                    break;
                case AtivGenerateDynamicsCollider.ColliderTypes.CapsuleInside:
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
