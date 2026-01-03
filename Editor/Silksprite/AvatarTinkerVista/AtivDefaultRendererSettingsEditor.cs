using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Utils;
using Silksprite.AvatarTinkerVista.DataObjects;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivDefaultRendererSettings))]
    class AtivDefaultRendererSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Setup"))
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
                    value = new AvatarRelativeTransform
                    {
                        relativePath = AvatarRelativeReference<Transform>.RelativePath(avatarRoot, hips)
                    }
                };
                ativ.overwriteRootBone = new OverwriteAvatarRelativeTransform
                {
                    willOverwrite = true,
                    value = new AvatarRelativeTransform
                    {
                        relativePath = AvatarRelativeReference<Transform>.RelativePath(avatarRoot, hips)
                    }
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
