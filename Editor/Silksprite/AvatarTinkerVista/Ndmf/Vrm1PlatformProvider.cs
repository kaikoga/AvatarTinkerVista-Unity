#if ATIV_VRM1

using System;
using nadena.dev.ndmf.platform;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [NDMFPlatformProvider]
    class Vrm1PlatformProvider : INDMFPlatformProvider
    {
        string INDMFPlatformProvider.QualifiedName => "net.kaikoga.ativ.univrm.vrm1"; 
        string INDMFPlatformProvider.DisplayName => "VRM 1.0 (ATiV)";

        Type INDMFPlatformProvider.AvatarRootComponentType => typeof(Vrm10Instance);
    }
}

#endif
