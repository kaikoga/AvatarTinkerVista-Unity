using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.VRM1.Converter;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM1.Layers
{
    [AbletLayer]
    class GenerateVRM1SpringBonesPass : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.GenerateVRM1SpringBones";
        public string DisplayName => "ATiV: Generate VRM1 SpringBones";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<GeneratingPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create((IBuildContext context) =>
            {
                if (context.CurrentRootObject.TryGetComponent<Vrm10Instance>(out var vrm10Instance))
                {
                    new DynamicsConverterToVRM1SpringBone().Convert(vrm10Instance, true);
                }
            });
        }
    }
}
