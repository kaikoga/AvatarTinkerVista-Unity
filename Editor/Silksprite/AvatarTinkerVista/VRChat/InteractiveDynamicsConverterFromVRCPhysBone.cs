using Silksprite.AvatarTinkerVista.Base;
using Silksprite.AvatarTinkerVista.VRChat.Converter;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public class InteractiveDynamicsConverterFromVRCPhysBone : InteractiveConverterBase<Transform>
    {
        protected override string UndoName => "ATiV: Extract VRCPhysBones";
        protected override string Title => "Extract VRCPhysBones as ATiVGenerateVRMSpringBones";
        protected override string DestroyTarget => "VRC PhysBones";

        protected override void Convert(Transform context, bool destroy)
        {
            new DynamicsConverterFromVRCPhysBone().Convert(context.transform, destroy);
        }
    }
}