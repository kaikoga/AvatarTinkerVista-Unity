using Silksprite.Loch;
using UnityEditor;
using UnityEngine;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista.Common
{
    public abstract class InteractiveConverterBase<TContext>
    where TContext : Component
    {
        protected abstract string UndoName { get; }
        protected abstract string Title { get; }
        protected abstract string DestroyTarget { get; }

        public void InteractiveConvert(TContext context)
        {
            Undo.RegisterFullObjectHierarchyUndo(context.gameObject, UndoName);
            var substitution = new Substitution
            {
                ["DestroyTarget"] = DestroyTarget
            };
            var result = EditorUtility.DisplayDialogComplex(
                Title,
                Loc("InteractiveConverterBase::Message?").Format(substitution).Tr,
                Loc("InteractiveConverterBase::Ok").Tr,
                Loc("InteractiveConverterBase::Cancel").Tr,
                Loc("InteractiveConverterBase::Alt").Format(substitution).Tr);
            if (result != 1)
            {
                Convert(context, result == 2);
            }
        }

        protected abstract void Convert(TContext context, bool destroy);
    }
}
