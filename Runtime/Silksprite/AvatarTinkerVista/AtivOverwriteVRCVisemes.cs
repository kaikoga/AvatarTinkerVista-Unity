using System;
using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.Loch.Attributes;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Overwrite VRC Visemes")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_overwrite_vrc_visemes")]
    public class AtivOverwriteVRCVisemes : AtivGeneratingComponent
    {
        public List<VisemeOption> options = new List<VisemeOption>();
        
        [LEnum]
        public enum VisemeStyle
        {
            None = 0,
            SingleBlendShape = 100,
            OculusVisemes = 1500,
        }

        [Serializable] 
        public class VisemeOption
        {
            public VisemeStyle visemeStyle;

            public AvatarRelativeSkinnedMeshRenderer faceMesh = new AvatarRelativeSkinnedMeshRenderer();

            public string singleBlendShape = "";
            
            public string oculusSil = "";
            public string oculusPp = "";
            public string oculusFf = "";
            public string oculusTh = "";
            public string oculusDd = "";
            public string oculusKk = "";
            public string oculusCh = "";
            public string oculusSs = "";
            public string oculusNn = "";
            public string oculusRr = "";
            public string oculusAa = "";
            public string oculusE = "";
            public string oculusI = "";
            public string oculusO = "";
            public string oculusU = "";
        }
    }
}