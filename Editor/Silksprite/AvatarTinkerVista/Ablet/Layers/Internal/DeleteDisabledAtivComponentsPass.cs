using System.Linq;
using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.Common;
using Silksprite.AvatarTinkerVista.Common.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ablet.Layers.Internal
{
    [AbletLayer]
    class DeleteDisabledAtivComponentsPass : IAbletLayer
    {
        public string Id => "net.kaikoga.ativ.delete-disabled-ativ-components";
        public string DisplayName => "ATiV: Delete disabled components";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<PruningPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create((IBuildContext context) =>
            {
                foreach (var disableAtiv in context.CurrentRootTransform.GetComponentsInChildren<IAtivDisableAtivComponents>().Cast<Component>())
                {
                    foreach (var ativComponent in disableAtiv.GetComponents<AtivComponent>())
                    {
                        Object.DestroyImmediate(ativComponent);
                    }
                }
            });
        }
    }
}
