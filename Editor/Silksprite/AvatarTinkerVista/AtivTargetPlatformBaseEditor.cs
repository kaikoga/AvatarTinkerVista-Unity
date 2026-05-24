using System;
using System.Collections.Generic;
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
        Dictionary<Type, Component> _allComponentTypes = null!;

        LocalizedProperty _useOutputPlatform = null!;
        SerializedProperty _focusPlatformMode = null!;

        LocalizedProperty _applyMode = null!;

        protected virtual void OnEnable()
        {
            _targets = targets.OfType<T>().ToArray();
            var soleTarget = _targets.First();
            _allPlatforms = soleTarget.AllPlatforms().ToArray();
            _allComponentTypes = _targets.SelectMany(t => t.GetComponents<Component>())
                .Where(component => component switch
                {
                    null => false,
                    Transform => false,
                    AtivTargetPlatformBase => false,
                    _ => true
                })
                .GroupBy(component => component.GetType())
                .OrderBy(g => g.Key.FullName)
                .ToDictionary(g => g.Key, g => g.First());

            if (EnableUseOutputPlatform)
            {
                _useOutputPlatform = Lop("useOutputPlatform", Loc("AtivTargetPlatformBase::useOutputPlatform"));
            }
            _applyMode = Lop(nameof(AtivTargetPlatformBase.applyMode), Loc("AtivTargetPlatformBase::applyMode"));

            _focusPlatformMode = serializedObject.FindProperty(nameof(AtivTargetPlatformBase.targetPlatformMode));
        }
        
        protected override void OnInnerInspectorGUI()
        {
            if (EnableUseOutputPlatform)
            {
                LEditorGUILayout.Prop(_useOutputPlatform);
                serializedObject.ApplyModifiedProperties();
            }

            PlatformSectionGUI();
            ApplyModeGUI();
        }

        void PlatformSectionGUI()
        {
            LGUILayout.Heading(Loc("AtivTargetPlatformBase::Platforms"));

            serializedObject.Update();
            EditorGUI.showMixedValue = _focusPlatformMode.hasMultipleDifferentValues;
            EditorGUI.BeginChangeCheck();
            var newFocusModeIsExclude = EditorGUILayout.ToggleLeft("Any Platform", _focusPlatformMode.intValue == (int)AtivTargetPlatformBase.TargetPlatformMode.Exclude);
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
                var newValue = EditorGUILayout.ToggleLeft((isSelected ? "* " : "") + platform.DisplayName, isFocus[0]);
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
                if (LGUILayout.Button(Loc("AtivTargetPlatformBase::SelectAllPlatform")))
                {
                    foreach (var t in _targets)
                    {
                        Undo.RecordObject(t, "Select All Platforms");
                        t.SetAllTargetPlatform(true);
                    }
                }
                if (LGUILayout.Button(Loc("AtivTargetPlatformBase::DeselectAllPlatform")))
                {
                    foreach (var t in _targets)
                    {
                        Undo.RecordObject(t, "Deselect All Platforms");
                        t.SetAllTargetPlatform(false);
                    }
                }
            }
        }

        void ApplyModeGUI()
        {
            LGUILayout.Heading(Loc("AtivTargetPlatformBase::ApplyMode"));

            LEditorGUILayout.PropAsEnumPopup<AtivTargetPlatformBase.ApplyMode>(_applyMode);
            serializedObject.ApplyModifiedProperties();
            if (_applyMode.Property is { hasMultipleDifferentValues: false, intValue: (int)AtivTargetPlatformBase.ApplyMode.GameObject})
            {
                return;
            }

            foreach (var (type, component) in _allComponentTypes)
            {
                var isFocus = _targets.Select(t => t.componentTypeQualifiedNames.Contains(type.FullName)).Distinct().ToArray();
                EditorGUI.showMixedValue = isFocus.Length > 1;
                EditorGUI.BeginChangeCheck();
                var toggleLabel = new GUIContent(type.Name, EditorGUIUtility.GetIconForObject(component) ?? AssetPreview.GetMiniTypeThumbnail(type));
                var newValue = EditorGUILayout.ToggleLeft(toggleLabel, isFocus[0]);
                if (EditorGUI.EndChangeCheck())
                {
                    foreach (var t in _targets)
                    {
                        Undo.RecordObject(t, "Change Apply To Component");
                        t.componentTypeQualifiedNames.Remove(type.FullName);
                        if (newValue)
                        {
                            t.componentTypeQualifiedNames.Add(type.FullName);
                        }
                    }
                }
            }
            EditorGUI.showMixedValue = false;

            using (new EditorGUILayout.HorizontalScope())
            {
                if (LGUILayout.Button(Loc("AtivTargetPlatformBase::SelectAllComponent")))
                {
                    var allTypeNames = _allComponentTypes.Select(type => type.Key.FullName).ToArray();
                    foreach (var t in _targets)
                    {
                        Undo.RecordObject(t, "Select All Components");
                        t.componentTypeQualifiedNames.Clear();
                        t.componentTypeQualifiedNames.AddRange(allTypeNames);
                    }
                }
                if (LGUILayout.Button(Loc("AtivTargetPlatformBase::DeselectAllComponent")))
                {
                    foreach (var t in _targets)
                    {
                        Undo.RecordObject(t, "Deselect All Components");
                        t.componentTypeQualifiedNames.Clear();
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
