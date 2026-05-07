using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.Utils;
using Silksprite.Loch;
using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.AvatarTinkerVista.AtivOverwriteVRCBlink;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivOverwriteVRCBlink))]
    [CanEditMultipleObjects]
    public class AtivOverwriteVRCBlinkEditor : AtivEditorBase
    {
        AtivOverwriteVRCBlink[] _overwriteBlinks = null!;
        LocalizedProperty _options = null!;

        public delegate void PlatformUIHandler(AtivOverwriteVRCBlink overwriteBlink, Transform transform);
        public static event PlatformUIHandler? PlatformUI;

        void OnEnable()
        {
            _overwriteBlinks = targets.Cast<AtivOverwriteVRCBlink>().ToArray();
            _options = Lop(nameof(AtivOverwriteVRCBlink.options), Loc("AtivOverwriteVRCBlink::options"));
        }

        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.Prop(_options);
            serializedObject.ApplyModifiedProperties();
            
            var overwriteBlink = _overwriteBlinks.First();
            if (AtivRuntimeUtil.FindAvatarInParents(overwriteBlink.transform) is { } avatarRoot)
            {
                PlatformUI?.Invoke(overwriteBlink, avatarRoot);
            }
        }
    }

    [CustomPropertyDrawer(typeof(BlinkOption))]
    public class BlinkOptionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            Rect Next()
            {
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                return position;
            }

            var blinkStyle = serializedProperty.Lop(nameof(BlinkOption.blinkStyle), Loc("BlinkOption::blinkStyle"));
            position.height = EditorGUIUtility.singleLineHeight;
            LEditorGUI.PropAsEnumPopup<BlinkStyle>(position, blinkStyle);
            
            var faceMesh = serializedProperty.Lop(nameof(BlinkOption.faceMesh), Loc("BlinkOption::faceMesh"));
            var faceSkinnedMesh = AtivEditorUtil.ResolveAvatarRelativeSkinnedMeshRenderer(faceMesh);

            switch (GetBlinkStyle(blinkStyle))
            {
                case BlinkStyle.Inherit:
                    break;
                case BlinkStyle.None:
                    break;
                case BlinkStyle.SingleBlendShape:
                    LEditorGUI.Prop(Next(), faceMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(BlinkOption.singleBlendShape), Loc("BlinkOption::singleBlendShape")), faceSkinnedMesh);
                    break;
                case BlinkStyle.SeparateBlendShapes:
                    LEditorGUI.Prop(Next(), faceMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(BlinkOption.separateBlendShapeLeft), Loc("BlinkOption::separateBlendShapeLeft")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(BlinkOption.separateBlendShapeRight), Loc("BlinkOption::separateBlendShapeRight")), faceSkinnedMesh);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            var blinkStyle = serializedProperty.Lop(nameof(BlinkOption.blinkStyle), Loc("BlinkOption::blinkStyle"));
            var result = (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * GetBlinkStyle(blinkStyle) switch
            {
                BlinkStyle.Inherit => 1,
                BlinkStyle.None => 1,
                BlinkStyle.SingleBlendShape => 3,
                BlinkStyle.SeparateBlendShapes => 4,
                _ => throw new ArgumentOutOfRangeException()
            };
            return result;
        }

        static BlinkStyle GetBlinkStyle(LocalizedProperty blinkStyle) => (BlinkStyle)Enum.ToObject(typeof(BlinkStyle), blinkStyle.Property.intValue);
    }
}
