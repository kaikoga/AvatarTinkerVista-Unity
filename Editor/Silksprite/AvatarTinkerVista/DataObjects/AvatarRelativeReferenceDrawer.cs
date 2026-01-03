using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.DataObjects
{
    public abstract class AvatarRelativeReferenceDrawer<T> : PropertyDrawer
    where T : Component
    {
        public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, serializedProperty))
            {
                var transform = (serializedProperty.serializedObject.targetObject as Component)?.transform;
                var serializedRelativePath = serializedProperty.FindPropertyRelative("relativePath");
                var obj = AvatarRelativeReference<T>.ResolveNow(transform, serializedRelativePath.stringValue);
                var change = new EditorGUI.ChangeCheckScope();
                obj = EditorGUI.ObjectField(position, label, obj, typeof(T), true) as T;
                if (change.changed)
                {
                    serializedRelativePath.stringValue = AvatarRelativeReference<T>.RelativePath(transform, obj);
                }
            }
        }
    }
    
    [CustomPropertyDrawer(typeof(AvatarRelativeTransform))]
    public class AvatarRelativeTransformDrawer : AvatarRelativeReferenceDrawer<Transform> { }
}

