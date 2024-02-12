using Silksprite.AvatarTinkerVista.Ndmf.Base;
using Silksprite.AvatarTinkerVista.Ndmf.DataObjects;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Default VRM0+1 FirstPerson")]
    public class AtivDefaultVrmFirstPerson : AtivOptimizingComponent
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
