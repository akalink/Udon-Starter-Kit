
using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;
using VRC.SDKBase;
using VRC.Udon;

public class namedetect : UdonSharpBehaviour
{
    public String[] names;
    [UdonSynced()] private int[] syncedIDs;
    private PositionConstraint[] assignTags;
    private Transform resetPosition;
    private bool noRange = false;
    private bool assignedObject = false;
    public TextMeshProUGUI logger;
    
    private void LoggerPrint(string text)
    {
        if (logger != null)
        {
            logger.text += "-" + this.name + "-" + text + "\n";
        }
    }
    
    void Start()
    {
        //move line
        if (names.Length == 0)
        {
            noRange = true;
            return;
        }
        resetPosition = GetComponent<Transform>();
        Debug.Log("namesplates don't crash yet");
        assignTags = GetComponentsInChildren<PositionConstraint>(true);
        syncedIDs = new int[assignTags.Length];
        Debug.Log("namespates don't crash yet 2");
        for (int i = 0; i < syncedIDs.Length; i++)
        {
            syncedIDs[i] = -1;
        }
        Debug.Log("Length of PC array = " + assignTags.Length);
        Debug.Log("Length of PID array = " + syncedIDs.Length);
        if (Networking.LocalPlayer.IsOwner(gameObject))
        {
            CheckName(Networking.LocalPlayer);
        }
    }
        

    private void CheckName(VRCPlayerApi player)
    {/*
        if (noRange)
        {
            return;
        }
        string tempName = player.displayName;
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i] == tempName)
            {
                for (int k = 0; k < syncedIDs.Length; k++)
                {
                    if (syncedIDs[k] == -1)
                    {
                        syncedIDs[k] = player.playerId;
                        Networking.SetOwner(player, assignTags[k].gameObject);
                        assignTags[k].enabled = true;
                        assignedObject = true;
                        RequestSerialization();
                        return;
                    }
                }
            }
        }*/
    }

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        LoggerPrint("checking if "+ player.displayName + " should get a object");
        if (noRange) { return; }
        if (Networking.LocalPlayer.IsOwner(gameObject))
        {
            LoggerPrint(Networking.LocalPlayer.displayName + " is owner");
            string tempName = player.displayName;
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == tempName)
                {
                    Debug.Log("display name is found");
                    for (int k = 0; k < syncedIDs.Length; k++)
                    {
                        if (syncedIDs[k] == -1)
                        {
                            LoggerPrint(player.playerId + " is assigned a space on the array");
                            syncedIDs[k] = player.playerId;
                            RequestSerialization();
                            IterateThroughNames();
                            return;
                        }
                    }
                }
            }
        }
    }

    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        if(noRange){ return; }
        int tempID = player.playerId;
        for (int i = 0; i < syncedIDs.Length; i++)
        {
            if (syncedIDs[i] == tempID)
            {
                syncedIDs[i] = -1;
                assignTags[i].enabled = false;
                assignTags[i].gameObject.transform.position = resetPosition.position;
                RequestSerialization();
                return;
            }
        }
    }

    public void IterateThroughNames()
    {
        if(assignedObject){return;}
        if (noRange) { return;}
        int tempID = Networking.LocalPlayer.playerId;
        for (int i = 0; i < syncedIDs.Length; i++)
        {
            if (syncedIDs[i] == tempID)
            {
                LoggerPrint(Networking.LocalPlayer.displayName + " their ID matched and are assigned a object");
                Networking.SetOwner(Networking.LocalPlayer, assignTags[i].gameObject);
                assignTags[i].enabled = true;
                assignedObject = true;
            }
        }
    }
    public override void OnDeserialization()
    {
        IterateThroughNames();
    }
}
