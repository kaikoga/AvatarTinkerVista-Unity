using System.Linq;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Processors
{
    public static class MergeVRM1SpringBonesProcessor
    {
        public static void Process(Vrm10Instance vrmInstance)
        {
            var sources = vrmInstance.GetComponentsInChildren<AtivMergeVRM1SpringBones>(); 

            vrmInstance.SpringBone.ColliderGroups = vrmInstance.SpringBone.ColliderGroups
                .Concat(sources.SelectMany(bone => bone.colliderGroups))
                .Distinct()
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