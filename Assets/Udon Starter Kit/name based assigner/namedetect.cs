
using System;
using System.Collections.Generic;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.Animations;
using VRC.SDK3.Data;
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

        [SerializeField] private GameObject prefab;
        //private VRCPlayerApi[] allPlayers = new VRCPlayerApi[82];
        private DataDictionary allPlayerDD;
        private ParentConstraint[] assignTags;
        private VRCPlayerApi[] assignedPlayers;
        private bool noRange = true;
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
            allPlayerDD = new DataDictionary();
            assignTags = GetComponentsInChildren<ParentConstraint>(true);
            assignedPlayers = new VRCPlayerApi[assignTags.Length];

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
            
            LoggerPrint("Success Result " + result.Result);
            downloadIsReady = true;
            IterateThroughNames();
        }

        public override void OnStringLoadError(IVRCStringDownload result)
        {
            LoggerPrint(result.ErrorCode.ToString());
        }




        public override void OnPlayerJoined(VRCPlayerApi player) //check if download is ready first, also save local array of player ids in instance
        {
            //LoggerPrint("checking if " + player.displayName + " should get a object");
            
            // for (int i = 0; i < allPlayers.Length; i++)
            // {
            //     if (allPlayers[i] == null)
            //     {
            //         allPlayers[i] = player;
            //         break;
            //     }
            // }
            allPlayerDD.Add(player.displayName,player.playerId);
            
            
            IterateThroughNames();
            
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
            // for (int i = 0; i < allPlayers.Length; i++)
            // {
            //     if (allPlayers[i] == player)
            //     {
            //         allPlayers[i] = null;
            //         break;
            //     }
            // }
            allPlayerDD.Remove(player.displayName);


            IterateThroughNames();
            
        }

        public void IterateThroughNames()
        {
            if (noRange || !downloadIsReady)
            {
                return;
            }

            string tempName;
            
            int indexOfTags = 1;
            // for (int j = 0; j < allPlayers.Length; j++)
            // {
            //     if (allPlayers[j] == null)
            //     {
            //         continue;
            //     }
            //     for (int i = 0; i < names.Length; i++)
            //     {
            //         tempName = allPlayers[j].displayName;
            //         
            //         if (names[i] == tempName) 
            //         { 
            //             LoggerPrint(tempName + " is getting an object"); 
            //             assignTags[indexOfTags].enabled = true; 
            //             GameObject obj = assignTags[indexOfTags].gameObject; 
            //             obj.SetActive(true);
            //             
            //             Networking.SetOwner(allPlayers[j],obj); 
            //             assignedPlayers[indexOfTags] = allPlayers[j]; 
            //             LoggerPrint($"{obj.name} at index {indexOfTags} is assigned to player {Networking.GetOwner(obj).displayName}"); 
            //             indexOfTags++; 
            //             break;
            //         }
            //     }
            // }
            for (int i = 0; i < names.Length; i++)
            {
                if(indexOfTags > assignTags.Length-1) return; //number of parent constraints is one more than amount of assignable crowns.
                
                if(allPlayerDD.TryGetValue(names[i], out DataToken pId))
                {
                    VRCPlayerApi playerApi = VRCPlayerApi.GetPlayerById((int) pId);
                    assignTags[indexOfTags].enabled = true; 
                    GameObject obj = assignTags[indexOfTags].gameObject; 
                    obj.SetActive(true);
                    if(Networking.LocalPlayer == playerApi) Networking.SetOwner(playerApi,obj); 
                    
                    assignedPlayers[indexOfTags] = playerApi; 
                    LoggerPrint($"{obj.name} at index {indexOfTags} is assigned to player {Networking.GetOwner(obj).displayName}"); 
                    indexOfTags++; 
                }
            }
        }

        public override void OnDeserialization()
        {
            IterateThroughNames();
        }
    }
}
