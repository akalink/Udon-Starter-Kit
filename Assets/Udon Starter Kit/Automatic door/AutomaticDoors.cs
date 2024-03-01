
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace StarterKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AutomaticDoors : UdonSharpBehaviour
    {
        [Header("A door that will automatically Open when you walk towards it.\nCan be networked or not.")]
        [SerializeField] private bool networked = false;
        [SerializeField] private DoorLock lockable;
        [SerializeField] private TextMeshProUGUI logger;
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

            if (!networked)
            {
                if (player.isLocal)
                {
                    doorAnim.SetBool(doorAnimName, true);
                }
            }
            else
            {
                EnterTriggerZone();
            }
        }

        private void EnterTriggerZone()
        {
            if (lockable != null)
            {
                if (!lockable._LockCheck())
                {
                    doorAnim.SetBool(doorAnimName, false);
                    return;
                }
            }
            doorAnim.SetBool(doorAnimName, true);
            storePlayersInCollider++;
            LoggerPrint("The stored value of players is "+ storePlayersInCollider);
        }
        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            LoggerPrint(player.displayName + " had left the trigger collider");

            if (!networked)
            {
                if (player.isLocal)
                {
                    doorAnim.SetBool(doorAnimName ,false);
                }
            }
            else
            {
                ExitTriggerZone();
            }
            
        }
        private void ExitTriggerZone()
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
