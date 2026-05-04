using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.Common.Registries;
using Silksprite.AvatarTinkerVista.VRM1.Converters;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM1.Layers
{
    [AbletLayer]
    class GenerateVRM1SpringBonesLayer : IAbletLayer
    {
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.GenerateVRM1SpringBones";
        string IAbletDefinition.DisplayName => "ATiV: Generate VRM1 SpringBones";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<GeneratingPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
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
                    case DynamicsConverterToVRM1SpringBone.DynamicsId:
                        if (context.CurrentRootObject.TryGetComponent<Vrm10Instance>(out var vrm10Instance))
                        {
                            DoConvertVRM1(vrm10Instance);
                        }
                        break;
                }
            });
        }

        static void DoConvertVRM1(Vrm10Instance vrm10Instance)
        {
            new DynamicsConverterToVRM1SpringBone().Convert(vrm10Instance, true);
        }
    }
}
