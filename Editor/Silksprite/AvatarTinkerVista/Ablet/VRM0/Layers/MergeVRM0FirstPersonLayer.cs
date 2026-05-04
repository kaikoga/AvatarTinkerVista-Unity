using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.VRM0.Processors;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ablet.VRM0.Layers
{
    [AbletLayer]
    class MergeVRM0FirstPersonLayer : IAbletLayer
    {
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.MergeVRM0FirstPerson";
        string IAbletDefinition.DisplayName => "ATiV: Merge VRM0 FirstPerson";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<TransformingPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                if (context.CurrentRootObject.TryGetComponent<VRMFirstPerson>(out var vrmFirstPerson))
                {
                    MergeVRM0FirstPersonProcessor.Process(vrmFirstPerson);
                }
            });
        }
    }
}
