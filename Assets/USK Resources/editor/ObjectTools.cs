using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

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
                    Object obj = PrefabUtility.InstantiatePrefab(userPrefab);
                    Undo.RegisterCreatedObjectUndo(obj, $"Undo create {obj.name}");
                }
            }

            if (GUILayout.Button("Spawn Folder and Re-parent"))
            {
                GameObject folderParent = new GameObject("Folder Object " + folderIndex);
                folderIndex++;
                folderParent.isStatic = true;
                Undo.RegisterCreatedObjectUndo(folderParent, "Undo add folder parent");
                
                foreach (GameObject obj in Selection.gameObjects)
                {
                    Undo.SetTransformParent(obj.transform, folderParent.transform, "Undo re-parent");
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
                        if(light == null) continue;
                        Undo.RecordObject(light, $"Undo set {obj.name} to Baked");
                        light.lightmapBakeType = LightmapBakeType.Baked;
                        
                    }
                }
            }

            if (GUILayout.Button("Copy Over Global Position"))
            {
                if(Selection.gameObjects.Length < 2) return;
                
                Vector3 vector3 = Selection.gameObjects[0].transform.position;
                int l = Selection.gameObjects.Length;
                for (int i = 1 - 1; i < l; i++)
                {
                    Undo.RecordObject(Selection.gameObjects[i].transform, $"Changed position of {Selection.gameObjects[i].name} to {vector3}");
                    Selection.gameObjects[i].transform.position = vector3;
                    
                }
            }
            
            if (GUILayout.Button("Copy Over Global Rotation"))
            {
                if(Selection.gameObjects.Length < 2) return;
                
                Quaternion rotation = Selection.gameObjects[0].transform.rotation;
                int l = Selection.gameObjects.Length;
                for (int i = 1 - 1; i < l; i++)
                {
                    Undo.RecordObject(Selection.gameObjects[i], $"Changed position of {Selection.gameObjects[i].name} to {rotation}");
                    Selection.gameObjects[i].transform.rotation = rotation;
                    
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
            
            if (GUILayout.Button("Copy Over Local Rotation"))
            {
                if(Selection.gameObjects.Length < 2) return;
                
                Quaternion rotation = Selection.gameObjects[0].transform.localRotation;
                int l = Selection.gameObjects.Length;
                for (int i = 1 - 1; i < l; i++)
                {
                    Undo.RecordObject(Selection.gameObjects[i].transform, $"Changed position of {Selection.gameObjects[i].name} to {rotation}");
                    Selection.gameObjects[i].transform.localRotation = rotation;
                    
                }
            }

            GUILayout.Label("Additional tools are to be added with time.");
        }
        
    }
    

}
