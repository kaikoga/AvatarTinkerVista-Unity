using Silksprite.AvatarTinkerVista.VRM0.Converter;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista.VRM0
{
    public static class MenuItems
    {
        const string BakeDynamicsMenu = "GameObject/Avatar Tinker Vista/Bake ATiVGenerateSpringBones as VRM0 SpringBones"; 
        [MenuItem(BakeDynamicsMenu, true, 61000)]
        public static bool ValidateBakeVRM0SpringBones(MenuCommand menuCommand)
        {
            return Selection.activeGameObject;
        }

        [MenuItem(BakeDynamicsMenu, false, 61000)]
        public static void BakeVRM0SpringBones(MenuCommand menuCommand)
        {
            if (!Selection.activeGameObject) return;
            var context = Selection.activeGameObject.transform;
            if (!context) return;

            new InteractiveDynamicsConverterToVRM0SpringBone().InteractiveConvert(context.transform);
        }
    }
}
