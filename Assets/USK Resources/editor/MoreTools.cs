using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StarterKit
{
    public class MoreTools : EditorWindow
    {
        private static Texture iconCyanTriggers, iconPrefabs, iconVRWTC, iconEQS, iconWCA, iconBlender, iconGimp,
            iconKrita, iconVowgan, iconVRCDis, iconVRCVids; 
        [MenuItem("Window/Udon Starter Kit/Suggested Resources", false, 42)]
        public static void SetWindow()
        {
            var window = (MoreTools) GetWindow(typeof(MoreTools), true, "Udon Starter Kit");
            window.minSize = new Vector2(600, 500);
            window.maxSize = new Vector2(600, 500);
            window.Show();
            
            iconVRWTC = Resources.Load("vrwtk-thumbnail") as Texture2D;
            iconEQS = Resources.Load("eqs-thumbnail") as Texture2D;
            iconPrefabs = Resources.Load("prefabs-thumbnail") as Texture2D;
            iconCyanTriggers = Resources.Load("cyantrigger-thumbnail") as Texture2D;
            iconWCA = Resources.Load("wca logo") as Texture2D;
            iconVRCDis = Resources.Load("vrc discord") as Texture2D;
            iconVRCVids = Resources.Load("vrchat tutorials") as Texture2D;
            iconVowgan = Resources.Load("vowgan logo") as Texture2D;
            iconBlender = Resources.Load("blender logo") as Texture2D;
            iconKrita = Resources.Load("krita logo") as Texture2D;
            iconGimp = Resources.Load("gimp logo") as Texture2D;
            
            
        }

        private void OnGUI()
        {
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(iconVRWTC, GUIStyle.none)) Application.OpenURL("https://github.com/oneVR/VRWorldToolkit/releases");
            if (GUILayout.Button(iconEQS, GUIStyle.none)) Application.OpenURL("https://booth.pm/ja/items/1893576");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(iconPrefabs, GUIStyle.none)) Application.OpenURL("https://docs.google.com/spreadsheets/d/e/2PACX-1vTP-eIkYLZh7pDhpO-untxy1zbuoiqdzVP2z5-vg_9ijBW7k8ZC9VP6cVL-ct5yKrySPBPJ6V2ymlWS/pubhtml");
            if (GUILayout.Button(iconCyanTriggers, GUIStyle.none)) Application.OpenURL("https://cyanlaser.booth.pm/items/3194594");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(iconWCA, GUIStyle.none)) Application.OpenURL("https://github.com/Varneon/WorldCreatorAssistant");
            if (GUILayout.Button(iconVRCDis, GUIStyle.none)) Application.OpenURL("https://discord.com/invite/vrchat");
            if (GUILayout.Button(iconVRCVids, GUIStyle.none)) Application.OpenURL("https://www.youtube.com/watch?v=8gXzBTqlP6I");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(iconVowgan, GUIStyle.none)) Application.OpenURL("https://www.youtube.com/c/VowganVR");
            if (GUILayout.Button(iconBlender, GUIStyle.none)) Application.OpenURL("https://www.blender.org/");
            if (GUILayout.Button(iconKrita, GUIStyle.none)) Application.OpenURL("https://krita.org/en/");
            if (GUILayout.Button(iconGimp, GUIStyle.none)) Application.OpenURL("https://www.gimp.org/");
            EditorGUILayout.EndHorizontal();
        }
    }
}

