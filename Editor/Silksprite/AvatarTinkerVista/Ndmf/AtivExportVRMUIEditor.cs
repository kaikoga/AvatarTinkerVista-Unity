using nadena.dev.ndmf.runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [CustomEditor(typeof(AtivExportVRMUI))]
    [CanEditMultipleObjects]
    class AtivExportVRMUIEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var exportVrm = (AtivExportVRMUI)target;
            var container = new VisualElement();
            var avatarRoot = RuntimeUtil.FindAvatarInParents(exportVrm.gameObject.transform);
            if (avatarRoot)
            {
#if ATIV_VRM0
                if (VRM0PlatformProvider.Instance.CreateBuildUI() is { } vrm0BuildUI)
                {
                    vrm0BuildUI.AvatarRoot = avatarRoot.gameObject;
                    container.Add(vrm0BuildUI);
                }
#endif
#if ATIV_VRM1
                if (VRM1PlatformProvider.Instance.CreateBuildUI() is { } vrm1BuildUI)
                {
                    vrm1BuildUI.AvatarRoot = avatarRoot.gameObject;
                    container.Add(vrm1BuildUI);
                }
#endif
            }
            return container;
        }
    }
}
