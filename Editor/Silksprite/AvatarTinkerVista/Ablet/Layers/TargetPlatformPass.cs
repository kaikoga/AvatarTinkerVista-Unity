using System.Linq;
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
        public string Id => "Silksprite.AvatarTinkerVista.TargetPlatform";
        public string DisplayName => "ATiV: Target Platform";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<PruningPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;
            
            return AbletBuildProcedure.Create((IBuildContext context) =>
            {
                AtivTargetPlatformBase.ApplyToAvatarRoot(context.CurrentRootTransform, true);
            });
        }
    }
}
