
using UdonSharp;
using UnityEditor;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class handtracker : UdonSharpBehaviour
    {
        public bool allowVRHandCollision = true;
        private HumanBodyBones LeftBone;
        private HumanBodyBones RightBone;

        private VRCPlayerApi LocalPlayer;
        private bool isNull = false;
        private Transform[] trackedPoints;
        

        void Start()
        {
            if (Networking.LocalPlayer == null)
            {
                isNull = true;
                return;
            }
            LocalPlayer = Networking.LocalPlayer;
            if (allowVRHandCollision)
            {
                allowVRHandCollision = _Checkbones();
            }
            trackedPoints = GetComponentsInChildren<Transform>();
            Debug.Log("Length of tracked array " + trackedPoints.Length);
            SendCustomEventDelayedSeconds(nameof(CheckVR), 1);
        }

        public void CheckVR()
        {
            Debug.Log("It delayed 1 second to check vr");
            if (allowVRHandCollision)
            {
                allowVRHandCollision = LocalPlayer.IsUserInVR();
                Debug.Log("tracking bool is " + allowVRHandCollision);
                
            }
            
            if (!allowVRHandCollision)
            {
                trackedPoints[1].gameObject.SetActive(false);
                trackedPoints[2].gameObject.SetActive(false);
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

        private void Update()
        {
            if (!isNull)
            {
                trackedPoints[0].position = LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                trackedPoints[0].rotation = LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;

                if (allowVRHandCollision)
                {
                    trackedPoints[1].position = LocalPlayer.GetBonePosition(RightBone);
                    trackedPoints[2].position = LocalPlayer.GetBonePosition(LeftBone);
                }
            }


        }
    }
}
