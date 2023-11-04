using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATV Delete All PhysBones")]
    public class AtvDeleteAllPhysBones : AtvDeleteComponentsBase
    {
        public override IEnumerable<string> ComponentTypeNamePrefixes
        {
            get
            {
                yield return "VRC.SDK3.Dynamics.PhysBone.Components.VRCPhysBone";
                yield return "VRC.SDK3.Dynamics.PhysBone.Components.VRCPhysBoneCollider";
            }
        }
    }
}
