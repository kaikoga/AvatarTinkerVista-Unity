using System;
using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UniGLTF.Extensions.VRMC_vrm;
using UnityEngine;
using VRM;

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

#if ATIV_VRM0
            public FirstPersonFlag Vrm0FirstPersonFlag
            {
                get
                {
                    switch (firstPersonFlag)
                    {
                        case AtivFirstPersonFlag.Auto: return FirstPersonFlag.Auto;
                        case AtivFirstPersonFlag.Both: return FirstPersonFlag.Both;
                        case AtivFirstPersonFlag.ThirdPersonOnly: return FirstPersonFlag.ThirdPersonOnly;
                        case AtivFirstPersonFlag.FirstPersonOnly: return FirstPersonFlag.FirstPersonOnly;
                        default: throw new ArgumentOutOfRangeException();
                    }
                }
            }
#endif

#if ATIV_VRM1
            public FirstPersonType Vrm1FirstPersonType
            {
                get
                {
                    switch (firstPersonFlag)
                    {
                        case AtivFirstPersonFlag.Auto: return FirstPersonType.auto;
                        case AtivFirstPersonFlag.Both: return FirstPersonType.both;
                        case AtivFirstPersonFlag.ThirdPersonOnly: return FirstPersonType.thirdPersonOnly;
                        case AtivFirstPersonFlag.FirstPersonOnly: return FirstPersonType.firstPersonOnly;
                        default: throw new ArgumentOutOfRangeException();
                    }
                }
            }
#endif
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
