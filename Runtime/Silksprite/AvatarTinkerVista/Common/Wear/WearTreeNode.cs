using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Wear
{
    public class WearTreeNode
    {
        public readonly Transform Bone;
        public readonly string Name;
        public readonly HumanBodyBones? MaybeHumanBone;
        public readonly WearTreeNode[] Children;

        WearTreeNode(Transform bone, string name, HumanBodyBones? maybeHumanBone, WearTreeNode[] children)
        {
            Bone = bone;
            Name = name;
            MaybeHumanBone = maybeHumanBone;
            Children = children;
        }

        public static WearTreeNode Build(Transform rootBone, WearArmatureMode armatureMode, Transform[] ignore, Transform[] leaf)
        {
            var animator = rootBone.GetComponentInParent<Animator>();
            var dict = new Dictionary<HumanBodyBones, Transform>();
            if (animator && animator.avatar && animator.avatar is { isValid: true, isHuman: true })
            {
                var bonesAndLower = rootBone.GetComponentsInChildren<Transform>()
                    .Select(bone => (bone, lower: bone.gameObject.name.ToLowerInvariant()))
                    .ToArray();
                foreach (var bone in Enum.GetValues(typeof(HumanBodyBones)).Cast<HumanBodyBones>().Where(bone => bone != HumanBodyBones.LastBone))
                {
                    Transform DetectBoneTransform()
                    {
                        if (animator.GetBoneTransform(bone) is { } fastResult)
                        {
                            return fastResult;
                        }
                        const string missingBoneName = "/";
                        var boneNameLower = animator.avatar.humanDescription.human
                            .FirstOrDefault(hb => hb.humanName == HumanTrait.BoneName[(int)bone])
                            .boneName ?? missingBoneName
                            .ToLowerInvariant();
                        return bonesAndLower
                            .OrderBy(b => WearUtil.NameDistance(b.lower, boneNameLower))
                            .FirstOrDefault().bone;
                    }

                    dict.Add(bone, DetectBoneTransform());
                }
            }
            return BuildInternal(rootBone, armatureMode, ignore, leaf, dict);
        }

        static WearTreeNode BuildInternal(Transform rootBone, WearArmatureMode armatureMode, Transform[] ignore, Transform[] leaf, Dictionary<HumanBodyBones, Transform> humanBones)
        {
            var humanBone = humanBones.FirstOrDefault(kv => kv.Value == rootBone);
            
            return new WearTreeNode(
                rootBone,
                rootBone.gameObject.name.ToLowerInvariant(),
                humanBone.Value ? humanBone.Key : null,
                leaf.Contains(rootBone)
                    ? Array.Empty<WearTreeNode>()
                    : rootBone.OfType<Transform>()
                        .Where(child => armatureMode switch
                        {
                            WearArmatureMode.None => false,
                            WearArmatureMode.Humanoid => humanBones.FirstOrDefault(kv => kv.Value == child).Value && !ignore.Contains(child),
                            WearArmatureMode.All => !ignore.Contains(child),
                            _ => false,
                        })
                        .Select(child => BuildInternal(child, armatureMode, ignore, leaf, humanBones))
                        .ToArray()
            );
        }
    }
}
