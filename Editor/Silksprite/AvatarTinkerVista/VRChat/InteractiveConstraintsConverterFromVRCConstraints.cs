using Silksprite.AvatarTinkerVista.Base;
using Silksprite.AvatarTinkerVista.VRChat.Converter;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public class InteractiveConstraintsConverterFromVRCConstraints : InteractiveConverterBase<Transform>
    {
        protected override string UndoName => "ATiV: Extract VRCConstraints";
        protected override string Title => "Extract VRCConstraints as ATiVGenerateVRMConstraints";
        protected override string DestroyTarget => "VRC Constraints";

        protected override void Convert(Transform context, bool destroy)
        {
            new ConstraintsConverterFromVRCConstraint().Convert(context.transform, destroy);
        }
    }
}