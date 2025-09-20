using Silksprite.AvatarTinkerVista.VRChat.Converter;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRChat
{
    public static class MenuItems
    {
        [MenuItem("GameObject/Avatar Tinker Vista/Extract VRCPhysBones as GenerateVrmSpringBones", true)]
        public static bool ValidateExtractVrcPhysBones(MenuCommand menuCommand)
        {
            return Selection.activeGameObject;
        }

        [MenuItem("GameObject/Avatar Tinker Vista/Extract VRCPhysBones as GenerateVrmSpringBones", false)]
        public static void ExtractVrcPhysBones(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject;
            if (!context) return;

            Undo.RegisterFullObjectHierarchyUndo(context, "ATiV: Extract VRCPhysBones");
            var result = EditorUtility.DisplayDialogComplex(
                "Extract VRCPhysBones as GenerateVrmSpringBones",
                "Do you want to also destroy existing VRC PhysBones?",
                "Just generate",
                "Cancel",
                "Destroy VRC PhysBones");
            if (result != 1)
            {
                new DynamicsConverterFromVRCPhysBone().Convert(context.transform, result == 2);
            }
        }
    }
}
