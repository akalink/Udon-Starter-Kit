using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StarterKit
{

    public class PrefabSpawner : EditorWindow
    {
        private static Object WorldPrefab;
        //private static Texture2D worldPrefabPreview;
        private static readonly string FolderPath = "Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/";
        private static readonly string[] Prefabs = new[] {"VideoPlayers/UdonSyncPlayer (Unity)","VideoPlayers/UdonSyncPlayer (AVPro)","VRCChair/VRCChair3","AvatarPedestal", "ImageDownload","SimplePenSystem","StringDownloader", "Udon Variable Sync", "VRCMirror", "VRCPlayerVisualDamage", "VRCPortalMarker","VRCWorld"};
        private static Texture USKLogo;
        
        [MenuItem("Window/Udon Starter Kit/Prefab Spawner", false, 36)]
        
        public static void ShowWindow()
        {
            PrefabSpawner window = (PrefabSpawner) GetWindow(typeof(PrefabSpawner));
            window.Show();
            window.minSize = new Vector2(300,300);
            window.maxSize = new Vector2(300,1080);
            
            USKLogo = Resources.Load("usk-logo-thumbnail") as Texture2D;
        }

        public static void SetWindow()
        {

        }
        private void OnGUI()
        {
            if(GUILayout.Button(USKLogo,GUIStyle.none))Application.OpenURL("https://github.com/akalink/Udon-Starter-Kit");
            for (int i = 0; i < Prefabs.Length; i++)
            {
                if (GUILayout.Button("Generate " + Prefabs[i]))
                {
                    Debug.Log("You generated " + FolderPath + Prefabs[i] + ".prefab");
                    if(Prefabs[i] == "VRCWorld" && GameObject.Find("VRCWorld")){return;}

                    object prefab =
                        AssetDatabase.LoadAssetAtPath(FolderPath + Prefabs[i] + ".prefab", typeof(object));
                    if (prefab != null)
                    {
                        PrefabUtility.InstantiatePrefab(prefab as GameObject);
                    }
                    else
                    {
                        Debug.LogError($"Could not generate {Prefabs[i]} prefab");
                    }
                }
            }
        }
    }
}
