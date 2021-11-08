
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
        private Animator doorAnim;
        private string doorAnimName = "Open";
        private int storePlayersInCollider = 0;
        void Start() 
        {
            doorAnim = this.GetComponentInChildren<Animator>();
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
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
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
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
            if (storePlayersInCollider < 0)
            {
                storePlayersInCollider = 0;
            }

            if (storePlayersInCollider == 0)
            {
                doorAnim.SetBool(doorAnimName, false);
            }
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (storePlayersInCollider > 0)
            {
                storePlayersInCollider--;
            }
        }
    }
    
}
