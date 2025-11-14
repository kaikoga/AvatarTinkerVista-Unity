#if ATIV_NDMF
#endif

using System.Linq;
using nadena.dev.ndmf.runtime;
using UnityEditor;
using UnityEngine;
using VRC.Dynamics;

namespace Silksprite.AvatarTinkerVista.Nondestructive
{
    [CustomEditor(typeof(AtivReduceDynamics))]
    class AtivReduceDynamicsEditor : Editor
    {
        AtivReduceDynamics _reduceDynamics;
        SerializedProperty _propReduceOnPC;
        SerializedProperty _propReduceOnMobile;
        SerializedProperty _propKeepBoneRoots;
        Transform _avatarRoot;

#if ATIV_VRCSDK3_AVATARS
        VRCPhysBoneBase[] _allVrcPhysBones;
#endif

        void OnEnable()
        {
            _reduceDynamics = (AtivReduceDynamics)target;
            _propReduceOnPC = serializedObject.FindProperty(nameof(AtivReduceDynamics.reduceOnPC));
            _propReduceOnMobile = serializedObject.FindProperty(nameof(AtivReduceDynamics.reduceOnMobile));
            _propKeepBoneRoots = serializedObject.FindProperty(nameof(AtivReduceDynamics.keepBoneRoots));
#if ATIV_NDMF
            _avatarRoot = RuntimeUtil.FindAvatarInParents(_reduceDynamics.transform);
#if ATIV_VRCSDK3_AVATARS
            _allVrcPhysBones = _avatarRoot?.GetComponentsInChildren<VRCPhysBoneBase>();
            if (_allVrcPhysBones != null)
            {
                foreach (var pb in _allVrcPhysBones)
                {
                    pb.InitTransforms(true);
                }
            }
#endif
#endif
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_propReduceOnPC);
            EditorGUILayout.PropertyField(_propReduceOnMobile);
            EditorGUILayout.PropertyField(_propKeepBoneRoots);
            serializedObject.ApplyModifiedProperties();
#if ATIV_VRCSDK3_AVATARS
            if (_allVrcPhysBones == null)
            {
                return;
            }
            if (GUILayout.Button("Add All VRC PhysBones"))
            {
                _reduceDynamics.keepBoneRoots = _reduceDynamics.keepBoneRoots
                    .Concat(_allVrcPhysBones.Select(pb => pb.GetRootTransform()))
                    .Distinct().ToArray();
            }
            var vrcPhysBones = _allVrcPhysBones
                .Where(pb => _reduceDynamics.keepBoneRoots.Contains(pb.GetRootTransform()))
                .ToArray(); 
            var pbTransforms = vrcPhysBones.Sum(pb => pb.bones.Count); 
            var pbColliders = vrcPhysBones.SelectMany(pb => pb.colliders)
                .Where(collider => collider)
                .Distinct().Count();
            var pbCollisions = vrcPhysBones.Sum(pb => pb.bones.Count * pb.colliders.Count);
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.IntField("Est. Components", vrcPhysBones.Length);
                EditorGUILayout.IntField("Est. Transforms", pbTransforms);
                EditorGUILayout.IntField("Est. Colliders", pbColliders);
                EditorGUILayout.IntField("Est. Collisions", pbCollisions);
            }
#endif
        }
    }
}
