
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace StarterKit
{
    public class AutomaticDoors : UdonSharpBehaviour
    {
        [Header("A door that will automatically Open when you walk towards it. Can be networked or not.")]
        public bool networked = false;

        public TextMeshProUGUI logger;
        private Animator doorAnim;
        private string doorAnimName = "Open";
        private int storePlayersInCollider = 0;
        
        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
        }
        void Start() 
        {
            doorAnim = this.GetComponentInChildren<Animator>();
        }

        #region Player Trigger stuff
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            LoggerPrint(player.displayName + " had entered the trigger collider");
            if (player.isLocal)
            {
                if (!networked)
                {
                    doorAnim.SetBool(doorAnimName, true);
                }
                else
                {
                    SendCustomNetworkEvent(NetworkEventTarget.All,nameof(EnterTriggerZone));
                }
            }
        }

        public void EnterTriggerZone()
        {
            doorAnim.SetBool(doorAnimName, true);
            storePlayersInCollider++;
            LoggerPrint("The stored value of players is "+ storePlayersInCollider);
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            LoggerPrint(player.displayName + " had left the trigger collider");
            if (player.isLocal)
            {
                if (!networked)
                {
                
                    doorAnim.SetBool(doorAnimName ,false);
                }
                else
                {
                    SendCustomNetworkEvent(NetworkEventTarget.All, nameof(ExitTriggerZone));
                }
            }
            
        }
        public void ExitTriggerZone()
        {
            
            storePlayersInCollider--;
            LoggerPrint("The stored value of players is "+ storePlayersInCollider);
            if (storePlayersInCollider < 0)
            {
                storePlayersInCollider = 0;
            }

            if (storePlayersInCollider == 0)
            {
                doorAnim.SetBool(doorAnimName, false);
            }
        }
        #endregion

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (storePlayersInCollider > 0)
            {
                storePlayersInCollider--;
            }
            
            LoggerPrint(player.displayName + " has left the instance, the stored value of players is "+ storePlayersInCollider);
        }
    }
    
}
