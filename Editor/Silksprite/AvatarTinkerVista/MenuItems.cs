using Silksprite.AvatarTinkerVista.Utils;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    public static class MenuItems
    {
        const string SimpleWearSetupAsHumanoidModuleMenu = "GameObject/Avatar Tinker Vista/Simple Wear Setup as Humanoid Module";
        const string SimpleWearSetupAsAccessoryModuleMenu = "GameObject/Avatar Tinker Vista/Simple Wear Setup as Accessory Module";
        
        [MenuItem(SimpleWearSetupAsHumanoidModuleMenu, true, 62000)]
        [MenuItem(SimpleWearSetupAsAccessoryModuleMenu, true, 62001)]
        public static bool ValidateSimpleWearSetup(MenuCommand menuCommand)
        {
            if (!Selection.activeGameObject) return false;
            return Selection.activeGameObject.TryGetComponent(out Animator _);
        }

        [MenuItem(SimpleWearSetupAsHumanoidModuleMenu, false, 62000)]
        public static void SimpleWearSetupAsHumanoidModule(MenuCommand menuCommand)
        {
            if (!Selection.activeGameObject) return;
            if (Selection.activeGameObject.TryGetComponent(out Animator context))
            {
                SimpleWearSetup.PopulateHumanoidModules(context);
            }
        }

        [MenuItem(SimpleWearSetupAsAccessoryModuleMenu, false, 62001)]
        public static void SimpleWearSetupAsAccessoryModule(MenuCommand menuCommand)
        {
            if (!Selection.activeGameObject) return;
            if (Selection.activeGameObject.TryGetComponent(out Animator context))
            {
                SimpleWearSetup.PopulateAccessoryModule(context);
            }
        }
    }
}
