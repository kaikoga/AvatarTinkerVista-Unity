using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converters;
using UnityEngine;
using UniVRM10;
using static Silksprite.AvatarTinkerVista.AtivOverwriteBlink;

namespace Silksprite.AvatarTinkerVista.VRM1.Converters
{
    public class BlinkConverterSeparateForVRM1 : AtivOptionConverterBase<AtivOverwriteBlink, BlinkOption, Vrm10Instance>
    {
        public override void ToAtiv(AtivOverwriteBlink ativ, Vrm10Instance platform)
        {
            throw new NotSupportedException();
        }

        public override BlinkOption GetOption(AtivOverwriteBlink overwriteVisemes)
        {
            return overwriteVisemes.options.FirstOrDefault(option => option.blinkStyle switch
            {
                BlinkStyle.Inherit => true,
                BlinkStyle.None => true,
                BlinkStyle.SingleBlendShape => false,
                BlinkStyle.SeparateBlendShapes => true,
                _ => throw new ArgumentOutOfRangeException()
            }) ?? new BlinkOption
            {
                blinkStyle = BlinkStyle.None
            };
        }

        public override void ToPlatform(AtivOverwriteBlink ativ, Vrm10Instance platform)
        {
            var options = GetOption(ativ);
            if (options.faceMesh.ResolveFromAvatar(platform.transform) is not { sharedMesh: { } sharedMesh })
            {
                return;
            }

            VRM10Expression CreateClip(string blendShapeName)
            {
                var expression = ScriptableObject.CreateInstance<VRM10Expression>();
                expression.MorphTargetBindings = new[]
                {
                    new MorphTargetBinding
                    {
                        RelativePath = options.faceMesh.RelativePath,
                        Index = sharedMesh.GetBlendShapeIndex(blendShapeName),
                        Weight = 1f
                    }
                };
                return expression;
            }

            switch (options.blinkStyle)
            {
                case BlinkStyle.Inherit:
                    break;
                case BlinkStyle.None:
                    platform.Vrm.Expression.BlinkLeft = null;
                    platform.Vrm.Expression.BlinkRight = null;
                    break;
                case BlinkStyle.SingleBlendShape:
                    throw new NotSupportedException();
                case BlinkStyle.SeparateBlendShapes:
                    platform.Vrm.Expression.BlinkLeft = CreateClip(options.separateBlendShapeLeft);
                    platform.Vrm.Expression.BlinkRight = CreateClip(options.separateBlendShapeRight);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
