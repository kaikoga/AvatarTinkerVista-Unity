using Silksprite.AvatarTinkerVista.VRChat.Converters;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public static class AtivOverwriteVRCVisemesVRChatEditor
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            AtivOverwriteVRCVisemesEditor.PlatformUI += OnPlatformUI;
        }

        static void OnPlatformUI(AtivOverwriteVRCVisemes overwriteVisemes, Transform avatarRoot)
        {
            if (avatarRoot.TryGetComponent(out VRCAvatarDescriptor avatarDescriptor))
            {
                LEditorGUILayout.HelpBox(Loc("AtivOverwriteVRCVisemes::OutcomeVRChat.").Format(new Substitution
                {
                    ["outcome"] = TrEnum(new VRCVisemesConverterForVRChat().GetOption(overwriteVisemes).visemeStyle)
                }), MessageType.Info);
                if (LGUILayout.Button(Loc("AtivOverwriteVRCVisemes::ExtractFromVRChat")))
                {
                    new VRCVisemesConverterForVRChat().ToAtiv(overwriteVisemes, avatarDescriptor);
                }
            }
        }
    }
}
