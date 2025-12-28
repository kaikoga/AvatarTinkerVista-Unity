using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
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
        public WearRootBoneEntry[] avatarRootBones = { new WearRootBoneEntry() };
        public Transform[] avatarIgnoreBones = { };
        public Transform[] avatarLeafBones = { };

        public WearTreeNode[] ResolveModule()
        {
            var ignore = moduleIgnoreBones
                .Concat(moduleRootBones.Select(e => e.rootBone))
                .ToArray();
            return moduleRootBones
                .Where(entry => entry.rootBone)
                .Select(entry => WearTreeNode.Build(entry.rootBone, entry.armatureMode, ignore, moduleLeafBones))
                .ToArray();
        }

        public WearTreeNode[] ResolveAvatar()
        {
            var ignore = avatarIgnoreBones
                .Concat(avatarRootBones.Select(e => e.rootBone))
                .ToArray();
            return avatarRootBones
                .Where(entry => entry.rootBone)
                .Select(entry => WearTreeNode.Build(entry.rootBone, entry.armatureMode, ignore, avatarLeafBones))
                .ToArray();
        }
    }
}
