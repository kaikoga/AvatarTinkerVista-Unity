using Silksprite.AvatarTinkerVista.Ndmf.VRM1.PlatformDefinition;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRM1
{
    static class AtivExportVRM1UIEditor
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            AtivExportVRMUIEditor.NdmfExportUI += OnNdmfExportUI;
        }

        static void OnNdmfExportUI(VisualElement container, Transform avatarRoot)
        {
            if (VRM1Platform.Instance.CreateBuildUI() is { } vrm1BuildUI)
            {
                vrm1BuildUI.AvatarRoot = avatarRoot.gameObject;
                container.Add(vrm1BuildUI);
            }
        }
    }
}
