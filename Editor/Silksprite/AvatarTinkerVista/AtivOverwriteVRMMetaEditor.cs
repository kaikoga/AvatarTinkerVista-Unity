using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivOverwriteVRMMeta))]
    [CanEditMultipleObjects]
    class AtivOverwriteVRMMetaEditor : AtivEditorBase
    {
        LocalizedProperty _nameOrTitle = null!;
        LocalizedProperty _version = null!;
        LocalizedProperty _author = null!;
        LocalizedProperty _vrm1MoreAuthors = null!;

        LocalizedProperty _vrm1CopyrightInformation = null!;
        LocalizedProperty _contactInformation = null!;
        LocalizedProperty _reference = null!;
        LocalizedProperty _vrm1MoreReferences = null!;
        LocalizedProperty _vrm1ThirdPartyLicenses = null!;
        LocalizedProperty _thumbnail = null!;

        LocalizedProperty _allowedUser = null!;
        LocalizedProperty _violentUsage = null!;
        LocalizedProperty _sexualUsage = null!;
        LocalizedProperty _vrm0CommercialUsage = null!;
        LocalizedProperty _vrm1CommercialUsage = null!;
        LocalizedProperty _vrm1PoliticalOrReligiousUsage = null!;
        LocalizedProperty _vrm1AntisocialOrHateUsage = null!;
        LocalizedProperty _vrm0OtherPermissionUrl = null!;

        LocalizedProperty _vrm0LicenseType = null!;
        LocalizedProperty _vrm1CreditNotation = null!;
        LocalizedProperty _vrm1Redistribution = null!;
        LocalizedProperty _vrm1Modification = null!;
        LocalizedProperty _otherLicenseUrl = null!;

        void OnEnable()
        {
            _nameOrTitle = Lop(nameof(AtivOverwriteVRMMeta.nameOrTitle), Loc("AtivOverwriteVRMMeta::nameOrTitle"));
            _version = Lop(nameof(AtivOverwriteVRMMeta.version), Loc("AtivOverwriteVRMMeta::version"));
            _author = Lop(nameof(AtivOverwriteVRMMeta.author), Loc("AtivOverwriteVRMMeta::author"));
            _vrm1MoreAuthors = Lop(nameof(AtivOverwriteVRMMeta.vrm1MoreAuthors), Loc("AtivOverwriteVRMMeta::vrm1MoreAuthors"));
            
            _vrm1CopyrightInformation = Lop(nameof(AtivOverwriteVRMMeta.vrm1CopyrightInformation), Loc("AtivOverwriteVRMMeta::vrm1CopyrightInformation"));
            _contactInformation = Lop(nameof(AtivOverwriteVRMMeta.contactInformation), Loc("AtivOverwriteVRMMeta::contactInformation"));
            _reference = Lop(nameof(AtivOverwriteVRMMeta.reference), Loc("AtivOverwriteVRMMeta::reference"));
            _vrm1MoreReferences = Lop(nameof(AtivOverwriteVRMMeta.vrm1MoreReferences), Loc("AtivOverwriteVRMMeta::vrm1MoreReferences"));
            _vrm1ThirdPartyLicenses = Lop(nameof(AtivOverwriteVRMMeta.vrm1ThirdPartyLicenses), Loc("AtivOverwriteVRMMeta::vrm1ThirdPartyLicenses"));
            _thumbnail = Lop(nameof(AtivOverwriteVRMMeta.thumbnail), Loc("AtivOverwriteVRMMeta::thumbnail"));
            
            _allowedUser = Lop(nameof(AtivOverwriteVRMMeta.allowedUser), Loc("AtivOverwriteVRMMeta::allowedUser"));
            _violentUsage = Lop(nameof(AtivOverwriteVRMMeta.violentUsage), Loc("AtivOverwriteVRMMeta::violentUsage"));
            _sexualUsage = Lop(nameof(AtivOverwriteVRMMeta.sexualUsage), Loc("AtivOverwriteVRMMeta::sexualUsage"));
            _vrm0CommercialUsage = Lop(nameof(AtivOverwriteVRMMeta.vrm0CommercialUsage), Loc("AtivOverwriteVRMMeta::vrm0CommercialUsage"));
            _vrm1CommercialUsage = Lop(nameof(AtivOverwriteVRMMeta.vrm1CommercialUsage), Loc("AtivOverwriteVRMMeta::vrm1CommercialUsage"));
            _vrm1PoliticalOrReligiousUsage = Lop(nameof(AtivOverwriteVRMMeta.vrm1PoliticalOrReligiousUsage), Loc("AtivOverwriteVRMMeta::vrm1PoliticalOrReligiousUsage"));
            _vrm1AntisocialOrHateUsage = Lop(nameof(AtivOverwriteVRMMeta.vrm1AntisocialOrHateUsage), Loc("AtivOverwriteVRMMeta::vrm1AntisocialOrHateUsage"));
            _vrm0OtherPermissionUrl = Lop(nameof(AtivOverwriteVRMMeta.vrm0OtherPermissionUrl), Loc("AtivOverwriteVRMMeta::vrm0OtherPermissionUrl"));
            
            _vrm0LicenseType = Lop(nameof(AtivOverwriteVRMMeta.vrm0LicenseType), Loc("AtivOverwriteVRMMeta::vrm0LicenseType"));
            _vrm1CreditNotation = Lop(nameof(AtivOverwriteVRMMeta.vrm1CreditNotation), Loc("AtivOverwriteVRMMeta::vrm1CreditNotation"));
            _vrm1Redistribution = Lop(nameof(AtivOverwriteVRMMeta.vrm1Redistribution), Loc("AtivOverwriteVRMMeta::vrm1Redistribution"));
            _vrm1Modification = Lop(nameof(AtivOverwriteVRMMeta.vrm1Modification), Loc("AtivOverwriteVRMMeta::vrm1Modification"));
            _otherLicenseUrl = Lop(nameof(AtivOverwriteVRMMeta.otherLicenseUrl), Loc("AtivOverwriteVRMMeta::otherLicenseUrl"));
        }

        protected override void OnInnerInspectorGUI()
        {
            LGUILayout.Heading(Loc("AtivOverwriteVRMMeta::Info"));
            LEditorGUILayout.Prop(_nameOrTitle);
            LEditorGUILayout.Prop(_version);
            LEditorGUILayout.Prop(_author);
            LEditorGUILayout.Prop(_vrm1MoreAuthors);

            LEditorGUILayout.Prop(_vrm1CopyrightInformation);
            LEditorGUILayout.Prop(_contactInformation);
            LEditorGUILayout.Prop(_reference);
            LEditorGUILayout.Prop(_vrm1MoreReferences);
            LEditorGUILayout.Prop(_vrm1ThirdPartyLicenses);
            LEditorGUILayout.Prop(_thumbnail);
            
            LGUILayout.Heading(Loc("AtivOverwriteVRMMeta::Permission"));
            LEditorGUILayout.Prop(_allowedUser);
            LEditorGUILayout.Prop(_violentUsage);
            LEditorGUILayout.Prop(_sexualUsage);
            LEditorGUILayout.Prop(_vrm0CommercialUsage);
            LEditorGUILayout.Prop(_vrm1CommercialUsage);
            LEditorGUILayout.Prop(_vrm1PoliticalOrReligiousUsage);
            LEditorGUILayout.Prop(_vrm1AntisocialOrHateUsage);
            LEditorGUILayout.Prop(_vrm0OtherPermissionUrl);

            LGUILayout.Heading(Loc("AtivOverwriteVRMMeta::DistributionLicense"));
            LEditorGUILayout.Prop(_vrm0LicenseType);
            LEditorGUILayout.Prop(_vrm1CreditNotation);
            LEditorGUILayout.Prop(_vrm1Redistribution);
            LEditorGUILayout.Prop(_vrm1Modification);
            LEditorGUILayout.Prop(_otherLicenseUrl);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
