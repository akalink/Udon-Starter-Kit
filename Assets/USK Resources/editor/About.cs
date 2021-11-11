using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

namespace StarterKit
{
    public class About : EditorWindow
    {
        private static Texture USKLogo;

        private static Texture PatreonIcon, twitterIcon, discordIcon, boothIcon;
        
        [MenuItem("Udon Starter Kit/About")]
        public static void SetWindow()
        {
            var window = (About) GetWindow(typeof(About), true, "About Udon Starter Kit");
            window.minSize = new Vector2(300, 450);
            window.maxSize = new Vector2(300, 450);
            window.Show();
        
            USKLogo = Resources.Load("usk-logo-thumbnail") as Texture2D;
            twitterIcon = Resources.Load("icon-twitter") as Texture2D;
            discordIcon = Resources.Load("icon-discord") as Texture2D;
            PatreonIcon = Resources.Load("icon-patreon") as Texture2D;
            boothIcon = Resources.Load("icon-booth") as Texture2D;
            
            
            
        }
        
        private static GUIStyle text;

        private void OnEnable()
        {
            text = new GUIStyle("Label")
            {
                wordWrap = true,
                richText = true
            };
        }

        private void OnGUI()
        {
            if(GUILayout.Button(USKLogo,GUIStyle.none))Application.OpenURL("https://github.com/akalink/Udon-Starter-Kit");

            GUILayout.Label("Welcome to Udon Starter Kit", EditorStyles.boldLabel);
            GUILayout.Space(10);
            GUILayout.Label("Udon Starter Kit is a series of assets I have developed to get the beginner world developer started without the need to start learning how to use Udon right out the gate. As they progress they will be able to integrate their code from this package with their own custom scripts. On top of udon script and prefabs, there is also an assortment of shaders and editor scripts. While built for beginners, developers of all skill levels will find some value from this package.", text);
            GUILayout.Label("If you would like to make suggestions, report issues, or just follow my work, please feel free to follow me on my social media channels", text);
            
            //GUILayout.Label("MIT License\n \nCopyright (c) 2021 Garrett McPherson");
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if(GUILayout.Button(twitterIcon,GUIStyle.none))Application.OpenURL("https://twitter.com/McPhersonsound");
            if(GUILayout.Button(PatreonIcon,GUIStyle.none))Application.OpenURL("https://www.patreon.com/mcphersonsound");
            if(GUILayout.Button(discordIcon,GUIStyle.none))Application.OpenURL("https://discord.gg/u4SNU3eRrd");
            if(GUILayout.Button(boothIcon,GUIStyle.none))Application.OpenURL("https://mcphersonsound.booth.pm/");
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.Label("MIT License\n \nCopyright (c) 2021 Garrett McPherson");
            
        }
    }
    
    
}
