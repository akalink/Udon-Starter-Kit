
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;
using VRC.SDKBase;
using VRC.Udon;

public class namedetect : UdonSharpBehaviour
{
    public String[] names;
    private int[] syncedNames;
    private PositionConstraint[] assignTags;
    private Transform resetPosition;


    void Start()
    {
        if(names.Length == 0){return;}
        resetPosition = GetComponent<Transform>();
        Debug.Log("namespates don't crash yet");
        assignTags = GetComponentsInChildren<PositionConstraint>(true);
        syncedNames = new int[assignTags.Length];
        Debug.Log("namespates don't crash yet 2");
        for (int i = 0; i < syncedNames.Length; i++)
        {
            syncedNames[i] = -1;
        }
        Debug.Log("Length of PC array = " + assignTags.Length);
        Debug.Log("Length of PID array = " + syncedNames.Length);
        CheckName(Networking.LocalPlayer);
    }

    private void CheckName(VRCPlayerApi player)
    {
        Debug.Log("does it crash here");
        string tempName = player.displayName;
        for (int i = 0; i < names.Length; i++)
        {
            Debug.Log("or here?");
            if (names[i] == tempName)
            {
                for (int k = 0; k < syncedNames.Length; k++)
                {
                    if (syncedNames[k] == -1)
                    {
                        syncedNames[k] = player.playerId;
                        Networking.SetOwner(player, assignTags[k].gameObject);
                        assignTags[k].enabled = true;
                        return;
                    }
                }
            }
        }
    }
    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        if(names.Length == 0){return;}
        for (int i = 0; i < names.Length; i++)
        {
            string tempName = player.displayName;
            if (names[i] == tempName)
            {
                syncedNames[i] = -1;
                assignTags[i].enabled = false;
                assignTags[i].gameObject.transform.position = resetPosition.position;
                
                return;
            }
        }
    }
}
