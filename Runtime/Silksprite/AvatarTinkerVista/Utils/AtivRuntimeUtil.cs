using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Utils
{
    public static class AtivRuntimeUtil
    {
        public static string GuessOriginalAvatarName(string avatarName)
        {
            if (avatarName.EndsWith("(Clone)"))
            {
                avatarName = avatarName.Substring(0, avatarName.Length - "(Clone)".Length);
            }
            return avatarName;
        }
        
        public static string VrmAuthor => "no name";
        public static string VrmVersion => "0.1.0";

        public static Transform CreateChild(this Transform parent, string name)
        {
            var newChild = new GameObject(name).transform;
            newChild.SetParent(parent, false);
            return newChild;
        }

        static Transform FindOrCreateChild(this Transform parent, string name)
        {
            var child = parent.Find(name);
            if (child)
            {
                return child;
            }
            return parent.CreateChild(name);
        }

        public static Transform FindOrCreateSecondary(this Transform avatarRootTransform)
        {
            return avatarRootTransform.FindOrCreateChild("Secondary");
        }

        public static Transform FindOrCreateSecondary(this Transform avatarRootTransform, string name)
        {
            return avatarRootTransform.FindOrCreateSecondary().FindOrCreateChild(name);
        }
    }
}
