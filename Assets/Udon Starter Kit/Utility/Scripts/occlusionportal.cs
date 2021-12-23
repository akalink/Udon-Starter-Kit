
using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class occlusionportal : UdonSharpBehaviour
    {
        private Animator animator;
        private string animatorName = "open";
        [Header("A prebuilt player enter trigger toggle for Occlusion Portals, to be used with Occlusion Culling")]
        public TextMeshProUGUI logger;

        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
        }
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player.isLocal)
            {
                animator.SetBool(animatorName, true);
                LoggerPrint("The occlusion portal has opened");
            }
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (player.isLocal)
            {
                animator.SetBool(animatorName, false);
                LoggerPrint("The occlusion portal has closed");
            }
        }
    }
}
