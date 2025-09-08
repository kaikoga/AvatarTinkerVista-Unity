using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class DeleteAtvComponentsPass<T> : Pass<DeleteAtvComponentsPass<T>>
    where T : AtivComponent
    {
        protected override void Execute(BuildContext context)
        {
            foreach (var atvComponent in context.AvatarRootTransform.GetComponentsInChildren<T>())
            {
                Object.DestroyImmediate(atvComponent);
            }
        }
    }
}
