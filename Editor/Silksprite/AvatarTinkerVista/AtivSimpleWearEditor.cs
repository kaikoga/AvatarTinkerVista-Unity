using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common;
using Silksprite.AvatarTinkerVista.Common.Wear;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivSimpleWear))]
    [CanEditMultipleObjects]
    class AtivSimpleWearEditor : Editor
    {
        AtivSimpleWear[] _simpleWears;

        SerializedProperty _serializedModuleRootBones;
        SerializedProperty _serializedModuleIgnoreBones;
        SerializedProperty _serializedModuleLeafBones;
        SerializedProperty _serializedAvatarRootBones;
        SerializedProperty _serializedAvatarIgnoreBones;
        SerializedProperty _serializedAvatarLeafBones;

        static bool _showModuleBoneTree;
        static bool _showAvatarBoneTree;
        static bool _showMapping;
        WearTreeNode[] _cachedModuleBoneTree;
        WearTreeNode[] _cachedAvatarBoneTree;
        List<(Transform moduleBone, Transform avatarBone)> _cachedMapping;

        void OnEnable()
        {
            _simpleWears = targets.Cast<AtivSimpleWear>().ToArray();
            _serializedModuleRootBones = serializedObject.FindProperty(nameof(AtivSimpleWear.moduleRootBones));
            _serializedModuleRootBones.isExpanded = true;
            _serializedModuleIgnoreBones = serializedObject.FindProperty(nameof(AtivSimpleWear.moduleIgnoreBones));
            _serializedModuleLeafBones = serializedObject.FindProperty(nameof(AtivSimpleWear.moduleLeafBones));
            _serializedAvatarRootBones = serializedObject.FindProperty(nameof(AtivSimpleWear.avatarRootBones));
            _serializedAvatarRootBones.isExpanded = true;
            _serializedAvatarIgnoreBones = serializedObject.FindProperty(nameof(AtivSimpleWear.avatarIgnoreBones));
            _serializedAvatarLeafBones = serializedObject.FindProperty(nameof(AtivSimpleWear.avatarLeafBones));
        }

        public override void OnInspectorGUI()
        {
            using var changed = new EditorGUI.ChangeCheckScope();
            EditorGUILayout.HelpBox("This component is in beta state.", MessageType.Info);
            Action<AtivSimpleWear> defer = null;
            AtivGUILayout.Header("Module Settings");
            EditorGUILayout.PropertyField(_serializedModuleRootBones);
            EditorGUILayout.PropertyField(_serializedModuleIgnoreBones);
            EditorGUILayout.PropertyField(_serializedModuleLeafBones);

            if (GUILayout.Button("Setup as Humanoid Module"))
            {
                defer = SimpleWearSetup.SetupHumanoidModule;
            }

            if (GUILayout.Button("Setup as Accessory Module"))
            {
                defer = SimpleWearSetup.SetupAccessoryModule;
            }

            AtivGUILayout.Header("Avatar Settings");
            EditorGUILayout.PropertyField(_serializedAvatarRootBones);
            EditorGUILayout.PropertyField(_serializedAvatarIgnoreBones);
            EditorGUILayout.PropertyField(_serializedAvatarLeafBones);
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
                AtivGUILayout.Header("Merge Dry Run");
                _showModuleBoneTree = EditorGUILayout.Foldout(_showModuleBoneTree, "Show Module Bone Tree");
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
                
                _showAvatarBoneTree = EditorGUILayout.Foldout(_showAvatarBoneTree, "Show Avatar Bone Tree");
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
                
                _showMapping = EditorGUILayout.Foldout(_showMapping, "Show Mapping");
                if (_showMapping)
                {
                    _cachedMapping ??= WearProcessor.Map(simpleWear.ResolveModule(), simpleWear.ResolveAvatar());
                    using var _ = new EditorGUI.DisabledScope(true);
                    foreach (var m in _cachedMapping)
                    {
                        using (new GUILayout.HorizontalScope())
                        {
                            EditorGUILayout.ObjectField(new GUIContent(""), m.moduleBone, typeof(Transform), true);
                            EditorGUILayout.ObjectField(new GUIContent(""), m.avatarBone, typeof(Transform), true);
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
                EditorGUILayout.ObjectField(new GUIContent(""), node.Bone, typeof(Transform), true);
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
