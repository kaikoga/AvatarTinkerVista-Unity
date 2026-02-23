using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.Wear;
using Silksprite.AvatarTinkerVista.Utils;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivSimpleWear))]
    [CanEditMultipleObjects]
    class AtivSimpleWearEditor : AtivEditorBase
    {
        AtivSimpleWear[] _simpleWears;

        LocalizedProperty _moduleRootBones;
        LocalizedProperty _moduleIgnoreBones;
        LocalizedProperty _moduleLeafBones;
        LocalizedProperty _avatarRootBones;
        LocalizedProperty _avatarIgnoreBones;
        LocalizedProperty _avatarLeafBones;

        static bool _showModuleBoneTree;
        static bool _showAvatarBoneTree;
        static bool _showMapping;
        WearTreeNode[] _cachedModuleBoneTree;
        WearTreeNode[] _cachedAvatarBoneTree;
        List<(Transform moduleBone, Transform avatarBone)> _cachedMapping;

        void OnEnable()
        {
            _simpleWears = targets.Cast<AtivSimpleWear>().ToArray();
            _moduleRootBones = Lop(nameof(AtivSimpleWear.moduleRootBones), Loc("AtivSimpleWear::moduleRootBones"));
            _moduleRootBones.Property.isExpanded = true;
            _moduleIgnoreBones = Lop(nameof(AtivSimpleWear.moduleIgnoreBones), Loc("AtivSimpleWear::moduleIgnoreBones"));
            _moduleLeafBones = Lop(nameof(AtivSimpleWear.moduleLeafBones), Loc("AtivSimpleWear::moduleLeafBones"));
            _avatarRootBones = Lop(nameof(AtivSimpleWear.avatarRootBones), Loc("AtivSimpleWear::avatarRootBones"));
            _avatarRootBones.Property.isExpanded = true;
            _avatarIgnoreBones = Lop(nameof(AtivSimpleWear.avatarIgnoreBones), Loc("AtivSimpleWear::avatarIgnoreBones"));
            _avatarLeafBones = Lop(nameof(AtivSimpleWear.avatarLeafBones), Loc("AtivSimpleWear::avatarLeafBones"));
        }

        protected override void OnInnerInspectorGUI()
        {
            using var changed = new EditorGUI.ChangeCheckScope();
            LEditorGUILayout.HelpBox(Loc("AtivSimpleWear::BetaWarning."), MessageType.Info);
            Action<AtivSimpleWear> defer = null;
            LGUILayout.Heading(Loc("AtivSimpleWear::ModuleSettings"));
            LEditorGUILayout.Prop(_moduleRootBones);
            LEditorGUILayout.Prop(_moduleIgnoreBones);
            LEditorGUILayout.Prop(_moduleLeafBones);

            if (GUILayout.Button("Setup as Humanoid Module"))
            {
                defer = SimpleWearSetup.SetupHumanoidModule;
            }

            if (GUILayout.Button("Setup as Accessory Module"))
            {
                defer = SimpleWearSetup.SetupAccessoryModule;
            }

            LGUILayout.Heading(Loc("AtivSimpleWear::AvatarSettings"));
            LEditorGUILayout.Prop(_avatarRootBones);
            LEditorGUILayout.Prop(_avatarIgnoreBones);
            LEditorGUILayout.Prop(_avatarLeafBones);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Setup Avatar"))
            {
                defer = SimpleWearSetup.SetupAvatar;
            }

            if (defer is not null)
            {
                foreach (var simpleWear in _simpleWears)
                {
                    defer.Invoke(simpleWear);
                    EditorUtility.SetDirty(simpleWear);
                }
            }

            if (changed.changed)
            {
                _cachedModuleBoneTree = null;
                _cachedAvatarBoneTree = null;
                _cachedMapping = null;
            }

            if (!serializedObject.isEditingMultipleObjects)
            {
                var simpleWear = _simpleWears.First();
                LGUILayout.Heading(Loc("AtivSimpleWear::MergeDryRun"));
                _showModuleBoneTree = LEditorGUILayout.Foldout(_showModuleBoneTree, Loc("AtivSimpleWear::ShowModuleBoneTree"));
                if (_showModuleBoneTree)
                {
                    _cachedModuleBoneTree ??= simpleWear.ResolveModule();
                    using var _ = new EditorGUI.DisabledScope(true);
                    DrawWearTree(_cachedModuleBoneTree);
                }
                else
                {
                    _cachedModuleBoneTree = null;
                }
                
                _showAvatarBoneTree = LEditorGUILayout.Foldout(_showAvatarBoneTree, Loc("AtivSimpleWear::ShowAvatarBoneTree"));
                if (_showAvatarBoneTree)
                {
                    _cachedAvatarBoneTree ??= simpleWear.ResolveAvatar();
                    using var _ = new EditorGUI.DisabledScope(true);
                    DrawWearTree(_cachedAvatarBoneTree);
                }
                else
                {
                    _cachedAvatarBoneTree = null;
                }
                
                _showMapping = LEditorGUILayout.Foldout(_showMapping, Loc("AtivSimpleWear::ShowMapping"));
                if (_showMapping)
                {
                    _cachedMapping ??= WearProcessor.Map(simpleWear.ResolveModule(), simpleWear.ResolveAvatar());
                    using var _ = new EditorGUI.DisabledScope(true);
                    foreach (var m in _cachedMapping)
                    {
                        using (new GUILayout.HorizontalScope())
                        {
                            EditorGUILayout.ObjectField(m.moduleBone, typeof(Transform), true);
                            EditorGUILayout.ObjectField(m.avatarBone, typeof(Transform), true);
                        }
                    }
                }
                else
                {
                    _cachedMapping = null;
                }
            }
        }

        void DrawWearTree(WearTreeNode[] tree)
        {
            foreach (var node in tree)
            {
                EditorGUILayout.ObjectField(node.Bone, typeof(Transform), true);
                using var _ = new EditorGUI.IndentLevelScope();
                DrawWearTree(node.Children);
            }
        }

        [CustomPropertyDrawer(typeof(WearRootBoneEntry))]
        public class RootBoneEntryDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                var rootBone = property.FindPropertyRelative(nameof(WearRootBoneEntry.rootBone));
                var armatureMode = property.FindPropertyRelative(nameof(WearRootBoneEntry.armatureMode));
                var humanBone = property.FindPropertyRelative(nameof(WearRootBoneEntry.humanBone));

                var rootBoneRect = position;
                rootBoneRect.height = EditorGUIUtility.singleLineHeight;
                var armatureModeRect = rootBoneRect;
                armatureModeRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                var humanBoneRect = armatureModeRect;
                humanBoneRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(rootBoneRect, rootBone);
                EditorGUI.PropertyField(armatureModeRect, armatureMode);
                EditorGUI.PropertyField(humanBoneRect, humanBone);
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing * 2;
            }
        } 
    }
}
