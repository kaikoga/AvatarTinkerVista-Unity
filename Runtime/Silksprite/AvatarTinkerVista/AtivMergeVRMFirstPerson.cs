using System;
using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Common.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Merge VRM0+1 FirstPerson")]
    [DisallowMultipleComponent]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_merge_vrm_firstperson")]
    public class AtivMergeVRMFirstPerson : AtivTransformingComponent
    {
        public List<RendererFirstPersonFlags> renderers = new List<RendererFirstPersonFlags>();
        
        [Serializable]
        public struct RendererFirstPersonFlags
        {
            public Renderer renderer;
            public AtivFirstPersonFlag firstPersonFlag;
        }

        public enum AtivFirstPersonFlag
        {
            Auto,
            Both,
            ThirdPersonOnly,
            FirstPersonOnly,
        }
    }
}
