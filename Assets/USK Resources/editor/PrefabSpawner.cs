using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StarterKit
{

    public class PrefabSpawner : EditorWindow
    {
        private static Object WorldPrefab;
        //private static Texture2D worldPrefabPreview;
        private static readonly string FolderPath = "Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/";//Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs
        private static List<string> Prefabs = new List<string>();// new[] {"VideoPlayers/UdonSyncPlayer (Unity)","VideoPlayers/UdonSyncPlayer (AVPro)","VRCChair/VRCChair3","AvatarPedestal", "ImageDownload","SimplePenSystem","StringDownloader", "Udon Variable Sync", "VRCMirror", "VRCPlayerVisualDamage", "VRCPortalMarker","VRCWorld"};
        private static Texture USKLogo;
        
        [MenuItem("Window/Udon Starter Kit/Prefab Spawner", false, 36)]
        
        public static void ShowWindow()
        {
            PrefabSpawner window = (PrefabSpawner) GetWindow(typeof(PrefabSpawner));
            window.Show();
            window.minSize = new Vector2(300,300);
            window.maxSize = new Vector2(300,1080);
            
            USKLogo = Resources.Load("usk-logo-thumbnail") as Texture2D;
            if(Prefabs.Count > 0){return;}

            string[] s = Directory.GetFiles(FolderPath, ".", SearchOption.TopDirectoryOnly);
            string[] d = Directory.GetDirectories(FolderPath, "*", SearchOption.TopDirectoryOnly);
            
            foreach (string obj in s)
            {
                if(obj.Contains("meta")) continue;
                Prefabs.Add(obj);
            }

            foreach (string dir in d)
            {
                s = Directory.GetFiles(dir, ".", SearchOption.TopDirectoryOnly);
                foreach (string obj in s)
                {
                    if (obj.Contains(".prefab") && !obj.Contains(".meta"))
                    {
                        Prefabs.Add(obj);
                    }
                }
            }
        }

        // public static void SetWindow()
        // {
        //
        // }
        private void OnGUI()
        {
            if(GUILayout.Button(USKLogo,GUIStyle.none))Application.OpenURL("https://github.com/akalink/Udon-Starter-Kit");
            GUILayout.Label("Generate any of listed prefabs from the sdk");   
            for (int i = 0; i < Prefabs.Count; i++)
            {
                
                string[] name = Prefabs[i].Split("/");
                name = name[name.Length - 1].Split(".");
                if (GUILayout.Button(name[0]))
                {
                    
                    if(name[0] == "VRCWorld" && GameObject.Find("VRCWorld")){return;}
                    Debug.Log("You generated " + name[0]);
                    object prefab =
                        AssetDatabase.LoadAssetAtPath( Prefabs[i], typeof(object));
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
