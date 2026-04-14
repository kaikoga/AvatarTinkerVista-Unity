using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.AdLib.VRM0.Processors;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM0.Layers
{
    [AbletLayer]
    class CloneVRM0ObjectsLayer : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.CloneVRM0Objects";
        public string DisplayName => "ATiV: Clone VRM0 Objects";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<ImportingPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                if (context.CurrentRootObject.TryGetComponent<VRMMeta>(out var vrmMeta))
                {
                    CloneVRM0ObjectsProcessor.Process(vrmMeta);
                }
            });
        }
    }
}
