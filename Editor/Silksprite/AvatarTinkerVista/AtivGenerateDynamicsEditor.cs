using System.Linq;
using Silksprite.AvatarTinkerVista.Common;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateDynamics))]
    [CanEditMultipleObjects]
    class AtivGenerateDynamicsEditor : AtivEditorBase
    {
        LocalizedProperty _stiffness;
        LocalizedProperty _gravityPower;
        LocalizedProperty _gravityDir;
        LocalizedProperty _dragForce;
        LocalizedProperty _radius;
        LocalizedProperty _rootBone;
        LocalizedProperty _center;
        LocalizedProperty _colliderGroups;

        void OnEnable()
        {
            _stiffness = Lop(nameof(AtivGenerateDynamics.stiffness), Loc("AtivGenerateDynamics::stiffness"));
            _gravityPower = Lop(nameof(AtivGenerateDynamics.gravityPower), Loc("AtivGenerateDynamics::gravityPower"));
            _gravityDir = Lop(nameof(AtivGenerateDynamics.gravityDir), Loc("AtivGenerateDynamics::gravityDir"));
            _dragForce = Lop(nameof(AtivGenerateDynamics.dragForce), Loc("AtivGenerateDynamics::dragForce"));
            _radius = Lop(nameof(AtivGenerateDynamics.radius), Loc("AtivGenerateDynamics::radius"));
            _rootBone = Lop(nameof(AtivGenerateDynamics.rootBone), Loc("AtivGenerateDynamics::rootBone"));
            _center = Lop(nameof(AtivGenerateDynamics.center), Loc("AtivGenerateDynamics::center"));
            _colliderGroups = Lop(nameof(AtivGenerateDynamics.colliderGroups), Loc("AtivGenerateDynamics::colliderGroups"));
        }

        protected override void OnInnerInspectorGUI()
        {
            AtivGUILayout.GizmosDarkModeToggle();
            LEditorGUILayout.Prop(_stiffness);
            LEditorGUILayout.Prop(_gravityPower);
            LEditorGUILayout.Prop(_gravityDir);
            LEditorGUILayout.Prop(_dragForce);
            LEditorGUILayout.Prop(_radius);
            LEditorGUILayout.Prop(_rootBone);
            LEditorGUILayout.Prop(_center);
            LEditorGUILayout.Prop(_colliderGroups);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space();

            foreach (var ativ in targets.OfType<AtivGenerateDynamics>())
            {
                if (serializedObject.isEditingMultipleObjects)
                {
                    EditorGUILayout.ObjectField(ativ, typeof(AtivGenerateDynamics), true);
                }
                LGUILayout.Heading(Loc("AtivGenerateDynamics::Joints"));
                using (new EditorGUI.IndentLevelScope(1))
                using (new EditorGUI.DisabledScope(true))
                {
                    foreach (var joint in ativ.GuessJoints())
                    {
                        EditorGUILayout.ObjectField(joint, typeof(Transform), true);
                    }
                }
            }
        }
    }
}
