
using System;
using System.Security.Policy;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace StarterKit
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class NameDetect : UdonSharpBehaviour
    {
        [Header("Assigns a object to specified users, requires a head tracker in your scene.")]
        [Header("Highly recommended to read the wiki, setting this up is not drag and drop")]
        [Header("Udon Starter Kit >> Wiki")]
        [SerializeField]
        private bool downloadList;

        [SerializeField]
        private VRCUrl url;

        private bool downloadIsReady;
        [SerializeField] private String[] names;
 
        private VRCPlayerApi[] allPlayers = new VRCPlayerApi[82];
        private ParentConstraint[] assignTags;
        private VRCPlayerApi[] assignedPlayers;
        private int indexAssignment = -1;
        private bool noRange = true;
        private bool assignedObject = false;
        public TextMeshProUGUI logger;

        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
            else
            {
                Debug.Log("-" + this.name + "-" + text + "\n");
            }
        }

        void Start()
        {
            assignTags = GetComponentsInChildren<ParentConstraint>(true);
            assignedPlayers = new VRCPlayerApi[assignTags.Length];
            Debug.Log("tag length = " + assignTags.Length);

            if (downloadList)
            {
                VRCStringDownloader.LoadUrl(url,(IUdonEventReceiver)this);
            }
            else if(names.Length == 0)
            {
                noRange = true;
            }
            else
            {
                noRange = false;
                downloadIsReady = true;
            }
        }


        

        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            string s = result.Result;
            names = s.Split('\n');
            noRange = false;
            
            Debug.Log("Success Result " + names[0]);
            downloadIsReady = true;
            IterateThroughNames();
        }

        public override void OnStringLoadError(IVRCStringDownload result)
        {
            Debug.LogWarning(result.ErrorCode);
        }




        public override void OnPlayerJoined(VRCPlayerApi player) //check if download is ready first, also save local array of player ids in instance
        {
            //LoggerPrint("checking if " + player.displayName + " should get a object");
            
            for (int i = 0; i < allPlayers.Length; i++)
            {
                if (allPlayers[i] == null)
                {
                    allPlayers[i] = player;
                    break;
                }
            }
            
            IterateThroughNames();
            
            // // if (noRange)
            // // {
            // //     return;
            // // }
            //
            // if (Networking.LocalPlayer.IsOwner(gameObject))
            // {
            //     LoggerPrint(Networking.LocalPlayer.displayName + " is owner");
            //     //string tempName = player.displayName;
            //     for (int i = 0; i < syncedIDs.Length; i++)
            //     {
            //         if (syncedIDs[i] == -1)
            //         {
            //             syncedIDs[i] = player.playerId;
            //             LoggerPrint($"Assign {player.displayName} to the int array");
            //             RequestSerialization();
            //             IterateThroughNames();
            //             break;
            //         }
            //     }
            //     // for (int i = 0; i < names.Length; i++)
            //     // {
            //     //     if (names[i] == tempName)
            //     //     {
            //     //         for (int k = 0; k < syncedIDs.Length; k++)
            //     //         {
            //     //             if (syncedIDs[k] == -1)
            //     //             {
            //     //                 LoggerPrint(player.playerId + " is assigned a space on the array");
            //     //                 syncedIDs[k] = player.playerId;
            //     //                 RequestSerialization();
            //     //                 IterateThroughNames();
            //     //                 return;
            //     //             }
            //     //         }
            //     //     }
            //     // }
            // }
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            LoggerPrint($"{player.displayName} has left");
            for (int i = 0; i < assignTags.Length; i++)
            {
                LoggerPrint($"{assignTags[i].gameObject.name} is owned by {Networking.GetOwner(assignTags[i].gameObject).displayName}");
                if (assignedPlayers[i] != null && assignedPlayers[i] == player)
                {
                    LoggerPrint(player + "'s object is being removed");
                    assignTags[i].enabled = false;
                    assignTags[i].gameObject.SetActive(false);
                    
                }
            }
            for (int i = 0; i < allPlayers.Length; i++)
            {
                if (allPlayers[i] == player)
                {
                    allPlayers[i] = null;
                    break;
                }
            }


            IterateThroughNames();
            
            //RequestSerialization();
            
            // int tempID = player.playerId;
            // for (int i = 0; i < assignTags.Length; i++)
            // {
            //     VRCPlayerApi to = Networking.GetOwner(assignTags[i].gameObject);
            //     if (to.Equals(player))
            //     {
            //         assignTags[i].enabled = false;
            //         assignTags[i].gameObject.transform.position = resetPosition;
            //         break;
            //     }
            // }
            //
            // if (Networking.LocalPlayer.IsOwner(gameObject))
            // {
            //     for (int i = 0; i < syncedIDs.Length; i++)
            //     {
            //         if (syncedIDs[i] == tempID)
            //         {
            //             syncedIDs[i] = -1;
            //             RequestSerialization();
            //             IterateThroughNames();
            //             return;
            //         }
            //     }
            // }
        }

        public void IterateThroughNames()
        {
            if (noRange || !downloadIsReady)
            {
                return;
            }

            string tempName;// = Networking.LocalPlayer.displayName;
            
            int indexOfTags = 1;
            for (int j = 0; j < allPlayers.Length; j++)
            {
                //LoggerPrint("Iteration #" + (j+1));
                if (allPlayers[j] == null)
                {
                    continue;
                }
                for (int i = 0; i < names.Length; i++)
                {
                    //LoggerPrint($"i index {i}");
                    tempName = allPlayers[j].displayName;
                    
                    if (names[i] == tempName) 
                    { 
                        LoggerPrint(tempName + " is getting an object"); 
                        assignTags[indexOfTags].enabled = true; 
                        GameObject obj = assignTags[indexOfTags].gameObject; 
                        obj.SetActive(true);
                        
                        Networking.SetOwner(allPlayers[j],obj); 
                        assignedPlayers[indexOfTags] = allPlayers[j]; 
                        LoggerPrint($"{obj.name} at index {indexOfTags} is assigned to player {Networking.GetOwner(obj).displayName}"); 
                        indexOfTags++; 
                        break;
                    }
                }
            }
        }

        public override void OnDeserialization()
        {
            IterateThroughNames();
        }
    }
}
