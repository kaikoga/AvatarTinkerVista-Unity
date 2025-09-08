using System;
using System.Collections.Generic;
using System.Linq;
using Silksprite.AvatarTinkerVista.Utils;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Dynamics.PhysBone.Components;

namespace Silksprite.AvatarTinkerVista.Ndmf.VRChat
{
    public class DynamicsConverterFromVRCPhysBone
    {
        public void Convert(GameObject context)
        {
            var ativColliderGroups = new Dictionary<VRCPhysBoneColliderBase, AtivGenerateVRMSpringBoneColliderGroup>();
            foreach (var pbCollider in context.GetComponentsInChildren<VRCPhysBoneCollider>())
            {
                var ativColliderGroup = GenerateSpringBoneColliderGroup(context, pbCollider);
                if (ativColliderGroup)
                {
                    ativColliderGroups.Add(pbCollider, ativColliderGroup);
                }
            }
            foreach (var pb in context.GetComponentsInChildren<VRCPhysBone>())
            {
                GenerateSpringBones(context, pb, ativColliderGroups);
            }
        }

        AtivGenerateVRMSpringBoneColliderGroup GenerateSpringBoneColliderGroup(GameObject context, VRCPhysBoneCollider pbCollider)
        {
            var secondary = context.transform.FindOrCreateSecondary(pbCollider.gameObject.name);
            var ativCollider = secondary.gameObject.AddComponent<AtivGenerateVRMSpringBoneCollider>();
            ativCollider.rootBone = pbCollider.transform;
            switch (pbCollider.shapeType)
            {
                case VRCPhysBoneColliderBase.ShapeType.Sphere:
                    ativCollider.colliderType = pbCollider.insideBounds
                        ? AtivGenerateVRMSpringBoneCollider.ColliderTypes.SphereInside
                        : AtivGenerateVRMSpringBoneCollider.ColliderTypes.Sphere;
                    ativCollider.offset = pbCollider.position;
                    ativCollider.radius = pbCollider.radius;
                    break;
                case VRCPhysBoneColliderBase.ShapeType.Capsule:
                    ativCollider.colliderType = pbCollider.insideBounds
                        ? AtivGenerateVRMSpringBoneCollider.ColliderTypes.CapsuleInside
                        : AtivGenerateVRMSpringBoneCollider.ColliderTypes.Capsule;
                    var height = Mathf.Max(pbCollider.height - pbCollider.radius * 2f, 0f);
                    var offset = pbCollider.rotation * Vector3.up * height / 2f;
                    ativCollider.offset = pbCollider.position - offset;
                    ativCollider.tail = pbCollider.position + offset;
                    ativCollider.radius = pbCollider.radius;
                    break;
                case VRCPhysBoneColliderBase.ShapeType.Plane:
                    ativCollider.colliderType = AtivGenerateVRMSpringBoneCollider.ColliderTypes.Plane;
                    ativCollider.offset = pbCollider.position;
                    ativCollider.normal = pbCollider.rotation * Vector3.up;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var ativColliderGroup = secondary.gameObject.AddComponent<AtivGenerateVRMSpringBoneColliderGroup>();
            ativColliderGroup.colliders.Add(ativCollider);
            return ativColliderGroup;
        }

        void GenerateSpringBones(GameObject context, VRCPhysBone pb, Dictionary<VRCPhysBoneColliderBase, AtivGenerateVRMSpringBoneColliderGroup> ativColliderGroups)
        {
            if (pb.transform.childCount == 0)
            {
                return;
            }
            var secondary = context.transform.FindOrCreateSecondary(pb.gameObject.name);
            if (pb.transform.childCount == 1)
            {
                GenerateSpring(pb.transform);
            }
            else
            {
                switch (pb.multiChildType)
                {
                    // FIXME: this is completely different logic from AtivGenerateVrmSpringBones
                    case VRCPhysBoneBase.MultiChildType.Ignore:
                        foreach (var child in pb.transform.OfType<Transform>())
                        {
                            GenerateSpring(child);
                        }
                        break;
                    case VRCPhysBoneBase.MultiChildType.First:
                    case VRCPhysBoneBase.MultiChildType.Average:
                        GenerateSpring(pb.transform);
                        foreach (var child in pb.transform.OfType<Transform>().Skip(1))
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
                var ativSpringBone = secondary.gameObject.AddComponent<AtivGenerateVRMSpringBones>();
                ativSpringBone.rootBone = root;
                // FIXME adjust parameters
                ativSpringBone.stiffness = pb.pull;
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
