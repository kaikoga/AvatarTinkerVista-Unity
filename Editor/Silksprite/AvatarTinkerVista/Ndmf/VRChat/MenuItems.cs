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

            new DynamicsConverterFromVRCPhysBone().Convert(context);
        }
    }
}
