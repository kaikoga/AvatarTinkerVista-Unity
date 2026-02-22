using Silksprite.Loch;
using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using Silksprite.Loch.Tools;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivMergeVRMFirstPerson))]
    [CanEditMultipleObjects]
    class AtivMergeVRMFirstPersonEditor : Editor
    {
        LocalizedProperty _renderers;

        void OnEnable()
        {
            _renderers = serializedObject.Lop(nameof(AtivMergeVRMFirstPerson.renderers), LochTool.Loc("AtivMergeVRMFirstPerson::renderers"));
        }
        
        public override void OnInspectorGUI()
        {
            LEditorGUILayout.Prop(_renderers);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
