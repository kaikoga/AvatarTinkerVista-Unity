using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.VRChat.Converter;
using Silksprite.AvatarTinkerVista.VRChat.Converters;
using VRC.SDK3.Avatars.Components;

namespace Silksprite.AvatarTinkerVista.Ablet.VRChat.Layers
{
    [AbletLayer]
    class OverwriteVRChatViewPositionLayer : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.OverwriteVRChatViewPosition";
        public string DisplayName => "ATiV: Overwrite VRChat View Position";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<GeneratingPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                if (context.CurrentRootTransform.TryGetComponent<VRCAvatarDescriptor>(out var avatarDescriptor))
                {
                    foreach (var ativ in context.CurrentRootTransform.GetComponentsInChildren<AtivOverwriteViewPosition>())
                    {
                        new ViewPositionConverterForVRChat().ToPlatform(ativ, avatarDescriptor);
                    }
                }
            });
        }
    }
}
