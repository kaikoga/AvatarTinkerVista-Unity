using System.Linq;
using nadena.dev.ndmf;
using nadena.dev.ndmf.runtime;
using Silksprite.AdLib.Utils.VRM1;
using UniGLTF.Extensions.VRMC_vrm;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class DefaultVRM1FirstPersonPass : Pass<DefaultVRM1FirstPersonPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmInstance = context.AvatarRootTransform.GetComponent<Vrm10Instance>();
            if (!vrmInstance) return;

            var ativ = context.AvatarRootTransform.GetComponentInChildren<AtivDefaultVRMFirstPerson>();
            if (!ativ) return;

            var vrm = vrmInstance.Vrm;
            if (!vrm) return;
            
            var newVrm = new CustomCloneVRM10Object().Clone(vrmInstance.Vrm).mainAsset;
            vrmInstance.Vrm = newVrm;

            ativ.firstPersonOffset.OverwriteValue(ref newVrm.LookAt.OffsetFromHead);

            DoOverwrite(context, ativ, newVrm.FirstPerson);
        }

        void DoOverwrite(BuildContext context, AtivDefaultVRMFirstPerson ativ, VRM10ObjectFirstPerson newFirstPerson)
        {
            var renderers = context.AvatarRootTransform.GetComponentsInChildren<Renderer>(true)
                .Where(renderer => renderer is not SkinnedMeshRenderer smr || (bool)smr.sharedMesh);
            
            FirstPersonType defaultValue;
            switch (ativ.defaultValue)
            {
                case AtivDefaultVRMFirstPerson.AtivFirstPersonFlag.Auto:
                default:
                    defaultValue = FirstPersonType.auto;
                    break;
                case AtivDefaultVRMFirstPerson.AtivFirstPersonFlag.Both:
                    defaultValue = FirstPersonType.both;
                    break;
                case AtivDefaultVRMFirstPerson.AtivFirstPersonFlag.ThirdPersonOnly:
                    defaultValue = FirstPersonType.thirdPersonOnly;
                    break;
                case AtivDefaultVRMFirstPerson.AtivFirstPersonFlag.FirstPersonOnly:
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
