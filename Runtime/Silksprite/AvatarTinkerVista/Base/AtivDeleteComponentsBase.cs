using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Common.Base;

namespace Silksprite.AvatarTinkerVista.Base
{
    public abstract class AtivDeleteComponentsBase : AtivResolvingComponent
    {
        public abstract IEnumerable<string> ComponentTypeNamePrefixes { get; }
    }
}