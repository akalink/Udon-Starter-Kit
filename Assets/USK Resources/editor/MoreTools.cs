using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StarterKit
{
    public class MoreTools : EditorWindow
    {
        private static Texture _iconCyanTriggers, _iconPrefabs, _iconVrWTC, _iconEQS, _iconUdonTk, _iconBlender, _iconGimp,
            _iconKrita, _iconVowgan, _iconVRCDis, _iconVRCVids, _iconProTV, _iconLibrary; 
        [MenuItem("Window/Udon Starter Kit/Suggested Resources", false, 42)]
        public static void SetWindow()
        {
            var window = (MoreTools) GetWindow(typeof(MoreTools), true, "Udon Starter Kit");
            window.minSize = new Vector2(600, 500);
            window.maxSize = new Vector2(600, 500);
            window.Show();
            
            _iconVrWTC = Resources.Load("vrwtk-thumbnail") as Texture2D;
            _iconEQS = Resources.Load("eqs-thumbnail") as Texture2D;
            _iconPrefabs = Resources.Load("prefabs-thumbnail") as Texture2D;
            _iconCyanTriggers = Resources.Load("cyantrigger-thumbnail") as Texture2D;
            _iconUdonTk = Resources.Load("udon toolkit logo") as Texture2D;
            _iconVRCDis = Resources.Load("vrc discord") as Texture2D;
            _iconVRCVids = Resources.Load("vrchat tutorials") as Texture2D;
            _iconVowgan = Resources.Load("vowgan logo") as Texture2D;
            _iconBlender = Resources.Load("blender logo") as Texture2D;
            _iconKrita = Resources.Load("krita logo") as Texture2D;
            _iconGimp = Resources.Load("gimp logo") as Texture2D;
            _iconProTV = Resources.Load("protv logo") as Texture2D;
            _iconLibrary = Resources.Load("library logo") as Texture2D;


        }

        private void OnGUI()
        {
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(_iconVrWTC, GUIStyle.none)) Application.OpenURL("https://github.com/oneVR/VRWorldToolkit/releases");
            if (GUILayout.Button(_iconEQS, GUIStyle.none)) Application.OpenURL("https://booth.pm/ja/items/1893576");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(_iconPrefabs, GUIStyle.none)) Application.OpenURL("https://docs.google.com/spreadsheets/d/e/2PACX-1vTP-eIkYLZh7pDhpO-untxy1zbuoiqdzVP2z5-vg_9ijBW7k8ZC9VP6cVL-ct5yKrySPBPJ6V2ymlWS/pubhtml");
            if (GUILayout.Button(_iconCyanTriggers, GUIStyle.none)) Application.OpenURL("https://cyanlaser.booth.pm/items/3194594");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(_iconUdonTk, GUIStyle.none)) Application.OpenURL("https://github.com/orels1/UdonToolkit");
            if (GUILayout.Button(_iconVRCDis, GUIStyle.none)) Application.OpenURL("https://discord.com/invite/vrchat");
            if (GUILayout.Button(_iconVRCVids, GUIStyle.none)) Application.OpenURL("https://www.youtube.com/watch?v=8gXzBTqlP6I");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(_iconVowgan, GUIStyle.none)) Application.OpenURL("https://www.youtube.com/c/VowganVR");
            if (GUILayout.Button(_iconBlender, GUIStyle.none)) Application.OpenURL("https://www.blender.org/");
            if (GUILayout.Button(_iconKrita, GUIStyle.none)) Application.OpenURL("https://krita.org/en/");
            if (GUILayout.Button(_iconGimp, GUIStyle.none)) Application.OpenURL("https://www.gimp.org/");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(_iconProTV, GUIStyle.none)) Application.OpenURL("https://ask.vrchat.com/t/protv-by-architechanon-usage-guides-and-walkthroughs/7029");
            if (GUILayout.Button(_iconLibrary, GUIStyle.none)) Application.OpenURL("https://vrclibrary.com/wiki/");
            EditorGUILayout.EndHorizontal();
        }
    }
}

