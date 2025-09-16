using System.Diagnostics;

namespace Silksprite.AvatarTinkerVista.Utils
{
    public static class AtivEditorUtil
    {
        public static void OpenInExplorer(string directoryPath)
        {
#if UNITY_EDITOR_WIN
            Process.Start("explorer.exe", directoryPath);
#else
            Process.Start("open", directoryPath);
#endif
        }
    }
}
