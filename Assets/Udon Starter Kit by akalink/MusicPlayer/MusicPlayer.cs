
using System;
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class MusicPlayer : UdonSharpBehaviour
    {
        [Header("A very simple music player that plays a playlist of songs.")]
        public AudioClip[] tracklist;

        public bool synced = false;

        public TextMeshProUGUI logger;

        [UdonSynced()] private int clipIndexSync = 0;
        private int clipIndexLocal = 0;
        private AudioSource aSource;
        private float deltaTimeCheck;
        
        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
        }
        void Start()
        {
            aSource = this.GetComponent<AudioSource>();
            if (synced)
            {
                UpdateTrackListSync();
            }
            else
            {
                UpdateTrackListLocal();
            }
        }

        private void Update()
        {
            deltaTimeCheck += Time.deltaTime;
            if (deltaTimeCheck > aSource.clip.length)
            {
                if (synced)
                {
                    clipIndexSync++;
                    if (clipIndexSync >= tracklist.Length)
                    {
                        clipIndexSync = 0;
                    }
                    RequestSerialization();
                    UpdateTrackListSync();
                }
                else
                {
                    clipIndexLocal++;
                    if (clipIndexLocal >= tracklist.Length)
                    {
                        clipIndexLocal = 0;
                        UpdateTrackListLocal();
                    }
                }
                
            }
        }

        public void UpdateTrackListSync()
        {
            LoggerPrint("track number is " + clipIndexSync);
            aSource.clip = tracklist[clipIndexSync];
            deltaTimeCheck = 0;
            aSource.Play();
        }
        
        public void UpdateTrackListLocal()
        {
            LoggerPrint("track number is " + clipIndexLocal);
            aSource.clip = tracklist[clipIndexLocal];
            deltaTimeCheck = 0;
            aSource.Play();
        }

        public override void OnDeserialization()
        {
            UpdateTrackListSync();
        }
    }
}
