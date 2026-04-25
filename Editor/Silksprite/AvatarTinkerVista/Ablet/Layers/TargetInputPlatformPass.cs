using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.Common.Base;

namespace Silksprite.AvatarTinkerVista.Ablet.Layers
{
    [AbletLayer]
    class TargetInputPlatformLayer : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.TargetInputPlatform";
        public string DisplayName => "ATiV: Target Input Platform";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<ImportingPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;
            
            return AbletBuildProcedure.Create((IBuildContext context) =>
            {
                AtivTargetPlatformBase.ApplyToAvatarRoot(context.CurrentRootTransform, false);
            });
        }
    }
}
