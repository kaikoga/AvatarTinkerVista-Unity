#if ATIV_VRM1

using System;
using System.Collections.Generic;
using Silksprite.AdLib.Utils.VRM1;
using nadena.dev.ndmf;
using UniGLTF.Extensions.VRMC_vrm;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.Ndmf.Passes
{
    class OverwriteVrm1MetaPass : Pass<OverwriteVrm1MetaPass>
    {
        protected override void Execute(BuildContext context)
        {
            var vrmInstance = context.AvatarRootTransform.GetComponent<Vrm10Instance>();
            if (!vrmInstance) return;

            var overwrites = context.AvatarRootTransform.GetComponentsInChildren<AtivOverwriteVrmMeta>(true);
            if (overwrites.Length == 0) return;

            var vrm = vrmInstance.Vrm;
            if (!vrm) return;
            
            var newVrm = new CustomCloneVRM10Object().Clone(vrmInstance.Vrm).mainAsset;
            vrmInstance.Vrm = newVrm;
            foreach (var overwrite in overwrites) DoOverwrite(overwrite, newVrm.Meta);
        }

        void DoOverwrite(AtivOverwriteVrmMeta overwrite, VRM10ObjectMeta newMeta)
        {
            overwrite.nameOrTitle.OverwriteValue(ref newMeta.Name);
            overwrite.version.OverwriteValue(ref newMeta.Version);
            overwrite.author.OverwriteValue(ref newMeta.Authors, MapStringList);
            newMeta.Authors.AddRange(overwrite.vrm1MoreAuthors);
            overwrite.vrm1CopyrightInformation.OverwriteValue(ref newMeta.Name);
            overwrite.contactInformation.OverwriteValue(ref newMeta.Name);
            overwrite.reference.OverwriteValue(ref newMeta.References, MapStringList);
            newMeta.References.AddRange(overwrite.vrm1MoreReferences);
            overwrite.vrm1ThirdPartyLicenses.OverwriteValue(ref newMeta.ThirdPartyLicenses);
            overwrite.thumbnail.OverwriteValue(ref newMeta.Thumbnail);

            overwrite.allowedUser.OverwriteValue(ref newMeta.AvatarPermission, MapAvatarPermission);
            overwrite.violentUsage.OverwriteValue(ref newMeta.ViolentUsage);
            overwrite.sexualUsage.OverwriteValue(ref newMeta.SexualUsage);
            overwrite.vrm1CommercialUsage.OverwriteValue(ref newMeta.CommercialUsage, MapCommercialUsage);
            overwrite.vrm1PoliticalOrReligiousUsage.OverwriteValue(ref newMeta.PoliticalOrReligiousUsage);
            overwrite.vrm1AntisocialOrHateUsage.OverwriteValue(ref newMeta.AntisocialOrHateUsage);

            overwrite.vrm1CreditNotation.OverwriteValue(ref newMeta.CreditNotation, MapCreditNotation);
            overwrite.vrm1Redistribution.OverwriteValue(ref newMeta.Redistribution);
            overwrite.vrm1Modification.OverwriteValue(ref newMeta.Modification, MapModification);
            overwrite.otherLicenseUrl.OverwriteValue(ref newMeta.OtherLicenseUrl);
        }

        List<string> MapStringList(string atvValue)
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(atvValue)) list.Add(atvValue);
            return list;
        }
        AvatarPermissionType MapAvatarPermission(AtivOverwriteVrmMeta.AllowedUser atvValue)
        {
            switch (atvValue)
            {
                case AtivOverwriteVrmMeta.AllowedUser.OnlyAuthor:
                    return AvatarPermissionType.onlyAuthor;
                case AtivOverwriteVrmMeta.AllowedUser.ExplicitlyLicensedPerson:
                    return AvatarPermissionType.onlySeparatelyLicensedPerson;
                case AtivOverwriteVrmMeta.AllowedUser.Everyone:
                    return AvatarPermissionType.everyone;
                default:
                    throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null);
            }
        }

        CommercialUsageType MapCommercialUsage(AtivOverwriteVrmMeta.Vrm1CommercialUsageType atvValue)
        {
            switch (atvValue)
            {
                case AtivOverwriteVrmMeta.Vrm1CommercialUsageType.PersonalNonProfit:
                    return CommercialUsageType.personalNonProfit;
                case AtivOverwriteVrmMeta.Vrm1CommercialUsageType.PersonalProfit:
                    return CommercialUsageType.personalProfit;
                case AtivOverwriteVrmMeta.Vrm1CommercialUsageType.Corporation:
                    return CommercialUsageType.corporation;
                default:
                    throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null);
            }
        }

        ModificationType MapModification(AtivOverwriteVrmMeta.Vrm1ModificationType atvValue)
        {
            switch (atvValue)
            {
                case AtivOverwriteVrmMeta.Vrm1ModificationType.Prohibited:
                    return ModificationType.prohibited;
                case AtivOverwriteVrmMeta.Vrm1ModificationType.AllowModification:
                    return ModificationType.allowModification;
                case AtivOverwriteVrmMeta.Vrm1ModificationType.AllowModificationRedistribution:
                    return ModificationType.allowModificationRedistribution;
                default:
                    throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null);
            }
        }

        CreditNotationType MapCreditNotation(AtivOverwriteVrmMeta.Vrm1CreditNotationType atvValue)
        {
            switch (atvValue)
            {
                case AtivOverwriteVrmMeta.Vrm1CreditNotationType.Required:
                    return CreditNotationType.required;
                case AtivOverwriteVrmMeta.Vrm1CreditNotationType.Unnecessary:
                    return CreditNotationType.unnecessary;
                default:
                    throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null);
            }
        }
    }
}

#endif
