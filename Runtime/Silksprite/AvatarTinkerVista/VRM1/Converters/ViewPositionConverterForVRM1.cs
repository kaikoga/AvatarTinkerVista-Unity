using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converters;
using UniHumanoid;
using UnityEngine;
using UniVRM10;
using static Silksprite.AvatarTinkerVista.AtivOverwriteViewPosition;

namespace Silksprite.AvatarTinkerVista.VRM1.Converters
{
    public class ViewPositionConverterForVRM1 : AtivOptionConverterBase<AtivOverwriteViewPosition, ViewPositionOption, Vrm10Instance>
    {
        public override void ToAtiv(AtivOverwriteViewPosition ativ, Vrm10Instance platform)
        {
            if (!(platform.Vrm?.LookAt is { } lookAt))
            {
                return;
            }
            const ViewPositionStyle viewPositionStyle = ViewPositionStyle.HeadLocal;

            var options = ativ.options.FirstOrDefault(o => o.viewPositionStyle == viewPositionStyle);
            if (options == null)
            {
                options = new ViewPositionOption
                {
                    viewPositionStyle = viewPositionStyle
                };
                ativ.options.Add(options);
            }

            options.headLocalPosition = lookAt.OffsetFromHead;
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
                viewPositionStyle = ViewPositionStyle.HeadLocal,
                headLocalPosition = new Vector3(0, 0.06f, 0)
            };
        }

        public override void ToPlatform(AtivOverwriteViewPosition ativ, Vrm10Instance platform)
        {
            if (!(platform.Vrm?.LookAt is { } lookAt))
            {
                return;
            }
            var options = GetOption(ativ);
            switch (options.viewPositionStyle) 
            {
                case ViewPositionStyle.Global:
                    var rootBone = platform.transform;
                    var headBone = platform.GetComponent<Humanoid>()?.Head ?? rootBone;
                    lookAt.OffsetFromHead = headBone.InverseTransformPoint(rootBone.TransformPoint(options.globalPosition));
                    break;
                case ViewPositionStyle.HeadLocal:
                    lookAt.OffsetFromHead = options.headLocalPosition;
                    break;
                case ViewPositionStyle.TransformLocal:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            };
        }
    }
}
