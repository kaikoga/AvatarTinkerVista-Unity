using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Vrm1;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class ExportVrm1Pass : Pass<ExportVrm1Pass>
    {
        protected override void Execute(BuildContext context)
        {
            var exportVrm = context.AvatarRootObject.GetComponentInChildren<AtivExportVrm>();
            if (!exportVrm) return;

            Vrm1FileExporter.ExportVrm1File(context.AvatarRootObject, exportVrm.fileName);
        }
    }
}
