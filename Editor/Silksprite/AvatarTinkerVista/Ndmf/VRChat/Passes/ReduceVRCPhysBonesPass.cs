using System.Collections.Generic;
using System.Linq;
using nadena.dev.ndmf;
using nadena.dev.ndmf.vrchat;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDKBase;
using VRC.SDKBase.Network;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRChat.Passes
{
    class ReduceVRCPhysBonesPass : Pass<ReduceVRCPhysBonesPass>
    {
#if UNITY_STANDALONE
        static bool ReduceOnPlatform => false;
#else
        static bool ReduceOnPlatform => true;
#endif

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
            var allPbs = rootTransform.GetComponentsInChildren<VRCPhysBoneBase>();
            var keepPbs = allPbs
                .Where(pb => keepBoneRoots.Contains(pb.GetRootTransform()))
                .ToArray();
            var reducePbs = allPbs.Except(keepPbs);
            // this is based on VRCSDK assign strategy; large ID numbers are not likely to be auto assigned
            RecordNetworkIdsToSync(context, keepPbs, GenerateUnusedIds(context));
            if (ReduceOnPlatform)
            {
                DestroyPhysBones(reducePbs);
            }
            else
            {
                RecordNetworkIdsToSync(context, reducePbs, GenerateUnusedIdsDescending(context));
            }
        }

        static void DestroyPhysBones(IEnumerable<VRCPhysBoneBase> reducePbs)
        {
            foreach (var pb in reducePbs)
            {
                Object.DestroyImmediate(pb);
            }
        }

        static void RecordNetworkIdsToSync(BuildContext context, IEnumerable<VRCPhysBoneBase> pbs, IEnumerable<int> idGen)
        {
            var avatarDescriptor = context.VRChatAvatarDescriptor();
            var networkIdPairs = avatarDescriptor.NetworkIDCollection;
            using var unusedIds = idGen.GetEnumerator();;
            foreach (var networkId in pbs.OfType<INetworkID>())
            {
                var networkObject = ((Component)networkId).gameObject;
                if (networkIdPairs.Any(pair => pair.gameObject == networkObject))
                {
                    // just add if not exists, in case they are pre assigned, or VQT NetworkIDAssigner has already done the work 
                    continue;
                }
                if (!unusedIds.MoveNext())
                {
                    // ran out of Network IDs
                    Debug.LogError("ATiV Reduce VRC PhysBones: ran out of Network IDs");
                    break;
                }
                networkIdPairs.Add(new NetworkIDPair
                {
                    gameObject = networkObject,
                    ID = unusedIds.Current,
                    SerializedTypeNames = { }
                });
            }
            avatarDescriptor.NetworkIDCollection = networkIdPairs;
        }

        const int MinId = 10;
        const int MaxId = 100000;

        static IEnumerable<int> GenerateUnusedIds(BuildContext context)
        {
            var networkIdPairs = context.VRChatAvatarDescriptor().NetworkIDCollection;
            var usedIds = networkIdPairs.Select(pair => pair.ID).ToHashSet();
            for (var id = MinId; id < MaxId; id++)
            {
                if (usedIds.Contains(id))
                {
                    continue;
                }
                yield return id;
            }
        }

        static IEnumerable<int> GenerateUnusedIdsDescending(BuildContext context)
        {
            var networkIdPairs = context.VRChatAvatarDescriptor().NetworkIDCollection;
            var usedIds = networkIdPairs.Select(pair => pair.ID).ToHashSet();
            for (var id = MaxId - 1; id >= MinId; id--)
            {
                if (usedIds.Contains(id))
                {
                    continue;
                }
                yield return id;
            }
        }
    }
}
