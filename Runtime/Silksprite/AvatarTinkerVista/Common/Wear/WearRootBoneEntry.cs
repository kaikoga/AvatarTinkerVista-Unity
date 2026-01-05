using System;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Wear
{
    [Serializable]
    public abstract class WearRootBoneEntryBase<T>
    {
        public T rootBone;
        public WearArmatureMode armatureMode;
        public HumanBodyBones humanBone;
    }
    
    [Serializable]
    public class WearRootBoneEntry : WearRootBoneEntryBase<Transform> { }

    [Serializable]
    public class WearRelativeRootBoneEntry : WearRootBoneEntryBase<AvatarRelativeTransform> { }
}
