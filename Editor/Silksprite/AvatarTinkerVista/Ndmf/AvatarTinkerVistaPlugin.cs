using System;
using Silksprite.AvatarTinkerVista.Ndmf;
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
            var seq = InPhase(BuildPhase.Resolving);
            seq.Run(DeleteComponentsPass.Instance);
        }
    }
}
