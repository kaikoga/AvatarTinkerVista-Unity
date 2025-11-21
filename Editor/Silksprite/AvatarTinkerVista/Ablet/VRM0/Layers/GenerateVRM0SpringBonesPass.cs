using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.VRM0.Converter;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM0.Layers
{
    [AbletLayer]
    class GenerateVRM0SpringBonesPass : IAbletLayer
    {
        public string Id => "net.kaikoga.ativ.vrm0.generate-vrm0-spring-bones";
        public string DisplayName => "ATiV: Generate VRM0 SpringBones";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<GeneratingPhase>();
        }
        public IAbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return new AbletBuildProcedure(context =>
            {
                if (context.CurrentRootObject.TryGetComponent<VRMMeta>(out _))
                {
                    new DynamicsConverterToVRM0SpringBone().Convert(context.CurrentRootTransform, true);
                }
            });
        }
    }
}
