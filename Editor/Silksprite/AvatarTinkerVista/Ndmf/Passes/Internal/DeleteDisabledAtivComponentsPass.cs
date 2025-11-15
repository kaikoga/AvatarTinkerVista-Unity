using System.Linq;
using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Common;
using Silksprite.AvatarTinkerVista.Common.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes.Internal
{
    class DeleteDisabledAtivComponentsPass : Pass<DeleteDisabledAtivComponentsPass>
    {
        protected override void Execute(BuildContext context)
        {
            foreach (var disableAtiv in context.AvatarRootTransform.GetComponentsInChildren<IAtivDisableAtivComponents>().Cast<Component>())
            {
                foreach (var ativComponent in disableAtiv.GetComponents<AtivComponent>())
                {
                    Object.DestroyImmediate(ativComponent);
                }
            }
        }
    }
}
