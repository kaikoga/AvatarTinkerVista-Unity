using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.Utils;
using Silksprite.AvatarTinkerVista.Common.Wear;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Simple Wear")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_simple_wear")]
    public class AtivSimpleWear : AtivTransformingComponent
    {
        public WearRootBoneEntry[] moduleRootBones = { new WearRootBoneEntry() };
        public Transform[] moduleIgnoreBones = { };
        public Transform[] moduleLeafBones = { };
        public WearRelativeRootBoneEntry[] avatarRootBones = { new WearRelativeRootBoneEntry() };
        public Transform[] avatarIgnoreBones = { };
        public Transform[] avatarLeafBones = { };

        public WearTreeNode[] ResolveModule()
        {
            var ignore = moduleIgnoreBones
                .Concat(moduleRootBones.Select(e => e.rootBone).OfType<Transform>())
                .ToArray();
            return moduleRootBones
                .Select(entry =>
                {
                    var rootBone = entry.rootBone;
                    return rootBone != null ? WearTreeNode.Build(rootBone, entry.armatureMode, ignore, moduleLeafBones) : null;
                })
                .OfType<WearTreeNode>()
                .ToArray();
        }

        public WearTreeNode[] ResolveAvatar()
        {
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(transform);
            if (avatarRoot == null)
            {
                return Array.Empty<WearTreeNode>();
            }
            var ignore = avatarIgnoreBones
                .Concat(avatarRootBones.Select(e => e.rootBone?.ResolveFromAvatar(avatarRoot)).OfType<Transform>())
                .ToArray();
            return avatarRootBones
                .Select(entry =>
                {
                    var rootBone = entry.rootBone?.ResolveFromAvatar(avatarRoot);
                    return rootBone != null ? WearTreeNode.Build(rootBone, entry.armatureMode, ignore, moduleLeafBones) : null;
                })
                .OfType<WearTreeNode>()
                .ToArray();
        }
    }
}
