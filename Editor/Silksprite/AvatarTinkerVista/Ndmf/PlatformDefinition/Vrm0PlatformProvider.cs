#if ATIV_VRM0

using System;
using nadena.dev.ndmf.platform;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [NDMFPlatformProvider]
    class Vrm0PlatformProvider : INDMFPlatformProvider
    {
        string INDMFPlatformProvider.QualifiedName => "net.kaikoga.ativ.univrm.vrm0"; 
        string INDMFPlatformProvider.DisplayName => "VRM 0.x (ATiV)";

        Type INDMFPlatformProvider.AvatarRootComponentType => typeof(VRMMeta);
    }
}

#endif
