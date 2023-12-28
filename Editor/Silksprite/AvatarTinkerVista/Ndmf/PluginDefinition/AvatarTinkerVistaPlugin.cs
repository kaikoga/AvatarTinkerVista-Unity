using System;
using Silksprite.AvatarTinkerVista.Ndmf;
using Silksprite.AvatarTinkerVista.Ndmf.Aao.Passes;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using Silksprite.AvatarTinkerVista.Ndmf.Passes;
using nadena.dev.ndmf;
using UnityEngine;

[assembly: ExportsPlugin(typeof(AvatarTinkerVistaPlugin))]

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    class AvatarTinkerVistaPlugin : Plugin<AvatarTinkerVistaPlugin>
    {
        public override string QualifiedName => "net.kaikoga.atv";
        public override string DisplayName => "Avatar Tinker Vista";

        protected override void OnUnhandledException(Exception e)
        {
            Debug.LogException(e);
        }

        protected override void Configure()
        {
            var resolving = InPhase(BuildPhase.Resolving);
            resolving.Run(DeleteComponentsPass.Instance);
            resolving.Run(DeleteAtvComponentsPass<AtivResolvingComponent>.Instance);

            var generating = InPhase(BuildPhase.Generating);
#if ATIV_AAO
            generating.Run(AaoMergeOtherSkinnedMeshPass.Instance);
#endif
#if ATIV_VRM0
            generating.Run(OverwriteVrm0MetaPass.Instance);
#endif
#if ATIV_VRM1
            generating.Run(OverwriteVrm1MetaPass.Instance);
#endif
            generating.Run(DeleteAtvComponentsPass<AtivGeneratingComponent>.Instance);
        }
    }
}
