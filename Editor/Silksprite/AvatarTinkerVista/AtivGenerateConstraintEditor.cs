using System;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateConstraint))]
    [CanEditMultipleObjects]
    class AtivGenerateConstraintEditor : AtivEditorBase
    {
        LocalizedProperty _kind;
        LocalizedProperty _source;
        LocalizedProperty _target;
        LocalizedProperty _weight;
        LocalizedProperty _aimAxis;
        LocalizedProperty _rollAxis;

        void OnEnable()
        {
            _kind = Lop(nameof(AtivGenerateConstraint.kind), Loc("AtivGenerateConstraint::kind"));
            _source = Lop(nameof(AtivGenerateConstraint.source), Loc("AtivGenerateConstraint::source"));
            _target = Lop(nameof(AtivGenerateConstraint.target), Loc("AtivGenerateConstraint::target"));
            _weight = Lop(nameof(AtivGenerateConstraint.weight), Loc("AtivGenerateConstraint::weight"));
            _aimAxis = Lop(nameof(AtivGenerateConstraint.aimAxis), Loc("AtivGenerateConstraint::aimAxis"));
            _rollAxis = Lop(nameof(AtivGenerateConstraint.rollAxis), Loc("AtivGenerateConstraint::rollAxis"));
        }
        
        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.Prop(_kind);
            LEditorGUILayout.Prop(_source);
            LEditorGUILayout.Prop(_target);
            LEditorGUILayout.Prop(_weight);
            switch ((AtivGenerateConstraint.ConstraintKind)_kind.Property.intValue)
            {
                case AtivGenerateConstraint.ConstraintKind.Aim:
                    LEditorGUILayout.Prop(_aimAxis);
                    break;
                case AtivGenerateConstraint.ConstraintKind.Roll:
                    LEditorGUILayout.Prop(_rollAxis);
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
