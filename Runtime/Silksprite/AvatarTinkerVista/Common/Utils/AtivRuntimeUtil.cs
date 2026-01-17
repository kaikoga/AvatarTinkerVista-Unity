using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

#if ATIV_NDMF
using NdmfRuntimeUtil = nadena.dev.ndmf.runtime.RuntimeUtil;
#endif

#if ATIV_ABLET
using Ablet;
#endif

#if ATIV_VRCSDK3_AVATARS
using VRC.SDK3.Avatars.Components;
#endif

namespace Silksprite.AvatarTinkerVista.Common.Utils
{
    public static class AtivRuntimeUtil
    {
        public static Transform FindAvatarInParents(Transform transform)
        {
            if (!transform) return null;

#if ATIV_ABLET
            return AbletFacade.GetEntrypointFor(transform.gameObject).gameObject?.transform;
#elif ATIV_NDMF
            return NdmfRuntimeUtil.FindAvatarInParents(transform);
#elif ATIV_VRCSDK3_AVATARS
            return transform.GetComponentInParent<VRCAvatarDescriptor>()?.transform;
#else
            return null;
#endif
        }

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

        public static IEnumerable<T> GetEligibleComponentsInChildren<T>(this Component parent)
        {
            return parent.GetComponentsInChildren<T>()
                .Where(t => t is Component c && c && !c.TryGetComponent<IAtivDisableAtivComponents>(out _));
        }

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

        const string AvatarRootPath = "";
        const string ModularAvatarAvatarRootMagic = "$$$AVATAR_ROOT$$$";

        public static string RelativePath(Transform root, Transform child, bool isModularAvatarMagic = false)
        {
            return RelativePath(root, child, isModularAvatarMagic ? ModularAvatarAvatarRootMagic : AvatarRootPath);
        }

        static string RelativePath(Transform root, Transform child, string rootName)
        {
            if (!root) return null;
            if (!child) return null;
            if (root == child) return rootName;

            var cursor = child;
            var path = child.gameObject.name;
            while (true)
            {
                cursor = cursor.parent;
                if (cursor == root) break;
                if (!cursor) break;
                path = Path.Combine(cursor.gameObject.name, path);
            }
            return path;
        }

        public static Transform FromRelativePath(Transform root, string relativePath, bool isModularAvatarMagic = false)
        {
            return !root ? null :
                relativePath == null ? null :
                relativePath == (isModularAvatarMagic ? ModularAvatarAvatarRootMagic : AvatarRootPath) ? root :
                root.Find(relativePath);
        }
    }
}
