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
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            void HelpLabel(string message)
            {
                GUILayout.Label(message.Replace(" ", " "), new GUIStyle{wordWrap = true});
            }

            GUILayout.Label("Replace Material Texture", new GUIStyle { fontStyle = FontStyle.Bold });
            GUILayout.Space(4f);
            EditorGUILayout.HelpBox("マテリアルからテクスチャへの参照を一括で置き換えます。".Replace(" ", " "), MessageType.Info);
            GUILayout.Space(4f);

            HelpLabel("1. マテリアルを選択する");
            var serializedCore = new SerializedObject(this).FindProperty(nameof(core));
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedCore.FindPropertyRelative(nameof(ReplaceMaterialTexture.materials)));
            if (EditorGUI.EndChangeCheck())
            {
                serializedCore.serializedObject.ApplyModifiedProperties();
            }

            HelpLabel("2. 置き換えるテクスチャを設定する\n置き換え元→置き換え先");
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

            HelpLabel("3. 「Apply」を押す");
            if (GUILayout.Button("Apply"))
            {
                core.Apply();
            }

            EditorGUILayout.EndScrollView();

        }

        [MenuItem("Window/Avatar Tinker/Replace Material Texture", false, 60000)]
        static void ShowWindow()
        {
            CreateInstance<ReplaceMaterialTextureWindow>().Show();
        }
    }
}