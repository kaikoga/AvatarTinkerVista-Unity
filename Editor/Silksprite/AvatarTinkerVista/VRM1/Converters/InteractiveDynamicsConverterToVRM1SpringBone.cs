using Silksprite.AvatarTinkerVista.Common;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Converters
{
    public class InteractiveDynamicsConverterToVRM1SpringBone : InteractiveConverterBase<Vrm10Instance>
    {
        protected override string UndoName => "ATiV: Bake ATiVSpringBones into VRM1 SpringBones";
        protected override string Title => "Bake ATiVGenerateSpringBones into Vrm10Instance SpringBones";
        protected override string DestroyTarget => "ATiV SpringBones";

        protected override void Convert(Vrm10Instance context, bool destroy)
        {
            new DynamicsConverterToVRM1SpringBone().Convert(context, destroy);
        }
    }
}