using System.IO;
using UnityEngine;
using VRM;

namespace Silksprite.AvatarTinkerVista.VRM0
{
    public static class VRM0FileExporter
    {
        public static void ExportVRM0File(VRMMeta vrmMeta, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = vrmMeta.gameObject.name;
            }

            ExportVRM0File(VRM0Exporter.ExportVRM0(vrmMeta), fileName);
        }

        public static void ExportVRM0File(byte[] bytes, string fileName)
        {
            Directory.CreateDirectory("ATiV_VRM0~");
            File.WriteAllBytes($"ATiV_VRM0~/{fileName}.vrm", bytes);
        }
    }
}
