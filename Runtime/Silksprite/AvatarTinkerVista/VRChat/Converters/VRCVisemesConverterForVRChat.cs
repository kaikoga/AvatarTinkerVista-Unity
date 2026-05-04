using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converters;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using VRC.SDK3.Avatars.Components;
using VRC.SDKBase;
using static Silksprite.AvatarTinkerVista.AtivOverwriteVRCVisemes;

namespace Silksprite.AvatarTinkerVista.VRChat.Converters
{
    public class VRCVisemesConverterForVRChat : AtivOptionConverterBase<AtivOverwriteVRCVisemes, VisemeOption, VRCAvatarDescriptor>
    {
        public override void ToAtiv(AtivOverwriteVRCVisemes ativ, VRCAvatarDescriptor platform)
        {
            var visemeStyle = platform.lipSync switch
            {
                VRC_AvatarDescriptor.LipSyncStyle.Default => VisemeStyle.OculusVisemes,
                VRC_AvatarDescriptor.LipSyncStyle.JawFlapBlendShape => VisemeStyle.SingleBlendShape,
                VRC_AvatarDescriptor.LipSyncStyle.VisemeBlendShape => VisemeStyle.OculusVisemes,
                _ => VisemeStyle.None
            };

            var options = ativ.options.FirstOrDefault(o => o.visemeStyle == visemeStyle);
            if (options == null)
            {
                options = new VisemeOption
                {
                    visemeStyle = visemeStyle
                };
                ativ.options.Add(options);
            } 

            options.faceMesh = AvatarRelativeSkinnedMeshRenderer.OfAvatar(platform.transform, platform.VisemeSkinnedMesh);
            switch (visemeStyle)
            {
                case VisemeStyle.None:
                    break;
                case VisemeStyle.SingleBlendShape:
                    options.singleBlendShape = platform.MouthOpenBlendShapeName;
                    break;
                case VisemeStyle.OculusVisemes:
                    if (platform.VisemeBlendShapes is { } visemeBlendShapesValue)
                    {
                        var visemeBlendShapes = new Queue<string>(visemeBlendShapesValue);
                        options.oculusSil = visemeBlendShapes.Dequeue();
                        options.oculusPp = visemeBlendShapes.Dequeue();
                        options.oculusFf = visemeBlendShapes.Dequeue();
                        options.oculusTh = visemeBlendShapes.Dequeue();
                        options.oculusDd = visemeBlendShapes.Dequeue();
                        options.oculusKk = visemeBlendShapes.Dequeue();
                        options.oculusCh = visemeBlendShapes.Dequeue();
                        options.oculusSs = visemeBlendShapes.Dequeue();
                        options.oculusNn = visemeBlendShapes.Dequeue();
                        options.oculusRr = visemeBlendShapes.Dequeue();
                        options.oculusAa = visemeBlendShapes.Dequeue();
                        options.oculusE = visemeBlendShapes.Dequeue();
                        options.oculusI = visemeBlendShapes.Dequeue();
                        options.oculusO = visemeBlendShapes.Dequeue();
                        options.oculusU = visemeBlendShapes.Dequeue();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override VisemeOption GetOption(AtivOverwriteVRCVisemes overwriteVisemes)
        {
            return overwriteVisemes.options.FirstOrDefault(option => option.visemeStyle switch
            {
                VisemeStyle.None => true,
                VisemeStyle.SingleBlendShape => true,
                VisemeStyle.OculusVisemes => true,
                _ => throw new ArgumentOutOfRangeException()
            }) ?? new VisemeOption
            {
                visemeStyle = VisemeStyle.None
            };
        }

        public override void ToPlatform(AtivOverwriteVRCVisemes ativ, VRCAvatarDescriptor platform)
        {
            var options = GetOption(ativ);
            switch (options.visemeStyle)
            {
                case VisemeStyle.None:
                    platform.lipSync = VRC_AvatarDescriptor.LipSyncStyle.VisemeParameterOnly;
                    break;
                case VisemeStyle.SingleBlendShape:
                    platform.VisemeSkinnedMesh = options.faceMesh.ResolveFromAvatar(platform.transform);
                    platform.lipSync = VRC_AvatarDescriptor.LipSyncStyle.JawFlapBlendShape;
                    platform.MouthOpenBlendShapeName = options.singleBlendShape;
                    break;
                case VisemeStyle.OculusVisemes:
                    platform.VisemeSkinnedMesh = options.faceMesh.ResolveFromAvatar(platform.transform);
                    platform.lipSync = VRC_AvatarDescriptor.LipSyncStyle.VisemeBlendShape;
                    platform.VisemeBlendShapes = new []
                    {
                        options.oculusSil,
                        options.oculusPp,
                        options.oculusFf,
                        options.oculusTh,
                        options.oculusDd,
                        options.oculusKk,
                        options.oculusCh,
                        options.oculusSs,
                        options.oculusNn,
                        options.oculusRr,
                        options.oculusAa,
                        options.oculusE,
                        options.oculusI,
                        options.oculusO,
                        options.oculusU,
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
