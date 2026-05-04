using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.VRChat.Processors;
using VRC.SDK3.Avatars.Components;

namespace Silksprite.AvatarTinkerVista.Ablet.VRChat.Layers
{
    [AbletLayer]
    class ReduceVRCPhysBonesPass : IAbletLayer
    {
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.ReduceVRCPhysBones";
        string IAbletDefinition.DisplayName => "ATiV: Reduce VRC PhysBones";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<TransformingPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                if (context.CurrentRootTransform.TryGetComponent<VRCAvatarDescriptor>(out var avatarDescriptor))
                {
                    ReduceVRCPhysBonesProcessor.Process(avatarDescriptor);
                }
            });
        }
    }
}
