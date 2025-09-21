using Silksprite.AvatarTinkerVista.VRM0.Converter;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public static class MenuItems
    {
        [MenuItem("GameObject/Avatar Tinker Vista/Bake ATiVSpringBones as VRM0", true)]
        public static bool ValidateExtractVrcComponents(MenuCommand menuCommand)
        {
            return Selection.activeGameObject;
        }

        [MenuItem("GameObject/Avatar Tinker Vista/Bake ATiVSpringBones as VRM0", false)]
        public static void ExtractVrcPhysBones(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject;
            if (!context) return;

            Undo.RegisterFullObjectHierarchyUndo(context, "ATiV: Bake ATiVSpringBones as VRM0");
            var result = EditorUtility.DisplayDialogComplex(
                "Bake ATiVSpringBones as VRM0 SpringBones",
                "Do you want to also destroy existing ATiV SpringBones?",
                "Just generate",
                "Cancel",
                "Destroy ATiV PhysBones");
            if (result != 1)
            {
                new DynamicsConverterToVRM0SpringBone().Convert(context.transform, result == 2);
            }
        }
    }
}
