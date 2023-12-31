#if ATIV_VRM1

using System.Linq;
using nadena.dev.ndmf;
using nadena.dev.ndmf.runtime;
using Silksprite.AdLib.Utils.VRM1;
using UniGLTF.Extensions.VRMC_vrm;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class DefaultVrm1FirstPersonPass : Pass<DefaultVrm1FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmInstance = context.AvatarRootTransform.GetComponent<Vrm10Instance>();
            if (!vrmInstance) return;

            var ativ = context.AvatarRootTransform.GetComponentInChildren<AtivDefaultVrmFirstPerson>();
            if (!ativ) return;

            var vrm = vrmInstance.Vrm;
            if (!vrm) return;
            
            var newVrm = new CustomCloneVRM10Object().Clone(vrmInstance.Vrm).mainAsset;
            vrmInstance.Vrm = newVrm;
            DoOverwrite(context, ativ, newVrm.FirstPerson);
        }

        void DoOverwrite(BuildContext context, AtivDefaultVrmFirstPerson ativ, VRM10ObjectFirstPerson newFirstPerson)
        {
            var renderers = context.AvatarRootTransform.GetComponentsInChildren<Renderer>(true);
            
            FirstPersonType defaultValue;
            switch (ativ.defaultValue)
            {
                case AtivDefaultVrmFirstPerson.AtivFirstPersonFlag.Auto:
                default:
                    defaultValue = FirstPersonType.auto;
                    break;
                case AtivDefaultVrmFirstPerson.AtivFirstPersonFlag.Both:
                    defaultValue = FirstPersonType.both;
                    break;
                case AtivDefaultVrmFirstPerson.AtivFirstPersonFlag.ThirdPersonOnly:
                    defaultValue = FirstPersonType.thirdPersonOnly;
                    break;
                case AtivDefaultVrmFirstPerson.AtivFirstPersonFlag.FirstPersonOnly:
                    defaultValue = FirstPersonType.firstPersonOnly;
                    break;
            }

            newFirstPerson.Renderers = renderers.Select(renderer =>
            {
                var firstPersonFlag = defaultValue;
                foreach (var rendererFpf in newFirstPerson.Renderers)
                {
                    if (rendererFpf.GetRenderer(context.AvatarRootTransform) == renderer)
                    {
                        firstPersonFlag = rendererFpf.FirstPersonFlag;
                        break;
                    }
                }
                return new RendererFirstPersonFlags
                {
                    Renderer = RuntimeUtil.RelativePath(context.AvatarRootObject, renderer.gameObject),
                    FirstPersonFlag = firstPersonFlag
                };
            }).ToList();
        }
    }
}

#endif
