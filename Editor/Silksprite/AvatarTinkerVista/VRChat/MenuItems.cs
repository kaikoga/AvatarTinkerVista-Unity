using Silksprite.AvatarTinkerVista.VRChat.Converter;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public static class MenuItems
    {
        [MenuItem("GameObject/Avatar Tinker Vista/Extract VRCPhysBones as ATiVGenerateVRMSpringBones", true)]
        [MenuItem("GameObject/Avatar Tinker Vista/Extract VRCConstraints as ATiVGenerateVRMConstraints", true)]
        public static bool ValidateExtractVrcComponents(MenuCommand menuCommand)
        {
            return Selection.activeGameObject;
        }

        [MenuItem("GameObject/Avatar Tinker Vista/Extract VRCPhysBones as ATiVGenerateVRMSpringBones", false)]
        public static void ExtractVrcPhysBones(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject;
            if (!context) return;

            Undo.RegisterFullObjectHierarchyUndo(context, "ATiV: Extract VRCPhysBones");
            var result = EditorUtility.DisplayDialogComplex(
                "Extract VRCPhysBones as GenerateVRMSpringBones",
                "Do you want to also destroy existing VRC PhysBones?",
                "Just generate",
                "Cancel",
                "Destroy VRC PhysBones");
            if (result != 1)
            {
                new DynamicsConverterFromVRCPhysBone().Convert(context.transform, result == 2);
            }
        }

        [MenuItem("GameObject/Avatar Tinker Vista/Extract VRCConstraints as ATiVGenerateVRMConstraints", false)]
        public static void ExtractVrcConstraints(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject;
            if (!context) return;

            Undo.RegisterFullObjectHierarchyUndo(context, "ATiV: Extract VRCConstraints");
            var result = EditorUtility.DisplayDialogComplex(
                "Extract VRCConstraints as ATiVGGenerateVRMConstraints",
                "Do you want to also destroy existing VRC Constraints?",
                "Just generate",
                "Cancel",
                "Destroy VRC Constraints");
            if (result != 1)
            {
                new ConstraintsConverterFromVRCConstraints().Convert(context.transform, result == 2);
            }
        }
    }
}
