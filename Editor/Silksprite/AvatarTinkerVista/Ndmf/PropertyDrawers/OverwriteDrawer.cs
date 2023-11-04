using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(AtvOverwriteVrmMeta.OverwriteBool))]
    [CustomPropertyDrawer(typeof(AtvOverwriteVrmMeta.OverwriteString))]
    [CustomPropertyDrawer(typeof(AtvOverwriteVrmMeta.OverwriteTexture2D))]
    [CustomPropertyDrawer(typeof(AtvOverwriteVrmMeta.OverwriteAllowedUser))]
    [CustomPropertyDrawer(typeof(AtvOverwriteVrmMeta.OverwriteVrm1CommercialUsageType))]
    [CustomPropertyDrawer(typeof(AtvOverwriteVrmMeta.OverwriteVrm0LicenseType))]
    [CustomPropertyDrawer(typeof(AtvOverwriteVrmMeta.OverwriteVrm1CreditNotationType))]
    [CustomPropertyDrawer(typeof(AtvOverwriteVrmMeta.OverwriteVrm1ModificationType))]
    public class OverwriteDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            var serializedWillOverwrite = serializedProperty.FindPropertyRelative(nameof(AtvOverwriteVrmMeta.Overwrite<bool>.willOverwrite));
            var serializedValue = serializedProperty.FindPropertyRelative(nameof(AtvOverwriteVrmMeta.Overwrite<bool>.value));
            
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
}

