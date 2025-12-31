using Ablet.API;
using Ablet.API.V1;
using Ablet.API.V1.Attributes;
using Ablet.API.V1.Building;
using Ablet.Builtin;
using Ablet.Builtin.Utils;
using Silksprite.AvatarTinkerVista.Common.Base;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ablet.Layers.Internal
{
    abstract class DeleteAtivComponentsLayer<TAtiv, TAblet> : IAbletLayer
        where TAtiv : AtivComponent
        where TAblet: IAbletLayer
    {
        public abstract string Id { get; }
        public abstract string DisplayName { get; }

        public void Configure(IDependencyConfigurator config)
        {
            config.AddDependency<AfterLayer<TAblet>>();
        }

        public AbletProcedure ToProcedure(IBuildArgument argument)
        {
            if (!AbletSymbols.PreferAblet) return null;

            return AbletBuildProcedure.Create((IBuildContext context) =>
            {
                foreach (var ativComponent in context.CurrentRootObject.GetComponentsInChildren<TAtiv>())
                {
                    Object.DestroyImmediate(ativComponent);
                }
            });
        }
    }

    [AbletLayer]
    class DeleteAtivResolvingComponentsLayer : DeleteAtivComponentsLayer<AtivResolvingComponent, PruningPhase>
    {
        public override string Id => "Silksprite.AvatarTinkerVista.DeleteAtivResolvingComponents";
        public override string DisplayName => "ATiV: Delete ATiV Resolving Components";
    }

    [AbletLayer]
    class DeleteAtivGeneratingComponentsLayer : DeleteAtivComponentsLayer<AtivGeneratingComponent, GeneratingPhase>
    {
        public override string Id => "Silksprite.AvatarTinkerVista.DeleteAtivGeneratingComponents";
        public override string DisplayName => "ATiV: Delete ATiV Generating Components";
    }

    [AbletLayer]
    class DeleteAtivTransformingComponentsLayer : DeleteAtivComponentsLayer<AtivTransformingComponent, TransformingPhase>
    {
        public override string Id => "Silksprite.AvatarTinkerVista.DeleteAtivTransformingComponents";
        public override string DisplayName => "ATiV: Delete ATiV Transforming Components";
    }

    [AbletLayer]
    class DeleteAtivOptimizingComponentsLayer : DeleteAtivComponentsLayer<AtivOptimizingComponent, MaterializingPhase>
    {
        public override string Id => "Silksprite.AvatarTinkerVista.DeleteAtivOptimizingComponents";
        public override string DisplayName => "ATiV: Delete ATiV Optimizing Components";
    }
}
