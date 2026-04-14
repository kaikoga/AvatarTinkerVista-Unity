using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converters;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using static Silksprite.AvatarTinkerVista.AtivOverwriteViewPosition;

namespace Silksprite.AvatarTinkerVista.VRChat.Converters
{
    public class ViewPositionConverterForVRChat : AtivOptionConverterBase<AtivOverwriteViewPosition, ViewPositionOption, VRCAvatarDescriptor>
    {
        public override void ToAtiv(AtivOverwriteViewPosition ativ, VRCAvatarDescriptor platform)
        {
            const ViewPositionStyle viewPositionStyle = ViewPositionStyle.Global;

            var options = ativ.options.FirstOrDefault(o => o.viewPositionStyle == viewPositionStyle);
            if (options == null)
            {
                options = new ViewPositionOption
                {
                    viewPositionStyle = viewPositionStyle
                };
                ativ.options.Add(options);
            }

            options.globalPosition = platform.ViewPosition;
        }

        public override ViewPositionOption GetOption(AtivOverwriteViewPosition overwriteViewPosition)
        {
            return overwriteViewPosition.options.FirstOrDefault(option => option.viewPositionStyle switch
            {
                ViewPositionStyle.Global => true,
                ViewPositionStyle.HeadLocal => true,
                ViewPositionStyle.TransformLocal => false,
                _ => throw new ArgumentOutOfRangeException()
            }) ?? new ViewPositionOption
            {
                viewPositionStyle = ViewPositionStyle.Global,
                globalPosition = new Vector3(0, 1.6f, 0.2f)
            };
        }

        public override void ToPlatform(AtivOverwriteViewPosition ativ, VRCAvatarDescriptor platform)
        {
            var options = GetOption(ativ);
            switch (options.viewPositionStyle) 
            {
                case ViewPositionStyle.Global:
                    platform.ViewPosition = options.globalPosition;
                    break;
                case ViewPositionStyle.HeadLocal:
                    var rootBone = platform.transform;
                    var headBone = platform.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head) ?? rootBone;
                    platform.ViewPosition = rootBone.InverseTransformPoint(headBone.TransformPoint(options.headLocalPosition));
                    break;
                case ViewPositionStyle.TransformLocal:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            };
        }
    }
}
