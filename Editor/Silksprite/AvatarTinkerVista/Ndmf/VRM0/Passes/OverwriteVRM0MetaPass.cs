using System;
using Silksprite.AdLib.Utils.VRM0;
using nadena.dev.ndmf;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class OverwriteVRM0MetaPass : Pass<OverwriteVRM0MetaPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmMeta = context.AvatarRootTransform.GetComponent<VRMMeta>();
            if (!vrmMeta) return;

            var overwrites = context.AvatarRootTransform.GetComponentsInChildren<AtivOverwriteVRMMeta>(true);
            if (overwrites.Length == 0) return;

            var meta = vrmMeta.Meta;
            if (!meta) return;
            
            var newMeta = new CustomCloneVRMMetaObject().Clone(vrmMeta.Meta).mainAsset;
            vrmMeta.Meta = newMeta;
            foreach (var overwrite in overwrites) DoOverwrite(overwrite, newMeta);
        }

        void DoOverwrite(AtivOverwriteVRMMeta overwrite, VRMMetaObject newMeta)
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

        AllowedUser MapAllowedUser(AtivOverwriteVRMMeta.AllowedUser atvValue)
        {
            switch (atvValue)
            {
                case AtivOverwriteVRMMeta.AllowedUser.OnlyAuthor:
                    return AllowedUser.OnlyAuthor;
                case AtivOverwriteVRMMeta.AllowedUser.ExplicitlyLicensedPerson:
                    return AllowedUser.ExplicitlyLicensedPerson;
                case AtivOverwriteVRMMeta.AllowedUser.Everyone:
                    return AllowedUser.Everyone;
                default:
                    throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null);
            }
        }

        UssageLicense MapUsageLicense(bool atvValue)
        {
            return atvValue ? UssageLicense.Allow : UssageLicense.Disallow;
        }

        LicenseType MapLicenseType(AtivOverwriteVRMMeta.VRM0LicenseType atvValue)
        {
            switch (atvValue)
            {
                case AtivOverwriteVRMMeta.VRM0LicenseType.Redistribution_Prohibited:
                    return LicenseType.Redistribution_Prohibited;
                case AtivOverwriteVRMMeta.VRM0LicenseType.CC0:
                    return LicenseType.CC0;
                case AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY:
                    return LicenseType.CC_BY;
                case AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY_NC:
                    return LicenseType.CC_BY_NC;
                case AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY_SA:
                    return LicenseType.CC_BY_SA;
                case AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY_NC_SA:
                    return LicenseType.CC_BY_NC_SA;
                case AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY_ND:
                    return LicenseType.CC_BY_ND;
                case AtivOverwriteVRMMeta.VRM0LicenseType.CC_BY_NC_ND:
                    return LicenseType.CC_BY_NC_ND;
                case AtivOverwriteVRMMeta.VRM0LicenseType.Other:
                    return LicenseType.Other;
                default:
                    throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null);
            }
        }
    }
}
