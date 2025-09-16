using System;
using System.IO;
using nadena.dev.ndmf;
using nadena.dev.ndmf.platform;
using Silksprite.AvatarTinkerVista.Utils;
using Silksprite.AvatarTinkerVista.VRM1;
using UniHumanoid;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UniVRM10;
using Object = UnityEngine.Object;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [NDMFPlatformProvider]
    class VRM1PlatformProvider : INDMFPlatformProvider
    {
        public static readonly INDMFPlatformProvider Instance = new VRM1PlatformProvider();

        string INDMFPlatformProvider.QualifiedName => "net.kaikoga.ativ.univrm.vrm1"; 
        string INDMFPlatformProvider.DisplayName => "VRM 1.0 (ATiV)";

        Type INDMFPlatformProvider.AvatarRootComponentType => typeof(Vrm10Instance);
        
        public BuildUIElement CreateBuildUI() => new VRM1BuildUIElement();

        public bool HasNativeConfigData => true;

        public bool CanInitFromCommonAvatarInfo(GameObject avatarRoot, CommonAvatarInfo info) => true;

        public void InitFromCommonAvatarInfo(GameObject avatarRoot, CommonAvatarInfo info)
        {
            DoInitFromCommonAvatarInfo(avatarRoot, info, false);
        }

        public void InitBuildFromCommonAvatarInfo(BuildContext context, CommonAvatarInfo info)
        {
            DoInitFromCommonAvatarInfo(context.AvatarRootObject, info, true);
        }

        static void DoInitFromCommonAvatarInfo(GameObject avatarRoot, CommonAvatarInfo info, bool createAssets)
        {
            // note: CommonAvatarInfo is not extracted from VRM because I was too lazy to do so
            if (!avatarRoot.TryGetComponent<Vrm10Instance>(out var vrm10Instance))
            {
                vrm10Instance = avatarRoot.AddComponent<Vrm10Instance>();
            }
            if (!avatarRoot.TryGetComponent<Humanoid>(out var humanoid))
            {
                humanoid = avatarRoot.AddComponent<Humanoid>();
                humanoid.AssignBonesFromAnimator();
            }

            if (createAssets)
            {
                if (!vrm10Instance.Vrm)
                {
                    vrm10Instance.Vrm = ScriptableObject.CreateInstance<VRM10Object>();
                }
                if (info.EyePosition is { } eyePosition)
                {
                    var rootBone = avatarRoot.transform;
                    var headBone = humanoid.Head ?? rootBone;
                    vrm10Instance.Vrm.LookAt.OffsetFromHead = headBone.InverseTransformPoint(rootBone.TransformPoint(eyePosition));
                    ;
                }
            }

        }
    }

    class VRM1BuildUIElement : BuildUIElement
    {
        public VRM1BuildUIElement()
        {
            var buildButton = new Button
            {
                text = "Export VRM1.0 Avatar with NDMF"
            };
            buildButton.clicked += OnBuild;
            hierarchy.Add(buildButton);
        }
        
        void OnBuild()
        {
            const string lastDirectoryPrefsKey = "net.kaikoga.ativ.VRM1.LastDirectory";
            var lastDirectory = PlayerPrefs.GetString(lastDirectoryPrefsKey, "");

            var filePath = EditorUtility.SaveFilePanel(
                "Save VRM1.0 File",
                lastDirectory,
                $"{AvatarRoot.name}.vrm",
                ".vrm"); 
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                var directory = Path.GetDirectoryName(filePath) ?? "";
                PlayerPrefs.SetString(lastDirectoryPrefsKey, directory);

                var avatar = Object.Instantiate(AvatarRoot).GetComponent<Vrm10Instance>();
                try
                {
                    AvatarProcessor.ProcessAvatar(avatar.gameObject, VRM1PlatformProvider.Instance);
                    VRM1FileExporter.ExportVRM1File(avatar, filePath);
                    AtivEditorUtil.OpenInExplorer(directory);
                }
                finally
                {
                    Object.DestroyImmediate(avatar.gameObject);
                }
            }
        }
    }
}
