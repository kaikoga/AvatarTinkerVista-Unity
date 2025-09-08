using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Utils
{
    public static class AtivRuntimeUtil
    {
        static Transform FindOrCreateChild(this Transform parent, string name)
        {
            var child = parent.Find(name);
            if (child)
            {
                return child;
            }
            var newChild = new GameObject(name).transform;
            newChild.SetParent(parent, false);
            return newChild;
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
