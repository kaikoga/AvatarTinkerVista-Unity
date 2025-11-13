using System.Linq;
using Silksprite.AvatarTinkerVista.Nondestructive;
using UnityEngine;
using VRM;

namespace Silksprite.AvatarTinkerVista.VRM0.Processors
{
    public static class DefaultVRM0FirstPersonProcessor
    {
        public static void Process(VRMFirstPerson vrmFirstPerson)
        {
            var ativ = vrmFirstPerson.GetComponentInChildren<AtivDefaultVRMFirstPerson>(true);
            if (!ativ) return;

            if (ativ.firstPersonOffset.willOverwrite)
            {
                var animator = vrmFirstPerson.GetComponent<Animator>();
                if (animator) vrmFirstPerson.FirstPersonBone = animator.GetBoneTransform(HumanBodyBones.Head);
                vrmFirstPerson.FirstPersonOffset = ativ.firstPersonOffset.value;
            }

            var renderers = vrmFirstPerson.GetComponentsInChildren<Renderer>(true)
                .Where(renderer => renderer is not SkinnedMeshRenderer smr || (bool)smr.sharedMesh);

            FirstPersonFlag defaultValue;
            switch (ativ.defaultValue)
            {
                case AtivDefaultVRMFirstPerson.AtivFirstPersonFlag.Auto:
                default:
                    defaultValue = FirstPersonFlag.Auto;
                    break;
                case AtivDefaultVRMFirstPerson.AtivFirstPersonFlag.Both:
                    defaultValue = FirstPersonFlag.Both;
                    break;
                case AtivDefaultVRMFirstPerson.AtivFirstPersonFlag.ThirdPersonOnly:
                    defaultValue = FirstPersonFlag.ThirdPersonOnly;
                    break;
                case AtivDefaultVRMFirstPerson.AtivFirstPersonFlag.FirstPersonOnly:
                    defaultValue = FirstPersonFlag.FirstPersonOnly;
                    break;
            }

            vrmFirstPerson.Renderers = renderers.Select(renderer =>
            {
                var firstPersonFlag = defaultValue;
                foreach (var rendererFpf in vrmFirstPerson.Renderers)
                {
                    if (rendererFpf.Renderer == renderer)
                    {
                        firstPersonFlag = rendererFpf.FirstPersonFlag;
                        break;
                    }
                }
                return new VRMFirstPerson.RendererFirstPersonFlags
                {
                    Renderer = renderer,
                    FirstPersonFlag = firstPersonFlag
                };
            }).ToList();
        }
    }
}