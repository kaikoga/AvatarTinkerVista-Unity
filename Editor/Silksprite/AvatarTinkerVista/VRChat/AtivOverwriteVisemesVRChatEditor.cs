using Silksprite.AvatarTinkerVista.VRChat.Converters;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public static class AtivOverwriteVisemesVRChatEditor
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            AtivOverwriteVisemesEditor.PlatformUI += OnPlatformUI;
        }

        static void OnPlatformUI(AtivOverwriteVisemes overwriteVisemes, Transform avatarRoot)
        {
            if (avatarRoot.TryGetComponent(out VRCAvatarDescriptor avatarDescriptor))
            {
                LEditorGUILayout.HelpBox(Loc("AtivOverwriteVRCVisemes::OutcomeVRChat.").Format(new Substitution
                {
                    ["outcome"] = TrEnum(new VisemesConverterForVRChat().GetOption(overwriteVisemes).visemeStyle)
                }), MessageType.Info);
                if (LGUILayout.Button(Loc("AtivOverwriteVRCVisemes::ExtractFromVRChat")))
                {
                    new VisemesConverterForVRChat().ToAtiv(overwriteVisemes, avatarDescriptor);
                }
            }
        }
    }
}
