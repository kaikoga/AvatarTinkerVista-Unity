using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Common.Registries
{
    public static class DynamicsMarkerRegistry
    {
        static readonly List<MarkerEntry> Markers = new List<MarkerEntry>();

        public static void Register(Type markerComponentType, string dynamicsId) => Markers.Add(new MarkerEntry(markerComponentType, dynamicsId));

        public static string? GuessDynamicsIdOf(Transform transform)
        {
            return Markers
                .FirstOrDefault(dynamics => transform.GetComponentsInChildren(dynamics.MarkerComponentType).Length > 0)
                .DynamicsId;
        }

        readonly struct MarkerEntry
        {
            public readonly Type MarkerComponentType;
            public readonly string DynamicsId;

            public MarkerEntry(Type markerComponentType, string dynamicsId)
            {
                MarkerComponentType = markerComponentType;
                DynamicsId = dynamicsId;
            }
        }
    }
}