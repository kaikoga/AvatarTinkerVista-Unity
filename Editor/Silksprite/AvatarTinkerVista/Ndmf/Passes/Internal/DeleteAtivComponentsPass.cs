using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class DeleteAtivComponentsPass<T> : Pass<DeleteAtivComponentsPass<T>>
    where T : AtivComponent
    {
        protected override void Execute(BuildContext context)
        {
            foreach (var ativComponent in context.AvatarRootTransform.GetComponentsInChildren<T>())
            {
                Object.DestroyImmediate(ativComponent);
            }
        }
    }
}
