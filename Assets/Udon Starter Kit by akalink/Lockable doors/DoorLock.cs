
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class DoorLock : UdonSharpBehaviour
    {
        [UdonSynced()] private int iDplayer = -1;
        private bool isLocked = false;
        
        public override void Interact()
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            if (isLocked)
            {
                if (iDplayer != -1)
                {
                    DoorIsUnlocked();
                }
            }
            else
            {
                DoorIsLocked();
            }
        }

        public override void OnPlayerRespawn(VRCPlayerApi player)
        {
            if (player.playerId == iDplayer)
            {
                DoorIsUnlocked();
            }
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (player.playerId == iDplayer)
            {
                DoorIsUnlocked();
            }
        }

        private void DoorIsLocked()
        {
            isLocked = true;
            iDplayer = Networking.LocalPlayer.playerId;
            RequestSerialization();
            UpdateStatus();
        }

        private void DoorIsUnlocked()
        {
            iDplayer = -1;
            RequestSerialization();
            UpdateStatus();
        }

        public bool _TeleportCheck()
        {
            if (iDplayer != -1)
            {
                return false;
            }

            return true;
        }

        public override void OnDeserialization()
        {
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            if (iDplayer == -1)
            {
                isLocked = false;
            }
            else
            {
                isLocked = true;
            }
            
        } ////TODO visual button and hand collider functionality
    }
}

