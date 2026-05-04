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
    class OverwriteVRM1MetaLayer : IAbletLayer
    {
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.OverwriteVRM1Meta";
        string IAbletDefinition.DisplayName => "ATiV: Overwrite VRM1 Meta";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<GeneratingPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                if (context.CurrentRootObject.TryGetComponent<Vrm10Instance>(out var vrmInstance))
                {
                    OverwriteVRM1MetaProcessor.Process(vrmInstance);
                }
            });
        }

    }
}
