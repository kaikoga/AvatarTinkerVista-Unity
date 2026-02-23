using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Ablet.Querying;
using Silksprite.AvatarTinkerVista.Common.Wear;
using Silksprite.AvatarTinkerVista.Utils;

namespace Silksprite.AvatarTinkerVista.Ablet.Layers
{
    [AbletLayer]
    class SimpleWearPass : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.SimpleWear";
        public string DisplayName => "ATiV: Simple Wear";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<TransformingPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                context.RootObject.GetComponentsInChildren<AtivSimpleWear>().Observe(ativ =>
                {
                    SimpleWearSetup.SetupAvatarIfNeeded(ativ);
                    var sourceTree = ativ.ResolveModule();
                    var targetTree = ativ.ResolveAvatar();
                    WearProcessor.Wear(sourceTree, targetTree);
                });
            });
        }
    }
}
