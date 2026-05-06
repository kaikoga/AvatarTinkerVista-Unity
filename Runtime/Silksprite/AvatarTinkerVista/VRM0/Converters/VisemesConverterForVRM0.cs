using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converters;
using UnityEngine;
using VRM;
using static Silksprite.AvatarTinkerVista.AtivOverwriteVisemes;

namespace Silksprite.AvatarTinkerVista.VRM0.Converters
{
    public class VisemesConverterForVRM0 : AtivOptionConverterBase<AtivOverwriteVisemes, VisemeOption, VRMBlendShapeProxy>
    {
        public override void ToAtiv(AtivOverwriteVisemes ativ, VRMBlendShapeProxy platform)
        {
            throw new NotSupportedException();
        }

        public override VisemeOption GetOption(AtivOverwriteVisemes overwriteVisemes)
        {
            return overwriteVisemes.options.FirstOrDefault(option => option.visemeStyle switch
            {
                VisemeStyle.None => true,
                VisemeStyle.SingleBlendShape => true,
                VisemeStyle.VrmBlendShapes => true,
                VisemeStyle.OculusVisemes => true,
                _ => throw new ArgumentOutOfRangeException()
            }) ?? new VisemeOption
            {
                visemeStyle = VisemeStyle.None
            };
        }

        public override void ToPlatform(AtivOverwriteVisemes ativ, VRMBlendShapeProxy platform)
        {
            var options = GetOption(ativ);
            if (options.faceMesh.ResolveFromAvatar(platform.transform) is not { sharedMesh: { } sharedMesh })
            {
                return;
            }

            void SetBlendShape(BlendShapePreset preset, string blendShapeName)
            {
                var clip = ScriptableObject.CreateInstance<BlendShapeClip>();
                clip.Values = new[]
                {
                    new BlendShapeBinding
                    {
                        RelativePath = options.faceMesh.RelativePath,
                        Index = sharedMesh.GetBlendShapeIndex(blendShapeName),
                        Weight = 100f
                    }
                };
                platform.BlendShapeAvatar.SetClip(BlendShapeKey.CreateFromPreset(preset), clip);
            }

            switch (options.visemeStyle)
            {
                case VisemeStyle.None:
                    break;
                case VisemeStyle.SingleBlendShape:
                    SetBlendShape(BlendShapePreset.A, options.singleBlendShape);
                    SetBlendShape(BlendShapePreset.I, options.singleBlendShape);
                    SetBlendShape(BlendShapePreset.U, options.singleBlendShape);
                    SetBlendShape(BlendShapePreset.E, options.singleBlendShape);
                    SetBlendShape(BlendShapePreset.O, options.singleBlendShape);
                    break;
                case VisemeStyle.VrmBlendShapes:
                    SetBlendShape(BlendShapePreset.A, options.vrmA);
                    SetBlendShape(BlendShapePreset.I, options.vrmI);
                    SetBlendShape(BlendShapePreset.U, options.vrmU);
                    SetBlendShape(BlendShapePreset.E, options.vrmE);
                    SetBlendShape(BlendShapePreset.O, options.vrmO);
                    break;
                case VisemeStyle.OculusVisemes:
                    SetBlendShape(BlendShapePreset.A, options.oculusAa);
                    SetBlendShape(BlendShapePreset.I, options.oculusI);
                    SetBlendShape(BlendShapePreset.U, options.oculusU);
                    SetBlendShape(BlendShapePreset.E, options.oculusE);
                    SetBlendShape(BlendShapePreset.O, options.oculusO);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
