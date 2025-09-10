using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM1;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Contexts
{
    public class VRM1BytesContext : IExtensionContext
    {
        Vrm10Instance _vrm10Instance;

        byte[] _bytes;

        public bool TryGetBytes(out byte[] bytes)
        {
            bytes = _bytes ??= _vrm10Instance ? VRM1Exporter.ExportVRM1(_vrm10Instance) : null;
            _vrm10Instance = null;
            return bytes != null;
        }

        public void OnActivate(BuildContext context)
        {
            _vrm10Instance = context.AvatarRootObject.GetComponent<Vrm10Instance>();
        }

        public void OnDeactivate(BuildContext context)
        {
        }
    }
}
