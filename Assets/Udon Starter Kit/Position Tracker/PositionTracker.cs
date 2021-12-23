
using TMPro;
using UdonSharp;
using UnityEditor;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class PositionTracker : UdonSharpBehaviour
    {
        [Header("A system that will follow the players position and optionally their head and hands allow them to press buttons with their hands while in vr")]
        public bool allowVRHandCollision = true;
        public bool allowHeadTracking = true;
        private bool fingerCollision = false;
        public TextMeshProUGUI logger;
        private HumanBodyBones LeftBone;
        private HumanBodyBones RightBone;

        private VRCPlayerApi LocalPlayer;
        private bool isNull = false;
        private Transform[] trackedPoints;
        
        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
        }

        #region InitializeAllTheThings
        void Start()
        {
            if (Networking.LocalPlayer == null)
            {
                isNull = true;
                return;
            }
            LocalPlayer = Networking.LocalPlayer;

            trackedPoints = GetComponentsInChildren<Transform>();
            if (!allowHeadTracking)
            {
                trackedPoints[3].gameObject.SetActive(false);
            }
            
            CheckVR();
        }
        public void CheckVR()
        {
            if (allowVRHandCollision)
            {
                allowVRHandCollision = LocalPlayer.IsUserInVR();
                if (allowVRHandCollision)
                {
                    fingerCollision = _Checkbones();
                }
                LoggerPrint("VR and bone check returned " + allowVRHandCollision);
            }
            
            if (!allowVRHandCollision)
            {
                trackedPoints[1].gameObject.SetActive(false);
                trackedPoints[2].gameObject.SetActive(false);
                LoggerPrint("Hand Colliders are disabled");
            }
        }
        

        public bool _Checkbones()
        {
            bool returnIfAssigned = false;
            if ((LocalPlayer.GetBonePosition(HumanBodyBones.RightIndexDistal) != Vector3.zero) ||
                (LocalPlayer.GetBonePosition(HumanBodyBones.LeftIndexDistal) != Vector3.zero))
            {
                RightBone = HumanBodyBones.RightIndexDistal;
                LeftBone = HumanBodyBones.LeftIndexDistal;
                returnIfAssigned = true;
            }
            else if ((LocalPlayer.GetBonePosition(HumanBodyBones.RightIndexIntermediate) != Vector3.zero) ||
                     (LocalPlayer.GetBonePosition(HumanBodyBones.LeftIndexIntermediate) != Vector3.zero))
            {
                RightBone = HumanBodyBones.RightIndexIntermediate;
                LeftBone = HumanBodyBones.LeftIndexIntermediate;
                returnIfAssigned = true;
            }
            else if ((LocalPlayer.GetBonePosition(HumanBodyBones.RightIndexProximal) != Vector3.zero) ||
                     (LocalPlayer.GetBonePosition(HumanBodyBones.LeftIndexProximal) != Vector3.zero))
            {
                RightBone = HumanBodyBones.RightIndexProximal;
                LeftBone = HumanBodyBones.LeftIndexProximal;
                returnIfAssigned = true;
            }

            return returnIfAssigned;
        }
        #endregion

        private void Update()
        {
            if (!isNull)
            {
                trackedPoints[0].position = LocalPlayer.GetPosition();
                trackedPoints[0].rotation = LocalPlayer.GetRotation();

                if (allowVRHandCollision)
                {
                    if (fingerCollision)
                    {
                        trackedPoints[1].position = LocalPlayer.GetBonePosition(RightBone);
                        trackedPoints[2].position = LocalPlayer.GetBonePosition(LeftBone);
                    }
                    else
                    {
                        trackedPoints[1].position = LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).position;
                        trackedPoints[2].position = LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).position;
                    }
                    
                }

                if (allowHeadTracking)
                {
                    trackedPoints[3].position = LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                    trackedPoints[3].rotation = LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
                }
            }
        }
    }
}
