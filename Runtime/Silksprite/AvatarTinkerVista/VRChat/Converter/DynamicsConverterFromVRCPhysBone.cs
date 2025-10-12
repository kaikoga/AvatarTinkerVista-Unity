using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Converter;
using Silksprite.AvatarTinkerVista.Utils;
using UnityEngine;
using VRC.Dynamics;

namespace Silksprite.AvatarTinkerVista.VRChat.Converter
{
    public class DynamicsConverterFromVRCPhysBone
    : DynamicsConverterBase<
        Transform,
        VRCPhysBoneBase,
        VRCPhysBoneColliderBase,
        AtivGenerateDynamicsColliderGroup
    >
    {
        protected override bool TryConvertCollider(Transform context, VRCPhysBoneColliderBase pbCollider, out AtivGenerateDynamicsColliderGroup result)
        {
            var secondary = context.transform.FindOrCreateSecondary(pbCollider.gameObject.name);
            result = secondary.gameObject.AddComponent<AtivGenerateDynamicsColliderGroup>();
            var ativCollider = secondary.transform.CreateChild(pbCollider.gameObject.name).gameObject.AddComponent<AtivGenerateDynamicsCollider>();
            ativCollider.rootBone = pbCollider.GetRootTransform();
            switch (pbCollider.shapeType)
            {
                case VRCPhysBoneColliderBase.ShapeType.Sphere:
                    ativCollider.colliderType = pbCollider.insideBounds
                        ? AtivGenerateDynamicsCollider.ColliderTypes.SphereInside
                        : AtivGenerateDynamicsCollider.ColliderTypes.Sphere;
                    ativCollider.offset = pbCollider.position;
                    ativCollider.radius = pbCollider.radius;
                    break;
                case VRCPhysBoneColliderBase.ShapeType.Capsule:
                    ativCollider.colliderType = pbCollider.insideBounds
                        ? AtivGenerateDynamicsCollider.ColliderTypes.CapsuleInside
                        : AtivGenerateDynamicsCollider.ColliderTypes.Capsule;
                    var height = Mathf.Max(pbCollider.height - pbCollider.radius * 2f, 0f);
                    var offset = pbCollider.rotation * Vector3.up * height / 2f;
                    ativCollider.offset = pbCollider.position - offset;
                    ativCollider.tail = pbCollider.position + offset;
                    ativCollider.radius = pbCollider.radius;
                    break;
                case VRCPhysBoneColliderBase.ShapeType.Plane:
                    ativCollider.colliderType = AtivGenerateDynamicsCollider.ColliderTypes.Plane;
                    ativCollider.offset = pbCollider.position;
                    ativCollider.normal = pbCollider.rotation * Vector3.up;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            result.colliders.Add(ativCollider);
            return true;
        }

        protected override void ConvertDynamics(Transform context, VRCPhysBoneBase pb, Dictionary<VRCPhysBoneColliderBase, AtivGenerateDynamicsColliderGroup> ativColliderGroups)
        {
            var rootTransform = pb.GetRootTransform();
            if (rootTransform.childCount == 0)
            {
                return;
            }
            var secondary = context.transform.FindOrCreateSecondary(pb.gameObject.name);
            if (rootTransform.childCount == 1)
            {
                GenerateSpring(rootTransform);
            }
            else
            {
                switch (pb.multiChildType)
                {
                    // FIXME: this is completely different logic from AtivGenerateVrmSpringBones
                    case VRCPhysBoneBase.MultiChildType.Ignore:
                        foreach (var child in rootTransform.OfType<Transform>())
                        {
                            GenerateSpring(child);
                        }
                        break;
                    case VRCPhysBoneBase.MultiChildType.First:
                    case VRCPhysBoneBase.MultiChildType.Average:
                        GenerateSpring(rootTransform);
                        foreach (var child in rootTransform.OfType<Transform>().Skip(1))
                        {
                            GenerateSpring(child);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            void GenerateSpring(Transform root)
            {
                var ativSpringBone = secondary.CreateChild(root.gameObject.name).gameObject.AddComponent<AtivGenerateDynamics>();
                ativSpringBone.rootBone = root;
                // FIXME adjust parameters
                ativSpringBone.stiffness = pb.pull * 4;
                ativSpringBone.gravityPower = pb.gravity;
                ativSpringBone.radius = pb.radius;
                ativSpringBone.dragForce = pb.stiffness;
                ativSpringBone.colliderGroups = pb.colliders
                    .Select(ativColliderGroups.GetValueOrDefault)
                    .Where(ativColliderGroup => ativColliderGroup)
                    .ToList();
            }
        }
    }
}
