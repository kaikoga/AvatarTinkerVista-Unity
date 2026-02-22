using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.Loch.Tools.LochTool;

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

            LEditorGUILayout.LocaleSelector();
            LGUILayout.Heading(Loc("ListComponents::ListComponents"));
            GUILayout.Space(4f);
            LEditorGUILayout.HelpBox(Loc("ListComponents::Help."), MessageType.Info);
            GUILayout.Space(4f);

            LGUILayout.Label(Loc("ListComponents::1"));
            var serializedCore = new SerializedObject(this).FindProperty(nameof(core));
            EditorGUI.BeginChangeCheck();
            LEditorGUILayout.Prop(serializedCore.Lop(nameof(ListComponents.avatarRoot), Loc("ListComponents::avatarRoot")));
            if (EditorGUI.EndChangeCheck())
            {
                serializedCore.serializedObject.ApplyModifiedProperties();
                core.Refresh();
            }

            LGUILayout.Label(Loc("ListComponents::2"));
            EditorGUI.BeginDisabledGroup(true);
            LEditorGUILayout.Prop(serializedCore.Lop(nameof(ListComponents.componentNames), Loc("ListComponents::componentNames")));
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