using System.Collections.Generic;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf.Base
{
    public abstract class AtvDeleteComponentsBase : MonoBehaviour
    {
        public abstract IEnumerable<string> ComponentTypeNamePrefixes { get; }
    }
}