using JetBrains.Annotations;
using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.Loch.UIElements.LEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Silksprite.Loch.Tools.LochTool;

#if ATIV_NDMF
using nadena.dev.ndmf.runtime;
#endif

#if ATIV_ABLET
using Ablet;
using Ablet.EditorAPI.V1.Extensions.Platform;
using Ablet.Models.Extensions;
using Ablet.Repositories;
#endif

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivExportVRMUI))]
    [CanEditMultipleObjects]
    public class AtivExportVRMUIEditor : AtivEditorBase
    {

#if ATIV_DETECTED_VRM0 || ATIV_DETECTED_VRM1

#if ATIV_ABLET
        static bool MayNdmfExport => !EditorSettingsRepository.Instance.Value.IsNdmfOnAblet;
        static bool MayAbletExport => !EditorSettingsRepository.Instance.Value.IsAbletOnNdmf;
#else
        static bool MayNdmfExport => true;
        static bool MayAbletExport => false;
#endif

        public delegate void NdmfExportUIHandler(VisualElement container, Transform avatarRoot);
        [PublicAPI]
        public static event NdmfExportUIHandler? NdmfExportUI;

        public override VisualElement CreateInspectorGUI()
        {
            var exportVrm = (AtivExportVRMUI)target;
            var container = new VisualElement();
            container.Add(new GlobalLocaleSelector());

#if ATIV_NDMF
            if (MayNdmfExport)
            {
                var avatarRoot = RuntimeUtil.FindAvatarInParents(exportVrm.gameObject.transform);
                if (avatarRoot != null)
                {
                    container.Add(new Loch.UIElements.Heading
                    {
                        text = "Export With NDMF",
                        loc = Loc("AtivExportVRMUI.ExportWithNDMF")
                    });
                    NdmfExportUI?.Invoke(container, avatarRoot);
                }
            }
#endif

#if ATIV_ABLET
            if (MayAbletExport)
            {
                if (AbletFacade.TryGetEntrypointFor(exportVrm.gameObject, out var entrypointObject, out var platform)
                    && platform.TryGetExtensionDef<IExportUIExtension>(out var exportUI))
                {
                    container.Add(new Loch.UIElements.Heading
                    {
                        text = "Export With Ablet",
                        loc = Loc("AtivExportVRMUI.ExportWithAblet")
                    });
                    container.Add(exportUI.RenderExportUI(entrypointObject));
                }
            }
#endif

            return container;
        }

#endif
    }
}
