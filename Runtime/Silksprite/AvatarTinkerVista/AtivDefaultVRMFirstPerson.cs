using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Default VRM0+1 FirstPerson")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_default_vrm_firstperson")]
    public class AtivDefaultVRMFirstPerson : AtivOptimizingComponent
    {
        public OverwriteVector3 firstPersonOffset = new OverwriteVector3();
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
