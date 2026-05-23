using Silksprite.AvatarTinkerVista.VRChat.Converters;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public static class AtivOverwriteBlinkVRChatEditor
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            AtivOverwriteBlinkEditor.PlatformUI += OnPlatformUI;
        }

        static void OnPlatformUI(AtivOverwriteBlink overwriteBlink, Transform avatarRoot)
        {
            if (avatarRoot.TryGetComponent(out VRCAvatarDescriptor avatarDescriptor))
            {
                LEditorGUILayout.HelpBox(Loc("AtivOverwriteBlink::OutcomeVRChat.").Format(new Substitution
                {
                    ["outcome"] = TrEnum(new BlinkConverterForVRChat().GetOption(overwriteBlink).blinkStyle)
                }), MessageType.Info);
                if (LGUILayout.Button(Loc("AtivOverwriteBlink::ExtractFromVRChat")))
                {
                    new BlinkConverterForVRChat().ToAtiv(overwriteBlink, avatarDescriptor);
                }
            }
        }
    }
}
