using System;
using System.IO;
using nadena.dev.ndmf;
using nadena.dev.ndmf.platform;
using Silksprite.AvatarTinkerVista.Utils;
using Silksprite.AvatarTinkerVista.VRM1;
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
