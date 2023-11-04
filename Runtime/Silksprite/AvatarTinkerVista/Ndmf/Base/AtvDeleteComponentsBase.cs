using System.Collections.Generic;

namespace Silksprite.AvatarTinkerVista.Ndmf.Base
{
    public abstract class AtvDeleteComponentsBase : AtvResolvingComponent
    {
        public abstract IEnumerable<string> ComponentTypeNamePrefixes { get; }
    }
}