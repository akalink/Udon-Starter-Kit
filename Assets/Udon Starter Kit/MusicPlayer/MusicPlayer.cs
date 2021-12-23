
using System;
using TMPro;
using UdonSharp;
using UnityEditor;
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
        private bool isPlaying = true;
        private bool noTracks = false;
        
        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
        }
        void Start()
        {
            if (tracklist.Length == 0)
            {
                noTracks = true;
                LoggerPrint("There are no tracks assigned to the music player");
                return;
            }
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
            if (!noTracks)
            {
                deltaTimeCheck += Time.deltaTime;
                if (deltaTimeCheck > aSource.clip.length)
                { 
                    LoggerPrint("Delta Time Check Exceeded clip leangh at " + deltaTimeCheck);
                    if (synced) 
                    { 
                        clipIndexSync++; 
                        if (clipIndexSync >= tracklist.Length) 
                        {
                            clipIndexSync = 0; 
                        }
                        
                        if (Networking.LocalPlayer.IsOwner(gameObject))
                        {
                            RequestSerialization();
                            UpdateTrackListSync();
                        }
                    }
                    else
                    {
                        clipIndexLocal++; 
                        if (clipIndexLocal >= tracklist.Length)
                        {
                            clipIndexLocal = 0;
                        }
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
            if (isPlaying)
            {
                aSource.Play();
            }
        }
        
        public void UpdateTrackListLocal()
        {
            LoggerPrint("track number is " + clipIndexLocal);
            aSource.clip = tracklist[clipIndexLocal];
            deltaTimeCheck = 0;
            if (isPlaying)
            {
                aSource.Play();
            }
            
        }

        public override void OnDeserialization()
        {
            UpdateTrackListSync();
        }

        public void _TogglePlayback()
        {
            if (isPlaying)
            {
                aSource.Stop();
            }
            else
            {
                aSource.Play();
                deltaTimeCheck = 0;
            }

            isPlaying = !isPlaying;
        }
    }
}
