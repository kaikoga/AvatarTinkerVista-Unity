#if ATIV_VRM0

using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Vrm0;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class ExportVrm0Pass : Pass<ExportVrm0Pass>
    {
        protected override void Execute(BuildContext context)
        {
            var exportVrm = context.AvatarRootObject.GetComponentInChildren<AtivExportVrm>();
            if (!exportVrm) return;

            Vrm0FileExporter.ExportVrm0File(context.AvatarRootObject, exportVrm.fileName);
        }
    }
}

#endif