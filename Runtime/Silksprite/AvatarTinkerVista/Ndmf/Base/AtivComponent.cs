using UnityEngine;
using VRC.SDKBase;

namespace Silksprite.AvatarTinkerVista.Ndmf.Base
{
    public abstract class AtivComponent : MonoBehaviour
#if ATIV_VRCSDK3_AVATARS
        , IEditorOnly
#endif
    {
    }
    
    public abstract class AtivResolvingComponent : AtivComponent
    {
    }

    public abstract class AtivGeneratingComponent : AtivComponent
    {
    }

    public abstract class AtivOptimizingComponent : AtivComponent
    {
    }
}