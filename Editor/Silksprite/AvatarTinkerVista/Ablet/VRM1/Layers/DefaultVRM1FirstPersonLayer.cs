using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.VRM1.Processors;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM1.Layers
{
    [AbletLayer]
    class DefaultVRM1FirstPersonLayer : IAbletLayer
    {
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.DefaultVRM1FirstPerson";
        string IAbletDefinition.DisplayName => "ATiV: Default VRM1 FirstPerson";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<MaterializingPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                if (context.CurrentRootObject.TryGetComponent<Vrm10Instance>(out var vrmInstance))
                {
                    DefaultVRM1FirstPersonProcessor.Process(vrmInstance);
                }
            });
        }

    }
}
