using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StarterKit
{

    public class ObjectTools : EditorWindow
    {
        private int folderIndex = 0;
        public GameObject userPrefab;

        [MenuItem("Udon Starter Kit/Object Tools", false, 38)]

        public static void ShowWindow()
        {
            ObjectTools window = (ObjectTools) GetWindow(typeof(ObjectTools));
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Object Tools", EditorStyles.boldLabel);

            GUILayout.Label("\nGenerate a VRCWorld prefab for your scene");

            if (GUILayout.Button("Generate World Prefab"))
            {
                object worldPrefab =
                    AssetDatabase.LoadAssetAtPath("Assets/VRChat Examples/Prefabs/VRCWorld.prefab", typeof(object));
                if(GameObject.Find("VRCWorld")){return;}
                if (worldPrefab != null)
                {
                    PrefabUtility.InstantiatePrefab(worldPrefab as GameObject);
                }
                else
                {
                    Debug.Log("the prefab is missing!! SOMEHOW!!");
                }
            }

            GUILayout.Label("Generate a default chair prefab for you scene");

            if (GUILayout.Button("Generate Chair Prefab"))
            {
                object chairPrefab =
                    AssetDatabase.LoadAssetAtPath("Assets/VRChat Examples/Prefabs/VRCChair/VRCChair3.prefab",
                        typeof(object));
                if (chairPrefab != null)
                {
                    PrefabUtility.InstantiatePrefab(chairPrefab as GameObject);
                }
                else
                {
                    Debug.Log("the prefab is missing!! SOMEHOW!!");
                }
            }
            
            userPrefab = EditorGUILayout.ObjectField("Any Prefab", userPrefab, typeof(object), false) as GameObject;
            if (GUILayout.Button("Generates Above Prefab"))
            {
                if (userPrefab != null)
                {
                    PrefabUtility.InstantiatePrefab((userPrefab as GameObject));
                }
            }

            if (GUILayout.Button("Spawn GameObject Folder and Re-parent"))
            {
                GameObject folderParent = new GameObject("Folder Object " + folderIndex);
                folderIndex++;
                folderParent.isStatic = true;
                foreach (GameObject obj in Selection.gameObjects)
                {
                    obj.transform.parent = folderParent.transform;
                }
            }

            if (GUILayout.Button("Set Selected Lights to Baked"))
            {
                if (Selection.objects.Length > 0)
                {
                    foreach (var obj in Selection.gameObjects)
                    {
                        Light light = obj.GetComponent<Light>();
                        light.lightmapBakeType = LightmapBakeType.Baked;
                    }
                }
            }

            GUILayout.Label("Additional tools are to be added with time.");
        }
    }
}
