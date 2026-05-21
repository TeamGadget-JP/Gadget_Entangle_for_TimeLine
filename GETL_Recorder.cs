using UnityEngine;
using UnityEditor;
using UnityEngine.Playables;
using System.Collections.Generic;
using System.IO;

namespace Gadget.GETL
{
    public class GETL_Recorder : EditorWindow
    {
        [MenuItem("Gadget/GETL Recorder")]
        public static void ShowWindow()
        {
            GetWindow<GETL_Recorder>("GETL Recorder");
        }

        private GameObject targetAvatar;
        private PlayableDirector playableDirector;
        private AnimationClip targetAnimationClip;
        private string newClipName = "GEC_Bake_Animation";
        
        [Header("Bake Settings")]
        private float frameRate = 60f;
        private float bakeDelay = 0.1f;
        
        // Facial Bake ON/OFF Toggle
        private bool enableFacialBake = true;

        private bool isBaking = false;
        private int currentBakeFrame = 0;
        private int totalBakeFrames = 0;
        private double nextBakeTime = 0.0;

        // Bone curves
        private Dictionary<string, List<Keyframe>> posXCurves = new Dictionary<string, List<Keyframe>>();
        private Dictionary<string, List<Keyframe>> posYCurves = new Dictionary<string, List<Keyframe>>();
        private Dictionary<string, List<Keyframe>> posZCurves = new Dictionary<string, List<Keyframe>>();
        private Dictionary<string, List<Keyframe>> rotXCurves = new Dictionary<string, List<Keyframe>>();
        private Dictionary<string, List<Keyframe>> rotYCurves = new Dictionary<string, List<Keyframe>>();
        private Dictionary<string, List<Keyframe>> rotZCurves = new Dictionary<string, List<Keyframe>>();
        private Dictionary<string, List<Keyframe>> rotWCurves = new Dictionary<string, List<Keyframe>>();

        // Curve for blend shape
        private Dictionary<string, Dictionary<string, List<Keyframe>>> blendshapeCurves = new Dictionary<string, Dictionary<string, List<Keyframe>>>();
        private SkinnedMeshRenderer[] targetSMRs; 

        private void OnGUI()
        {
            GUILayout.Label("Team Gadget : GETL Offline Baker V1.0", EditorStyles.boldLabel);
            GUILayout.Space(10);

            GUI.enabled = !isBaking;

            targetAvatar = (GameObject)EditorGUILayout.ObjectField("Target Avatar", targetAvatar, typeof(GameObject), true);
            playableDirector = (PlayableDirector)EditorGUILayout.ObjectField("Playable Director", playableDirector, typeof(PlayableDirector), true);
            
            GUILayout.Space(10);
            GUILayout.Label("=== .anim File Settings ===", EditorStyles.miniBoldLabel);
            targetAnimationClip = (AnimationClip)EditorGUILayout.ObjectField("Overwrite Clip", targetAnimationClip, typeof(AnimationClip), false);
            
            if (targetAnimationClip == null)
            {
                newClipName = EditorGUILayout.TextField("New Clip Name", newClipName);
            }

            GUILayout.Space(10);
            GUILayout.Label("=== Quality & Features ===", EditorStyles.miniBoldLabel);
            frameRate = EditorGUILayout.FloatField("Target Frame Rate", frameRate);
            bakeDelay = EditorGUILayout.Slider("Bake Delay (sec)", bakeDelay, 0.02f, 0.5f);
            
            // Facial toggle UI
            GUILayout.Space(5);
            enableFacialBake = EditorGUILayout.ToggleLeft(" Bake Facial (Blendshapes)", enableFacialBake, EditorStyles.boldLabel);

            GUILayout.Space(15);

            if (!isBaking)
            {
                GUI.backgroundColor = new Color(0.4f, 0.8f, 0.4f);
                if (GUILayout.Button("● START ANIMATION BAKE", GUILayout.Height(40)))
                {
                    StartBaking();
                }
            }
            
            GUI.enabled = true;

            if (isBaking)
            {
                GUI.backgroundColor = new Color(0.9f, 0.3f, 0.3f);
                if (GUILayout.Button("■ CANCEL BAKE", GUILayout.Height(40)))
                {
                    CancelBaking();
                }
                
                float progress = (float)currentBakeFrame / Mathf.Max(1, totalBakeFrames);
                string bakeMode = enableFacialBake ? "(Bones & Faces)" : "(Bones ONLY)";
                EditorGUI.ProgressBar(EditorGUILayout.GetControlRect(), progress, $"Baking {bakeMode}... Frame {currentBakeFrame} / {totalBakeFrames}");
            }
            GUI.backgroundColor = Color.white;
        }

        private void OnEnable() { EditorApplication.update += OnEditorUpdate; }
        private void OnDisable() { EditorApplication.update -= OnEditorUpdate; CancelBaking(); }

        private void StartBaking()
        {
            if (targetAvatar == null || playableDirector == null) return;
            
            isBaking = true;
            playableDirector.Pause();
            totalBakeFrames = Mathf.CeilToInt((float)playableDirector.duration * frameRate);
            currentBakeFrame = 0;

            posXCurves.Clear(); posYCurves.Clear(); posZCurves.Clear();
            rotXCurves.Clear(); rotYCurves.Clear(); rotZCurves.Clear(); rotWCurves.Clear();
            blendshapeCurves.Clear();

            if (enableFacialBake)
            {
                targetSMRs = targetAvatar.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            }

            playableDirector.time = 0;
            playableDirector.Evaluate();
            nextBakeTime = EditorApplication.timeSinceStartup + bakeDelay;

            Debug.Log($"<b>[GETL Baker]</b> Animation baking started(Facial: {(enableFacialBake ? "ON" : "OFF")})");
        }

        private void OnEditorUpdate()
        {
            if (!isBaking || playableDirector == null || targetAvatar == null) return;
            if (EditorApplication.timeSinceStartup < nextBakeTime) return;

            float currentTime = currentBakeFrame / frameRate;
            
            SampleAvatarTransforms(targetAvatar.transform, targetAvatar.transform, currentTime);
            
            // Sampling of facial features only when the toggle is ON.
            if (enableFacialBake)
            {
                SampleBlendshapes(currentTime);
            }

            currentBakeFrame++;
            
            if (currentBakeFrame > totalBakeFrames)
            {
                FinishBaking();
                return;
            }

            playableDirector.time = currentTime + (1.0f / frameRate);
            playableDirector.Evaluate();
            nextBakeTime = EditorApplication.timeSinceStartup + bakeDelay;
            Repaint();
        }

        private void SampleAvatarTransforms(Transform current, Transform root, float time)
        {
            if (current != root)
            {
                string path = AnimationUtility.CalculateTransformPath(current, root);
                
                if (!rotXCurves.ContainsKey(path))
                {
                    rotXCurves[path] = new List<Keyframe>(); rotYCurves[path] = new List<Keyframe>();
                    rotZCurves[path] = new List<Keyframe>(); rotWCurves[path] = new List<Keyframe>();
                }
                Quaternion rot = current.localRotation;
                rotXCurves[path].Add(new Keyframe(time, rot.x));
                rotYCurves[path].Add(new Keyframe(time, rot.y));
                rotZCurves[path].Add(new Keyframe(time, rot.z));
                rotWCurves[path].Add(new Keyframe(time, rot.w));

                string boneName = current.name.ToLower();
                if (boneName.Contains("hip") || boneName.Contains("pelvis"))
                {
                    if (!posXCurves.ContainsKey(path))
                    {
                        posXCurves[path] = new List<Keyframe>(); posYCurves[path] = new List<Keyframe>(); posZCurves[path] = new List<Keyframe>();
                    }
                    Vector3 pos = current.localPosition;
                    posXCurves[path].Add(new Keyframe(time, pos.x));
                    posYCurves[path].Add(new Keyframe(time, pos.y));
                    posZCurves[path].Add(new Keyframe(time, pos.z));
                }
            }

            for (int i = 0; i < current.childCount; i++)
            {
                SampleAvatarTransforms(current.GetChild(i), root, time);
            }
        }

        private void SampleBlendshapes(float time)
        {
            if (targetSMRs == null) return;

            foreach (var smr in targetSMRs)
            {
                if (smr.sharedMesh == null || smr.sharedMesh.blendShapeCount == 0) continue;

                string path = AnimationUtility.CalculateTransformPath(smr.transform, targetAvatar.transform);

                if (!blendshapeCurves.ContainsKey(path))
                {
                    blendshapeCurves[path] = new Dictionary<string, List<Keyframe>>();
                }

                for (int i = 0; i < smr.sharedMesh.blendShapeCount; i++)
                {
                    string shapeName = smr.sharedMesh.GetBlendShapeName(i);
                    float weight = smr.GetBlendShapeWeight(i);

                    if (!blendshapeCurves[path].ContainsKey(shapeName))
                    {
                        blendshapeCurves[path][shapeName] = new List<Keyframe>();
                    }
                    blendshapeCurves[path][shapeName].Add(new Keyframe(time, weight));
                }
            }
        }

        private void FinishBaking()
        {
            isBaking = false;
            AnimationClip clip = targetAnimationClip;

            if (clip == null)
            {
                clip = new AnimationClip();
                clip.frameRate = frameRate;
                string path = AssetDatabase.GenerateUniqueAssetPath($"Assets/{newClipName}.anim");
                AssetDatabase.CreateAsset(clip, path);
            }
            else
            {
                clip.ClearCurves();
                clip.frameRate = frameRate;
            }

            foreach (var path in rotXCurves.Keys)
            {
                if (posXCurves.ContainsKey(path))
                {
                    clip.SetCurve(path, typeof(Transform), "localPosition.x", new AnimationCurve(posXCurves[path].ToArray()));
                    clip.SetCurve(path, typeof(Transform), "localPosition.y", new AnimationCurve(posYCurves[path].ToArray()));
                    clip.SetCurve(path, typeof(Transform), "localPosition.z", new AnimationCurve(posZCurves[path].ToArray()));
                }

                clip.SetCurve(path, typeof(Transform), "localRotation.x", new AnimationCurve(rotXCurves[path].ToArray()));
                clip.SetCurve(path, typeof(Transform), "localRotation.y", new AnimationCurve(rotYCurves[path].ToArray()));
                clip.SetCurve(path, typeof(Transform), "localRotation.z", new AnimationCurve(rotZCurves[path].ToArray()));
                clip.SetCurve(path, typeof(Transform), "localRotation.w", new AnimationCurve(rotWCurves[path].ToArray()));
            }

            // Draw facial features only when the toggle is ON.
            if (enableFacialBake)
            {
                foreach (var path in blendshapeCurves.Keys)
                {
                    foreach (var shapeName in blendshapeCurves[path].Keys)
                    {
                        clip.SetCurve(path, typeof(SkinnedMeshRenderer), "blendShape." + shapeName, new AnimationCurve(blendshapeCurves[path][shapeName].ToArray()));
                    }
                }
            }

            clip.EnsureQuaternionContinuity();

            EditorUtility.SetDirty(clip);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.DisplayDialog("GETL Baker", $"Animation baking is complete.\n(Facial: {(enableFacialBake ? "ON" : "OFF")})", "OK");
        }

        private void CancelBaking()
        {
            isBaking = false;
            Debug.LogWarning("<b>[GETL Baker]</b> Animation baking has been canceled.");
        }
    }
}