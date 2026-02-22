using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista.Tools.ReplaceMaterialTexture
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

            LGUILayout.Heading(Loc("ReplaceMaterialTexture::ReplaceMaterialTexture"));
            GUILayout.Space(4f);
            LEditorGUILayout.HelpBox(Loc("ReplaceMaterialTexture::Help."), MessageType.Info);
            GUILayout.Space(4f);

            LGUILayout.Label(Loc("ReplaceMaterialTexture::1"));
            var serializedCore = new SerializedObject(this).FindProperty(nameof(core));
            EditorGUI.BeginChangeCheck();
            LEditorGUILayout.Prop(serializedCore.Lop(nameof(ReplaceMaterialTexture.materials), Loc("ReplaceMaterialTexture::materials")));
            if (EditorGUI.EndChangeCheck())
            {
                serializedCore.serializedObject.ApplyModifiedProperties();
            }

            LGUILayout.Label(Loc("ReplaceMaterialTexture::2"));
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

            LGUILayout.Label(Loc("ReplaceMaterialTexture::3"));
            if (LGUILayout.Button(Loc("ReplaceMaterialTexture::Apply")))
            {
                core.Apply();
            }

            EditorGUILayout.EndScrollView();

        }

        [MenuItem("Tools/Avatar Tinker Vista/Replace Material Texture", false, 60000)]
        static void ShowWindow()
        {
            CreateInstance<ReplaceMaterialTextureWindow>().Show();
        }
    }
}