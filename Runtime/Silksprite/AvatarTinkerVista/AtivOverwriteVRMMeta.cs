using System;
using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.Loch.Attributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Overwrite VRM0+1 Meta")]
    [HelpURL("https://docs.kaikoga.net/ativ/components/ativ_overwrite_vrm_meta")]
    public class AtivOverwriteVRMMeta : AtivGeneratingComponent
    {
        #region Info

        public OverwriteString nameOrTitle = new OverwriteString();
        public OverwriteString version = new OverwriteString();
        [FormerlySerializedAs("vrm0Author")] public OverwriteString author = new OverwriteString();
        public List<string> vrm1MoreAuthors = new List<string>();

        public OverwriteString vrm1CopyrightInformation = new OverwriteString();
        public OverwriteString contactInformation = new OverwriteString();
        [FormerlySerializedAs("vrm0Reference")] public OverwriteString reference = new OverwriteString();
        public List<string> vrm1MoreReferences = new List<string>();
        public OverwriteString vrm1ThirdPartyLicenses = new OverwriteString();
        public OverwriteTexture2D thumbnail = new OverwriteTexture2D();

        #endregion

        #region Permission

        public OverwriteAllowedUser allowedUser = new OverwriteAllowedUser();
        public OverwriteBool violentUsage = new OverwriteBool();
        public OverwriteBool sexualUsage = new OverwriteBool();
        public OverwriteBool vrm0CommercialUsage = new OverwriteBool();
        public OverwriteVRM1CommercialUsageType vrm1CommercialUsage = new OverwriteVRM1CommercialUsageType();
        public OverwriteBool vrm1PoliticalOrReligiousUsage = new OverwriteBool();
        public OverwriteBool vrm1AntisocialOrHateUsage = new OverwriteBool();
        public OverwriteString vrm0OtherPermissionUrl = new OverwriteString();

        #endregion

        #region Distribution License
        
        public OverwriteVRM0LicenseType vrm0LicenseType = new OverwriteVRM0LicenseType();
        public OverwriteVRM1CreditNotationType vrm1CreditNotation = new OverwriteVRM1CreditNotationType();
        public OverwriteBool vrm1Redistribution = new OverwriteBool();
        public OverwriteVRM1ModificationType vrm1Modification = new OverwriteVRM1ModificationType();
        public OverwriteString otherLicenseUrl = new OverwriteString();
        
        #endregion
        
        #region enums

        [LEnum]
        public enum AllowedUser
        {
            OnlyAuthor,
            ExplicitlyLicensedPerson,
            Everyone,
        }

        [LEnum]
        public enum VRM1CommercialUsageType
        {
            PersonalNonProfit,
            PersonalProfit,
            Corporation,
        }

        [Serializable] public class OverwriteAllowedUser : Overwrite<AllowedUser> { }
        [Serializable] public class OverwriteVRM1CommercialUsageType : Overwrite<VRM1CommercialUsageType> { }

        [LEnum]
        public enum VRM0LicenseType {
            // ReSharper disable InconsistentNaming
            Redistribution_Prohibited,
            CC0,
            CC_BY,
            CC_BY_NC,
            CC_BY_SA,
            CC_BY_NC_SA,
            CC_BY_ND,
            CC_BY_NC_ND,
            Other
            // ReSharper restore InconsistentNaming
        }

        [LEnum]
        public enum VRM1CreditNotationType
        {
            Required,
            Unnecessary
        }

        [LEnum]
        public enum VRM1ModificationType
        {
            Prohibited,
            AllowModification,
            AllowModificationRedistribution
        }
        
        [Serializable] public class OverwriteVRM0LicenseType : Overwrite<VRM0LicenseType> { }
        [Serializable] public class OverwriteVRM1CreditNotationType : Overwrite<VRM1CreditNotationType> { }
        [Serializable] public class OverwriteVRM1ModificationType : Overwrite<VRM1ModificationType> { }

        #endregion
    }
}