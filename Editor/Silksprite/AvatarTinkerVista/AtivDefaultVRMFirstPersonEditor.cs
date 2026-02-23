using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivDefaultVRMFirstPerson))]
    class AtivDefaultVRMFirstPersonEditor : AtivEditorBase
    {
        LocalizedProperty _firstPersonOffset;
        LocalizedProperty _defaultValue;

        void OnEnable()
        {
            _firstPersonOffset = Lop(nameof(AtivDefaultVRMFirstPerson.firstPersonOffset), Loc("AtivDefaultVRMFirstPerson::firstPersonOffset"));
            _defaultValue = Lop(nameof(AtivDefaultVRMFirstPerson.defaultValue), Loc("AtivDefaultVRMFirstPerson::defaultValue"));
        }

        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.Prop(_firstPersonOffset);
            LEditorGUILayout.Prop(_defaultValue);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
