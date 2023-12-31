using System;
using Silksprite.AvatarTinkerVista.Ndmf;
using Silksprite.AvatarTinkerVista.Ndmf.Aao.Passes;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using Silksprite.AvatarTinkerVista.Ndmf.Passes;
using nadena.dev.ndmf;
using nadena.dev.ndmf.fluent;
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
            void Phase<T>(BuildPhase phase, Action<Sequence> initializer)
            where T : AtivComponent
            {
                var sequence = InPhase(phase);
                initializer(sequence);
                sequence.Run(DeleteAtvComponentsPass<T>.Instance);
            }

            Phase<AtivResolvingComponent>(BuildPhase.Resolving, resolving =>
            {
                resolving.Run(DeleteComponentsPass.Instance);
            });

            Phase<AtivGeneratingComponent>(BuildPhase.Generating, generating =>
            {
#if ATIV_AAO
                generating.Run(AaoMergeOtherSkinnedMeshPass.Instance);
#endif
#if ATIV_VRM0
                generating.Run(OverwriteVrm0MetaPass.Instance);
#endif
#if ATIV_VRM1
                generating.Run(OverwriteVrm1MetaPass.Instance);
#endif
            });

            Phase<AtivOptimizingComponent>(BuildPhase.Optimizing, optimizing =>
            {
                optimizing.BeforePlugin("com.anatawa12.avatar-optimizer");
#if ATIV_VRM0
                optimizing.Run(DefaultVrm0FirstPersonPass.Instance);
#endif
#if ATIV_VRM1
                optimizing.Run(DefaultVrm1FirstPersonPass.Instance);
#endif
            });

        }
    }
}
