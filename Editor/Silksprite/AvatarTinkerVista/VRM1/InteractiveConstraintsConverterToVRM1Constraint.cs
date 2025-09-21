using Silksprite.AvatarTinkerVista.Base;
using Silksprite.AvatarTinkerVista.VRM1.Converter;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.VRM1
{
    public class InteractiveConstraintsConverterToVRM1Constraint : InteractiveConverterBase<Transform>
    {
        protected override string UndoName => "ATiV: Bake ATiVConstraints as VRM1";
        protected override string Title => "Bake ATiVGenerateConstraints as Vrm10Constraints";
        protected override string DestroyTarget => "ATiV Constraints";

        protected override void Convert(Transform context, bool destroy)
        {
            new ConstraintsConverterToVRM1Constraints().Convert(context.transform, destroy);
        }
    }
}