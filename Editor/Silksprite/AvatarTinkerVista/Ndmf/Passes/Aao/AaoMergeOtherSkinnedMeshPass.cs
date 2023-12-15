#if ATIV_AAO

using System;
using System.Collections.Generic;
using System.Linq;
using Anatawa12.AvatarOptimizer.PrefabSafeSet;
using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Aao;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes.Aao
{
    class AaoMergeOtherSkinnedMeshPass : Pass<AaoMergeOtherSkinnedMeshPass>
    {
        protected override void Execute(BuildContext context)
        {
            var generators = context.AvatarRootTransform.GetComponentsInChildren<AtivAaoMergeOtherSkinnedMesh>(true);
            if (generators.Length != 1) return;
            var generator = generators[0];

            var mergeSkinnedMeshType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .First(type => type.FullName == "Anatawa12.AvatarOptimizer.MergeSkinnedMesh");
            var renderersSetField = mergeSkinnedMeshType.GetField("renderersSet");
            var staticRenderersSetField = mergeSkinnedMeshType.GetField("staticRenderersSet");
            var skipEnablementMismatchedRenderersField = mergeSkinnedMeshType.GetField("skipEnablementMismatchedRenderers");

            var mergeSkinnedMeshes = context.AvatarRootTransform.GetComponentsInChildren<Component>(true)
                .Where(component => component.GetType() == mergeSkinnedMeshType);

            var renderers = new HashSet<SkinnedMeshRenderer>();
            var staticRenderers = new HashSet<MeshRenderer>();

            renderers.UnionWith(context.AvatarRootTransform.GetComponentsInChildren<SkinnedMeshRenderer>(true));
            staticRenderers.UnionWith(context.AvatarRootTransform.GetComponentsInChildren<MeshRenderer>(true));

            renderers.ExceptWith(generator.excludeRenderersSet.GetAsSet());
            staticRenderers.ExceptWith(generator.excludeStaticRenderersSet.GetAsSet());
            
            foreach (var mergeSkinnedMesh in mergeSkinnedMeshes)
            {
                var rendererSet = (SkinnedMeshRendererSet)renderersSetField.GetValue(mergeSkinnedMesh);
                var staticRenderersSet = (MeshRendererSet)staticRenderersSetField.GetValue(mergeSkinnedMesh);
                renderers.ExceptWith(rendererSet.GetAsSet());
                renderers.Remove(mergeSkinnedMesh.GetComponent<SkinnedMeshRenderer>());
                staticRenderers.ExceptWith(staticRenderersSet.GetAsSet());
            }

            {
                var mergeSkinnedMesh = generator.gameObject.AddComponent(mergeSkinnedMeshType);
                var rendererSet = (SkinnedMeshRendererSet)renderersSetField.GetValue(mergeSkinnedMesh);
                var staticRenderersSet = (MeshRendererSet)staticRenderersSetField.GetValue(mergeSkinnedMesh);
                rendererSet.SetValueNonPrefab(renderers);
                staticRenderersSet.SetValueNonPrefab(staticRenderers);
                skipEnablementMismatchedRenderersField.SetValue(mergeSkinnedMesh, true);
            }
        }
    }
}

#endif
