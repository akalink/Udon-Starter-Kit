
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class Teleport : UdonSharpBehaviour
    {
        [Header("A teleport with various options.")]
        [Header("Move the destination object to where you want the player to go.")]
        [Header(" The arrow will not appear in game, only in the unity editor. It indicates the direction of the teleport")]
        [Header("Toggling on 'Via Interaction' makes it so players have to click the teleport.")]
        [Header("Toggling on 'Seamless' makes players destination relative to their original position.")]
        public bool viaInteraction = false;
        public bool seamless = false;
        private Transform[] teleportPoints;
        [Header("Add the DoorLock prefab to make this teleport able to be turned off")]
        public DoorLock lockable;
        void Start()
        {
            teleportPoints = GetComponentsInChildren<Transform>();
            
            DisableInteractive = !viaInteraction;
        }

        public override void Interact()
        {
            if (viaInteraction)
            {
                if (lockable != null)
                {
                    if (!lockable._TeleportCheck())
                    {
                        return;
                    }
                }
                TeleportPlayer(Networking.LocalPlayer);
            }
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (viaInteraction == false && player.isLocal)
            {
                if (lockable != null)
                {
                    if (!lockable._TeleportCheck())
                    {
                        return;
                    }
                }
                TeleportPlayer(player);
            }
        }

        public void TeleportPlayer(VRCPlayerApi player)
        {
            if (seamless)
            {
                if (lockable != null)
                {
                    if (!lockable._TeleportCheck())
                    {
                        return;
                    }
                }
                teleportPoints[1].SetPositionAndRotation(player.GetPosition(),player.GetRotation()); //sets position of child of origin
                teleportPoints[3].localPosition = teleportPoints[1].localPosition; //sets position of child of destination to be the same realative position as child of origin
                teleportPoints[3].localRotation = teleportPoints[1].localRotation; //ditto but with rotation
                player.TeleportTo(teleportPoints[3].position, teleportPoints[3].rotation); //sets player to be in the position of child of destination.
            }
            else
            {
                player.TeleportTo(teleportPoints[2].position, teleportPoints[2].rotation);
            }
        }
    }
}
