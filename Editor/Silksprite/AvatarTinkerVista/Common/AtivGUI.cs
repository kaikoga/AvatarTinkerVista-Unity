using System;
using System.Linq;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common
{
    public static class AtivGUI
    {
        public static void PropAsBlendShapeName(Rect position, LocalizedProperty lop, SkinnedMeshRenderer skinnedMeshRenderer)
        {
            if (skinnedMeshRenderer && skinnedMeshRenderer.sharedMesh is { } sharedMesh)
            {
                var left = position;
                left.width -= 72f;
                var right = position;
                right.xMin = right.xMax - 70f;

                LEditorGUI.Prop(left, lop);

                EditorGUI.BeginChangeCheck();
                var blendShapeName = lop.Property.stringValue;
                var options = Enumerable.Range(0, sharedMesh.blendShapeCount)
                    .Select(i => sharedMesh.GetBlendShapeName(i))
                    .Where(name => name.StartsWith(blendShapeName))
                    .ToArray();
                var newBlendShapeNameValue = EditorGUI.Popup(
                    right,
                    "",
                    Array.IndexOf(options, blendShapeName),
                    options
                );
                if (EditorGUI.EndChangeCheck())
                {
                    lop.Property.stringValue = options[newBlendShapeNameValue];
                }
            }
            else
            {
                LEditorGUI.PropAsLabel(position, lop);
            }
        }
    }
}
