using System;
using System.Collections.Generic;
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
    class VRM1Platform : INDMFPlatformProvider
    {
        public static readonly INDMFPlatformProvider Instance = new VRM1Platform();

        string INDMFPlatformProvider.QualifiedName => "net.kaikoga.ativ.univrm.vrm1"; 
        string INDMFPlatformProvider.DisplayName => "VRM 1.0 (ATiV)";

        Type INDMFPlatformProvider.AvatarRootComponentType => typeof(Vrm10Instance);

        BuildUIElement INDMFPlatformProvider.CreateBuildUI() => new VRM1BuildUIElement();

        bool INDMFPlatformProvider.HasNativeConfigData => true;

        bool INDMFPlatformProvider.CanInitFromCommonAvatarInfo(GameObject avatarRoot, CommonAvatarInfo info) => true;

        CommonAvatarInfo INDMFPlatformProvider.ExtractCommonAvatarInfo(GameObject avatarRoot)
        {
            var info = new CommonAvatarInfo();
            
            if (avatarRoot.TryGetComponent<Vrm10Instance>(out var vrm10Instance))
            {
                var rootBone = avatarRoot.transform;
                Transform headBone = null;
                if (avatarRoot.TryGetComponent<Humanoid>(out var humanoid))
                {
                    headBone = humanoid.Head;
                }
                if (avatarRoot.TryGetComponent<Animator>(out var animator) && animator.isHuman)
                {
                    headBone ??= animator.GetBoneTransform(HumanBodyBones.Head);
                }
                headBone ??= rootBone;
                info.EyePosition = rootBone.InverseTransformPoint(headBone.TransformPoint(vrm10Instance.Vrm.LookAt.OffsetFromHead));
            }

            return info;
        }

        void INDMFPlatformProvider.InitFromCommonAvatarInfo(GameObject avatarRoot, CommonAvatarInfo info)
        {
            DoInitFromCommonAvatarInfo(avatarRoot, info, false);
        }

        void INDMFPlatformProvider.InitBuildFromCommonAvatarInfo(BuildContext context, CommonAvatarInfo info)
        {
            DoInitFromCommonAvatarInfo(context.AvatarRootObject, info, true);
        }

        static void DoInitFromCommonAvatarInfo(GameObject avatarRoot, CommonAvatarInfo info, bool createAssets)
        {
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
                    vrm10Instance.Vrm.Meta.Name = AtivRuntimeUtil.GuessOriginalAvatarName(avatarRoot.name);
                    vrm10Instance.Vrm.Meta.Authors = new List<string> { AtivRuntimeUtil.VrmAuthor };
                    vrm10Instance.Vrm.Meta.Version = AtivRuntimeUtil.VrmVersion;
                }
                if (info.EyePosition is { } eyePosition)
                {
                    var rootBone = avatarRoot.transform;
                    var headBone = humanoid.Head ?? rootBone;
                    vrm10Instance.Vrm.LookAt.OffsetFromHead = headBone.InverseTransformPoint(rootBone.TransformPoint(eyePosition));
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

                var clone = Object.Instantiate(AvatarRoot);
                try
                {
                    AvatarProcessor.ProcessAvatar(clone, VRM1Platform.Instance);
                    VRM1FileExporter.ExportVRM1File(clone.GetComponent<Vrm10Instance>(), filePath);
                    AtivEditorUtil.OpenInExplorer(directory);
                }
                finally
                {
                    Object.DestroyImmediate(clone);
                }
            }
        }
    }
}
