using System;
using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.AvatarTinkerVista.DataObjects;
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

        [Header("Info")]
        public OverwriteString nameOrTitle;
        public OverwriteString version;
        [FormerlySerializedAs("vrm0Author")] public OverwriteString author;
        public List<string> vrm1MoreAuthors = new List<string>();

        public OverwriteString vrm1CopyrightInformation;
        public OverwriteString contactInformation;
        [FormerlySerializedAs("vrm0Reference")] public OverwriteString reference;
        public List<string> vrm1MoreReferences = new List<string>();
        public OverwriteString vrm1ThirdPartyLicenses;
        public OverwriteTexture2D thumbnail;

        #endregion

        #region Permission

        [Header("Permission")]
        public OverwriteAllowedUser allowedUser;
        public OverwriteBool violentUsage;
        public OverwriteBool sexualUsage;
        public OverwriteBool vrm0CommercialUsage;
        public OverwriteVRM1CommercialUsageType vrm1CommercialUsage;
        public OverwriteBool vrm1PoliticalOrReligiousUsage;
        public OverwriteBool vrm1AntisocialOrHateUsage;
        public OverwriteString vrm0OtherPermissionUrl;

        #endregion

        #region Distribution License
        
        [Header("Distribution License")]
        public OverwriteVRM0LicenseType vrm0LicenseType;
        public OverwriteVRM1CreditNotationType vrm1CreditNotation;
        public OverwriteBool vrm1Redistribution;
        public OverwriteVRM1ModificationType vrm1Modification;
        public OverwriteString otherLicenseUrl;
        
        #endregion
        
        #region enums

        public enum AllowedUser
        {
            OnlyAuthor,
            ExplicitlyLicensedPerson,
            Everyone,
        }

        public enum VRM1CommercialUsageType
        {
            PersonalNonProfit,
            PersonalProfit,
            Corporation,
        }

        [Serializable] public class OverwriteAllowedUser : Overwrite<AllowedUser> { }
        [Serializable] public class OverwriteVRM1CommercialUsageType : Overwrite<VRM1CommercialUsageType> { }

        public enum VRM0LicenseType {
            Redistribution_Prohibited,
            CC0,
            CC_BY,
            CC_BY_NC,
            CC_BY_SA,
            CC_BY_NC_SA,
            CC_BY_ND,
            CC_BY_NC_ND,
            Other
        }

        public enum VRM1CreditNotationType
        {
            Required,
            Unnecessary
        }

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