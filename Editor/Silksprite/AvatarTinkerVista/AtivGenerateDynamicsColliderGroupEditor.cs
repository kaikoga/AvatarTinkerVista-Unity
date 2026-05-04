using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivGenerateDynamicsColliderGroup))]
    [CanEditMultipleObjects]
    class AtivGenerateDynamicsColliderGroupEditor : AtivEditorBase
    {
        LocalizedProperty _colliders = null!;

        void OnEnable()
        {
            _colliders = Lop(nameof(AtivGenerateDynamicsColliderGroup.colliders), Loc("AtivGenerateDynamicsColliderGroup.colliders"));
        }
        
        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.Prop(_colliders);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
