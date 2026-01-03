using System;
using Silksprite.AvatarTinkerVista.Common.Utils;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.DataObjects
{
    [Serializable]
    public abstract class AvatarRelativeReference<T>
    where T : Component
    {
        public string relativePath;

        public T ResolveNow(Transform transform) => ResolveNow(transform, relativePath);

        public static T ResolveNow(Transform transform, string relativePath)
        {
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(transform);
            return AtivRuntimeUtil.FromRelativePath(avatarRoot.transform, relativePath)?.GetComponent<T>();
        }

        public static string RelativePath(Transform transform, T obj)
        {
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(transform);
            return AtivRuntimeUtil.RelativePath(avatarRoot.transform, obj?.transform);
        }
    }

    [Serializable]
    public class AvatarRelativeTransform : AvatarRelativeReference<Transform> { }
}