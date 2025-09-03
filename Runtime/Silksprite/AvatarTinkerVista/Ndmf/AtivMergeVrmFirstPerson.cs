using System;
using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Merge VRM0+1 FirstPerson")]
    [DisallowMultipleComponent]
    public class AtivMergeVrmFirstPerson : AtivTransformingComponent
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
