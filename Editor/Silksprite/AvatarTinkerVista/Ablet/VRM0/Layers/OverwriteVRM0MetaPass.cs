using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.VRM0.AdLib.Processors;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM0.Layers
{
    [AbletLayer]
    class OverwriteVRM0MetaPass : IAbletLayer
    {
        public string Id => "net.kaikoga.ativ.vrm0.overwrite-vrm0-meta";
        public string DisplayName => "ATiV: Overwrite VRM0 Meta";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<GeneratingPhase>();
        }
        public IAbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return new AbletBuildProcedure(context =>
            {
                if (context.CurrentRootObject.TryGetComponent<VRMMeta>(out var vrmMeta))
                {
                    OverwriteVRM0MetaProcessor.Process(vrmMeta);
                }
            });
        }
    }
}
