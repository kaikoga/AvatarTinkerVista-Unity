using System.Linq;
using Silksprite.Loch;
using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using UnityEngine;
using static Silksprite.Loch.Tools.LochTool;

#if ATIV_NDMF
using nadena.dev.ndmf.runtime;
#endif

#if ATIV_VRCSDK3_AVATARS
using VRC.Dynamics;
#endif

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivReduceDynamics))]
    class AtivReduceDynamicsEditor : Editor
    {
        AtivReduceDynamics _reduceDynamics;

        LocalizedProperty _reduceOnPC;
        LocalizedProperty _reduceOnMobile;
        LocalizedProperty _keepBoneRoots;

        Transform _avatarRoot;

#if ATIV_VRCSDK3_AVATARS
        VRCPhysBoneBase[] _allVrcPhysBones;
#endif

        void OnEnable()
        {
            _reduceDynamics = (AtivReduceDynamics)target;
            _reduceOnPC = serializedObject.Lop(nameof(AtivReduceDynamics.reduceOnPC), Loc("AtivReduceDynamics::reduceOnPC"));
            _reduceOnMobile = serializedObject.Lop(nameof(AtivReduceDynamics.reduceOnMobile), Loc("AtivReduceDynamics::reduceOnMobile"));
            _keepBoneRoots = serializedObject.Lop(nameof(AtivReduceDynamics.keepBoneRoots), Loc("AtivReduceDynamics::keepBoneRoots"));
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
            LEditorGUILayout.LocaleSelector();
            LEditorGUILayout.Prop(_reduceOnPC);
            LEditorGUILayout.Prop(_reduceOnMobile);
            LEditorGUILayout.Prop(_keepBoneRoots);
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
                LEditorGUILayout.IntField(Loc("AtivReduceDynamics::EstComponents"), vrcPhysBones.Length);
                LEditorGUILayout.IntField(Loc("AtivReduceDynamics::EstTransforms"), pbTransforms);
                LEditorGUILayout.IntField(Loc("AtivReduceDynamics::EstColliders"), pbColliders);
                LEditorGUILayout.IntField(Loc("AtivReduceDynamics::EstCollisions"), pbCollisions);
            }
#endif
        }
    }
}
