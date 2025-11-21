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
    class MergeVRM1FirstPersonPass : IAbletLayer
    {
        public string Id => "net.kaikoga.ativ.vrm1.merge-vrm1-first-person";
        public string DisplayName => "ATiV: Merge VRM1 FirstPerson";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<TransformingPhase>();
        }
        public IAbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return new AbletBuildProcedure(context =>
            {
                if (context.CurrentRootObject.TryGetComponent<Vrm10Instance>(out var vrmInstance))
                {
                    MergeVRM1FirstPersonProcessor.Process(vrmInstance);
                }
            });
        }
    }
}
