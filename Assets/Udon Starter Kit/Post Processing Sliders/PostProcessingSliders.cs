
using System;
using System.Net.Mime;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{

    public class PostProcessingSliders : UdonSharpBehaviour
    {
        [Header("A Panel that allows users to adjust their post processing locally.")]
        [Header("Make sure to assign the Main Camera in this prefab to the reference camera slot in the descriptor.")]
        public TextMeshProUGUI logger;

        public bool enableDesktopVolumes = true;
        private Slider[] pPSliders;
        private Animator pPAnimator;
        private string[] animatorNames = {"bloom", "exposure", "night", "desktop" };
        
        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
        }
        void Start()
        {
            pPSliders = this.GetComponentsInChildren<Slider>();
            pPAnimator = this.GetComponent<Animator>();
            if (enableDesktopVolumes)
            {
                _DesktopCheck();
            }
        }

        public void _DesktopCheck()
        {
            if (!Networking.LocalPlayer.IsUserInVR())
            {
                LoggerPrint("Desktop Volumes are Enabled");
                pPAnimator.SetBool(animatorNames[3], true);
            }
        }

        public void _Bloom()
        {
            LoggerPrint("_bloom slider value is " + pPSliders[0].value);
            pPAnimator.SetFloat(animatorNames[0], pPSliders[0].value);
        }

        public void _AutoExpose()
        {
            LoggerPrint(" _AutoExposue slider value is " + pPSliders[1].value);
            pPAnimator.SetFloat(animatorNames[1], pPSliders[1].value);
        }

        public void _Night()
        {

            LoggerPrint("_Night slider value is " + pPSliders[2].value);
            pPAnimator.SetFloat(animatorNames[2], pPSliders[2].value);
        }
    }
}
