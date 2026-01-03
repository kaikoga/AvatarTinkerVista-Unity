using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Ablet.Querying;
using Silksprite.AvatarTinkerVista.Common.Wear;
using Silksprite.AvatarTinkerVista.Processors;

namespace Silksprite.AvatarTinkerVista.Ablet.Layers
{
    [AbletLayer]
    class DefaultRendererSettingsPass : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.DefaultRendererSettings";
        public string DisplayName => "ATiV: Default Renderer Settings";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<MaterializingPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                context.RootTransform.Observe(DefaultRendererSettingsProcessor.Process);
            });
        }
    }
}
