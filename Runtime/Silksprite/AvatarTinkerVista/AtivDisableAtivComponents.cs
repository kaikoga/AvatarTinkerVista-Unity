using Silksprite.AvatarTinkerVista.Common;
using Silksprite.AvatarTinkerVista.Common.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [AddComponentMenu("Avatar Tinker Vista/ATiV Disable ATiV Components")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_disable_ativ_components")]
    public class AtivDisableAtivComponents : AtivGeneratingComponent, IAtivDisableAtivComponents
    {
        public Component ToComponent() => this;
    }
}
