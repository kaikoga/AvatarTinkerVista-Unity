using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.DataObjects
{
    public abstract class AvatarRelativeReferenceDrawer<T> : PropertyDrawer
    where T : Component
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, serializedProperty))
            {
                var transform = (serializedProperty.serializedObject.targetObject as Component)?.transform;
                var serializedHasValue = serializedProperty.FindPropertyRelative("hasValue");
                var serializedRelativePath = serializedProperty.FindPropertyRelative("relativePath");
                var relativePathValue = serializedHasValue.boolValue ? serializedRelativePath.stringValue : null;
                var obj = AvatarRelativeReference.ResolveNow<T>(transform, relativePathValue);
                var change = new EditorGUI.ChangeCheckScope();
                obj = EditorGUI.ObjectField(position, label, obj, typeof(T), true) as T;
                if (change.changed)
                {
                    var newRelativePathValue = AvatarRelativeReference.RelativePath(transform, obj);
                    serializedRelativePath.stringValue = newRelativePathValue ?? "";
                    serializedHasValue.boolValue = newRelativePathValue != null;
                }
            }
        }
    }
    
    [CustomPropertyDrawer(typeof(AvatarRelativeTransform))]
    public class AvatarRelativeTransformDrawer : AvatarRelativeReferenceDrawer<Transform> { }
}

