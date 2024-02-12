using System;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Ndmf.DataObjects
{
    public abstract class Overwrite<T>
    {
        public bool willOverwrite;
        public T value;

        public void OverwriteValue(ref T original)
        {
            if (willOverwrite) original = value;
        }

        public void OverwriteValue<TOut>(ref TOut original, Func<T, TOut> filter)
        {
            if (willOverwrite) original = filter(value);
        }
    }
    
    [Serializable] public class OverwriteBool : Overwrite<bool> { }
    [Serializable] public class OverwriteString : Overwrite<string> { }
    [Serializable] public class OverwriteVector3 : Overwrite<Vector3> { }
    [Serializable] public class OverwriteTexture2D : Overwrite<Texture2D> { }

}