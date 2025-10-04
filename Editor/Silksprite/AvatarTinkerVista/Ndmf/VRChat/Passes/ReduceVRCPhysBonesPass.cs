using System.Linq;
using nadena.dev.ndmf;
using UnityEngine;
using VRC.Dynamics;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRChat.Passes
{
    class ReduceVRCPhysBonesPass : Pass<ReduceVRCPhysBonesPass>
    {
        protected override void Execute(BuildContext context)
        {
            var rootTransform = context.AvatarRootObject;
            var ativ = rootTransform.GetComponentsInChildren<AtivReduceDynamics>();
            if (ativ.Length == 0)
            {
                return;
            }
            var keepBoneRoots = ativ
                .SelectMany(c => c.keepBoneRoots)
                .Distinct().ToArray();
            var reducePbs = rootTransform.GetComponentsInChildren<VRCPhysBoneBase>()
                .Where(pb => !keepBoneRoots.Contains(pb.GetRootTransform()));
            foreach (var pb in reducePbs)
            {
                Object.DestroyImmediate(pb);
            }
        }
    }
}
