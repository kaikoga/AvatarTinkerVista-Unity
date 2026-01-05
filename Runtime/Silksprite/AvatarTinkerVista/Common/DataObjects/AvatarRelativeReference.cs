using System;
using Silksprite.AvatarTinkerVista.Common.Utils;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.DataObjects
{
    [Serializable]
    public abstract class AvatarRelativeReference<T>
    where T : Component
    {
        [SerializeField] bool hasValue;
        [SerializeField] string relativePath;

        public string RelativePath
        {
            get => hasValue ? relativePath : null;
            set {
                if (value is { } path)
                {
                    hasValue = true;
                    relativePath = path;
                }
                else
                {
                    hasValue = false;
                    relativePath = "";
                }
            } 
        }

        public T ResolveNow(Transform transform) => AvatarRelativeReference.ResolveNow<T>(transform, relativePath);
    }

    [Serializable]
    public class AvatarRelativeTransform : AvatarRelativeReference<Transform> { }

    public static class AvatarRelativeReference
    {
        public static T ResolveNow<T>(Transform transform, string relativePath)
            where T : Component
        {
            if (string.IsNullOrEmpty(relativePath)) return null;
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(transform);
            return avatarRoot? AtivRuntimeUtil.FromRelativePath(avatarRoot.transform, relativePath)?.GetComponent<T>() : null;
        }

        public static string RelativePath<T>(Transform transform, T obj)
            where T : Component
        {
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(transform);
            return avatarRoot? AtivRuntimeUtil.RelativePath(avatarRoot.transform, obj?.transform) : null;
        }
    }

}