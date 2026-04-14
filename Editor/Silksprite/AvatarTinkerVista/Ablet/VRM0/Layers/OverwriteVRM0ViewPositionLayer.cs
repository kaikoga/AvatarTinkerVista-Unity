using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.VRM0.Converters;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM0.Layers
{
    [AbletLayer]
    class OverwriteVRM0ViewPositionLayer : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.OverwriteVRM0ViewPosition";
        public string DisplayName => "ATiV: Overwrite VRM0 View Position";
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
                    foreach (var ativ in context.CurrentRootTransform.GetComponentsInChildren<AtivOverwriteViewPosition>())
                    {
                        new ViewPositionConverterForVRM0().ToPlatform(ativ, vrmMeta);
                    }
                }
            });
        }
    }
}
