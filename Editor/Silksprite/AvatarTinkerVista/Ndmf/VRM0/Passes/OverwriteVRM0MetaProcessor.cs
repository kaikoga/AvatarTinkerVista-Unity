using System;
using Silksprite.AdLib.Utils.VRM0;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0.Passes
{
    public static class OverwriteVRM0MetaProcessor
    {
        public static void Process(VRMMeta vrmMeta)
        {
            var overwrites = vrmMeta.GetComponentsInChildren<AtivOverwriteVRMMeta>(true);
            if (overwrites.Length == 0) return;

            var meta = vrmMeta.Meta;
            if (!meta) return;
            
            var newMeta = new CustomCloneVRMMetaObject().Clone(vrmMeta.Meta).mainAsset;
            vrmMeta.Meta = newMeta;
            foreach (var overwrite in overwrites)
            {
                DoOverwrite(overwrite, newMeta);
            }
        }

        static void DoOverwrite(AtivOverwriteVRMMeta overwrite, VRMMetaObject newMeta)
        {
            overwrite.nameOrTitle.OverwriteValue(ref newMeta.Title);
            overwrite.version.OverwriteValue(ref newMeta.Version);
            overwrite.author.OverwriteValue(ref newMeta.Author);
            overwrite.contactInformation.OverwriteValue(ref newMeta.ContactInformation);
            overwrite.reference.OverwriteValue(ref newMeta.Reference);
            overwrite.thumbnail.OverwriteValue(ref newMeta.Thumbnail);

            overwrite.allowedUser.OverwriteValue(ref newMeta.AllowedUser, MapAllowedUser);
            overwrite.violentUsage.OverwriteValue(ref newMeta.ViolentUssage, MapUsageLicense);
            overwrite.sexualUsage.OverwriteValue(ref newMeta.SexualUssage, MapUsageLicense);
            overwrite.vrm0CommercialUsage.OverwriteValue(ref newMeta.CommercialUssage, MapUsageLicense);
            overwrite.vrm0OtherPermissionUrl.OverwriteValue(ref newMeta.OtherPermissionUrl);

            overwrite.vrm0LicenseType.OverwriteValue(ref newMeta.LicenseType, MapLicenseType);
            overwrite.otherLicenseUrl.OverwriteValue(ref newMeta.OtherLicenseUrl);
        }

        static AllowedUser MapAllowedUser(AtivOverwriteVRMMeta.AllowedUser atvValue)
        {
            return atvValue switch
            {
                AtivOverwriteVRMMeta.AllowedUser.OnlyAuthor => AllowedUser.OnlyAuthor,
                AtivOverwriteVRMMeta.AllowedUser.ExplicitlyLicensedPerson => AllowedUser.ExplicitlyLicensedPerson,
                AtivOverwriteVRMMeta.AllowedUser.Everyone => AllowedUser.Everyone,
                _ => throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null)
            };
        }

        static UssageLicense MapUsageLicense(bool atvValue)
        {
            return atvValue ? UssageLicense.Allow : UssageLicense.Disallow;
        }

        static LicenseType MapLicenseType(AtivOverwriteVRMMeta.VRM0LicenseType atvValue)
        {
            return atvValue switch
            {
                AtivOverwriteVRMMeta.VRM0LicenseType.Redistribution_Prohibited => LicenseType.Redistribution_Prohibited,
                AtivOverwriteVRMMeta.VRM0LicenseType.CC0 => LicenseType.CC0,
                AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY => LicenseType.CC_BY,
                AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY_NC => LicenseType.CC_BY_NC,
                AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY_SA => LicenseType.CC_BY_SA,
                AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY_NC_SA => LicenseType.CC_BY_NC_SA,
                AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY_ND => LicenseType.CC_BY_ND,
                AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY_NC_ND => LicenseType.CC_BY_NC_ND,
                AtivOverwriteVRMMeta.VRM0LicenseType.Other => LicenseType.Other,
                _ => throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null)
            };
        }
    }
}