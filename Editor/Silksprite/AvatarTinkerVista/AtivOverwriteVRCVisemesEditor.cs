using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.Utils;
using Silksprite.Loch;
using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.AvatarTinkerVista.AtivOverwriteVRCVisemes;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivOverwriteVRCVisemes))]
    [CanEditMultipleObjects]
    public class AtivOverwriteVRCVisemesEditor : AtivEditorBase
    {
        AtivOverwriteVRCVisemes[] _overwriteVisemes;
        LocalizedProperty _options;

        public delegate void PlatformUIHandler(AtivOverwriteVRCVisemes overwriteVisemes, Transform transform);
        public static event PlatformUIHandler PlatformUI;

        void OnEnable()
        {
            _overwriteVisemes = targets.Cast<AtivOverwriteVRCVisemes>().ToArray();
            _options = Lop(nameof(AtivOverwriteVRCBlink.options), Loc("AtivOverwriteVRCVisemes::options"));
        }

        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.Prop(_options);

            serializedObject.ApplyModifiedProperties();

            var overwriteVisemes = _overwriteVisemes.First();
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(overwriteVisemes.transform);
            PlatformUI?.Invoke(overwriteVisemes, avatarRoot);
        }
    }

    [CustomPropertyDrawer(typeof(VisemeOption))]
    public class VisemeOptionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            Rect Next()
            {
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                return position;
            }
            var visemeStyle = serializedProperty.Lop(nameof(VisemeOption.visemeStyle), Loc("VisemeOption::visemeStyle"));
            position.height = EditorGUIUtility.singleLineHeight;
            LEditorGUI.PropAsEnumPopup<VisemeStyle>(position, visemeStyle);
            switch (GetVisemeStyle(visemeStyle))
            {
                case VisemeStyle.None:
                    break;
                case VisemeStyle.SingleBlendShape:
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.faceMesh), Loc("VisemeOption::faceMesh")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.singleBlendShape), Loc("VisemeOption::singleBlendShape")));
                    break;
                case VisemeStyle.OculusVisemes:
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.faceMesh), Loc("VisemeOption::faceMesh")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusSil), Loc("VisemeOption::oculusSil")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusPp), Loc("VisemeOption::oculusPp")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusFf), Loc("VisemeOption::oculusFf")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusTh), Loc("VisemeOption::oculusTh")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusDd), Loc("VisemeOption::oculusDd")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusKk), Loc("VisemeOption::oculusKk")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusCh), Loc("VisemeOption::oculusCh")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusSs), Loc("VisemeOption::oculusSs")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusNn), Loc("VisemeOption::oculusNn")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusRr), Loc("VisemeOption::oculusRr")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusAa), Loc("VisemeOption::oculusAa")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusE), Loc("VisemeOption::oculusE")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusI), Loc("VisemeOption::oculusI")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusO), Loc("VisemeOption::oculusO")));
                    LEditorGUI.Prop(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusU), Loc("VisemeOption::oculusU")));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            var visemeStyle = serializedProperty.Lop(nameof(VisemeOption.visemeStyle), Loc("VisemeOption::visemeStyle"));
            var result = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * GetVisemeStyle(visemeStyle) switch
            {
                VisemeStyle.None => 1,
                VisemeStyle.SingleBlendShape => 3,
                VisemeStyle.OculusVisemes => 17,
                _ => throw new ArgumentOutOfRangeException()
            };
            return result;
        }
        
        static VisemeStyle GetVisemeStyle(LocalizedProperty visemeStyle) => (VisemeStyle)Enum.ToObject(typeof(VisemeStyle), visemeStyle.Property.intValue);
    }
}
