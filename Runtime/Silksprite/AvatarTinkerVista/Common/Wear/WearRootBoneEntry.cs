using System;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Wear
{
    [Serializable]
    public struct WearRootBoneEntry
    {
        public Transform rootBone;
        public WearArmatureMode armatureMode;
        public HumanBodyBones humanBone;
    }
}
