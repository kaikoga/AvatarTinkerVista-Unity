using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Ndmf.VRM0.Contexts;
using Silksprite.AvatarTinkerVista.VRM1;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class ExportVRM1Pass : Pass<ExportVRM1Pass>
    {
        protected override void Execute(BuildContext context)
        {
            var exportVrm = context.AvatarRootObject.GetComponentInChildren<AtivExportVRM>();
            if (!exportVrm) return;

            if (context.Extension<VRM1BytesContext>().TryGetBytes(out var bytes))
            {
                VRM1FileExporter.ExportVRM1File(bytes, string.IsNullOrEmpty(exportVrm.fileName) ? context.AvatarRootObject.name : exportVrm.fileName);
            }
        }
    }
}
