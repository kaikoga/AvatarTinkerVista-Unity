using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Silksprite.AvatarTinkerVista.Utils
{
    public class AtivGizmos : IDisposable
    {
        enum CommandKind
        {
            Line,
            Sphere,
            WireSphere,
            WireCapsule,
            Plane
        }

        readonly struct Command
        {
            public readonly CommandKind K;
            public readonly Vector3 V;
            public readonly Vector3 X;
            public readonly float R;
            public readonly Color C;

            public Command(CommandKind k, Vector3 v, Vector3 x, float r, Color c)
            {
                K = k;
                V = v;
                X = x;
                R = r;
                C = c;
            }
        }


        static readonly Stack<List<Command>> Commands = new Stack<List<Command>>();
        readonly List<Command> _commands;

        public Color Color;

        public AtivGizmos()
        {
            if (!Commands.TryPop(out _commands))
            {
                _commands = new List<Command>();
            } 
        }

        public void DrawLineGlobal(Vector3 from, Vector3 to)
        {
            _commands.Add(new Command(CommandKind.Line, from, to, 0f, Color));
        }

        public void DrawSphereLocal(Transform transform, Vector3 localOffset, float radius)
        {
            DrawSphereGlobal(transform.TransformPoint(localOffset), radius);
        }

        void DrawSphereGlobal(Vector3 offset, float radius)
        {
            _commands.Add(new Command(CommandKind.Sphere, offset, Vector3.zero, radius, Color));
        }

        public void DrawWireSphereLocal(Transform transform, Vector3 localOffset, float radius)
        {
            DrawWireSphereGlobal(transform.TransformPoint(localOffset), radius);
        }

        void DrawWireSphereGlobal(Vector3 offset, float radius)
        {
            _commands.Add(new Command(CommandKind.WireSphere, offset, Vector3.zero, radius, Color));
        }

        public void DrawWireCapsuleLocal(Transform transform, Vector3 localFrom, Vector3 localTo, float radius)
        {
            DrawWireCapsuleGlobal(transform.TransformPoint(localFrom), transform.TransformPoint(localTo), radius);
        }

        void DrawWireCapsuleGlobal(Vector3 from, Vector3 to, float radius)
        {
            _commands.Add(new Command(CommandKind.WireCapsule, from, to, radius, Color));
        }

        public void DrawPlaneLocal(Transform transform, Vector3 localOffset, Vector3 localNormal, float radius)
        {
            DrawPlaneGlobal(transform.TransformPoint(localOffset), transform.TransformVector(localNormal), radius);
        }

        void DrawPlaneGlobal(Vector3 offset, Vector3 normal, float radius)
        {
            _commands.Add(new Command(CommandKind.Plane, offset, normal, radius, Color));
        }

        public void Dispose()
        {
            using var impl = new AtivGizmosImpl();
            var matrix = Gizmos.matrix;
            var cp = Vector3.zero; 
            var fv = 1f;
            var fr = 1f;
            var sceneCamera = SceneView.currentDrawingSceneView?.camera;
            if (sceneCamera)
            {
                if (!sceneCamera.orthographic)
                {
                    cp = sceneCamera.transform.position * 3f;
                    fv = 4f;
                    fr = 4f;
                }
            }
            foreach (var c in _commands)
            {
                impl.Color = c.C;
                switch (c.K)
                {
                    case CommandKind.Line:
                        impl.DrawLineGlobal((c.V + cp) / fv, (c.X + cp) / fv);
                        break;
                    case CommandKind.Sphere:
                        impl.DrawSphereGlobal((c.V + cp) / fv, c.R / fr);
                        break;
                    case CommandKind.WireSphere:
                        impl.DrawWireSphereGlobal((c.V + cp) / fv, c.R / fr);
                        break;
                    case CommandKind.WireCapsule:
                        impl.DrawWireCapsuleGlobal((c.V + cp) / fv, (c.X + cp) / fv, c.R / fr);
                        break;
                    case CommandKind.Plane:
                        impl.DrawPlaneGlobal((c.V + cp) / fv, c.X, c.R / fr);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            Gizmos.matrix = matrix;
            _commands.Clear();
            Commands.Push(_commands);
        }
    }
}
