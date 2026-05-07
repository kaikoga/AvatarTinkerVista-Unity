using System;
using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.Loch.Attributes;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Overwrite View Position")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_overwrite_view_position")]
    public class AtivOverwriteViewPosition : AtivGeneratingComponent
    {
        public List<ViewPositionOption> options = new List<ViewPositionOption>();
        
        [LEnum]
        public enum ViewPositionStyle
        {
            Inherit = -1,
            Global = 0,
            HeadLocal = 1,
            TransformLocal = 2,
        }

        [Serializable] 
        public class ViewPositionOption
        {
            public ViewPositionStyle viewPositionStyle;
            
            public Vector3 globalPosition;

            public Vector3 headLocalPosition;
            
            public AvatarRelativeTransform transform = new AvatarRelativeTransform();
            public Vector3 transformLocalPosition;
        }
    }
}