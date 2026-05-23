using System;
using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.Loch.Attributes;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Overwrite Blink")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_overwrite_blink")]
    public class AtivOverwriteBlink : AtivGeneratingComponent
    {
        public List<BlinkOption> options = new List<BlinkOption>();
        
        [LEnum]
        public enum BlinkStyle
        {
            Inherit = -1,
            None = 0,
            SingleBlendShape = 10,
            SeparateBlendShapes = 20,
        }

        [Serializable] 
        public class BlinkOption
        {
            public BlinkStyle blinkStyle;
            
            public AvatarRelativeSkinnedMeshRenderer faceMesh = new AvatarRelativeSkinnedMeshRenderer();

            public string singleBlendShape = "";

            public string separateBlendShapeLeft = "";
            public string separateBlendShapeRight = "";
        }
    }
}