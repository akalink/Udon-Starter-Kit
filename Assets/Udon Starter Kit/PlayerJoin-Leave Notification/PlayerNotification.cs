
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
        [Header(
            "Notifies everyone in the instance a player has joined or left. Can be done via sound or naming the player")]
        public TextMeshProUGUI logger;
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
        private bool toggledOn = true;
        private AudioSource _audioSouce;

        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
        }
        
        void Start()
        {
            ssText = this.GetComponentInChildren<TextMeshProUGUI>();
            anim = this.GetComponent<Animator>();
            _audioSouce = GetComponent<AudioSource>();
            _FillUnclaimedIndexQueue();
        }
        
        public void _ToggleSystem()
        {
            toggledOn = !toggledOn;
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if(!toggledOn){return;}
            LoggerPrint(player.displayName + " has joined and will trigger the animation / que");
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
            if(!toggledOn){return;}
            LoggerPrint(player.displayName + " has left and will trigger the animation / que");
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
                return "";
            }

            // Get the index for the start of the queue and increment the value.
            int index = _unclaimedQueueStart % _unclaimedQueue.Length;
            ++_unclaimedQueueStart;
            
            // Get the first element in the queue
            string element = _unclaimedQueue[index];
            // Clear the value at this index to ensure old elements are never reused.
            _unclaimedQueue[index] = "";
            LoggerPrint(index + " index value is now reinitialized");
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
                LoggerPrint(i + " index value is now initialized");
            }
        }

        // Add the given unclaimed index into the queue
        // O(1)
        private void _EnqueueItemToUnclaimedQueue(string value)
        {
            if (_UnclaimedQueueCount() >= _unclaimedQueue.Length)
            {
                LoggerPrint("The Que is full");
                return;
            }
            
            // Get the index for the end of the queue and increment the value.
            int index = _unclaimedQueueEnd % _unclaimedQueue.Length;
            ++_unclaimedQueueEnd;
            
            LoggerPrint("The value of the ques index is "+ index);
            _unclaimedQueue[index] = value;
        }

        public void _DisplayUIText(string text)
        {
            LoggerPrint(text + " text has been animated on the player view");
            ssText.text = text;
            anim.SetTrigger(animName);
            if (_audioSouce != null)
            {
                LoggerPrint("The Notification Sound has Played");
                _audioSouce.Play();
            }
        }

        public void _AnimMakesReady()
        {
            LoggerPrint("The animation has finished");
            readyToDisplay = true;
            string tempText = _DequeueItemFromUnclaimedQueue();
            if (tempText != "")
            {
                readyToDisplay = false;
                _DisplayUIText(tempText);
            }
        }
    }
}