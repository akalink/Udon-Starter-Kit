using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StarterKit
{
    public class MoreTools : EditorWindow
    {
        private static Texture iconCyanEmu, iconCyanTriggers, iconPrefabs, iconVRWTC, iconEQS, iconWCA, iconUsharp; 
        [MenuItem("Udon Starter Kit/Suggested Resources")]
        public static void SetWindow()
        {
            var window = (MoreTools) GetWindow(typeof(MoreTools), true, "Udon Starter Kit");
            window.minSize = new Vector2(600, 600);
            //window.maxSize = new Vector2(600, 600);
            window.Show();
        
            iconCyanEmu = Resources.Load("cyanEmu-thumbnail") as Texture2D;
            iconVRWTC = Resources.Load("vrwtk-thumbnail") as Texture2D;
            iconEQS = Resources.Load("eqs-thumbnail") as Texture2D;
            iconPrefabs = Resources.Load("prefabs-thumbnail") as Texture2D;
            iconCyanTriggers = Resources.Load("cyantrigger-thumbnail") as Texture2D;

        }

        private void OnGUI()
        {
            if (GUILayout.Button(iconCyanEmu, GUIStyle.none)) Application.OpenURL("https://github.com/CyanLaser/CyanEmu");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(iconVRWTC, GUIStyle.none)) Application.OpenURL("https://github.com/oneVR/VRWorldToolkit/releases");
            if (GUILayout.Button(iconEQS, GUIStyle.none)) Application.OpenURL("https://github.com/oneVR/VRWorldToolkit/releases");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(iconPrefabs, GUIStyle.none)) Application.OpenURL("https://github.com/oneVR/VRWorldToolkit/releases");
            if (GUILayout.Button(iconCyanTriggers, GUIStyle.none)) Application.OpenURL("https://github.com/oneVR/VRWorldToolkit/releases");
            EditorGUILayout.EndHorizontal();
        }
    }
}
