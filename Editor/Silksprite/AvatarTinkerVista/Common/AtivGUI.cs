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
                EditorGUI.BeginChangeCheck();
                var options = Enumerable.Range(0, sharedMesh.blendShapeCount)
                    .Select(i => sharedMesh.GetBlendShapeName(i))
                    .ToArray();
                var newBlendShapeNameValue = EditorGUI.Popup(
                    position,
                    lop.Loc.Tr,
                    Array.IndexOf(options, lop.Property.stringValue),
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
