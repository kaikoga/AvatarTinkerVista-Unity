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
                .Concat(moduleRootBones.Select(e => e.rootBone))
                .ToArray();
            return moduleRootBones
                .Select(entry =>
                {
                    var rootBone = entry.rootBone;
                    return rootBone ? WearTreeNode.Build(rootBone, entry.armatureMode, ignore, moduleLeafBones) : null;
                })
                .Where(tree => tree != null)
                .ToArray();
        }

        public WearTreeNode[] ResolveAvatar()
        {
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(transform);
            var ignore = avatarIgnoreBones
                .Concat(avatarRootBones.Select(e => e.rootBone.ResolveFromAvatar(avatarRoot)))
                .ToArray();
            return avatarRootBones
                .Select(entry =>
                {
                    var rootBone = entry.rootBone.ResolveFromAvatar(avatarRoot);
                    return rootBone ? WearTreeNode.Build(rootBone, entry.armatureMode, ignore, moduleLeafBones) : null;
                })
                .Where(tree => tree != null)
                .ToArray();
        }
    }
}
