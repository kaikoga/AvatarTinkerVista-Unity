using System;
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
        static bool _showMapping;

        SerializedProperty _serializedModuleRootBones;
        SerializedProperty _serializedModuleIgnoreBones;
        SerializedProperty _serializedModuleLeafBones;
        SerializedProperty _serializedAvatarRootBones;
        SerializedProperty _serializedAvatarIgnoreBones;
        SerializedProperty _serializedAvatarLeafBones;

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
                }
            }
            

            if (!serializedObject.isEditingMultipleObjects)
            {
                AtivGUILayout.Header("Debug");
                _showMapping = EditorGUILayout.Foldout(_showMapping, "Show Mapping");
                if (_showMapping)
                {
                    var simpleWear = _simpleWears.First();
                    var map = WearProcessor.Map(simpleWear.ResolveModule(), simpleWear.ResolveAvatar());
                    using var _ = new EditorGUI.DisabledScope(true);
                    foreach (var m in map)
                    {
                        using (new GUILayout.HorizontalScope())
                        {
                            EditorGUILayout.ObjectField(new GUIContent(""), m.moduleBone, typeof(Transform), true);
                            EditorGUILayout.ObjectField(new GUIContent(""), m.avatarBone, typeof(Transform), true);
                        }
                    }
                }
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
