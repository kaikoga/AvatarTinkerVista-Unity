using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.Common.Base;

namespace Silksprite.AvatarTinkerVista.Ablet.Layers
{
    [AbletLayer]
    class TargetPlatformLayer : IAbletLayer
    {
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.TargetPlatform";
        string IAbletDefinition.DisplayName => "ATiV: Target Platform";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<PruningPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;
            
            return AbletBuildProcedure.Create(context =>
            {
                AtivTargetPlatformBase.ApplyToAvatarRoot(context.CurrentRootTransform, true);
            });
        }
    }
}
