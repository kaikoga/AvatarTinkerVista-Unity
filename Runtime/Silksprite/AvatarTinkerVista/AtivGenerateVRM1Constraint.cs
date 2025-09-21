using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Generate VRM1 Constraint")]
    public class AtivGenerateVRM1Constraint : AtivGeneratingComponent
    {
        public ConstraintKind kind;

        public Transform source;
        public Transform target;

        [Range(0, 1.0f)]
        public float weight = 1.0f;

        public AimVector aimAxis;
        public RollAxis rollAxis;

        public Transform ActualTarget => target ? target : transform;

        public enum ConstraintKind
        {
            Aim,
            Roll,
            Rotation
        }
        
        public enum AimVector
        {
            PositiveX,
            NegativeX,
            PositiveY,
            NegativeY,
            PositiveZ,
            NegativeZ,

        }
        public enum RollAxis
        {
            X,
            Y,
            Z
        }

    }
}
