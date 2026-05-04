using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.VRM1.Converters;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM1.Layers
{
    [AbletLayer]
    class OverwriteVRM1ViewPositionLayer : IAbletLayer
    {
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.OverwriteVRM1ViewPosition";
        string IAbletDefinition.DisplayName => "ATiV: Overwrite VRM1 View Position";
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
                    foreach (var ativ in context.CurrentRootTransform.GetComponentsInChildren<AtivOverwriteViewPosition>())
                    {
                        new ViewPositionConverterForVRM1().ToPlatform(ativ, vrmInstance);
                    }
                }
            });
        }
    }
}
