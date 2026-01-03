using Silksprite.AvatarTinkerVista.Tools.ReplaceMaterialTexture;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Tools.ListComponents
{
    public class ListComponentsWindow : EditorWindow
    {
        Vector2 _scrollPosition = new Vector2(0, 0);

        [SerializeField] ListComponents core = new ListComponents();

        void OnEnable()
        {
            titleContent = new GUIContent("List Components");

            var serializedCore = new SerializedObject(this).FindProperty(nameof(core));
            serializedCore.FindPropertyRelative(nameof(ListComponents.componentNames)).isExpanded = true;
            serializedCore.serializedObject.ApplyModifiedProperties();
        }

        void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            void HelpLabel(string message)
            {
                GUILayout.Label(message.Replace(" ", " "), new GUIStyle{wordWrap = true});
            }

            GUILayout.Label("List Components", new GUIStyle { fontStyle = FontStyle.Bold });
            GUILayout.Space(4f);
            EditorGUILayout.HelpBox("選択されたコンポーネント以下で使用されているコンポーネントを表示します。".Replace(" ", " "), MessageType.Info);
            GUILayout.Space(4f);

            HelpLabel("1. アバター（など）を選択する");
            var serializedCore = new SerializedObject(this).FindProperty(nameof(core));
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedCore.FindPropertyRelative(nameof(ListComponents.avatarRoot)));
            if (EditorGUI.EndChangeCheck())
            {
                serializedCore.serializedObject.ApplyModifiedProperties();
                core.Refresh();
            }

            HelpLabel("2. コンポーネント名が表示される");
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(serializedCore.FindPropertyRelative(nameof(ListComponents.componentNames)));
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndScrollView();

        }

        [MenuItem("Tools/Avatar Tinker Vista/List Components", false, 60000)]
        static void ShowWindow()
        {
            CreateInstance<ListComponentsWindow>().Show();
        }
    }
}