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
        public Mesh MeshObject;

        [MenuItem("Window/Udon Starter Kit/Object Tools", false, 38)]

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
                if(GameObject.Find("VRCWorld")){return;}
                object worldPrefab =
                    AssetDatabase.LoadAssetAtPath("Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCWorld.prefab", typeof(object));
                if (worldPrefab != null)
                {
                    PrefabUtility.InstantiatePrefab(worldPrefab as GameObject);
                }
                else
                {
                    worldPrefab =
                        AssetDatabase.LoadAssetAtPath("Assets/VRChat Examples/Prefabs/VRCWorld.prefab", typeof(object));
                    if (worldPrefab != null)
                    {
                        PrefabUtility.InstantiatePrefab(worldPrefab as GameObject);
                        return;
                    }
                    
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
                    chairPrefab =
                        AssetDatabase.LoadAssetAtPath("Assets/VRChat Examples/Prefabs/VRCChair/VRCChair3.prefab",
                            typeof(object));
                    if (chairPrefab != null)
                    {
                        PrefabUtility.InstantiatePrefab(chairPrefab as GameObject);
                        return;
                    }
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
            
            MeshObject = EditorGUILayout.ObjectField("Mesh Object", MeshObject, typeof(Mesh), true) as Mesh;
            
            if (GUILayout.Button("Override Mesh Filter"))
            {
                foreach (var obj in Selection.gameObjects)
                {
                    if (obj.GetComponent<MeshFilter>() != null)
                    {
                        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
                        meshFilter.mesh = MeshObject;
                    }
                }
                
            }

            GUILayout.Label("Additional tools are to be added with time.");
        }
    }
}
