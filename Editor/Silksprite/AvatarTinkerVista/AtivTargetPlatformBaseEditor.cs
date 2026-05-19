using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    abstract class AtivTargetPlatformBaseEditor<T> : AtivEditorBase
    where T : AtivTargetPlatformBase
    {
        protected abstract bool EnableUseOutputPlatform { get; }

        T[] _targets = null!;
        AtivPlatformHandle[] _allPlatforms = null!;

        LocalizedProperty _useOutputPlatform = null!;

        SerializedProperty _focusPlatformMode = null!;

        protected virtual void OnEnable()
        {
            _targets = targets.OfType<T>().ToArray();
            _allPlatforms = _targets.First().AllPlatforms().ToArray();

            if (EnableUseOutputPlatform)
            {
                _useOutputPlatform = Lop("useOutputPlatform", Loc("AtivTargetPlatformBase::useOutputPlatform"));
            }

            _focusPlatformMode = serializedObject.FindProperty(nameof(AtivTargetPlatformBase.targetPlatformMode));
        }
        
        protected override void OnInnerInspectorGUI()
        {
            if (EnableUseOutputPlatform)
            {
                LEditorGUILayout.Prop(_useOutputPlatform);
            }
            serializedObject.ApplyModifiedProperties();

            LGUILayout.Heading(Loc("AtivTargetPlatformBase::Platforms"));

            serializedObject.Update();
            EditorGUI.showMixedValue = _focusPlatformMode.hasMultipleDifferentValues;
            EditorGUI.BeginChangeCheck();
            var newFocusModeIsExclude = EditorGUILayout.Toggle("Any Platform", _focusPlatformMode.intValue == (int)AtivTargetPlatformBase.TargetPlatformMode.Exclude);
            if (EditorGUI.EndChangeCheck())
            {
                foreach (var t in _targets)
                {
                    Undo.RecordObject(t, "Change Focus Platform Mode");
                    t.SetTargetPlatformMode(newFocusModeIsExclude ? AtivTargetPlatformBase.TargetPlatformMode.Exclude : AtivTargetPlatformBase.TargetPlatformMode.Include);
                }
                serializedObject.Update();
                serializedObject.SetIsDifferentCacheDirty();
                _focusPlatformMode = serializedObject.FindProperty(nameof(AtivTargetPlatformBase.targetPlatformMode));
            }
            EditorGUILayout.Space();

            var selectedPlatformIds = _targets.Select(t => t.SelectedPlatformId()).ToArray();
            foreach (var platform in _allPlatforms)
            {
                var isFocus = _targets.Select(t => t.GetIsTargetPlatform(platform)).Distinct().ToArray();
                EditorGUI.showMixedValue = isFocus.Length > 1;
                EditorGUI.BeginChangeCheck();
                var isSelected = selectedPlatformIds.Contains(platform.Id);
                var oldFontStyle = EditorStyles.label.fontStyle;
                EditorStyles.label.fontStyle = isSelected ? FontStyle.Bold : FontStyle.Normal;
                var newValue = EditorGUILayout.Toggle((isSelected ? "* ": "") + platform.DisplayName, isFocus[0]);
                EditorStyles.label.fontStyle = oldFontStyle;
                if (EditorGUI.EndChangeCheck())
                {
                    foreach (var t in _targets)
                    {
                        Undo.RecordObject(t, "Change Focus Platform");
                        t.SetIsTargetPlatform(platform, newValue);
                    }
                }
            }
            EditorGUI.showMixedValue = false;

            using (new EditorGUILayout.HorizontalScope())
            {
                if (LGUILayout.Button(Loc("AtivTargetPlatformBase::SelectAll")))
                {
                    foreach (var t in _targets)
                    {
                        Undo.RecordObject(t, "Select All Platforms");
                        t.SetAllTargetPlatform(true);
                    }
                }
                if (LGUILayout.Button(Loc("AtivTargetPlatformBase::DeselectAll")))
                {
                    foreach (var t in _targets)
                    {
                        Undo.RecordObject(t, "Deselect All Platforms");
                        t.SetAllTargetPlatform(false);
                    }
                }
            }
        }
    }

    [CustomEditor(typeof(AtivTargetUnityPlatform))]
    [CanEditMultipleObjects]
    class AtivTargetUnityPlatformEditor : AtivTargetPlatformBaseEditor<AtivTargetUnityPlatform>
    {
        protected override bool EnableUseOutputPlatform => false;
    }

    [CustomEditor(typeof(AtivTargetNdmfPlatform))]
    [CanEditMultipleObjects]
    class AtivTargetNdmfPlatformEditor : AtivTargetPlatformBaseEditor<AtivTargetNdmfPlatform>
    {
        protected override bool EnableUseOutputPlatform => true;
    }

    [CustomEditor(typeof(AtivTargetAbletPlatform))]
    [CanEditMultipleObjects]
    class AtivTargetAbletPlatformEditor : AtivTargetPlatformBaseEditor<AtivTargetAbletPlatform>
    {
        protected override bool EnableUseOutputPlatform => false;
    }

    [CustomEditor(typeof(AtivTargetAbletSubplatform))]
    [CanEditMultipleObjects]
    class AtivTargetAbletSubplatformEditor : AtivTargetPlatformBaseEditor<AtivTargetAbletSubplatform>
    {
        protected override bool EnableUseOutputPlatform => false;
    }

    [CustomEditor(typeof(AtivTargetSelectedDynamicsPlatform))]
    [CanEditMultipleObjects]
    class AtivTargetSelectedDynamicsPlatformEditor : AtivTargetPlatformBaseEditor<AtivTargetSelectedDynamicsPlatform>
    {
        protected override bool EnableUseOutputPlatform => false;
    }

}
