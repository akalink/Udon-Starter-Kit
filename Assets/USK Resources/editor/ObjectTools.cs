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
        // public Mesh MeshObject;
        private static Texture USKLogo;

        [MenuItem("Window/Udon Starter Kit/Object Tools", false, 38)]

        public static void ShowWindow()
        {
            ObjectTools window = (ObjectTools) GetWindow(typeof(ObjectTools));
            window.Show();
            window.minSize = new Vector2(300,300);
            window.maxSize = new Vector2(300,1080);
            USKLogo = Resources.Load("usk-logo-thumbnail") as Texture2D;
        }
        

        private void OnGUI()
        {
            
            if(GUILayout.Button(USKLogo,GUIStyle.none))Application.OpenURL("https://github.com/akalink/Udon-Starter-Kit");
            
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
            
            // MeshObject = EditorGUILayout.ObjectField("Mesh Object", MeshObject, typeof(Mesh), true) as Mesh;
            //
            // if (GUILayout.Button("Override Mesh Filter"))
            // {
            //     foreach (var obj in Selection.gameObjects)
            //     {
            //         if (obj.GetComponent<MeshFilter>() != null)
            //         {
            //             MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            //             meshFilter.mesh = MeshObject;
            //         }
            //     }
            //     
            // }

            if (GUILayout.Button("Copy Over Global Position"))
            {
                if(Selection.gameObjects.Length < 2) return;
                
                Vector3 vector3 = Selection.gameObjects[0].transform.position;
                int l = Selection.gameObjects.Length;
                for (int i = 1 - 1; i < l; i++)
                {
                    Undo.RecordObject(Selection.gameObjects[i], $"Changed position of {Selection.gameObjects[i].name} to {vector3}");
                    Selection.gameObjects[i].transform.position = vector3;
                    
                }
            }
            
            if (GUILayout.Button("Copy Over Local Position"))
            {
                if(Selection.gameObjects.Length < 2) return;
                
                Vector3 vector3 = Selection.gameObjects[0].transform.localPosition;
                int l = Selection.gameObjects.Length;
                for (int i = 1 - 1; i < l; i++)
                {
                    Undo.RecordObject(Selection.gameObjects[i], $"Changed position of {Selection.gameObjects[i].name} to {vector3}");
                    Selection.gameObjects[i].transform.localPosition = vector3;
                    
                }
            }

            GUILayout.Label("Additional tools are to be added with time.");
        }
        
    }
    

}
