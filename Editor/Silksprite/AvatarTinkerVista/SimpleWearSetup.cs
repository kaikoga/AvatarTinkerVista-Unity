using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Utils;
using Silksprite.AvatarTinkerVista.Common.Wear;
using UnityEditor;
using UnityEngine;

#if ATIV_VRCSDK3_AVATARS
using VRC.Dynamics;
#endif

#if ATIV_UNIVRM_VRM1
using Silksprite.AvatarTinkerVista.VRM1.Nondestructive;
#endif

namespace Silksprite.AvatarTinkerVista
{
    public static class SimpleWearSetup
    {
        public static void PopulateHumanoidModules(Animator module)
        {
            var armatureRoots = module.GetComponentsInChildren<SkinnedMeshRenderer>()
                .Select(renderer => renderer.rootBone.parent)
                .Distinct();
            foreach (var armatureRoot in armatureRoots)
            {
                var simpleWear = armatureRoot.gameObject.AddComponent<AtivSimpleWear>();
                Undo.RegisterCreatedObjectUndo(simpleWear, "Create AtivSimpleWear");
                SetupHumanoidModule(simpleWear);
            }
        }

        public static void PopulateAccessoryModule(Animator module)
        {
            var simpleWear = module.gameObject.AddComponent<AtivSimpleWear>();
            Undo.RegisterCreatedObjectUndo(simpleWear, "Create AtivSimpleWear");
            SetupAccessoryModule(simpleWear);
        }

        public static void SetupHumanoidModule(AtivSimpleWear simpleWear)
        {
            var animator = simpleWear.GetComponentInParent<Animator>();
            var hips = simpleWear.transform.GetChild(0);
            simpleWear.moduleRootBones = new[]
            {
                new WearRootBoneEntry
                {
                    rootBone = hips,
                    armatureMode = animator.isHuman && animator.GetBoneTransform(HumanBodyBones.Hips) == hips ? WearArmatureMode.Humanoid : WearArmatureMode.All,
                    humanBone = HumanBodyBones.Hips
                }
            };
            simpleWear.moduleLeafBones = GuessLeafBones(animator.transform).ToArray();
        }
        
        public static void SetupAccessoryModule(AtivSimpleWear simpleWear)
        {
            simpleWear.moduleRootBones = new[]
            {
                new WearRootBoneEntry
                {
                    rootBone = simpleWear.transform,
                    armatureMode = WearArmatureMode.None,
                    humanBone = HumanBodyBones.Hips
                }
            };
            simpleWear.moduleLeafBones = GuessLeafBones(simpleWear.transform).ToArray();
        }

        public static void SetupAvatarIfNeeded(AtivSimpleWear simpleWear)
        {
            if (simpleWear.avatarRootBones.All(rootBone => !rootBone.rootBone))
            {
                SetupAvatar(simpleWear);
            }
        }

        public static void SetupAvatar(AtivSimpleWear simpleWear)
        {
            var animator = simpleWear.GetComponentInParent<Animator>();
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(animator.transform);
            var avatarAnimator = avatarRoot.GetComponent<Animator>();
            simpleWear.avatarRootBones = simpleWear.moduleRootBones.Select(moduleRootBone => new WearRootBoneEntry
            {
                rootBone = avatarAnimator.GetBoneTransform(moduleRootBone.humanBone),
                armatureMode = WearArmatureMode.Humanoid,
                humanBone = moduleRootBone.humanBone
            }).ToArray();
            simpleWear.avatarLeafBones = GuessLeafBones(avatarRoot).ToArray();
        }

        static IEnumerable<Transform> GuessLeafBones(Transform root)
        {
            var leafBones = new HashSet<Transform>();
#if ATIV_VRCSDK3_AVATARS
            foreach (var pb in root.GetComponentsInChildren<VRCPhysBoneBase>())
            {
                leafBones.Add(pb.GetRootTransform());
            }
#endif
            foreach (var gd in root.GetComponentsInChildren<AtivGenerateDynamics>())
            {
                leafBones.Add(gd.ActualRootBone);
            }
#if ATIV_UNIVRM_VRM1
            foreach (var gsb1 in root.GetComponentsInChildren<AtivMergeVRM1SpringBones>())
            {
                foreach (var spring in gsb1.springs)
                {
                    if (spring.Joints
                        .OrderBy(joint => AtivRuntimeUtil.RelativePath(root.transform, joint.transform))
                        .FirstOrDefault() is { } jointRoot)
                    {
                        leafBones.Add(jointRoot.transform);
                    }
                }
            }
#endif
            var childModules = root.GetComponentsInChildren<Animator>()
                .Select(a => a.transform)
                .Where(t => t != root)
                .ToArray();
            return leafBones .Where(leafBone => !childModules.Any(leafBone.IsChildOf));
        }
    }
}
