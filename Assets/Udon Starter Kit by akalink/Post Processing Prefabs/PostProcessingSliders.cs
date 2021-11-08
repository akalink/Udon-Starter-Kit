
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
        private Slider[] pPSliders;
        private Animator pPAnimator;
        private string[] animatorNames = {"bloom", "exposure", "night", "desktop" };
        
        void Start()
        {
            pPSliders = this.GetComponentsInChildren<Slider>();
            pPAnimator = this.GetComponent<Animator>();
            SendCustomEventDelayedSeconds(nameof(_DesktopCheck), 1.0f);
        }

        public void _DesktopCheck()
        {
            if (!Networking.LocalPlayer.IsUserInVR())
            {
                if (logger != null)
                {
                    logger.text += "-set to desktop mode\n";
                }

                pPAnimator.SetBool(animatorNames[3], true);
                
            }
        }

        public void _Bloom()
        {
            if (logger != null)
            {
                logger.text += "-_bloom slider value is " + pPSliders[0].value +"\n";
            }
            float temp = pPSliders[0].value;
            pPAnimator.SetFloat(animatorNames[0], temp);
        }

        public void _AutoExpose()
        {
            if (logger != null)
            {
                logger.text += " _bloom slider value is " + pPSliders[1].value + "\n";
            }
            
            float temp = pPSliders[1].value;
            pPAnimator.SetFloat(animatorNames[1], temp);
        }

        public void _Night()
        {
            if (logger != null)
            {
                logger.text += "_bloom slider value is " + pPSliders[2].value + "\n";
            }
            pPAnimator.SetFloat(animatorNames[2], pPSliders[2].value);
        }
    }
}
