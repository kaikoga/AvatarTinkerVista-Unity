using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Nondestructive.DataObjects;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Nondestructive
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Default VRM0+1 FirstPerson")]
    [HelpURL("https://docs.kaikoga.net/ativ/ndmf_components/ativ_default_vrm_firstperson")]
    public class AtivDefaultVRMFirstPerson : AtivOptimizingComponent
    {
        public OverwriteVector3 firstPersonOffset;
        public AtivFirstPersonFlag defaultValue;

        public enum AtivFirstPersonFlag
        {
            Auto,
            Both,
            ThirdPersonOnly,
            FirstPersonOnly,
        }
    }
}
