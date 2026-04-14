using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.AdLib.VRM1.Processors;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM1.Layers
{
    [AbletLayer]
    class CloneVRM1ObjectsLayer : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.CloneVRM1Objects";
        public string DisplayName => "ATiV: Clone VRM1 Objects";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<ImportingPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create((IBuildContext context) =>
            {
                if (context.CurrentRootObject.TryGetComponent<Vrm10Instance>(out var vrmInstance))
                {
                    CloneVRM1ObjectsProcessor.Process(vrmInstance);
                }
            });
        }

    }
}
