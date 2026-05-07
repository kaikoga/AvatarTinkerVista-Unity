using System;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Converters;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using UnityEngine;
using VRM;
using static Silksprite.AvatarTinkerVista.AtivOverwriteViewPosition;

namespace Silksprite.AvatarTinkerVista.VRM0.Converters
{
    public class ViewPositionConverterForVRM0 : AtivOptionConverterBase<AtivOverwriteViewPosition, ViewPositionOption, VRMMeta>
    {
        public override void ToAtiv(AtivOverwriteViewPosition ativ, VRMMeta platform)
        {
            if (!platform.TryGetComponent<VRMFirstPerson>(out var firstPerson))
            {
                return;
            }
            var rootBone = platform.transform;
            var headBone = firstPerson.FirstPersonBone ?? platform.GetComponent<Animator>()?.GetBoneTransform(HumanBodyBones.Head) ?? rootBone;
            var transformLocalPosition = firstPerson.FirstPersonOffset;
            var transformReference = AvatarRelativeTransform.OfAvatar(platform.transform, headBone);
            var globalPosition = rootBone.InverseTransformPoint(headBone.TransformPoint(firstPerson.FirstPersonOffset));

            foreach (var viewPositionStyle in new[] { ViewPositionStyle.TransformLocal, ViewPositionStyle.Global })
            {
                var options = ativ.options.FirstOrDefault(o => o.viewPositionStyle == viewPositionStyle);
                if (options == null)
                {
                    options = new ViewPositionOption
                    {
                        viewPositionStyle = viewPositionStyle
                    };
                    ativ.options.Add(options);
                }

                options.transform = transformReference;
                options.transformLocalPosition = transformLocalPosition;
                options.globalPosition = globalPosition;
            }
        }

        public override ViewPositionOption GetOption(AtivOverwriteViewPosition overwriteViewPosition)
        {
            return overwriteViewPosition.options.FirstOrDefault(option => option.viewPositionStyle switch
            {
                ViewPositionStyle.Inherit => true,
                ViewPositionStyle.Global => true,
                ViewPositionStyle.HeadLocal => true,
                ViewPositionStyle.TransformLocal => true,
                _ => throw new ArgumentOutOfRangeException()
            }) ?? new ViewPositionOption
            {
                viewPositionStyle = ViewPositionStyle.HeadLocal,
                globalPosition = new Vector3(0, 0.06f, 0f)
            };
        }

        public override void ToPlatform(AtivOverwriteViewPosition ativ, VRMMeta platform)
        {
            if (!platform.TryGetComponent<VRMFirstPerson>(out var firstPerson))
            {
                return;
            }
            var rootBone = platform.transform;
            var headBone = platform.GetComponent<Animator>()?.GetBoneTransform(HumanBodyBones.Head) ?? rootBone;
            var options = GetOption(ativ);
            switch (options.viewPositionStyle) 
            {
                case ViewPositionStyle.Inherit:
                    break;
                case ViewPositionStyle.Global:
                    firstPerson.FirstPersonBone = headBone;
                    firstPerson.FirstPersonOffset = headBone.InverseTransformPoint(rootBone.TransformPoint(options.globalPosition));
                    break;
                case ViewPositionStyle.HeadLocal:
                    firstPerson.FirstPersonBone = headBone;
                    firstPerson.FirstPersonOffset = options.headLocalPosition;
                    break;
                case ViewPositionStyle.TransformLocal:
                    firstPerson.FirstPersonBone = options.transform.ResolveFromAvatar(platform.transform) ?? rootBone;
                    firstPerson.FirstPersonOffset = options.transformLocalPosition;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
