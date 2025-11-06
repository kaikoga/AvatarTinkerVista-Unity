using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Converter
{
    public class InteractiveDynamicsConverterToMergeVRM1SpringBone : InteractiveConverterBase<Transform>
    {
        protected override string UndoName => "ATiV: Bake ATiVSpringBones as ATiV Merge VRM1 SpringBones";
        protected override string Title => "Bake ATiVGenerateSpringBones as ATiV Merge VRM1 SpringBones";
        protected override string DestroyTarget => "ATiV SpringBones";

        protected override void Convert(Transform context, bool destroy)
        {
            new DynamicsConverterToMergeVRM1SpringBone().Convert(context, destroy);
        }
    }
}