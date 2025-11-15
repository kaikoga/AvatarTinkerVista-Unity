using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.Utils;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Converter
{
    public abstract class ConstraintsConverterBase<TContext, TConstraintsFrom>
    where TContext : Component
    {
        public void Convert(TContext context, bool destroy)
        {
            foreach (var constraintFrom in CollectConstraints(context))
            {
                ConvertConstraint(context, constraintFrom);
            }
            if (destroy)
            {
                DestroyComponents(context);
            }
        }

        IEnumerable<TConstraintsFrom> CollectConstraints(TContext context)
        {
            return context.GetEligibleComponentsInChildren<TConstraintsFrom>();
        }

        protected abstract void ConvertConstraint(TContext context, TConstraintsFrom constraintFrom);
        
        void DestroyComponents(TContext context)
        {
            foreach (var component in CollectConstraints(context).OfType<Component>())
            {
                Object.DestroyImmediate(component);
            }
        }
    }
}
