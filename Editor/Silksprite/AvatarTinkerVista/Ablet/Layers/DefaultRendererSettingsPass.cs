using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.Processors;

namespace Silksprite.AvatarTinkerVista.Ablet.Layers
{
    [AbletLayer]
    class DefaultRendererSettingsPass : IAbletLayer
    {
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.DefaultRendererSettings";
        string IAbletDefinition.DisplayName => "ATiV: Default Renderer Settings";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<MaterializingPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                context.RootTransform.Observe(DefaultRendererSettingsProcessor.Process);
            });
        }
    }
}
