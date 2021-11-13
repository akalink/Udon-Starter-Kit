
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace StarterKit
{
    public class SyncedObjectToggle : UdonSharpBehaviour
    {
        [Header("Takes a input of Game Objects and toggles them on or off based on their original state")]
        [Header("Alternative, toggle objects either on or off using Public Methods")]
        public bool Synced = false;
        public GameObject[] objects;
        [UdonSynced()] private bool[] objectsBoolSynched;
        private bool[] objectsBoolLocal;
        private bool noRange = false;
        void Start()
        {
            if (objects.Length == 0)
            {
                noRange = true;
                return;
            }

            objectsBoolSynched = new bool [objects.Length];
            for (int i = 0; i < objectsBoolSynched.Length || i < objects.Length; i++)
            { 
                objectsBoolSynched[i] = objects[i].activeSelf;
            }
                
            objectsBoolLocal = new bool [objects.Length];
            for (int i = 0; i < objectsBoolLocal.Length || i < objects.Length; i++)
            {
                objectsBoolLocal[i] = objects[i].activeSelf;
            }
        }

        public override void Interact()
        {
            if (!noRange)
            {
                if (Synced)
                {
                    SendCustomNetworkEvent(NetworkEventTarget.All,nameof(ToggleObjects));
                }
                else
                {
                    ToggleObjects();
                }
                
            }
            
        }

        public void ToggleObjects()
        {
            if (Synced)
            { 
                if (Networking.LocalPlayer.IsOwner(gameObject)) 
                { 
                    for (int i = 0; i < objectsBoolSynched.Length; i++) 
                    { 
                        objectsBoolSynched[i] = !objectsBoolSynched[i]; 
                        RequestSerialization(); 
                        IterateThroughObjects();
                    }
                }
            }
            else 
            { 
                for (int i = 0; i < objectsBoolLocal.Length; i++) 
                { 
                    objectsBoolLocal[i] = !objectsBoolLocal[i];
                    IterateThroughObjects();
                    
                }
            }
        }

        public void SetFalse()
        {
            
        }

        public void SetTrue()
        {
            
        }
        
        

        private void IterateThroughObjects()
        {
            if (Synced)
            {
                for (int i = 0; i < objectsBoolSynched.Length || i < objects.Length; i++)
                {
                    objects[i].SetActive(objectsBoolSynched[i]);
                }
            }
            else
            {
                for (int i = 0; i < objectsBoolLocal.Length || i < objects.Length; i++)
                {
                    objects[i].SetActive(objectsBoolLocal[i]);
                }
            }
        }

        public override void OnDeserialization()
        {
            IterateThroughObjects();
        }
    }
}

