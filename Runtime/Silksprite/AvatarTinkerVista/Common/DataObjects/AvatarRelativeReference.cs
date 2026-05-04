using System;
using Silksprite.AvatarTinkerVista.Common.Utils;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.DataObjects
{
    [Serializable]
    public abstract class AvatarRelativeReference<T>
    where T : Component
    {
        [SerializeField] internal bool hasValue;
        [SerializeField] internal string relativePath = null!;

        public string? RelativePath
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

        public T? ResolveNow(Transform transform) => AvatarRelativeReference.ResolveNow<T>(transform, RelativePath);
        public T? ResolveFromAvatar(Transform avatarRoot) => AvatarRelativeReference.ResolveFromAvatar<T>(avatarRoot, RelativePath);
    }

    [Serializable]
    public class AvatarRelativeTransform : AvatarRelativeReference<Transform>
    {
        public static AvatarRelativeTransform OfAvatar(Transform? avatarRoot, Transform? transform) =>
            new AvatarRelativeTransform
            {
                RelativePath = AvatarRelativeReference.RelativePathFromAvatar(avatarRoot, transform)
            };
    }

    [Serializable]
    public class AvatarRelativeSkinnedMeshRenderer : AvatarRelativeReference<SkinnedMeshRenderer>
    {
        public static AvatarRelativeSkinnedMeshRenderer OfAvatar(Transform? avatarRoot, SkinnedMeshRenderer? skinnedMeshRenderer) =>
            new AvatarRelativeSkinnedMeshRenderer
            {
                RelativePath = AvatarRelativeReference.RelativePathFromAvatar(avatarRoot, skinnedMeshRenderer)
            };
    }

    public static class AvatarRelativeReference
    {
        public static T? ResolveNow<T>(Transform? transform, string? relativePath)
            where T : Component
        {
            if (relativePath == null) return null;
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(transform);
            return ResolveFromAvatar<T>(avatarRoot, relativePath);
        }

        public static string? RelativePath<T>(Transform? transform, T? obj)
            where T : Component
        {
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(transform);
            return RelativePathFromAvatar(avatarRoot, obj);
        }
        
        public static T? ResolveFromAvatar<T>(Transform? avatarRoot, string? relativePath)
            where T : Component
        {
            if (relativePath == null) return null;
            return avatarRoot != null ? AtivRuntimeUtil.FromRelativePath(avatarRoot.transform, relativePath)?.GetComponent<T>() : null;
        }

        public static string? RelativePathFromAvatar<T>(Transform? avatarRoot, T? obj)
            where T : Component
        {
            return avatarRoot != null ? AtivRuntimeUtil.RelativePath(avatarRoot.transform, obj?.transform) : null;
        }
    }
}