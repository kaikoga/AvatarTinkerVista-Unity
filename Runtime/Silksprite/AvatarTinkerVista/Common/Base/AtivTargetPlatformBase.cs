using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Common.DataObjects;
using Silksprite.Loch.Attributes;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Base
{
    public abstract class AtivTargetPlatformBase : AtivGeneratingComponent
    {
        public TargetPlatformMode targetPlatformMode = TargetPlatformMode.Exclude;
        public List<string> platformIds = new List<string>();

        public virtual bool UseOutputPlatform => false;

        public abstract IEnumerable<AtivPlatformHandle> AllPlatforms();

        public abstract string SelectedPlatformId();

        bool GetIsTargetPlatform(string platformId)
        {
            return targetPlatformMode switch
            {
                TargetPlatformMode.Include => platformIds.Contains(platformId),
                TargetPlatformMode.Exclude => !platformIds.Contains(platformId),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public bool GetIsTargetPlatform(AtivPlatformHandle platformHandle) => GetIsTargetPlatform(platformHandle.Id);

        public void SetIsTargetPlatform(AtivPlatformHandle platformHandle, bool value)
        {
            switch (focusPlatformMode: targetPlatformMode, value)
            {
                case (TargetPlatformMode.Include, false):
                case (TargetPlatformMode.Exclude, true):
                    platformIds.Remove(platformHandle.Id);
                    break;
                case (TargetPlatformMode.Include, true):
                case (TargetPlatformMode.Exclude, false):
                    platformIds.Add(platformHandle.Id);
                    platformIds = platformIds.Distinct().ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetAllTargetPlatform(bool value)
        {
            platformIds = (focusPlatformMode: targetPlatformMode, value) switch
            {
                (TargetPlatformMode.Include, false) or (TargetPlatformMode.Exclude, true) => new List<string>(),
                (TargetPlatformMode.Include, true) or (TargetPlatformMode.Exclude, false) => AllPlatforms().Select(platform => platform.Id).ToList(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void SetTargetPlatformMode(TargetPlatformMode value)
        {
            var allPlatformIds = AllPlatforms().Select(platform => platform.Id).ToList();
            var includePlatformIds = allPlatformIds.Where(GetIsTargetPlatform).ToList();
            targetPlatformMode = value;
            switch (value)
            {
                case TargetPlatformMode.Include:
                    platformIds = includePlatformIds;
                    break;
                case TargetPlatformMode.Exclude:
                    platformIds = allPlatformIds.Except(includePlatformIds).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void ApplyTarget()
        {
            if (!GetIsTargetPlatform(SelectedPlatformId()))
            {
                DestroyImmediate(gameObject);
            }
        }

        public static void ApplyToAvatarRoot(Transform avatarRoot, bool forOutputPlatform)
        {
            var components = avatarRoot.GetComponentsInChildren<AtivTargetPlatformBase>(true)
                .Where(component => component.UseOutputPlatform == forOutputPlatform);
            foreach (var component in components)
            {
                component.ApplyTarget();
            }
        }

        [LEnum]
        public enum TargetPlatformMode
        {
            Include,
            Exclude
        }
    }
}