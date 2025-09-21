using System;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateVRM1Constraint))]
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
            _propKind = serializedObject.FindProperty(nameof(AtivGenerateVRM1Constraint.kind));
            _propSource = serializedObject.FindProperty(nameof(AtivGenerateVRM1Constraint.source));
            _propTarget = serializedObject.FindProperty(nameof(AtivGenerateVRM1Constraint.target));
            _propWeight = serializedObject.FindProperty(nameof(AtivGenerateVRM1Constraint.weight));
            _propAimAxis = serializedObject.FindProperty(nameof(AtivGenerateVRM1Constraint.aimAxis));
            _propRollAxis = serializedObject.FindProperty(nameof(AtivGenerateVRM1Constraint.rollAxis));
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_propKind);
            EditorGUILayout.PropertyField(_propSource);
            EditorGUILayout.PropertyField(_propTarget);
            EditorGUILayout.PropertyField(_propWeight);
            switch ((AtivGenerateVRM1Constraint.ConstraintKind)_propKind.intValue)
            {
                case AtivGenerateVRM1Constraint.ConstraintKind.Aim:
                    EditorGUILayout.PropertyField(_propAimAxis);
                    break;
                case AtivGenerateVRM1Constraint.ConstraintKind.Roll:
                    EditorGUILayout.PropertyField(_propRollAxis);
                    break;
                case AtivGenerateVRM1Constraint.ConstraintKind.Rotation:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
