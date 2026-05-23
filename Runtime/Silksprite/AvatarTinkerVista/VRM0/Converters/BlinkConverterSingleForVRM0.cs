using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converters;
using UnityEngine;
using VRM;
using static Silksprite.AvatarTinkerVista.AtivOverwriteBlink;

namespace Silksprite.AvatarTinkerVista.VRM0.Converters
{
    public class BlinkConverterSingleForVRM0 : AtivOptionConverterBase<AtivOverwriteBlink, BlinkOption, VRMBlendShapeProxy>
    {
        public override void ToAtiv(AtivOverwriteBlink ativ, VRMBlendShapeProxy platform)
        {
            throw new NotSupportedException();
        }

        public override BlinkOption GetOption(AtivOverwriteBlink overwriteVisemes)
        {
            return overwriteVisemes.options.FirstOrDefault(option => option.blinkStyle switch
            {
                BlinkStyle.Inherit => true,
                BlinkStyle.None => true,
                BlinkStyle.SingleBlendShape => true,
                BlinkStyle.SeparateBlendShapes => false,
                _ => throw new ArgumentOutOfRangeException()
            }) ?? new BlinkOption
            {
                blinkStyle = BlinkStyle.None
            };
        }

        public override void ToPlatform(AtivOverwriteBlink ativ, VRMBlendShapeProxy platform)
        {
            var options = GetOption(ativ);
            if (options.faceMesh.ResolveFromAvatar(platform.transform) is not { sharedMesh: { } sharedMesh })
            {
                return;
            }

            void SetBlendShape(BlendShapePreset preset, string? blendShapeName)
            {
                var clip = ScriptableObject.CreateInstance<BlendShapeClip>();
                if (blendShapeName is not null)
                {
                    clip.Values = new[]
                    {
                        new BlendShapeBinding
                        {
                            RelativePath = options.faceMesh.RelativePath,
                            Index = sharedMesh.GetBlendShapeIndex(blendShapeName),
                            Weight = 100f
                        }
                    };
                }
                platform.BlendShapeAvatar.SetClip(BlendShapeKey.CreateFromPreset(preset), clip);
            }

            switch (options.blinkStyle)
            {
                case BlinkStyle.Inherit:
                    break;
                case BlinkStyle.None:
                    SetBlendShape(BlendShapePreset.Blink, null);
                    break;
                case BlinkStyle.SingleBlendShape:
                    SetBlendShape(BlendShapePreset.Blink, options.singleBlendShape);
                    break;
                case BlinkStyle.SeparateBlendShapes:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
