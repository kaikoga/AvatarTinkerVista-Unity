using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.Registries;
using Silksprite.Loch;
using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.AvatarTinkerVista.AtivSelectDynamics;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivSelectDynamics))]
    class AtivSelectDynamicsEditor : AtivEditorBase
    {
        LocalizedProperty _options = null!;

        void OnEnable()
        {
            _options = Lop(nameof(AtivSelectDynamics.options), Loc("AtivSelectDynamics::options"));
        }

        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.Prop(_options);
            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomPropertyDrawer(typeof(DynamicsOption))]
    public class DynamicsOptionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            var lop = serializedProperty.Lop(nameof(DynamicsOption.dynamicsId), Loc("DynamicsOption::dynamicsId"));
            DynamicsIdPopup(position, lop);
        }

        static void DynamicsIdPopup(Rect position, LocalizedProperty lop)
        {
            var dynamicsHandles = DynamicsRegistry.All().OrderBy(dynamics => dynamics.Order).ThenBy(dynamics => dynamics.DisplayName).ToArray();
            var dynamicsIds = dynamicsHandles.Select(dynamics => dynamics.Id).ToArray();
            var dynamicsDisplayNames = dynamicsHandles.Select(dynamics => dynamics.DisplayName).ToArray();
            var label = lop.GUIContent;
            EditorGUI.BeginProperty(position, label, lop.Property);
            var selectedIndex = Array.IndexOf(dynamicsIds, lop.Property.stringValue);
            EditorGUI.BeginChangeCheck();
            selectedIndex = EditorGUI.Popup(position, lop.Loc.Tr, selectedIndex, dynamicsDisplayNames);
            if (EditorGUI.EndChangeCheck())
            {
                lop.Property.stringValue = dynamicsIds[selectedIndex];
            }
            EditorGUI.EndProperty();
        }

    }
}
