using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class DeleteDisabledAtivComponentsPass : Pass<DeleteDisabledAtivComponentsPass>
    {
        protected override void Execute(BuildContext context)
        {
            foreach (var disableAtiv in context.AvatarRootTransform.GetComponentsInChildren<AtivDisableAtivComponents>())
            {
                foreach (var ativComponent in disableAtiv.GetComponents<AtivComponent>())
                {
                    Object.DestroyImmediate(ativComponent);
                }
            }
        }
    }
}
