using Silksprite.AvatarTinkerVista.Ndmf.VRM0.PlatformDefinition;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM0
{
    static class AtivExportVRM0UIEditor
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            AtivExportVRMUIEditor.NdmfExportUI += OnNdmfExportUI;
        }

        static void OnNdmfExportUI(VisualElement container, Transform avatarRoot)
        {
            if (VRM0Platform.Instance.CreateBuildUI() is { } vrm0BuildUI)
            {
                vrm0BuildUI.AvatarRoot = avatarRoot.gameObject;
                container.Add(vrm0BuildUI);
            }
        }
    }
}
