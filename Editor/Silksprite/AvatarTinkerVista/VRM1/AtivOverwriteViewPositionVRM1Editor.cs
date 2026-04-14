using Silksprite.AvatarTinkerVista.VRM1.Converters;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using UniVRM10;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista.VRM1
{
    public static class AtivOverwriteViewPositionVRM1Editor
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            AtivOverwriteViewPositionEditor.PlatformUI += OnPlatformUI;
        }

        static void OnPlatformUI(AtivOverwriteViewPosition overwriteViewPosition, Transform avatarRoot)
        {
            if (avatarRoot.TryGetComponent(out Vrm10Instance vrm10Instance))
            {
                LEditorGUILayout.HelpBox(Loc("AtivOverwriteViewPosition::OutcomeVRM1.").Format(new Substitution
                {
                    ["outcome"] = TrEnum(new ViewPositionConverterForVRM1().GetOption(overwriteViewPosition).viewPositionStyle)
                }), MessageType.Info);
                if (LGUILayout.Button(Loc("AtivOverwriteViewPosition::ExtractFromVRM1")))
                {
                    new ViewPositionConverterForVRM1().ToAtiv(overwriteViewPosition, vrm10Instance);
                }
            }
        }
    }
}
