using Silksprite.AvatarTinkerVista.VRM1.Converter;
using UnityEditor;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public static class MenuItems
    {
        [MenuItem("GameObject/Avatar Tinker Vista/Bake ATiVSpringBones as VRM1", true)]
        [MenuItem("GameObject/Avatar Tinker Vista/Bake ATiVConstraints as VRM1", true)]
        public static bool ValidateExtractVrcComponents(MenuCommand menuCommand)
        {
            return Selection.activeGameObject;
        }

        [MenuItem("GameObject/Avatar Tinker Vista/Bake ATiVSpringBones as VRM1", false)]
        public static void ExtractVrcPhysBones(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject.GetComponent<Vrm10Instance>();
            if (!context) return;

            Undo.RegisterFullObjectHierarchyUndo(context, "ATiV: Bake ATiVSpringBones as VRM1");
            var result = EditorUtility.DisplayDialogComplex(
                "Bake ATiVSpringBones as Vrm10SpringBones",
                "Do you want to also destroy existing ATiV SpringBones?",
                "Just generate",
                "Cancel",
                "Destroy ATiV PhysBones");
            if (result != 1)
            {
                new DynamicsConverterToVRM1SpringBone().Convert(context, result == 2);
            }
        }

        [MenuItem("GameObject/Avatar Tinker Vista/Bake ATiVConstraints as VRM1", false)]
        public static void ExtractVrcConstraints(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject;
            if (!context) return;

            Undo.RegisterFullObjectHierarchyUndo(context, "ATiV: Bake ATiVConstraints as VRM1");
            var result = EditorUtility.DisplayDialogComplex(
                "Bake ATiVConstraints as Vrm10Constraints",
                "Do you want to also destroy existing ATiV Constraints?",
                "Just generate",
                "Cancel",
                "Destroy ATiV Constraints");
            if (result != 1)
            {
                new ConstraintsConverterToVRM1Constraint().Convert(context.transform, result == 2);
            }
        }
    }
}
