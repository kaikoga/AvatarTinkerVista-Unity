using Silksprite.AvatarTinkerVista.VRM0.Converters;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using VRM;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista.VRM0
{
    public static class AtivOverwriteViewPositionVRM0Editor
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            AtivOverwriteViewPositionEditor.PlatformUI += OnPlatformUI;
        }

        static void OnPlatformUI(AtivOverwriteViewPosition overwriteViewPosition, Transform avatarRoot)
        {
            if (avatarRoot.TryGetComponent(out VRMMeta avatarDescriptor))
            {
                LEditorGUILayout.HelpBox(Loc("AtivOverwriteViewPosition::OutcomeVRM0.").Format(new Substitution
                {
                    ["outcome"] = TrEnum(new ViewPositionConverterForVRM0().GetOption(overwriteViewPosition).viewPositionStyle)
                }), MessageType.Info);
                if (LGUILayout.Button(Loc("AtivOverwriteViewPosition::ExtractFromVRM0")))
                {
                    new ViewPositionConverterForVRM0().ToAtiv(overwriteViewPosition, avatarDescriptor);
                }
            }
        }
    }
}
