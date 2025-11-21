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
        public string Id => "net.kaikoga.ativ.vrchat.reduce-vrc-physbones";
        public string DisplayName => "ATiV: Reduce VRC PhysBones";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<TransformingPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create((IBuildContext context) =>
            {
                if (context.CurrentRootTransform.TryGetComponent<VRCAvatarDescriptor>(out var avatarDescriptor))
                {
                    ReduceVRCPhysBonesProcessor.Process(avatarDescriptor);
                }
            });
        }
    }
}
