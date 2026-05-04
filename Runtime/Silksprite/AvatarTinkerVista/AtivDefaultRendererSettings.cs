using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Default Renderer Settings")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_default_renderer_settings")]
    public class AtivDefaultRendererSettings : AtivOptimizingComponent
    {
        public bool preferParentSettings;
        public OverwriteAvatarRelativeTransform overwriteProbeAnchor = new OverwriteAvatarRelativeTransform();
        public OverwriteAvatarRelativeTransform overwriteRootBone = new OverwriteAvatarRelativeTransform();
        public OverwriteBounds overwriteBounds = new OverwriteBounds();
    }
}
