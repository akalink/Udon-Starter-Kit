
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
        private VRC_AvatarPedestal pedestal;

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
            pedestal = (VRC_AvatarPedestal)GetComponent(typeof(VRC_AvatarPedestal));
            LoggerPrint("The pedestal has been found and assigned");
        }

        public void _SetPlayerAsAvatar()
        {
            LoggerPrint(Networking.LocalPlayer.displayName + "has clicked the pedestal");
            if (pedestal != null)
            {
                pedestal.SetAvatarUse(Networking.LocalPlayer);
            }
        }
    }
}
