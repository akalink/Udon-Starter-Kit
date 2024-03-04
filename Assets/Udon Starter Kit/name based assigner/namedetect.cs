
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
        
        private DataDictionary allPlayerDD;
        
        private ParentConstraint[] assignedTags;
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
            assignedTags = GetComponentsInChildren<ParentConstraint>(true);
            assignedPlayers = new VRCPlayerApi[assignedTags.Length];

            if (downloadList)
            {
                LoggerPrint("Using string download");
                VRCStringDownloader.LoadUrl(url,(IUdonEventReceiver)this);
            }
            else if(names.Length == 0)
            {
                noRange = true;
            }
            else
            {
                LoggerPrint("Using name array");
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

            allPlayerDD.Add(player.displayName,player.playerId);
            LoggerPrint($"Number of players in dd is {allPlayerDD.Count}");
            
            IterateThroughNames();
            
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            LoggerPrint($"{player.displayName} has left");
            for (int i = 0; i < assignedTags.Length; i++)
            {
                LoggerPrint($"{assignedTags[i].gameObject.name} is owned by {Networking.GetOwner(assignedTags[i].gameObject).displayName}");
                if (assignedPlayers[i] != null && assignedPlayers[i] == player)
                {
                    LoggerPrint(player + "'s object is being removed");
                    assignedPlayers[i] = null;
                    assignedTags[i].enabled = false;
                    assignedTags[i].gameObject.SetActive(false);
                    
                }
            }
            allPlayerDD.Remove(player.displayName);

        }

        public void IterateThroughNames()
        {
            if (noRange || !downloadIsReady)
            {
                return;
            }
            LoggerPrint("Going through names");
            
            
            int indexOfTags = 1;
            
            for (int i = 0; i < names.Length; i++)
            {
                LoggerPrint("Entered for loop");
                if(indexOfTags > assignedTags.Length-1) return; //number of parent constraints is one more than amount of assignable crowns.
                
                if(allPlayerDD.TryGetValue(names[i], out DataToken pId))
                {
                    LoggerPrint($"found {names[i]}");
                    
                    VRCPlayerApi playerApi = VRCPlayerApi.GetPlayerById((int) pId);
                    if(assignedPlayers[indexOfTags] == null)
                    {
                        GameObject obj = assignedTags[indexOfTags].gameObject; 
                        obj.SetActive(true);
                        Networking.SetOwner(playerApi,obj);
                        if (Networking.LocalPlayer == playerApi)
                        {
                            assignedTags[indexOfTags].enabled = true; 
                        } 
                    
                        assignedPlayers[indexOfTags] = playerApi;
                        LoggerPrint($"{obj.name} at index {indexOfTags} is assigned to player {Networking.GetOwner(obj).displayName}"); 
                        indexOfTags++; 
                    }
                    else
                    {
                        indexOfTags++;
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
