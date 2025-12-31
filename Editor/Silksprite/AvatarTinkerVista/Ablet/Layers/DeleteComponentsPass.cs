using System.Linq;
using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Silksprite.AvatarTinkerVista.Nondestructive.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ablet.Layers
{
    [AbletLayer]
    class DeleteComponentsPass : IAbletLayer
    {
        public string Id => "Silksprite.AvatarTinkerVista.DeleteComponents";
        public string DisplayName => "ATiV: Delete Components";
        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<PruningPhase>();
        }
        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;
            
            return AbletBuildProcedure.Create((IBuildContext context) =>
            {
                var typeNamePrefixes = context.CurrentRootTransform.GetComponentsInChildren<AtivDeleteComponentsBase>(true)
                    .SelectMany(atv => atv.ComponentTypeNamePrefixes)
                    .Distinct()
                    .ToArray();

                foreach (var component in context.CurrentRootTransform.GetComponentsInChildren<Component>(true))
                {
                    for (var type = component.GetType(); type != null; type = type.BaseType)
                    {
                        var typeFullName = type.FullName;
                        if (string.IsNullOrEmpty(typeFullName)) break;
                        if (!typeNamePrefixes.Any(prefix => typeFullName.StartsWith(prefix))) continue;
                        Object.DestroyImmediate(component);
                        break;
                    }
                }
            });
        }
    }
}
