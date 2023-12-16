using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.ReplaceMaterialTexture
{
    public class ReplaceMaterialTextureWindow : EditorWindow
    {
        Vector2 _scrollPosition = new Vector2(0, 0);

        [SerializeField] ReplaceMaterialTexture core = new ReplaceMaterialTexture();

        void OnEnable()
        {
            titleContent = new GUIContent("Replace Material Texture");

            var serializedCore = new SerializedObject(this).FindProperty(nameof(core));
            serializedCore.FindPropertyRelative(nameof(ReplaceMaterialTexture.materials)).isExpanded = true;
            serializedCore.serializedObject.ApplyModifiedProperties();
        }

        void OnGUI()
        {
            var serializedCore = new SerializedObject(this).FindProperty(nameof(core));
            EditorGUILayout.PropertyField(serializedCore.FindPropertyRelative(nameof(ReplaceMaterialTexture.materials)));

            if (GUILayout.Button("Apply"))
            {
                core.Apply();
            }
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            foreach (var replacement in core.Replacements())
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField(replacement.oldTexture, typeof(Texture), false);
                EditorGUI.EndDisabledGroup();

                EditorGUI.BeginChangeCheck();
                var newTexture = EditorGUILayout.ObjectField(replacement.newTexture, typeof(Texture), false);
                if (EditorGUI.EndChangeCheck())
                {
                    core.ReplaceTexture(replacement.oldTexture, (Texture)newTexture);
                }

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            serializedCore.serializedObject.ApplyModifiedProperties();
        }

        [MenuItem("Window/Avatar Tinker/Replace Material Texture", false, 60000)]
        static void ShowWindow()
        {
            CreateInstance<ReplaceMaterialTextureWindow>().Show();
        }
    }
}