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
        private static Texture USKLogo;

        [MenuItem("Window/Udon Starter Kit/Object Tools", false, 38)]

        public static void ShowWindow()
        {
            ObjectTools window = (ObjectTools) GetWindow(typeof(ObjectTools));
            window.Show();
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
