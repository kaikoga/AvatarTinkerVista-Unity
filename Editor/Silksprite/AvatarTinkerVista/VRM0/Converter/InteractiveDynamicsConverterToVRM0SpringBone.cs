using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.VRM0.Converter
{
    public class InteractiveDynamicsConverterToVRM0SpringBone : InteractiveConverterBase<Transform>
    {
        protected override string UndoName => "ATiV: Bake ATiVSpringBones as VRM0";
        protected override string Title => "Bake ATiVGenerateSpringBones as VRM0 SpringBones";
        protected override string DestroyTarget => "Destroy ATiV PhysBones";

        protected override void Convert(Transform context, bool destroy)
        {
            new DynamicsConverterToVRM0SpringBone().Convert(context.transform, destroy);
        }
    }
}