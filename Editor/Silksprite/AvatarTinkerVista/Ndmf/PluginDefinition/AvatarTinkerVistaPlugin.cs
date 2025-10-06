using System;
using Silksprite.AvatarTinkerVista.Ndmf;
using Silksprite.AvatarTinkerVista.Ndmf.Passes;
using nadena.dev.ndmf;
using nadena.dev.ndmf.fluent;
using Silksprite.AvatarTinkerVista.Base;
using Silksprite.AvatarTinkerVista.Ndmf.VRChat.Passes;
using Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes;
using Silksprite.AvatarTinkerVista.Ndmf.VRM1.Passes;
using UnityEngine;

[assembly: ExportsPlugin(typeof(AvatarTinkerVistaPlugin))]
namespace Silksprite.AvatarTinkerVista.Ndmf
{
    // runs independently of NDMF platform
    [RunsOnAllPlatforms]
    class AvatarTinkerVistaPlugin : Plugin<AvatarTinkerVistaPlugin>
    {
        public override string QualifiedName => "net.kaikoga.ativ";
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
                sequence.Run(DeleteAtivComponentsPass<T>.Instance);
            }

            Phase<AtivResolvingComponent>(BuildPhase.Resolving, resolving =>
            {
                resolving.Run(DeleteDisabledAtivComponentsPass.Instance);
                resolving.Run(DeleteComponentsPass.Instance);
            });

            Phase<AtivGeneratingComponent>(BuildPhase.Generating, generating =>
            {
#if ATIV_DETECTED_VRM0
                generating.Run(OverwriteVRM0MetaPass.Instance);
                generating.Run(GenerateVRM0SpringBonesPass.Instance);
#endif
#if ATIV_DETECTED_VRM1
                generating.Run(OverwriteVRM1MetaPass.Instance);
                generating.Run(GenerateVRM1SpringBonesPass.Instance);
#endif
            });


            Phase<AtivTransformingComponent>(BuildPhase.Transforming, transforming =>
            {
#if ATIV_VRCSDK3_AVATARS
                transforming.Run(ReduceVRCPhysBonesPass.Instance);
#endif
#if ATIV_DETECTED_VRM0
                transforming.Run(MergeVRM0FirstPersonPass.Instance);
#endif
#if ATIV_DETECTED_VRM1
                transforming.Run(MergeVRM1SpringBonesPass.Instance);
                transforming.Run(MergeVRM1FirstPersonPass.Instance);
#endif
            });

            Phase<AtivOptimizingComponent>(BuildPhase.Optimizing, optimizing =>
            {
                optimizing.BeforePlugin("com.anatawa12.avatar-optimizer");
#if ATIV_DETECTED_VRM0
                optimizing.Run(DefaultVRM0FirstPersonPass.Instance);
#endif
#if ATIV_DETECTED_VRM1
                optimizing.Run(DefaultVRM1FirstPersonPass.Instance);
#endif
            });
        }
    }
}
