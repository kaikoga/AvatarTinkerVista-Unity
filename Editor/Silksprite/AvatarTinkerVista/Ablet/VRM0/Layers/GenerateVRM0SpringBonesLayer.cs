using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.Common.Registries;
using Silksprite.AvatarTinkerVista.VRM0.Converters;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM0.Layers
{
    [AbletLayer]
    class GenerateVRM0SpringBonesLayer : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.GenerateVRM0SpringBones";
        public string DisplayName => "ATiV: Generate VRM0 SpringBones";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<GeneratingPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet)
            {
                return null;
            }

            return AbletBuildProcedure.Create(context =>
            {
                switch (AtivSelectDynamics.GetDynamicsIdOf(context.CurrentRootTransform))
                {
                    case DynamicsRegistry.Auto:
                        if (context.CurrentRootObject.TryGetComponent<VRMMeta>(out _))
                        {
                            DoConvertVRM0(context);
                        }
                        break;
                    case DynamicsConverterToVRM0SpringBone.DynamicsId:
                        DoConvertVRM0(context);
                        break;
                }
            });
        }

        static void DoConvertVRM0(IBuildContext context)
        {
            new DynamicsConverterToVRM0SpringBone().Convert(context.CurrentRootTransform, true);
        }
    }
}
