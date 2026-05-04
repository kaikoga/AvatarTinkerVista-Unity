using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivMergeVRMFirstPerson))]
    [CanEditMultipleObjects]
    class AtivMergeVRMFirstPersonEditor : AtivEditorBase
    {
        LocalizedProperty _renderers = null!;

        void OnEnable()
        {
            _renderers = Lop(nameof(AtivMergeVRMFirstPerson.renderers), Loc("AtivMergeVRMFirstPerson::renderers"));
        }
        
        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.Prop(_renderers);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
