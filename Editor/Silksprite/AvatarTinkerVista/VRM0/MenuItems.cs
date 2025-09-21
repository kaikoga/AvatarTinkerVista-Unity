using UnityEditor;

namespace Silksprite.AvatarTinkerVista.VRM0
{
    public static class MenuItems
    {
        const string BakeDynamicsMenu = "GameObject/Avatar Tinker Vista/Bake ATiVGenerateSpringBones as VRM0 SpringBones"; 
        [MenuItem(BakeDynamicsMenu, true, 61000)]
        public static bool ValidateExtractVrcComponents(MenuCommand menuCommand)
        {
            return Selection.activeGameObject;
        }

        [MenuItem(BakeDynamicsMenu, false, 61000)]
        public static void ExtractVrcPhysBones(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject;
            if (!context) return;

            new InteractiveDynamicsConverterToVRM0SpringBone().InteractiveConvert(context.transform);
        }
    }
}
