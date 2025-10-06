using System.Linq;
using nadena.dev.ndmf;
using UnityEngine;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    class DefaultVRM0FirstPersonPass : Pass<DefaultVRM0FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmFirstPerson = context.AvatarRootTransform.GetComponent<VRMFirstPerson>();
            if (!vrmFirstPerson) return;

            var ativ = context.AvatarRootTransform.GetComponentInChildren<AtivDefaultVRMFirstPerson>(true);
            if (!ativ) return;

            if (ativ.firstPersonOffset.willOverwrite)
            {
                var animator = context.AvatarRootTransform.GetComponent<Animator>();
                if (animator) vrmFirstPerson.FirstPersonBone = animator.GetBoneTransform(HumanBodyBones.Head);
                vrmFirstPerson.FirstPersonOffset = ativ.firstPersonOffset.value;
            }


            var renderers = context.AvatarRootTransform.GetComponentsInChildren<Renderer>(true)
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
