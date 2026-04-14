using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.Utils;
using Silksprite.Loch;
using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.AvatarTinkerVista.AtivOverwriteViewPosition;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivOverwriteViewPosition))]
    [CanEditMultipleObjects]
    public class AtivOverwriteViewPositionEditor : AtivEditorBase
    {
        AtivOverwriteViewPosition[] _overwriteViewPositions;
        LocalizedProperty _options;

        public delegate void PlatformUIHandler(AtivOverwriteViewPosition overwriteViewPosition, Transform transform);
        public static event PlatformUIHandler PlatformUI;

        void OnEnable()
        {
            _overwriteViewPositions = targets.Cast<AtivOverwriteViewPosition>().ToArray();
            _options = Lop(nameof(AtivOverwriteViewPosition.options), Loc("AtivOverwriteViewPosition::options"));
        }

        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.Prop(_options);

            serializedObject.ApplyModifiedProperties();

            var overwriteViewPosition = _overwriteViewPositions.First();
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(overwriteViewPosition.transform);
            PlatformUI?.Invoke(overwriteViewPosition, avatarRoot);
        }
    }

    [CustomPropertyDrawer(typeof(ViewPositionOption))]
    public class ViewPositionOptionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            Rect Next()
            {
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                return position;
            }
            var viewPositionStyle = serializedProperty.Lop(nameof(ViewPositionOption.viewPositionStyle), Loc("ViewPositionOption::viewPositionStyle"));
            position.height = EditorGUIUtility.singleLineHeight;
            LEditorGUI.PropAsEnumPopup<ViewPositionStyle>(position, viewPositionStyle);
            switch (GetViewPositionStyle(viewPositionStyle))
            {
                case ViewPositionStyle.Global:
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(ViewPositionOption.globalPosition), Loc("ViewPositionOption::globalPosition")));
                    break;
                case ViewPositionStyle.HeadLocal:
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(ViewPositionOption.headLocalPosition), Loc("ViewPositionOption::headLocalPosition")));
                    break;
                case ViewPositionStyle.TransformLocal:
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(ViewPositionOption.transform), Loc("ViewPositionOption::transform")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(ViewPositionOption.transformLocalPosition), Loc("ViewPositionOption::transformLocalPosition")));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            var viewPositionStyle = serializedProperty.Lop(nameof(ViewPositionOption.viewPositionStyle), Loc("ViewPositionOption::viewPositionStyle"));
            var result = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * GetViewPositionStyle(viewPositionStyle) switch
            {
                ViewPositionStyle.Global => 2,
                ViewPositionStyle.HeadLocal => 2,
                ViewPositionStyle.TransformLocal => 3,
                _ => throw new ArgumentOutOfRangeException()
            };
            return result;
        }
        
        static ViewPositionStyle GetViewPositionStyle(LocalizedProperty visemeStyle) => (ViewPositionStyle)Enum.ToObject(typeof(ViewPositionStyle), visemeStyle.Property.intValue);
    }
}
