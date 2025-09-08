using System.IO;
using UniGLTF;
using UnityEngine;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1
{
    public static class VRM1FileExporter
    {
        public static void ExportVRM1File(GameObject avatarRootObject, string fileName)
        {
            if (!avatarRootObject.TryGetComponent<Vrm10Instance>(out var vrmInstance)) return;

            if (string.IsNullOrWhiteSpace(fileName)) fileName = avatarRootObject.name;
            
            var settings = ScriptableObject.CreateInstance<VRM10ExportSettings>();
            using var arrayManager = new NativeArrayManager();

            try
            {
                // ヒエラルキーからジオメトリーを収集
                var converter = new ModelExporter();
                var model = converter.Export(settings.MeshExportSettings, arrayManager, avatarRootObject);

                // 右手系に変換
                model.ConvertCoordinate(VrmLib.Coordinates.Vrm1);

                var exporter10 = new Vrm10Exporter(
                    settings.MeshExportSettings,
                    textureSerializer: new EditorTextureSerializer()
                );
                var option = new VrmLib.ExportArgs
                {
                    sparse = settings.MorphTargetUseSparse,
                };
                exporter10.Export(avatarRootObject, model, converter, option, vrmInstance.Vrm.Meta);

                var bytes = exporter10.Storage.ToGlbBytes();
                Directory.CreateDirectory("ATiV_VRM1~");
                File.WriteAllBytes($"ATiV_VRM1~/{fileName}.vrm", bytes);
            }
            finally
            {
                Object.DestroyImmediate(settings);
            }
        }
    }}
