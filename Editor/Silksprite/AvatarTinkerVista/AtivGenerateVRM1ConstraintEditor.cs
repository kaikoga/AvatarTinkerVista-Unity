using System;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateConstraint))]
    [CanEditMultipleObjects]
    class AtivGenerateVRM1ConstraintEditor : Editor
    {
        SerializedProperty _propKind;
        SerializedProperty _propSource;
        SerializedProperty _propTarget;
        SerializedProperty _propWeight;
        SerializedProperty _propAimAxis;
        SerializedProperty _propRollAxis;

        void OnEnable()
        {
            _propKind = serializedObject.FindProperty(nameof(AtivGenerateConstraint.kind));
            _propSource = serializedObject.FindProperty(nameof(AtivGenerateConstraint.source));
            _propTarget = serializedObject.FindProperty(nameof(AtivGenerateConstraint.target));
            _propWeight = serializedObject.FindProperty(nameof(AtivGenerateConstraint.weight));
            _propAimAxis = serializedObject.FindProperty(nameof(AtivGenerateConstraint.aimAxis));
            _propRollAxis = serializedObject.FindProperty(nameof(AtivGenerateConstraint.rollAxis));
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_propKind);
            EditorGUILayout.PropertyField(_propSource);
            EditorGUILayout.PropertyField(_propTarget);
            EditorGUILayout.PropertyField(_propWeight);
            switch ((AtivGenerateConstraint.ConstraintKind)_propKind.intValue)
            {
                case AtivGenerateConstraint.ConstraintKind.Aim:
                    EditorGUILayout.PropertyField(_propAimAxis);
                    break;
                case AtivGenerateConstraint.ConstraintKind.Roll:
                    EditorGUILayout.PropertyField(_propRollAxis);
                    break;
                case AtivGenerateConstraint.ConstraintKind.Rotation:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
