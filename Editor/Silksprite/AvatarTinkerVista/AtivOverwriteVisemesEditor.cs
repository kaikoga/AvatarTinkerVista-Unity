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
using static Silksprite.AvatarTinkerVista.AtivOverwriteVisemes;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivOverwriteVisemes))]
    [CanEditMultipleObjects]
    public class AtivOverwriteVisemesEditor : AtivEditorBase
    {
        AtivOverwriteVisemes[] _overwriteVisemes = null!;
        LocalizedProperty _options = null!;

        public delegate void PlatformUIHandler(AtivOverwriteVisemes overwriteVisemes, Transform transform);
        public static event PlatformUIHandler? PlatformUI;

        void OnEnable()
        {
            _overwriteVisemes = targets.Cast<AtivOverwriteVisemes>().ToArray();
            _options = Lop(nameof(AtivOverwriteBlink.options), Loc("AtivOverwriteVRCVisemes::options"));
        }

        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.Prop(_options);

            serializedObject.ApplyModifiedProperties();

            var overwriteVisemes = _overwriteVisemes.First();
            if (AtivRuntimeUtil.FindAvatarInParents(overwriteVisemes.transform) is { } avatarRoot)
            {
                PlatformUI?.Invoke(overwriteVisemes, avatarRoot);
            }
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

            var faceMesh = serializedProperty.Lop(nameof(VisemeOption.faceMesh), Loc("VisemeOption::faceMesh"));
            var faceSkinnedMesh = AtivEditorUtil.ResolveAvatarRelativeSkinnedMeshRenderer(faceMesh);

            switch (GetVisemeStyle(visemeStyle))
            {
                case VisemeStyle.Inherit:
                    break;
                case VisemeStyle.None:
                    break;
                case VisemeStyle.SingleBlendShape:
                    LEditorGUI.Prop(Next(), faceMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.singleBlendShape), Loc("VisemeOption::singleBlendShape")), faceSkinnedMesh);
                    break;
                case VisemeStyle.VrmBlendShapes:
                    LEditorGUI.Prop(Next(), faceMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.vrmA), Loc("VisemeOption::vrmA")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.vrmI), Loc("VisemeOption::vrmI")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.vrmU), Loc("VisemeOption::vrmU")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.vrmE), Loc("VisemeOption::vrmE")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.vrmO), Loc("VisemeOption::vrmO")), faceSkinnedMesh);
                    break;
                case VisemeStyle.OculusVisemes:
                    LEditorGUI.Prop(Next(), faceMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusSil), Loc("VisemeOption::oculusSil")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusPp), Loc("VisemeOption::oculusPp")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusFf), Loc("VisemeOption::oculusFf")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusTh), Loc("VisemeOption::oculusTh")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusDd), Loc("VisemeOption::oculusDd")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusKk), Loc("VisemeOption::oculusKk")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusCh), Loc("VisemeOption::oculusCh")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusSs), Loc("VisemeOption::oculusSs")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusNn), Loc("VisemeOption::oculusNn")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusRr), Loc("VisemeOption::oculusRr")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusAa), Loc("VisemeOption::oculusAa")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusE), Loc("VisemeOption::oculusE")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusI), Loc("VisemeOption::oculusI")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusO), Loc("VisemeOption::oculusO")), faceSkinnedMesh);
                    AtivGUI.PropAsBlendShapeName(Next(), serializedProperty.Lop(nameof(VisemeOption.oculusU), Loc("VisemeOption::oculusU")), faceSkinnedMesh);
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
                VisemeStyle.Inherit => 1,
                VisemeStyle.None => 1,
                VisemeStyle.SingleBlendShape => 3,
                VisemeStyle.VrmBlendShapes => 7,
                VisemeStyle.OculusVisemes => 17,
                _ => throw new ArgumentOutOfRangeException()
            };
            return result;
        }
        
        static VisemeStyle GetVisemeStyle(LocalizedProperty visemeStyle) => (VisemeStyle)Enum.ToObject(typeof(VisemeStyle), visemeStyle.Property.intValue);
    }
}
