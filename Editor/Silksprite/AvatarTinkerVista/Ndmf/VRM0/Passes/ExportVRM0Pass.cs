using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Ndmf.VRM0.Contexts;
using Silksprite.AvatarTinkerVista.VRM0;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class ExportVRM0Pass : Pass<ExportVRM0Pass>
    {
        protected override void Execute(BuildContext context)
        {
            var exportVrm = context.AvatarRootObject.GetComponentInChildren<AtivExportVRM>();
            if (!exportVrm) return;

            if (context.Extension<VRM0BytesContext>().TryGetBytes(out var bytes))
            {
                VRM0FileExporter.ExportVRM0File(bytes, string.IsNullOrEmpty(exportVrm.fileName) ? context.AvatarRootObject.name : exportVrm.fileName);
            }
        }
    }
}
