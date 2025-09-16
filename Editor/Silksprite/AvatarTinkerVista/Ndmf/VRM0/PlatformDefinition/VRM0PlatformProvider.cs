using System;
using System.Diagnostics;
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
    class VRM0PlatformProvider : INDMFPlatformProvider
    {
        public static readonly INDMFPlatformProvider Instance = new VRM0PlatformProvider();

        string INDMFPlatformProvider.QualifiedName => "net.kaikoga.ativ.univrm.vrm0"; 
        string INDMFPlatformProvider.DisplayName => "VRM 0.x (ATiV)";

        Type INDMFPlatformProvider.AvatarRootComponentType => typeof(VRMMeta);

        public BuildUIElement CreateBuildUI() => new VRM0BuildUIElement();
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

                var avatar = Object.Instantiate(AvatarRoot).GetComponent<VRMMeta>();
                try
                {
                    AvatarProcessor.ProcessAvatar(avatar.gameObject, VRM0PlatformProvider.Instance);
                    VRM0FileExporter.ExportVRM0File(avatar, filePath);
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
