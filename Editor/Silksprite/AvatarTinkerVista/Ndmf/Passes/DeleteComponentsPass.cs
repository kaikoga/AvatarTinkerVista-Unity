using System.Linq;
using nadena.dev.ndmf;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class DeleteComponentsPass : Pass<DeleteComponentsPass>
    {
        protected override void Execute(BuildContext context)
        {
            var typeNamePrefixes = context.AvatarRootTransform.GetComponentsInChildren<AtvDeleteComponentsBase>()
                .SelectMany(atv => atv.ComponentTypeNamePrefixes)
                .ToArray();

            foreach (var component in context.AvatarRootTransform.GetComponentsInChildren<Component>())
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
        }
    }
}
