using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.AvatarTinkerVista.Common.Utils;
using Silksprite.AvatarTinkerVista.Common.Wear;
using UnityEditor;
using UnityEngine;

#if ATIV_VRCSDK3_AVATARS
using VRC.Dynamics;
#endif

#if ATIV_DETECTED_VRM0
using VRM;
#endif

#if ATIV_DETECTED_VRM1
using Silksprite.AvatarTinkerVista.VRM1;
#endif

namespace Silksprite.AvatarTinkerVista.Utils
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
                    armatureMode = animator switch
                    {
                        { isHuman: true } => animator.GetBoneTransform(HumanBodyBones.Hips) == hips ? WearArmatureMode.Humanoid : WearArmatureMode.All,
                        _ => WearArmatureMode.All
                    },
                    humanBone = HumanBodyBones.Hips
                }
            };
            simpleWear.moduleIgnoreBones = GuessIgnoreBones(((Component)animator ?? simpleWear).transform).ToArray();
            simpleWear.moduleLeafBones = Array.Empty<Transform>();
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
            simpleWear.moduleIgnoreBones = GuessIgnoreBones(simpleWear.transform).ToArray();
            simpleWear.moduleLeafBones = Array.Empty<Transform>();
        }

        public static void SetupAvatarIfNeeded(AtivSimpleWear simpleWear)
        {
            if (simpleWear.avatarRootBones.All(rootBone => rootBone.rootBone?.ResolveNow(simpleWear.transform) == null))
            {
                SetupAvatar(simpleWear);
            }
        }

        public static void SetupAvatar(AtivSimpleWear simpleWear)
        {
            var animator = simpleWear.GetComponentInParent<Animator>();
            var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(animator.transform);
            if (avatarRoot == null)
            {
                throw new InvalidOperationException("Could not find avatar");
            }
            var avatarAnimator = avatarRoot.GetComponent<Animator>();
            simpleWear.avatarRootBones = simpleWear.moduleRootBones.Select(moduleRootBone => new WearRelativeRootBoneEntry
            {
                rootBone = AvatarRelativeTransform.OfAvatar(avatarRoot, avatarAnimator.GetBoneTransform(moduleRootBone.humanBone)),
                armatureMode = WearArmatureMode.Humanoid,
                humanBone = moduleRootBone.humanBone
            }).ToArray();
            simpleWear.avatarIgnoreBones = GuessIgnoreBones(avatarRoot).ToArray();
            simpleWear.avatarLeafBones = Array.Empty<Transform>();
        }

        static IEnumerable<Transform> GuessIgnoreBones(Transform root)
        {
            var ignoreBones = new HashSet<Transform>();
#if ATIV_VRCSDK3_AVATARS
            foreach (var pb in root.GetComponentsInChildren<VRCPhysBoneBase>())
            {
                ignoreBones.Add(pb.GetRootTransform());
            }
#endif
            foreach (var gd in root.GetComponentsInChildren<AtivGenerateDynamics>())
            {
                ignoreBones.Add(gd.ActualRootBone);
            }
#if ATIV_DETECTED_VRM0
            foreach (var springBone in root.GetComponentsInChildren<VRMSpringBone>())
            {
                foreach (var rootBone in springBone.RootBones)
                {
                    ignoreBones.Add(rootBone);
                }
            }
#endif
#if ATIV_DETECTED_VRM1
            foreach (var gsb1 in root.GetComponentsInChildren<AtivMergeVRM1SpringBones>())
            {
                foreach (var spring in gsb1.springs)
                {
                    if (spring.Joints
                        .OrderBy(joint => AtivRuntimeUtil.RelativePath(root.transform, joint.transform))
                        .FirstOrDefault() is { } jointRoot)
                    {
                        ignoreBones.Add(jointRoot.transform);
                    }
                }
            }
#endif
            var childModules = root.GetComponentsInChildren<Animator>()
                .Select(a => a.transform)
                .Where(t => t != root)
                .ToArray();
            return ignoreBones.Where(bone => !childModules.Any(bone.IsChildOf));
        }
    }
}
