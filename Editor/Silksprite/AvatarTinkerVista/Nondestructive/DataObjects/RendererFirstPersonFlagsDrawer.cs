using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Nondestructive.DataObjects
{
    [CustomPropertyDrawer(typeof(AtivMergeVRMFirstPerson.RendererFirstPersonFlags))]
    class RendererFirstPersonFlagsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rendererProp = property.FindPropertyRelative(nameof(AtivMergeVRMFirstPerson.RendererFirstPersonFlags.renderer));
            var flagProp = property.FindPropertyRelative(nameof(AtivMergeVRMFirstPerson.RendererFirstPersonFlags.firstPersonFlag));

            const float rightSideWidth = 140.0f;

            var leftSide = position;
            leftSide.xMax -= rightSideWidth;
            EditorGUI.PropertyField(leftSide, rendererProp, GUIContent.none);

            var rightSide = position;
            rightSide.xMin = rightSide.xMax - rightSideWidth;
            EditorGUI.PropertyField(rightSide, flagProp, GUIContent.none);
        }
    }
}
