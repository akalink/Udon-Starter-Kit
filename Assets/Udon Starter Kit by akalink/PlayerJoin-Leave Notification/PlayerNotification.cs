
using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class PlayerNotification : UdonSharpBehaviour
    {
        [Header("Notifies everyone in the instance a player has joined or left. Can be done via sound or naming the player")]
        public string joinInstanceText = "has joined the world.";

        public string leaveInstanceText = "has left the world.";
        private TextMeshProUGUI ssText;
        private bool readyToDisplay = true;
        private Animator anim;
        private string animName = "showMessage";
        private string[] textLineUp = new string[82];
        private string[] _unclaimedQueue;
        private int _unclaimedQueueStart = 0;
        private int _unclaimedQueueEnd = 0;
        private int _queueBufferSize = 10;

        void Start()
        {
            ssText = this.GetComponentInChildren<TextMeshProUGUI>();
            anim = this.GetComponent<Animator>();
            
            _FillUnclaimedIndexQueue();
            
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            string tempText = player.displayName + " " + joinInstanceText;
            if (readyToDisplay)
            {
                _DisplayUIText(tempText);
            }
            else
            {
                _EnqueueItemToUnclaimedQueue(tempText);
            }

            readyToDisplay = false;
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            
            string tempText = player.displayName + " " + leaveInstanceText;
            if (readyToDisplay)
            {
                _DisplayUIText(tempText);
            }
            else
            {
                _EnqueueItemToUnclaimedQueue(tempText);
            }

            readyToDisplay = false;
        }

        private int _UnclaimedQueueCount()
        {
            return _unclaimedQueueEnd - _unclaimedQueueStart;
        }
        
        private string _DequeueItemFromUnclaimedQueue()
        {
            // If the queue is empty, return invalid.
            if (_UnclaimedQueueCount() <= 0)
            {
                Debug.Log("this returned the first thing");
                return "";
            }

            // Get the index for the start of the queue and increment the value.
            int index = _unclaimedQueueStart % _unclaimedQueue.Length;
            ++_unclaimedQueueStart;
            
            // Get the first element in the queue
            string element = _unclaimedQueue[index];
            // Clear the value at this index to ensure old elements are never reused.
            _unclaimedQueue[index] = "";
            
            return element;
        }

        private void _FillUnclaimedIndexQueue()
        {
            //int size = _assignment.Length;
            _unclaimedQueue = new String[_queueBufferSize];
            
            for (int i = 0; i < _queueBufferSize; ++i)
            {
                // Ensure queue is initialized with -1.
                _unclaimedQueue[i] = "";
                
                // If the current index does not have a player assigned, add it to the queue.
                /*if (_assignment[i] == -1)
                {
                    _EnqueueItemToUnclaimedQueue(i);
                }*/
            }
        }

        // Add the given unclaimed index into the queue
        // O(1)
        private void _EnqueueItemToUnclaimedQueue(string value)
        {
            if (_UnclaimedQueueCount() >= _unclaimedQueue.Length)
            {
                Debug.Log("Trying to queue an item when the queue is full!");
                return;
            }
            
            // Get the index for the end of the queue and increment the value.
            int index = _unclaimedQueueEnd % _unclaimedQueue.Length;
            ++_unclaimedQueueEnd;
            Debug.Log("enqueue value " + value + "index is " + index);
            _unclaimedQueue[index] = value;
        }

        public void _DisplayUIText(string text)
        {
            ssText.text = text;
            anim.SetTrigger(animName);
        }

        public void _AnimMakesReady()
        {
            readyToDisplay = true;
            Debug.Log("the animator got here");
            string tempText = _DequeueItemFromUnclaimedQueue();
            Debug.Log("retunred dequeue with" + tempText);
            if (tempText != "")
            {
                readyToDisplay = false;
                Debug.Log("there is a name");
                _DisplayUIText(tempText);
            }
        }
    }
}