using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM0;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class ExportVRM0Pass : Pass<ExportVRM0Pass>
    {
        protected override void Execute(BuildContext context)
        {
            var exportVrm = context.AvatarRootObject.GetComponentInChildren<AtivExportVRM>();
            if (!exportVrm) return;

            VRM0FileExporter.ExportVRM0File(context.AvatarRootObject, exportVrm.fileName);
        }
    }
}
