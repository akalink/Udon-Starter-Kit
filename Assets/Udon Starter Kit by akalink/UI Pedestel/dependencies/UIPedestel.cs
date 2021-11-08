
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class UIPedestel : UdonSharpBehaviour
    {
        private VRC_AvatarPedestal pedestal;

        private void Start()
        {
            pedestal = (VRC_AvatarPedestal)GetComponent(typeof(VRC_AvatarPedestal));
        }

        public void _SetPlayerAsAvatar()
        {
            if (pedestal != null)
            {
                pedestal.SetAvatarUse(Networking.LocalPlayer);
            }
        }
    }
}
