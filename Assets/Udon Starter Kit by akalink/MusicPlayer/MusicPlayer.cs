
using System;
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

        [UdonSynced()] private int clipIndexSync = 0;
        private int clipIndexLocal = 0;
        private AudioSource aSource;
        private float deltaTimeCheck;
        void Start()
        {
            aSource = this.GetComponent<AudioSource>();
            UpdateTrackList();

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
                    UpdateTrackList();
                }
                else
                {
                    clipIndexLocal++;
                    if (clipIndexLocal >= tracklist.Length)
                    {
                        clipIndexLocal = 0;
                        UpdateTrackList();
                    }
                }
                
            }
        }

        public void UpdateTrackList()
        {
            aSource.clip = tracklist[clipIndexSync];
            deltaTimeCheck = 0;
            aSource.Play();
        }

        public override void OnDeserialization()
        {
            UpdateTrackList();
        }
    }
}
