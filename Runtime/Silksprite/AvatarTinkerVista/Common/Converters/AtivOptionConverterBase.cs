using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Converters
{
    public abstract class AtivOptionConverterBase<TAtiv, TOption, TPlatform>
        where TAtiv : Component
    {
        public abstract void ToAtiv(TAtiv ativ, TPlatform platform);
        public abstract TOption GetOption(TAtiv ativ);
        public abstract void ToPlatform(TAtiv ativ, TPlatform platform);
    }
}
