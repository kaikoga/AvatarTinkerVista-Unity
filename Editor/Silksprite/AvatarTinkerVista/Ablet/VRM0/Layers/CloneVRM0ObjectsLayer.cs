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
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.CloneVRM0Objects";
        string IAbletDefinition.DisplayName => "ATiV: Clone VRM0 Objects";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<ImportingPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
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
