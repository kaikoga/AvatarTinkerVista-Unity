using System;
using System.IO;
using nadena.dev.ndmf;
using nadena.dev.ndmf.platform;
using Silksprite.AvatarTinkerVista.Utils;
using Silksprite.AvatarTinkerVista.VRM0;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VRM;
using Object = UnityEngine.Object;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [NDMFPlatformProvider]
    class VRM0Platform : INDMFPlatformProvider
    {
        public static readonly INDMFPlatformProvider Instance = new VRM0Platform();

        string INDMFPlatformProvider.QualifiedName => "net.kaikoga.ativ.univrm.vrm0"; 
        string INDMFPlatformProvider.DisplayName => "VRM 0.x (ATiV)";

        Type INDMFPlatformProvider.AvatarRootComponentType => typeof(VRMMeta);

        BuildUIElement INDMFPlatformProvider.CreateBuildUI() => new VRM0BuildUIElement();

        bool INDMFPlatformProvider.HasNativeConfigData => true;

        bool INDMFPlatformProvider.CanInitFromCommonAvatarInfo(GameObject avatarRoot, CommonAvatarInfo info) => true;

        CommonAvatarInfo INDMFPlatformProvider.ExtractCommonAvatarInfo(GameObject avatarRoot)
        {
            var info = new CommonAvatarInfo();
            if (avatarRoot.TryGetComponent<VRMMeta>(out _)
                && avatarRoot.TryGetComponent<VRMFirstPerson>(out var vrmFirstPerson))
            {
                var rootBone = avatarRoot.transform;
                var headBone = vrmFirstPerson.FirstPersonBone ?? rootBone;
                info.EyePosition = rootBone.InverseTransformPoint(headBone.TransformPoint(vrmFirstPerson.FirstPersonOffset));
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
            if (!avatarRoot.TryGetComponent<VRMMeta>(out var vrmMeta))
            {
                vrmMeta = avatarRoot.AddComponent<VRMMeta>();
            }
            if (!avatarRoot.TryGetComponent<VRMBlendShapeProxy>(out var vrmBlendShapeProxy))
            {
                vrmBlendShapeProxy = avatarRoot.AddComponent<VRMBlendShapeProxy>();
            }
            if (!avatarRoot.TryGetComponent<VRMFirstPerson>(out var vrmFirstPerson))
            {
                vrmFirstPerson = avatarRoot.AddComponent<VRMFirstPerson>();
            }

            if (createAssets)
            {
                if (!vrmMeta.Meta)
                {
                    vrmMeta.Meta = ScriptableObject.CreateInstance<VRMMetaObject>();
                    vrmMeta.Meta.Title = AtivRuntimeUtil.GuessOriginalAvatarName(avatarRoot.name);
                    vrmMeta.Meta.Author = AtivRuntimeUtil.VrmAuthor;
                    vrmMeta.Meta.Version = AtivRuntimeUtil.VrmVersion;
                }
                if (!vrmBlendShapeProxy.BlendShapeAvatar)
                {
                    vrmBlendShapeProxy.BlendShapeAvatar = ScriptableObject.CreateInstance<BlendShapeAvatar>();
                }
            }

            if (info.EyePosition is { } eyePosition)
            {
                vrmFirstPerson.SetDefault();
                var rootBone = avatarRoot.transform;
                // question: should we force Head bone if eye position is provided?
                var headBone = vrmFirstPerson.FirstPersonBone ?? rootBone;
                vrmFirstPerson.FirstPersonOffset = headBone.InverseTransformPoint(rootBone.TransformPoint(eyePosition));
            }
        }
    }

    class VRM0BuildUIElement : BuildUIElement
    {
        public VRM0BuildUIElement()
        {
            var buildButton = new Button
            {
                text = "Export VRM0.x Avatar with NDMF"
            };
            buildButton.clicked += OnBuild;
            hierarchy.Add(buildButton);
        }
        
        void OnBuild()
        {
            const string lastDirectoryPrefsKey = "net.kaikoga.ativ.VRM0.LastDirectory";
            var lastDirectory = PlayerPrefs.GetString(lastDirectoryPrefsKey, "");

            var filePath = EditorUtility.SaveFilePanel(
                "Save VRM0.x File",
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
                    AvatarProcessor.ProcessAvatar(clone, VRM0Platform.Instance);
                    VRM0FileExporter.ExportVRM0File(clone.GetComponent<VRMMeta>(), filePath);
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
