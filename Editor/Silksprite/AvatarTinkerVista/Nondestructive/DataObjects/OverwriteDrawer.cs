using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Nondestructive.DataObjects
{
    [CustomPropertyDrawer(typeof(OverwriteBool))]
    [CustomPropertyDrawer(typeof(OverwriteString))]
    [CustomPropertyDrawer(typeof(OverwriteTexture2D))]
    [CustomPropertyDrawer(typeof(OverwriteAvatarRelativeTransform))]
    [CustomPropertyDrawer(typeof(AtivOverwriteVRMMeta.OverwriteAllowedUser))]
    [CustomPropertyDrawer(typeof(AtivOverwriteVRMMeta.OverwriteVRM1CommercialUsageType))]
    [CustomPropertyDrawer(typeof(AtivOverwriteVRMMeta.OverwriteVRM0LicenseType))]
    [CustomPropertyDrawer(typeof(AtivOverwriteVRMMeta.OverwriteVRM1CreditNotationType))]
    [CustomPropertyDrawer(typeof(AtivOverwriteVRMMeta.OverwriteVRM1ModificationType))]
    public class OverwriteDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            var serializedWillOverwrite = serializedProperty.FindPropertyRelative(nameof(Overwrite<bool>.willOverwrite));
            var serializedValue = serializedProperty.FindPropertyRelative(nameof(Overwrite<bool>.value));
            
            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = Mathf.Max(200f, position.width * 0.6f);
            using (new EditorGUI.PropertyScope(position, label, serializedProperty))
            {
                using (new EditorGUI.DisabledScope(!serializedWillOverwrite.boolValue))
                {
                    EditorGUI.PropertyField(position, serializedValue, label);
                }
                position.x = EditorGUIUtility.labelWidth - 16f;
                position.width = 16f;
                EditorGUIUtility.labelWidth = 1f;
                EditorGUI.PropertyField(position, serializedWillOverwrite);
            }
            EditorGUIUtility.labelWidth = oldLabelWidth;
        }
    }

    [CustomPropertyDrawer(typeof(OverwriteVector3))]
    [CustomPropertyDrawer(typeof(OverwriteBounds))]
    public class MultilineOverwriteDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            var serializedWillOverwrite = serializedProperty.FindPropertyRelative(nameof(Overwrite<bool>.willOverwrite));
            var serializedValue = serializedProperty.FindPropertyRelative(nameof(Overwrite<bool>.value));
            
            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = Mathf.Max(200f, position.width * 0.6f);
            using (new EditorGUI.PropertyScope(position, label, serializedProperty))
            {
                using (new EditorGUI.DisabledScope(!serializedWillOverwrite.boolValue))
                {
                    EditorGUI.PropertyField(position, serializedValue, label);
                }
                position.x = EditorGUIUtility.labelWidth - 16f;
                position.width = 16f;
                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUIUtility.labelWidth = 1f;
                EditorGUI.PropertyField(position, serializedWillOverwrite);
            }
            EditorGUIUtility.labelWidth = oldLabelWidth;
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            var serializedValue = serializedProperty.FindPropertyRelative(nameof(Overwrite<bool>.value));
            return EditorGUI.GetPropertyHeight(serializedValue);
        }
    }
}

