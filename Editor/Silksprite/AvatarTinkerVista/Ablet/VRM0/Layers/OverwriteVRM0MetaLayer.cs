using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.AdLib.VRM0.Processors;
using Silksprite.AvatarTinkerVista.VRM0.Processors;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM0.Layers
{
    [AbletLayer]
    class OverwriteVRM0MetaLayer : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.OverwriteVRM0Meta";
        public string DisplayName => "ATiV: Overwrite VRM0 Meta";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<GeneratingPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                if (context.CurrentRootObject.TryGetComponent<VRMMeta>(out var vrmMeta))
                {
                    OverwriteVRM0MetaProcessor.Process(vrmMeta);
                }
            });
        }
    }
}
