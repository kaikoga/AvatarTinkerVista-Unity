using UnityEditor;
using UnityEngine;

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
            var result = EditorUtility.DisplayDialogComplex(
                Title,
                $"Do you want to also destroy existing {DestroyTarget}?",
                "Just generate",
                "Cancel",
                $"Destroy {DestroyTarget}");
            if (result != 1)
            {
                Convert(context, result == 2);
            }
        }

        protected abstract void Convert(TContext context, bool destroy);
    }
}
