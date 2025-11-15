using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Utils;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Converter
{
    public abstract class DynamicsConverterBase<TContext, TDynamicsFrom, TColliderFrom, TColliderTo>
    where TContext : Component
    {
        public void Convert(TContext context, bool destroy)
        {
            var colliderMapping = new Dictionary<TColliderFrom, TColliderTo>();
            foreach (var colliderFrom in CollectColliders(context))
            {
                if (TryConvertCollider(context, colliderFrom, out var colliderTo))
                {
                    colliderMapping.Add(colliderFrom, colliderTo);
                }
            }
            foreach (var dynamicsFrom in CollectDynamics(context))
            {
                ConvertDynamics(context, dynamicsFrom, colliderMapping);
            }
            if (destroy)
            {
                DestroyComponents(context);
            }
        }
        protected virtual IEnumerable<TColliderFrom> CollectColliders(TContext context) => context.GetEligibleComponentsInChildren<TColliderFrom>();
        protected virtual IEnumerable<TDynamicsFrom> CollectDynamics(TContext context) => context.GetEligibleComponentsInChildren<TDynamicsFrom>();

        protected virtual void DestroyComponents(TContext context)
        {
            foreach (var component in CollectColliders(context).OfType<Component>().Concat(CollectDynamics(context).OfType<Component>()))
            {
                Object.DestroyImmediate(component);
            }
        }

        protected abstract bool TryConvertCollider(TContext context, TColliderFrom colliderFrom, out TColliderTo result);

        protected abstract void ConvertDynamics(TContext context, TDynamicsFrom dynamicsFrom, Dictionary<TColliderFrom, TColliderTo> colliderMapping);
    }
}
