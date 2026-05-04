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
        string IAbletDefinition.Id => "Silksprite.AvatarTinkerVista.DeleteDisabledAtivComponents";
        string IAbletDefinition.DisplayName => "ATiV: Delete disabled components";
        void IAbletLayer.Configure(IDependencyConfigurator config)
        {
            config.AddDependency<PruningPhase>();
        }
        AbletProcedure? IAbletLayer.ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create(context =>
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
