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
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.TargetInputPlatform";
        string IAbletDefinition.DisplayName => "ATiV: Target Input Platform";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<ImportingPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;
            
            return AbletBuildProcedure.Create(context =>
            {
                AtivTargetPlatformBase.ApplyToAvatarRoot(context.CurrentRootTransform, false);
            });
        }
    }
}
