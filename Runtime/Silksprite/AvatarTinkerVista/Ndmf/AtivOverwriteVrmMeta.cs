using System;
using System.Collections.Generic;
using Silksprite.AvatarTinkerVista.Ndmf.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace Silksprite.AvatarTinkerVista
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Avatar Tinker Vista/ATiV Overwrite VRM0+1 Meta")]
    public class AtivOverwriteVrmMeta : AtivGeneratingComponent
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
        public OverwriteVrm1CommercialUsageType vrm1CommercialUsage;
        public OverwriteBool vrm1PoliticalOrReligiousUsage;
        public OverwriteBool vrm1AntisocialOrHateUsage;
        public OverwriteString vrm0OtherPermissionUrl;

        #endregion

        #region Distribution License
        
        [Header("Distribution License")]
        public OverwriteVrm0LicenseType vrm0LicenseType;
        public OverwriteVrm1CreditNotationType vrm1CreditNotation;
        public OverwriteBool vrm1Redistribution;
        public OverwriteVrm1ModificationType vrm1Modification;
        public OverwriteString otherLicenseUrl;
        
        #endregion
        
        #region core

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
        
        #endregion

        #region enums

        public enum AllowedUser
        {
            OnlyAuthor,
            ExplicitlyLicensedPerson,
            Everyone,
        }

        public enum Vrm1CommercialUsageType
        {
            PersonalNonProfit,
            PersonalProfit,
            Corporation,
        }

        [Serializable] public class OverwriteAllowedUser : Overwrite<AllowedUser> { }
        [Serializable] public class OverwriteVrm1CommercialUsageType : Overwrite<Vrm1CommercialUsageType> { }

        public enum Vrm0LicenseType {
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

        public enum Vrm1CreditNotationType
        {
            Required,
            Unnecessary
        }

        public enum Vrm1ModificationType
        {
            Prohibited,
            AllowModification,
            AllowModificationRedistribution
        }
        
        [Serializable] public class OverwriteVrm0LicenseType : Overwrite<Vrm0LicenseType> { }
        [Serializable] public class OverwriteVrm1CreditNotationType : Overwrite<Vrm1CreditNotationType> { }
        [Serializable] public class OverwriteVrm1ModificationType : Overwrite<Vrm1ModificationType> { }

        #endregion
    }
}