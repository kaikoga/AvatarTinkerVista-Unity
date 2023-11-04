#if ATV_VRM0

using System;
using LibVRMTool.Utils.VRM0;
using nadena.dev.ndmf;
using VRM;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class OverwriteVrm0MetaPass : Pass<OverwriteVrm0MetaPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmMeta = context.AvatarRootTransform.GetComponent<VRMMeta>();
            if (!vrmMeta) return;

            var overwrites = context.AvatarRootTransform.GetComponentsInChildren<AtvOverwriteVrmMeta>();
            if (overwrites.Length == 0) return;

            var meta = vrmMeta.Meta;
            if (!meta) return;
            
            var newMeta = new CustomCloneVRMMetaObject().Clone(vrmMeta.Meta).mainAsset;
            vrmMeta.Meta = newMeta;
            foreach (var overwrite in overwrites) DoOverwrite(overwrite, newMeta);
        }

        void DoOverwrite(AtvOverwriteVrmMeta overwrite, VRMMetaObject newMeta)
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

        AllowedUser MapAllowedUser(AtvOverwriteVrmMeta.AllowedUser atvValue)
        {
            switch (atvValue)
            {
                case AtvOverwriteVrmMeta.AllowedUser.OnlyAuthor:
                    return AllowedUser.OnlyAuthor;
                case AtvOverwriteVrmMeta.AllowedUser.ExplicitlyLicensedPerson:
                    return AllowedUser.ExplicitlyLicensedPerson;
                case AtvOverwriteVrmMeta.AllowedUser.Everyone:
                    return AllowedUser.Everyone;
                default:
                    throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null);
            }
        }

        UssageLicense MapUsageLicense(bool atvValue)
        {
            return atvValue ? UssageLicense.Allow : UssageLicense.Disallow;
        }

        LicenseType MapLicenseType(AtvOverwriteVrmMeta.Vrm0LicenseType atvValue)
        {
            switch (atvValue)
            {
                case AtvOverwriteVrmMeta.Vrm0LicenseType.Redistribution_Prohibited:
                    return LicenseType.Redistribution_Prohibited;
                case AtvOverwriteVrmMeta.Vrm0LicenseType.CC0:
                    return LicenseType.CC0;
                case AtvOverwriteVrmMeta.Vrm0LicenseType.CC_BY:
                    return LicenseType.CC_BY;
                case AtvOverwriteVrmMeta.Vrm0LicenseType.CC_BY_NC:
                    return LicenseType.CC_BY_NC;
                case AtvOverwriteVrmMeta.Vrm0LicenseType.CC_BY_SA:
                    return LicenseType.CC_BY_SA;
                case AtvOverwriteVrmMeta.Vrm0LicenseType.CC_BY_NC_SA:
                    return LicenseType.CC_BY_NC_SA;
                case AtvOverwriteVrmMeta.Vrm0LicenseType.CC_BY_ND:
                    return LicenseType.CC_BY_ND;
                case AtvOverwriteVrmMeta.Vrm0LicenseType.CC_BY_NC_ND:
                    return LicenseType.CC_BY_NC_ND;
                case AtvOverwriteVrmMeta.Vrm0LicenseType.Other:
                    return LicenseType.Other;
                default:
                    throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null);
            }
        }
    }
}

#endif
