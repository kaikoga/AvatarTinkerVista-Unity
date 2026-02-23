using System;
using Silksprite.AvatarTinkerVista.Common;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateDynamicsCollider))]
    [CanEditMultipleObjects]
    class AtivGenerateDynamicsColliderEditor : AtivEditorBase
    {
        LocalizedProperty _renderers;
        LocalizedProperty _rootBone;
        LocalizedProperty _offset;
        LocalizedProperty _radius;
        LocalizedProperty _tail;
        LocalizedProperty _normal;

        void OnEnable()
        {
            _renderers = Lop(nameof(AtivGenerateDynamicsCollider.colliderType), Loc("AtivGenerateDynamicsCollider::colliderType"));
            _rootBone = Lop(nameof(AtivGenerateDynamicsCollider.rootBone), Loc("AtivGenerateDynamicsCollider::rootBone"));
            _offset = Lop(nameof(AtivGenerateDynamicsCollider.offset), Loc("AtivGenerateDynamicsCollider::offset"));
            _radius = Lop(nameof(AtivGenerateDynamicsCollider.radius), Loc("AtivGenerateDynamicsCollider::radius"));
            _tail = Lop(nameof(AtivGenerateDynamicsCollider.tail), Loc("AtivGenerateDynamicsCollider::tail"));
            _normal = Lop(nameof(AtivGenerateDynamicsCollider.normal), Loc("AtivGenerateDynamicsCollider::normal"));
        }
        
        protected override void OnInnerInspectorGUI()
        {
            AtivGUILayout.GizmosDarkModeToggle();
            LEditorGUILayout.Prop(_renderers);
            LEditorGUILayout.Prop(_rootBone);
            LEditorGUILayout.Prop(_offset);
            switch ((AtivGenerateDynamicsCollider.ColliderTypes)_renderers.Property.intValue)
            {
                case AtivGenerateDynamicsCollider.ColliderTypes.Sphere:
                    LEditorGUILayout.Prop(_radius);
                    break;
                case AtivGenerateDynamicsCollider.ColliderTypes.Capsule:
                    LEditorGUILayout.Prop(_radius);
                    LEditorGUILayout.Prop(_tail);
                    break;
                case AtivGenerateDynamicsCollider.ColliderTypes.Plane:
                    LEditorGUILayout.Prop(_normal);
                    break;
                case AtivGenerateDynamicsCollider.ColliderTypes.SphereInside:
                    LEditorGUILayout.Prop(_radius);
                    break;
                case AtivGenerateDynamicsCollider.ColliderTypes.CapsuleInside:
                    LEditorGUILayout.Prop(_radius);
                    LEditorGUILayout.Prop(_tail);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
