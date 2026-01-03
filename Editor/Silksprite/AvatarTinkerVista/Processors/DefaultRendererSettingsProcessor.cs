using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Processors
{
    public static class DefaultRendererSettingsProcessor
    {
        public static void Process(Transform avatarRoot)
        {
            foreach (var renderer in avatarRoot.GetComponentsInChildren<Renderer>(true))
            {
                IEnumerable<AtivDefaultRendererSettings> EnumerateAtiv()
                {
                    for (var transform = renderer.transform; transform; transform = transform.parent)
                    {
                        foreach (var ativ in transform.GetComponents<AtivDefaultRendererSettings>())
                        {
                            yield return ativ;
                        }
                        if (transform == avatarRoot)
                        {
                            break;
                        }
                    }
                }
                var list = EnumerateAtiv().ToList();
                var ativs = list.Where(ativ => !ativ.preferParentSettings)
                    .Concat(list.Where(ativ => ativ.preferParentSettings).Reverse())
                    .ToArray();
                
                if (ativs.Select(ativ => ativ.overwriteProbeAnchor).FirstOrDefault(overwrite => overwrite.willOverwrite) is { } overwriteProbeAnchor)
                {
                    renderer.probeAnchor = overwriteProbeAnchor.value.ResolveNow(avatarRoot);
                }
                if (renderer is SkinnedMeshRenderer skinnedMeshRenderer)
                {
                    if (ativs.Select(ativ => ativ.overwriteBounds).FirstOrDefault(overwrite => overwrite.willOverwrite) is { } overwriteBounds)
                    {
                        skinnedMeshRenderer.localBounds = overwriteBounds.value;
                    }
                    if (ativs.Select(ativ => ativ.overwriteRootBone).FirstOrDefault(overwrite => overwrite.willOverwrite) is { } overwriteRootBone)
                    {
                        skinnedMeshRenderer.rootBone = overwriteRootBone.value.ResolveNow(avatarRoot);
                    }
                }
            }
        }
    }
}
