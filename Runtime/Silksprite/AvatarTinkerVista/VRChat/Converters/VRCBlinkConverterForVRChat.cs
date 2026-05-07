using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converters;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using VRC.SDK3.Avatars.Components;
using static Silksprite.AvatarTinkerVista.AtivOverwriteVRCBlink;

namespace Silksprite.AvatarTinkerVista.VRChat.Converters
{
    public class VRCBlinkConverterForVRChat : AtivOptionConverterBase<AtivOverwriteVRCBlink, BlinkOption, VRCAvatarDescriptor>
    {
        public override void ToAtiv(AtivOverwriteVRCBlink ativ, VRCAvatarDescriptor platform)
        {
            var blinkStyle = (platform.enableEyeLook, platform.customEyeLookSettings.eyelidType) switch
            {
                (true, VRCAvatarDescriptor.EyelidType.Blendshapes) => BlinkStyle.SingleBlendShape,
                _ => BlinkStyle.None
            };

            var options = ativ.options.FirstOrDefault(o => o.blinkStyle == blinkStyle);
            if (options == null)
            {
                options = new BlinkOption
                {
                    blinkStyle = blinkStyle
                };
                ativ.options.Add(options);
            } 

            switch (blinkStyle)
            {
                case BlinkStyle.None:
                    break;
                case BlinkStyle.SingleBlendShape:
                    var skinnedMesh = platform.customEyeLookSettings.eyelidsSkinnedMesh;
                    options.faceMesh = AvatarRelativeSkinnedMeshRenderer.OfAvatar(platform.transform, skinnedMesh);
                    var blendShapeName = skinnedMesh.sharedMesh.GetBlendShapeName(platform.customEyeLookSettings.eyelidsBlendshapes.FirstOrDefault());
                    options.singleBlendShape = blendShapeName;
                    throw new NotImplementedException();
                case BlinkStyle.SeparateBlendShapes:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override BlinkOption GetOption(AtivOverwriteVRCBlink overwriteBlink)
        {
            return overwriteBlink.options.FirstOrDefault(option => option.blinkStyle switch
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

        public override void ToPlatform(AtivOverwriteVRCBlink ativ, VRCAvatarDescriptor platform)
        {
            var options = GetOption(ativ);
            switch (options.blinkStyle) 
            {
                case BlinkStyle.Inherit:
                    break;
                case BlinkStyle.None:
                    platform.enableEyeLook = true;
                    platform.customEyeLookSettings.eyelidType = VRCAvatarDescriptor.EyelidType.None;
                    break;
                case BlinkStyle.SingleBlendShape:
                    platform.enableEyeLook = true;
                    if (options.faceMesh.ResolveFromAvatar(platform.transform) is { } skinnedMesh)
                    {
                        platform.customEyeLookSettings.eyelidType = VRCAvatarDescriptor.EyelidType.Blendshapes;
                        platform.customEyeLookSettings.eyelidsSkinnedMesh = skinnedMesh;
                        var blendShapeIndex = skinnedMesh.sharedMesh.GetBlendShapeIndex(options.singleBlendShape);
                        platform.customEyeLookSettings.eyelidsBlendshapes = new[] { blendShapeIndex, -1, -1 };
                    }
                    else
                    {
                        platform.customEyeLookSettings.eyelidType = VRCAvatarDescriptor.EyelidType.None;
                    }
                    break;
                case BlinkStyle.SeparateBlendShapes:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
