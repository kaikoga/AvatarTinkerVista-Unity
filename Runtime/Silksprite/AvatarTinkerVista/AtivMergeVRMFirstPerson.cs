using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Merge VRM0+1 FirstPerson")]
    [DisallowMultipleComponent]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_merge_vrm_firstperson")]
    public class AtivMergeVRMFirstPerson : AtivTransformingComponent
    {
        public List<AtivRendererFirstPersonFlags> renderers = new List<AtivRendererFirstPersonFlags>();

    }
}
