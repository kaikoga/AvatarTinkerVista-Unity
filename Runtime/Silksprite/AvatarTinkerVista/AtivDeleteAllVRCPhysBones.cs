using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Delete All VRC PhysBones")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_delete_all_vrc_physbones")]
    public class AtivDeleteAllVRCPhysBones : AtivDeleteComponentsBase
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
