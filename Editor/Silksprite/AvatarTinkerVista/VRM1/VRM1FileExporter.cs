using System.IO;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1
{
    public static class VRM1FileExporter
    {
        public static void ExportVRM1File(Vrm10Instance vrm10Instance, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = vrm10Instance.gameObject.name;
            }

            ExportVRM1File(VRM1Exporter.ExportVRM1(vrm10Instance), fileName);
        }
        
        public static void ExportVRM1File(byte[] bytes, string fileName)
        {
            Directory.CreateDirectory("ATiV_VRM1~");
            File.WriteAllBytes($"ATiV_VRM1~/{fileName}.vrm", bytes);
        }
    }
}
