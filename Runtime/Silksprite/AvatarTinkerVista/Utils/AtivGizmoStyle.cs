using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Utils
{
    public class AtivGizmoStyle
    {
        const string IsDarkModeKey = "net.kaikoga.ativ.AtivGizmoStyle.IsDarkMode";

        [InitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            IsDarkMode = PlayerPrefs.GetInt(IsDarkModeKey) != 0;            
        }

        static bool _isDarkMode;
        public static bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value;
                PlayerPrefs.SetInt(IsDarkModeKey, value ? 1 : 0);
            }
        }

        public static AtivGizmoStyle Current => IsDarkMode ? Dark : Light;

        static readonly AtivGizmoStyle Light = new AtivGizmoStyle
        {
            JointRoot = new Color(1, 0.75f, 0f),
            Joint = Color.yellow,
            Collider = Color.magenta,
            InnerCollider = new Color(0.5f, 0, 1.0f),
        };

        static readonly AtivGizmoStyle Dark = new AtivGizmoStyle
        {
            JointRoot = new Color(0.25f, 0.1f, 0f),
            Joint = new Color(0.2f, 0.25f, 0f),
            Collider = new Color(0.2f, 0f, 0.4f),
            InnerCollider = new Color(0f, 0f, 0.4f),
        };
        
        public Color JointRoot { get; private set; }
        public Color Joint { get; private set; }
        public Color Collider { get; private set; }
        public Color InnerCollider { get; private set; }
    }
}
