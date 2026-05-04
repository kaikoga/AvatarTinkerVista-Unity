using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Utils;
using UniGLTF.Extensions.VRMC_vrm;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Processors
{
    public static class DefaultVRM1FirstPersonProcessor
    {
        public static void Process(Vrm10Instance vrmInstance)
        {
            var ativ = vrmInstance.GetComponentInChildren<AtivDefaultVRMFirstPerson>();
            if (!ativ) return;

            var vrm = vrmInstance.Vrm;
            if (!vrm) return;
            
            ativ.firstPersonOffset.OverwriteValue(ref vrm.LookAt.OffsetFromHead);

            DoOverwrite(vrmInstance, ativ, vrm.FirstPerson);
        }

        static void DoOverwrite(Vrm10Instance vrmInstance, AtivDefaultVRMFirstPerson ativ, VRM10ObjectFirstPerson newFirstPerson)
        {
            var renderers = vrmInstance.GetComponentsInChildren<Renderer>(true)
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
                    if (rendererFpf.GetRenderer(vrmInstance.transform) == renderer)
                    {
                        firstPersonFlag = rendererFpf.FirstPersonFlag;
                        break;
                    }
                }
                return new RendererFirstPersonFlags
                {
                    Renderer = AtivRuntimeUtil.RelativePath(vrmInstance.transform, renderer.transform),
                    FirstPersonFlag = firstPersonFlag
                };
            }).ToList();
        }
    }
}