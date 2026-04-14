using Silksprite.AvatarTinkerVista.Common;
using Silksprite.AvatarTinkerVista.VRChat.Converters;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.VRChat.Converter
{
    public class InteractiveConstraintsConverterFromVRCConstraints : InteractiveConverterBase<Transform>
    {
        protected override string UndoName => "ATiV: Extract VRC Constraints";
        protected override string Title => "Extract VRC Constraints as ATiV Generate VRM1 Constraints";
        protected override string DestroyTarget => "VRC Constraints";

        protected override void Convert(Transform context, bool destroy)
        {
            new ConstraintsConverterFromVRCConstraint().Convert(context.transform, destroy);
        }
    }
}