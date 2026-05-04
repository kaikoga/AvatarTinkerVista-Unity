using Silksprite.AvatarTinkerVista.Common.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Generate VRM1 Constraint")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_generate_vrm1_constraint")]
    public class AtivGenerateConstraint : AtivGeneratingComponent
    {
        public ConstraintKind kind;

        public Transform? source;
        public Transform? target;

        [Range(0, 1.0f)]
        public float weight = 1.0f;

        public AimAxis aimAxis;
        public RollAxis rollAxis;

        public Transform ActualTarget => target != null ? target : transform;

        public enum ConstraintKind
        {
            None,
            Aim,
            Roll,
            Rotation
        }
        
        public enum AimAxis
        {
            [InspectorName("+X")]
            PositiveX,
            [InspectorName("-X")]
            NegativeX,
            [InspectorName("+Y")]
            PositiveY,
            [InspectorName("-Y")]
            NegativeY,
            [InspectorName("+Z")]
            PositiveZ,
            [InspectorName("-Z")]
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
