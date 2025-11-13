using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Base;

namespace Silksprite.AvatarTinkerVista.Nondestructive.Base
{
    public abstract class AtivDeleteComponentsBase : AtivResolvingComponent
    {
        public abstract IEnumerable<string> ComponentTypeNamePrefixes { get; }
    }
}