using System;
using System.Collections.Generic;
using UniGLTF.Extensions.VRMC_vrm;
using UniVRM10;

namespace Silksprite.AvatarTinkerVista.VRM1.Processors
{
    public static class OverwriteVRM1MetaProcessor
    {
        public static void Process(Vrm10Instance vrmInstance)
        {
            var overwrites = vrmInstance.GetComponentsInChildren<AtivOverwriteVRMMeta>(true);
            if (overwrites.Length == 0) return;

            var vrm = vrmInstance.Vrm;
            if (!vrm) return;
            
            foreach (var overwrite in overwrites)
            {
                DoOverwrite(overwrite, vrm.Meta);
            }
        }

        static void DoOverwrite(AtivOverwriteVRMMeta overwrite, VRM10ObjectMeta newMeta)
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

        static List<string> MapStringList(string atvValue)
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(atvValue)) list.Add(atvValue);
            return list;
        }

        static AvatarPermissionType MapAvatarPermission(AtivOverwriteVRMMeta.AllowedUser atvValue)
        {
            return atvValue switch
            {
                AtivOverwriteVRMMeta.AllowedUser.OnlyAuthor => AvatarPermissionType.onlyAuthor,
                AtivOverwriteVRMMeta.AllowedUser.ExplicitlyLicensedPerson => AvatarPermissionType.onlySeparatelyLicensedPerson,
                AtivOverwriteVRMMeta.AllowedUser.Everyone => AvatarPermissionType.everyone,
                _ => throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null)
            };
        }

        static CommercialUsageType MapCommercialUsage(AtivOverwriteVRMMeta.VRM1CommercialUsageType atvValue)
        {
            return atvValue switch
            {
                AtivOverwriteVRMMeta.VRM1CommercialUsageType.PersonalNonProfit => CommercialUsageType.personalNonProfit,
                AtivOverwriteVRMMeta.VRM1CommercialUsageType.PersonalProfit => CommercialUsageType.personalProfit,
                AtivOverwriteVRMMeta.VRM1CommercialUsageType.Corporation => CommercialUsageType.corporation,
                _ => throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null)
            };
        }

        static ModificationType MapModification(AtivOverwriteVRMMeta.VRM1ModificationType atvValue)
        {
            return atvValue switch
            {
                AtivOverwriteVRMMeta.VRM1ModificationType.Prohibited => ModificationType.prohibited,
                AtivOverwriteVRMMeta.VRM1ModificationType.AllowModification => ModificationType.allowModification,
                AtivOverwriteVRMMeta.VRM1ModificationType.AllowModificationRedistribution => ModificationType.allowModificationRedistribution,
                _ => throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null)
            };
        }

        static CreditNotationType MapCreditNotation(AtivOverwriteVRMMeta.VRM1CreditNotationType atvValue)
        {
            return atvValue switch
            {
                AtivOverwriteVRMMeta.VRM1CreditNotationType.Required => CreditNotationType.required,
                AtivOverwriteVRMMeta.VRM1CreditNotationType.Unnecessary => CreditNotationType.unnecessary,
                _ => throw new ArgumentOutOfRangeException(nameof(atvValue), atvValue, null)
            };
        }
    }
}