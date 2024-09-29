#if ATIV_VRM1

using System.Linq;
using nadena.dev.ndmf;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class MergeVrm1SpringBonesPass : Pass<MergeVrm1SpringBonesPass>
    {
        protected override void Execute(BuildContext context)
        {
            var rootTransform = context.AvatarRootObject;
            var vrmInstance = rootTransform.GetComponent<Vrm10Instance>();
            if (!vrmInstance) return;

            var sources = rootTransform.GetComponentsInChildren<AtivMergeVrm1SpringBones>(); 

            vrmInstance.SpringBone.ColliderGroups = vrmInstance.SpringBone.ColliderGroups
                .Concat(sources.SelectMany(bone => bone.colliderGroups))
                .ToList();
            
            vrmInstance.SpringBone.Springs = vrmInstance.SpringBone.Springs
                .Concat(sources.SelectMany(bone => bone.springs))
                .ToList();

            foreach (var source in sources)
            {
                UnityEngine.Object.DestroyImmediate(source);
            }
        }
    }
}

#endif
