using System;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.DataObjects
{
    [Serializable]
    public struct AtivRendererFirstPersonFlags
    {
        public Renderer renderer;
        public AtivFirstPersonFlag firstPersonFlag;

        public enum AtivFirstPersonFlag
        {
            Auto,
            Both,
            ThirdPersonOnly,
            FirstPersonOnly,
        }
    }
}
