using System;
using System.Reflection;
using Silksprite.Loch;
using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using Silksprite.Loch.Tools;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.DataObjects
{
    abstract class OverwriteEnumDrawer<T> : PropertyDrawer
    where T : Enum
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            var serializedWillOverwrite = serializedProperty.FindPropertyRelative(nameof(Overwrite<bool>.willOverwrite));
            var serializedValue = serializedProperty.Lop(nameof(Overwrite<bool>.value), LochTool.LocEmpty());
            
            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = Mathf.Max(200f, position.width * 0.6f);
            using (new EditorGUI.PropertyScope(position, label, serializedProperty))
            {
                using (new EditorGUI.DisabledScope(!serializedWillOverwrite.boolValue))
                {
                    LEditorGUI.PropAsEnumPopup<T>(position, serializedValue, label);
                }
                position.x = EditorGUIUtility.labelWidth - 16f;
                position.width = 16f;
                EditorGUIUtility.labelWidth = 1f;
                EditorGUI.PropertyField(position, serializedWillOverwrite);
            }
            EditorGUIUtility.labelWidth = oldLabelWidth;
        }
    }

    [CustomPropertyDrawer(typeof(AtivOverwriteVRMMeta.OverwriteAllowedUser))]
    class OverwriteAllowedUserDrawer : OverwriteEnumDrawer<AtivOverwriteVRMMeta.AllowedUser> { }

    [CustomPropertyDrawer(typeof(AtivOverwriteVRMMeta.OverwriteVRM1CommercialUsageType))]
    class OverwriteVRM1CommercialUsageTypeDrawer : OverwriteEnumDrawer<AtivOverwriteVRMMeta.VRM1CommercialUsageType> { }
    
    [CustomPropertyDrawer(typeof(AtivOverwriteVRMMeta.OverwriteVRM0LicenseType))]
    class OverwriteVRM0LicenseTypeDrawer : OverwriteEnumDrawer<AtivOverwriteVRMMeta.VRM0LicenseType> { }
    
    [CustomPropertyDrawer(typeof(AtivOverwriteVRMMeta.OverwriteVRM1CreditNotationType))]
    class OverwriteVRM1CreditNotationTypeDrawer : OverwriteEnumDrawer<AtivOverwriteVRMMeta.VRM1CreditNotationType> { }

    [CustomPropertyDrawer(typeof(AtivOverwriteVRMMeta.OverwriteVRM1ModificationType))]
    class OverwriteVRM1ModificationTypeDrawer : OverwriteEnumDrawer<AtivOverwriteVRMMeta.VRM1ModificationType> { }
}

