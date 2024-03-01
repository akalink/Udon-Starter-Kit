
using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class UIPedestel : UdonSharpBehaviour
    {
        public VRC_AvatarPedestal pedestal;

        public TextMeshProUGUI logger;
        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
            else
            {
                Debug.Log( "-" + this.name + "-" + text);
            }
        }
        private void Start()
        {
            pedestal = GetComponent<VRC_AvatarPedestal>();
            LoggerPrint("The pedestal has been found and assigned " + (pedestal != null));
            
        }

        public void _SetPlayerAsAvatar()
        {
            LoggerPrint(Networking.LocalPlayer.displayName + " has clicked the pedestal");
            if (pedestal != null)
            {
                pedestal.SetAvatarUse(Networking.LocalPlayer);
            }
        }
    }
}
