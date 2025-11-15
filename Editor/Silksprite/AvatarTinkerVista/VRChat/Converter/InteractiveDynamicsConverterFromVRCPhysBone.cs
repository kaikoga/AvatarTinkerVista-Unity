using Silksprite.AvatarTinkerVista.Common;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.VRChat.Converter
{
    public class InteractiveDynamicsConverterFromVRCPhysBone : InteractiveConverterBase<Transform>
    {
        protected override string UndoName => "ATiV: Extract VRC PhysBones";
        protected override string Title => "Extract VRC PhysBones as ATiV Generate VRM0+1 SpringBones";
        protected override string DestroyTarget => "VRC PhysBones";

        protected override void Convert(Transform context, bool destroy)
        {
            new DynamicsConverterFromVRCPhysBone().Convert(context.transform, destroy);
        }
    }
}