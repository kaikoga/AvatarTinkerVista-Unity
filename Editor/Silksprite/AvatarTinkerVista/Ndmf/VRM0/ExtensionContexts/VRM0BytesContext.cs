using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.VRM0;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Contexts
{
    public class VRM0BytesContext : IExtensionContext
    {
        VRMMeta _vrmMeta;

        byte[] _bytes;

        public bool TryGetBytes(out byte[] bytes)
        {
            bytes = _bytes ??= (_vrmMeta ? VRM0Exporter.ExportVRM0(_vrmMeta) : null);
            _vrmMeta = null;
            return bytes != null;
        }

        public void OnActivate(BuildContext context)
        {
            _vrmMeta = context.AvatarRootObject.GetComponent<VRMMeta>();
        }

        public void OnDeactivate(BuildContext context)
        {
        }
    }
}
