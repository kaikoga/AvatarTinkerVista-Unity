using System;
using Silksprite.Loch;
using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.AvatarTinkerVista.Common.Base
{
    public abstract class AtivEditorBase : Editor
    {
        bool _isVisualElement;
        public override VisualElement? CreateInspectorGUI()
        {
            var visualElement = base.CreateInspectorGUI();
            _isVisualElement = visualElement != null;
            return visualElement;
        }

        public sealed override void OnInspectorGUI()
        {
            if (_isVisualElement)
            {
                return;
            }
            var hierarchyMode = EditorGUIUtility.hierarchyMode;
            EditorGUIUtility.hierarchyMode = false; // false because we use Headers to group things
            try
            {
                LEditorGUILayout.LocaleSelector();
                OnInnerInspectorGUI();
            }
            finally
            {
                EditorGUIUtility.hierarchyMode = hierarchyMode;
            }
        }

        protected LocalizedProperty Lop(string propertyPath, LocalizedContent loc) => serializedObject.Lop(propertyPath, loc);

        protected virtual void OnInnerInspectorGUI()
        {
            if (!_isVisualElement)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
