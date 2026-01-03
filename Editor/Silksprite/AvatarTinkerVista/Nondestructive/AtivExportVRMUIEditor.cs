using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#if ATIV_UNIVRM_VRM0 || ATIV_UNIVRM_VRM1

#if ATIV_NDMF
using nadena.dev.ndmf.runtime;
using Silksprite.AvatarTinkerVista.Ndmf.VRM0.PlatformDefinition;
using Silksprite.AvatarTinkerVista.Ndmf.VRM1.PlatformDefinition;
#endif

#if ATIV_ABLET
using Ablet;
using Ablet.EditorAPI.V1.Extensions.Platform;
using Ablet.Models.Extensions;
using Ablet.Repositories;
#endif

#endif

namespace Silksprite.AvatarTinkerVista.Nondestructive
{
    [CustomEditor(typeof(AtivExportVRMUI))]
    [CanEditMultipleObjects]
    class AtivExportVRMUIEditor : Editor
    {

#if ATIV_UNIVRM_VRM0 || ATIV_UNIVRM_VRM1

#if ATIV_ABLET
        static bool MayNdmfExport => !EditorSettingsRepository.Instance.Value.IsNdmfOnAblet;
        static bool MayAbletExport => !EditorSettingsRepository.Instance.Value.IsAbletOnNdmf;
#else
        static bool MayNdmfExport => true;
        static bool MayAbletExport => false;
#endif

        public override VisualElement CreateInspectorGUI()
        {
            var exportVrm = (AtivExportVRMUI)target;
            var container = new VisualElement();

#if ATIV_NDMF
            if (MayNdmfExport)
            {
                var avatarRoot = RuntimeUtil.FindAvatarInParents(exportVrm.gameObject.transform);
                if (avatarRoot)
                {
                    container.Add(new Label("Export with NDMF")
                    {
                        style =
                        {
                            unityFontStyleAndWeight = FontStyle.Bold
                        }
                    });
#if ATIV_UNIVRM_VRM0
                    if (VRM0Platform.Instance.CreateBuildUI() is { } vrm0BuildUI)
                    {
                        vrm0BuildUI.AvatarRoot = avatarRoot.gameObject;
                        container.Add(vrm0BuildUI);
                    }
#endif
#if ATIV_UNIVRM_VRM1
                    if (VRM1Platform.Instance.CreateBuildUI() is { } vrm1BuildUI)
                    {
                        vrm1BuildUI.AvatarRoot = avatarRoot.gameObject;
                        container.Add(vrm1BuildUI);
                    }
#endif
                }
            }
#endif

#if ATIV_ABLET
            if (MayAbletExport)
            {
                var entrypoint = AbletFacade.GetEntrypointFor(exportVrm.gameObject);
                if (entrypoint.gameObject
                    && entrypoint.platform.TryGetExtensionDef<IExportUIExtension>(out var exportUI))
                {
                    container.Add(new Label("Export with Ablet")
                    {
                        style =
                        {
                            unityFontStyleAndWeight = FontStyle.Bold
                        }
                    });
                    container.Add(exportUI.RenderExportUI(entrypoint.gameObject));
                }
            }
#endif

            return container;
        }

#endif
    }
}
