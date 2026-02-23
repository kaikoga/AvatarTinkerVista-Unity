using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.AvatarTinkerVista.Common.Utils;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivDefaultRendererSettings))]
    class AtivDefaultRendererSettingsEditor : AtivEditorBase
    {
        LocalizedProperty _preferParentSettings;
        LocalizedProperty _overwriteProbeAnchor;
        LocalizedProperty _overwriteRootBone;
        LocalizedProperty _overwriteBounds;

        void OnEnable()
        {
            _preferParentSettings = Lop(nameof(AtivDefaultRendererSettings.preferParentSettings), Loc("AtivDefaultRendererSettings::preferParentSettings"));
            _overwriteProbeAnchor = Lop(nameof(AtivDefaultRendererSettings.overwriteProbeAnchor), Loc("AtivDefaultRendererSettings::overwriteProbeAnchor"));
            _overwriteRootBone = Lop(nameof(AtivDefaultRendererSettings.overwriteRootBone), Loc("AtivDefaultRendererSettings::overwriteRootBone"));
            _overwriteBounds = Lop(nameof(AtivDefaultRendererSettings.overwriteBounds), Loc("AtivDefaultRendererSettings::overwriteBounds"));
        }

        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.Prop(_preferParentSettings);
            LEditorGUILayout.Prop(_overwriteProbeAnchor);
            LEditorGUILayout.Prop(_overwriteRootBone);
            LEditorGUILayout.Prop(_overwriteBounds);
            serializedObject.ApplyModifiedProperties();
            if (LGUILayout.Button(Loc("AtivDefaultRendererSettings::Setup")))
            {
                Setup();
            }
        }

        void Setup()
        {
            foreach (var ativ in targets.OfType<AtivDefaultRendererSettings>())
            {
                var avatarRoot = AtivRuntimeUtil.FindAvatarInParents(ativ.transform);
                var hips = avatarRoot.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips)
                           ?? avatarRoot;
                ativ.overwriteProbeAnchor = new OverwriteAvatarRelativeTransform
                {
                    willOverwrite = true,
                    value = AvatarRelativeTransform.OfAvatar(avatarRoot, hips)
                };
                ativ.overwriteRootBone = new OverwriteAvatarRelativeTransform
                {
                    willOverwrite = true,
                    value = AvatarRelativeTransform.OfAvatar(avatarRoot, hips)
                };
                ativ.overwriteBounds = new OverwriteBounds
                {
                    willOverwrite = true,
                    value = new Bounds
                    {
                        center = Vector3.zero,
                        extents = Vector3.one,
                    }
                };
            }
        }
    }
}
