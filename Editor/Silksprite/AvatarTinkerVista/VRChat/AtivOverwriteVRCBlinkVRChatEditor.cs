using Silksprite.AvatarTinkerVista.VRChat.Converters;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public static class AtivOverwriteVRCBlinkVRChatEditor
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            AtivOverwriteVRCBlinkEditor.PlatformUI += OnPlatformUI;
        }

        static void OnPlatformUI(AtivOverwriteVRCBlink overwriteBlink, Transform avatarRoot)
        {
            if (avatarRoot.TryGetComponent(out VRCAvatarDescriptor avatarDescriptor))
            {
                LEditorGUILayout.HelpBox(Loc("AtivOverwriteVRCBlink::OutcomeVRChat.").Format(new Substitution
                {
                    ["outcome"] = TrEnum(new VRCBlinkConverterForVRChat().GetOption(overwriteBlink).blinkStyle)
                }), MessageType.Info);
                if (LGUILayout.Button(Loc("AtivOverwriteVRCBlink::ExtractFromVRChat")))
                {
                    new VRCBlinkConverterForVRChat().ToAtiv(overwriteBlink, avatarDescriptor);
                }
            }
        }
    }
}
