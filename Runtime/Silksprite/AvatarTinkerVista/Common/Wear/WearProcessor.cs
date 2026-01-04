using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Wear
{
    public static class WearProcessor
    {
        public static List<(Transform moduleBone, Transform avatarBone)> Map(WearTreeNode[] moduleTrees, WearTreeNode[] avatarTrees)
        {
            var result = new List<(Transform, Transform)>();
            foreach (var pair in moduleTrees.Zip(avatarTrees, (moduleTree, avatarTree) => (moduleTree, avatarTree)))
            {
                MapInternal(pair.moduleTree, pair.avatarTree, result);
            }
            return result;
        }

        static void MapInternal(WearTreeNode moduleTree, WearTreeNode avatarTree, List<(Transform moduleBone, Transform avatarBone)> result)
        {
            if (moduleTree.Bone && avatarTree.Bone)
            {
                result.Add((moduleTree.Bone, avatarTree.Bone));
            }
            var moduleChildren = moduleTree.Children.ToHashSet();
            var avatarChildren = avatarTree.Children.ToHashSet();
            var mappingScores = moduleChildren.SelectMany(
                moduleChild => avatarChildren.Select(avatarChild =>
                {
                    int score;
                    if (moduleChild.MaybeHumanBone is { } humanBone
                        && avatarChild.MaybeHumanBone == humanBone)
                    {
                        score = 10000;
                    }
                    else
                    {
                        score = -WearUtil.NameDistance(moduleChild.Name, avatarChild.Name);
                    }
                    return (moduleChild, avatarChild, score);
                })).OrderByDescending(r => r.score);
            foreach (var r in mappingScores)
            {
                if (moduleChildren.Count == 0)
                {
                    break;
                }
                if (avatarChildren.Count == 0)
                {
                    break;
                }
                if (moduleChildren.Contains(r.moduleChild) && avatarChildren.Contains(r.avatarChild))
                {
                    MapInternal(r.moduleChild, r.avatarChild, result);
                    moduleChildren.Remove(r.moduleChild);
                    avatarChildren.Remove(r.avatarChild);
                }
            }
        }

        public static void Wear(WearTreeNode[] moduleTrees, WearTreeNode[] avatarTrees)
        {
            var map = Map(moduleTrees, avatarTrees);
            foreach (var m in map)
            {
                m.moduleBone.SetParent(m.avatarBone);
            }
        }
    }
}
