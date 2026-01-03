using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Nondestructive.DataObjects;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Nondestructive
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Default Renderer Settings")]
    [HelpURL("https://docs.kaikoga.net/ativ/ndmf_components/ativ_default_renderer_settings")]
    public class AtivDefaultRendererSettings : AtivOptimizingComponent
    {
        public bool preferParentSettings;
        public OverwriteAvatarRelativeTransform overwriteProbeAnchor;
        public OverwriteAvatarRelativeTransform overwriteRootBone;
        public OverwriteBounds overwriteBounds;
    }
}
