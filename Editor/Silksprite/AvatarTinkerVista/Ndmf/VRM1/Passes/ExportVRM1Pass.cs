using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM1;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class ExportVRM1Pass : Pass<ExportVRM1Pass>
    {
        protected override void Execute(BuildContext context)
        {
            var exportVrm = context.AvatarRootObject.GetComponentInChildren<AtivExportVRM>();
            if (!exportVrm) return;

            VRM1FileExporter.ExportVRM1File(context.AvatarRootObject, exportVrm.fileName);
        }
    }
}
