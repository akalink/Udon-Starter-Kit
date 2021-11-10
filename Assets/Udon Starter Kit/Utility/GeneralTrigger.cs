
using System;
using Microsoft.SqlServer.Server;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace StarterKit
{
    public class GeneralTrigger : UdonSharpBehaviour
    {
        [Header("A tool that adds allows you to trigger a public method"), Header("or animation for your custom Udon Behavior or Animator")]
        public bool Networked = false;
        public bool allowVRHandCollision = true;
        public UdonBehaviour behaviour;
        public Animator animator;
        public String methodOrTriggerName;
        [Header("True = Udon Method, False = Animation Trigger")]
        public bool udonOrAnimator = true;
        [Header("True = Interact, False = Trigger (or zone)")]
        [Header("If using a trigger be sure the object is on a layer that collides with the local player"), Header("and that the collider component has 'isTrigger' checked")]
        public bool isInteractOrTrigger = true;
        [Header("Delays activation by specified number of seconds")]
        public bool delayCall = false;
        public float delayTime = 1;
        [Header("Follows up with another call after a specified number of seconds")]
        public bool followUpCallWithDelay;
        public float followUpTime = 1;
        public String followUpMethodOrTriggerName;
        private string handTrackerName = "trackhand12345";
        

        private void Start()
        {
            SendCustomEventDelayedSeconds(nameof(VRCheck), 1);
            if (!isInteractOrTrigger)
            {
                DisableInteractive = true;
            }
        }

        public void VRCheck()
        {
            if (allowVRHandCollision && isInteractOrTrigger)
            {
                DisableInteractive = Networking.LocalPlayer.IsUserInVR();
            }
        }

        public void CoreLogic()
        {
            if (delayCall)
            {
                SendCustomEventDelayedSeconds(nameof(ProperCall),delayTime);
            }
            else
            {
                ProperCall();
            }
        }

        public void ProperCall()
        {
            if (udonOrAnimator)
            {
                
                behaviour.SendCustomEvent(methodOrTriggerName);
                Debug.Log("Sent Custom Event");
            }
            else
            {
                animator.SetTrigger(methodOrTriggerName);
            }
            if (followUpCallWithDelay)
            {
                SendCustomEventDelayedSeconds(nameof(FollowUpCall), followUpTime);
            }
        }

        public void FollowUpCall()
        {
            if (udonOrAnimator)
            {
                
                behaviour.SendCustomEvent(followUpMethodOrTriggerName);
                Debug.Log("Sent Custom Event");
            }
            else
            {
                animator.SetTrigger(methodOrTriggerName);
            }
        }

        public override void Interact()
        {
            GeneralInteractionMethod();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other != null && (other.gameObject.name.Contains(handTrackerName)))
            {
                GeneralInteractionMethod();
            }
        }

        private void GeneralInteractionMethod()
        {
            if (isInteractOrTrigger)
            {
                if (Networked)
                {
                    SendCustomNetworkEvent(NetworkEventTarget.All,nameof(CoreLogic));
                }
                else
                {
                    CoreLogic();
                }
            }
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player.isLocal && !isInteractOrTrigger)
            {
                if (Networked)
                {
                    SendCustomNetworkEvent(NetworkEventTarget.All,nameof(CoreLogic));
                }
                else
                {
                    CoreLogic();
                }
            }
        }
    }
}
