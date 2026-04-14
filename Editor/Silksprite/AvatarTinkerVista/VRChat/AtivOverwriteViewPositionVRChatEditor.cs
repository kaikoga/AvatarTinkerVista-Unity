using Silksprite.AvatarTinkerVista.VRChat.Converters;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista.VRChat
{
    public static class AtivOverwriteViewPositionVRChatEditor
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            AtivOverwriteViewPositionEditor.PlatformUI += OnPlatformUI;
        }

        static void OnPlatformUI(AtivOverwriteViewPosition overwriteViewPosition, Transform avatarRoot)
        {
            if (avatarRoot.TryGetComponent(out VRCAvatarDescriptor avatarDescriptor))
            {
                LEditorGUILayout.HelpBox(Loc("AtivOverwriteViewPosition::OutcomeVRChat."), MessageType.Info, new Substitution
                {
                    ["outcome"] = TrEnum(new ViewPositionConverterForVRChat().GetOption(overwriteViewPosition).viewPositionStyle)
                });
                if (LGUILayout.Button(Loc("AtivOverwriteViewPosition::ExtractFromVRChat")))
                {
                    new ViewPositionConverterForVRChat().ToAtiv(overwriteViewPosition, avatarDescriptor);
                }
            }
        }
    }
}
