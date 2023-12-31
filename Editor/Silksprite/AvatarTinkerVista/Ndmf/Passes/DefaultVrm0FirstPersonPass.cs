#if ATIV_VRM0

using System.Linq;
using nadena.dev.ndmf;
using UnityEngine;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class DefaultVrm0FirstPersonPass : Pass<DefaultVrm0FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmFirstPerson = context.AvatarRootTransform.GetComponent<VRMFirstPerson>();
            if (!vrmFirstPerson) return;

            var ativ = context.AvatarRootTransform.GetComponentInChildren<AtivDefaultVrmFirstPerson>(true);
            if (!ativ) return;

            var renderers = context.AvatarRootTransform.GetComponentsInChildren<Renderer>(true)
                .Where(renderer => renderer is not SkinnedMeshRenderer smr || (bool)smr.sharedMesh);

            FirstPersonFlag defaultValue;
            switch (ativ.defaultValue)
            {
                case AtivDefaultVrmFirstPerson.AtivFirstPersonFlag.Auto:
                default:
                    defaultValue = FirstPersonFlag.Auto;
                    break;
                case AtivDefaultVrmFirstPerson.AtivFirstPersonFlag.Both:
                    defaultValue = FirstPersonFlag.Both;
                    break;
                case AtivDefaultVrmFirstPerson.AtivFirstPersonFlag.ThirdPersonOnly:
                    defaultValue = FirstPersonFlag.ThirdPersonOnly;
                    break;
                case AtivDefaultVrmFirstPerson.AtivFirstPersonFlag.FirstPersonOnly:
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

#endif
