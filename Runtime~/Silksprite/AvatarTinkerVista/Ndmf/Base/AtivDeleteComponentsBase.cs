using System.Collections.Generic;

namespace Silksprite.AvatarTinkerVista.Ndmf.Base
{
    public abstract class AtivDeleteComponentsBase : AtivResolvingComponent
    {
        public abstract IEnumerable<string> ComponentTypeNamePrefixes { get; }
    }
}