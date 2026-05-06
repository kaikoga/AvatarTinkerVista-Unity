using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converters;
using UnityEngine;
using UniVRM10;
using static Silksprite.AvatarTinkerVista.AtivOverwriteVisemes;

namespace Silksprite.AvatarTinkerVista.VRM1.Converters
{
    public class VisemesConverterForVRM1 : AtivOptionConverterBase<AtivOverwriteVisemes, VisemeOption, Vrm10Instance>
    {
        public override void ToAtiv(AtivOverwriteVisemes ativ, Vrm10Instance platform)
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

        public override void ToPlatform(AtivOverwriteVisemes ativ, Vrm10Instance platform)
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

            switch (options.visemeStyle)
            {
                case VisemeStyle.None:
                    break;
                case VisemeStyle.SingleBlendShape:
                    platform.Vrm.Expression.Aa = CreateClip(options.singleBlendShape);
                    platform.Vrm.Expression.Ih = CreateClip(options.singleBlendShape);
                    platform.Vrm.Expression.Ou = CreateClip(options.singleBlendShape);
                    platform.Vrm.Expression.Ee = CreateClip(options.singleBlendShape);
                    platform.Vrm.Expression.Oh = CreateClip(options.singleBlendShape);
                    break;
                case VisemeStyle.VrmBlendShapes:
                    platform.Vrm.Expression.Aa = CreateClip(options.vrmA);
                    platform.Vrm.Expression.Ih = CreateClip(options.vrmI);
                    platform.Vrm.Expression.Ou = CreateClip(options.vrmU);
                    platform.Vrm.Expression.Ee = CreateClip(options.vrmE);
                    platform.Vrm.Expression.Oh = CreateClip(options.vrmO);
                    break;
                case VisemeStyle.OculusVisemes:
                    platform.Vrm.Expression.Aa = CreateClip(options.oculusAa);
                    platform.Vrm.Expression.Ih = CreateClip(options.oculusI);
                    platform.Vrm.Expression.Ou = CreateClip(options.oculusU);
                    platform.Vrm.Expression.Ee = CreateClip(options.oculusE);
                    platform.Vrm.Expression.Oh = CreateClip(options.oculusO);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
