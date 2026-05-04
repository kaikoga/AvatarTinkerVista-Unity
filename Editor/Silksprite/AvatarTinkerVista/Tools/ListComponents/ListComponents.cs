using System;
using System.Linq;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Tools.ListComponents
{
    [Serializable]
    public class ListComponents
    {
        public Transform avatarRoot = null!;
        public string[] componentNames = { };

        public void Refresh()
        {
            componentNames = avatarRoot.GetComponentsInChildren<Component>()
                .Where(component => component)
                .Select(component => component.GetType().FullName)
                .Distinct()
                .OrderBy(typeName => typeName)
                .ToArray();
        }
    }
}