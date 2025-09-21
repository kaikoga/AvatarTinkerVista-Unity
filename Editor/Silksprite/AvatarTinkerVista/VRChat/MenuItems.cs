using UnityEditor;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public static class MenuItems
    {
        const string ExtractDynamicsMenu = "GameObject/Avatar Tinker Vista/Extract VRCPhysBones as ATiVGenerateVRMSpringBones";
        const string ExtractConstraintsMenu = "GameObject/Avatar Tinker Vista/Extract VRCConstraints as ATiVGenerateVRMConstraints";
        
        [MenuItem(ExtractDynamicsMenu, true, 60000)]
        [MenuItem(ExtractConstraintsMenu, true, 60001)]
        public static bool ValidateExtractVrcComponents(MenuCommand menuCommand)
        {
            return Selection.activeGameObject;
        }

        [MenuItem(ExtractDynamicsMenu, false, 60000)]
        public static void ExtractVrcPhysBones(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject;
            if (!context) return;

            new InteractiveDynamicsConverterFromVRCPhysBone().InteractiveConvert(context.transform);
        }

        [MenuItem(ExtractConstraintsMenu, false, 60001)]
        public static void ExtractVrcConstraints(MenuCommand menuCommand)
        {
            var context = Selection.activeGameObject;
            if (!context) return;

            new InteractiveConstraintsConverterFromVRCConstraints().InteractiveConvert(context.transform);
        }
    }
}
