using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.VRChat.Converters;
using VRC.SDK3.Avatars.Components;

namespace Silksprite.AvatarTinkerVista.Ablet.VRChat.Layers
{
    [AbletLayer]
    class OverwriteVRChatBlinkLayer : IAbletLayer
    {
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.OverwriteVRChatBlink";
        string IAbletDefinition.DisplayName => "ATiV: Overwrite VRChat Blink";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<GeneratingPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
            {
                if (context.CurrentRootTransform.TryGetComponent<VRCAvatarDescriptor>(out var avatarDescriptor))
                {
                    foreach (var ativ in context.CurrentRootTransform.GetComponentsInChildren<AtivOverwriteBlink>())
                    {
                        new BlinkConverterForVRChat().ToPlatform(ativ, avatarDescriptor);
                    }
                }
            });
        }
    }
}
