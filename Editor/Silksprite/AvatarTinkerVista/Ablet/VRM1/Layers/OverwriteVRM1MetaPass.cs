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
    class OverwriteVRM1MetaPass : IAbletLayer
    {
        public string Id => "net.kaikoga.ativ.vrm1.overwrite-vrm1-meta";
        public string DisplayName => "ATiV: Overwrite VRM1 Meta";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<GeneratingPhase>();
        }
        public IAbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return new AbletBuildProcedure(context =>
            {
                if (context.CurrentRootObject.TryGetComponent<Vrm10Instance>(out var vrmInstance))
                {
                    OverwriteVRM1MetaProcessor.Process(vrmInstance);
                }
            });
        }

    }
}
