using Silksprite.AvatarTinkerVista.VRM1.Converter;
using UnityEditor;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1
{
    public static class MenuItems
    {
        const string BakeMergeDynamicsMenu = "GameObject/Avatar Tinker Vista/Bake ATiVGenerateSpringBones as ATiV Merge VRM1 SpringBones";

        const string BakeDynamicsMenu = "GameObject/Avatar Tinker Vista/Bake ATiVGenerateSpringBones into Vrm10Instance SpringBones";
        const string BakeConstraintsMenu = "GameObject/Avatar Tinker Vista/Bake ATiVGenerateConstraints as Vrm10Constraints";

        [MenuItem(BakeDynamicsMenu, true, 61110)]
        public static bool ValidateBakeVRM1SpringBones(MenuCommand menuCommand)
        {
            return Selection.activeGameObject && Selection.activeGameObject.TryGetComponent<Vrm10Instance>(out _);
        }

        [MenuItem(BakeMergeDynamicsMenu, true, 61100)]
        [MenuItem(BakeConstraintsMenu, true, 61111)]
        public static bool ValidateBakeVRM1Constraints(MenuCommand menuCommand)
        {
            return Selection.activeGameObject;
        }

        [MenuItem(BakeMergeDynamicsMenu, false, 61110)]
        public static void BakeMergeVRM1SpringBones(MenuCommand menuCommand)
        {
            if (!Selection.activeGameObject) return;
            var context = Selection.activeGameObject.transform;
            if (!context) return;

            new InteractiveDynamicsConverterToMergeVRM1SpringBone().InteractiveConvert(context);
        }

        [MenuItem(BakeDynamicsMenu, false, 61110)]
        public static void BakeVRM1SpringBones(MenuCommand menuCommand)
        {
            if (!Selection.activeGameObject) return;
            var context = Selection.activeGameObject.GetComponent<Vrm10Instance>();
            if (!context) return;

            new InteractiveDynamicsConverterToVRM1SpringBone().InteractiveConvert(context);
        }

        [MenuItem(BakeConstraintsMenu, false, 61111)]
        public static void BakeVRM1Constraints(MenuCommand menuCommand)
        {
            if (!Selection.activeGameObject) return;
            var context = Selection.activeGameObject.transform;
            if (!context) return;

            new InteractiveConstraintsConverterToVRM1Constraint().InteractiveConvert(context);
        }
    }
}
