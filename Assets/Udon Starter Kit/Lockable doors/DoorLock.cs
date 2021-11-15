
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class DoorLock : UdonSharpBehaviour
    {
        [Header("Assign to the teleport or automatic doors to add a locking mechanism to them.")]
        public bool allowVrHandCollision = true;
        public TextMeshProUGUI logger;
        [UdonSynced()] private int iDplayer = -1;
        private bool isLocked = false;

        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
        }

        #region Player Interaction
        public override void Interact()
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            LoggerPrint(Networking.LocalPlayer + " is now the owner");
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
        #endregion

        #region Playing Hooky Checks
        public override void OnPlayerRespawn(VRCPlayerApi player)
        {
            if (player.playerId == iDplayer)
            {
                LoggerPrint(player.displayName + " who had locked the door has respawned");
                DoorIsUnlocked();
            }
        }
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (player.playerId == iDplayer)
            {
                LoggerPrint(player.displayName + " who had locked the door has left");
                DoorIsUnlocked();
            }
        }
        #endregion

        #region Owner Variable Updates
        private void DoorIsLocked()
        {
            LoggerPrint("The door is Locked");
            isLocked = true;
            iDplayer = Networking.LocalPlayer.playerId;
            RequestSerialization();
            UpdateStatus();
        }

        private void DoorIsUnlocked()
        {
            LoggerPrint("The door is Unlocked");
            iDplayer = -1;
            RequestSerialization();
            UpdateStatus();
        }
        #endregion

        public bool _LockCheck()
        {
            if (iDplayer != -1)
            {
                LoggerPrint("A person tried to teleport but can't");
                return false;
            }
            LoggerPrint("A person tried to teleport and was successful");
            return true;
        }

        #region Sync Update
        public override void OnDeserialization()
        {
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            if (iDplayer == -1)
            {
                LoggerPrint("-Synced-The door is Unlocked");
                isLocked = false;
            }
            else
            {
                LoggerPrint("-Synced-The door is Locked");
                isLocked = true;
            }
            
        }
        #endregion
        ////TODO visual button and hand collider functionality
    }
}

