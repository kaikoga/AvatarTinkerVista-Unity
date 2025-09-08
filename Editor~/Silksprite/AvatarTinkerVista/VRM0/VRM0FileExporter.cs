using System.IO;
using UnityEngine;
using VRM;

namespace Silksprite.AvatarTinkerVista.VRM0
{
    public static class VRM0FileExporter
    {
        public static void ExportVRM0File(GameObject avatarRootObject, string fileName)
        {
            if (!avatarRootObject.TryGetComponent<VRMMeta>(out var vrmMeta)) return;

            if (string.IsNullOrWhiteSpace(fileName)) fileName = avatarRootObject.name;

            var settings = ScriptableObject.CreateInstance<VRMExportSettings>();
            try
            {
                var bytes = VRMEditorExporter.Export(avatarRootObject, vrmMeta.Meta, settings);
                Directory.CreateDirectory("ATiV_VRM0~");
                File.WriteAllBytes($"ATiV_VRM0~/{fileName}.vrm", bytes);
            }
            finally
            {
                Object.DestroyImmediate(settings);
            }
        }
    }
}
