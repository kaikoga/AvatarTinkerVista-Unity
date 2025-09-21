using UnityEditor;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1
{
    public static class MenuItems
    {
        const string BakeDynamicsMenu = "GameObject/Avatar Tinker Vista/Bake ATiVGenerateSpringBones as Vrm10SpringBones";
        const string BakeConstraintsMenu = "GameObject/Avatar Tinker Vista/Bake ATiVGenerateConstraints as Vrm10Constraints";

        [MenuItem(BakeDynamicsMenu, true, 61100)]
        [MenuItem(BakeConstraintsMenu, true, 61101)]
        public static bool ValidateExtractVrcComponents(MenuCommand menuCommand)
        {
            return Selection.activeGameObject;
        }

        [MenuItem(BakeDynamicsMenu, false, 61100)]
        public static void ExtractVrcPhysBones(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject.GetComponent<Vrm10Instance>();
            if (!context) return;

            new InteractiveDynamicsConverterToVRM1SpringBone().InteractiveConvert(context);
        }

        [MenuItem(BakeConstraintsMenu, false, 61101)]
        public static void ExtractVrcConstraints(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject;
            if (!context) return;

            new InteractiveConstraintsConverterToVRM1Constraint().InteractiveConvert(context.transform);
        }
    }
}
