using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StarterKit
{
    
    public class MeshTools : EditorWindow
    {
        public Transform anchor;
        public Mesh MeshWithVertexColors;
        public Mesh MeshObject;
        private static Texture USKLogo;

        [MenuItem("Window/Udon Starter Kit/Mesh Tools", false, 39)]
        public static void ShowWindow()
        {
            MeshTools window = (MeshTools) GetWindow(typeof(MeshTools));
            window.minSize = new Vector2(300,300);
            window.maxSize = new Vector2(300,1080);
            window.Show();
            USKLogo = Resources.Load("usk-logo-thumbnail") as Texture2D;
        }

        private void OnGUI()
        {
            if(GUILayout.Button(USKLogo,GUIStyle.none))Application.OpenURL("https://github.com/akalink/Udon-Starter-Kit");
            
            GUILayout.Label("\nOutput's the selected objects' details to the unity console");
        
            if (GUILayout.Button("Renderer Details"))
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();

                    if (renderer != null)
                    {
                        string temp = obj.name + ": bounds are " + renderer.bounds; 
                        temp += ". Static batching is " + renderer.isPartOfStaticBatch;
                        //will add more if I receive requests
                        Debug.Log(temp);
                    }
                    else
                    {
                        Debug.Log("There is no renderer on " + obj.name);
                    }
                }
            }
            if (GUILayout.Button("Mesh Details"))
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    if (obj.GetComponent<MeshFilter>() != null)
                    {
                        Mesh mesh = obj.GetComponent<MeshFilter>().sharedMesh;
                        if (mesh != null)
                        {
                            string temp = obj.name + ": ";
                            Color32[] vertexColors32 = mesh.colors32;
                            temp += vertexColors32.Length == 0 ? "Does not have " : "Has ";
                            temp += "vertex Colors. ";
                            temp += "A UV2 ";
                            temp += mesh.uv2.Length == 0 ? "does not exist. " : "exists on this mesh. ";
                            int triangles = 0;
                            for (int i = 0; i < mesh.subMeshCount; i++)
                            {
                                triangles += mesh.GetTriangles(i).Length;
                            }
                            temp += "It has " + triangles + " triangles and "+ mesh.subMeshCount + " submeshes";
                            Debug.Log(temp); 
                        }
                    }
                    else 
                    {
                        Debug.Log("There is no mesh on " + obj.name + ", said the joker to the thief"); 
                    } 
                } 
            }
            GUILayout.Label("\nAssigns an object to be the anchor override\nof the selected objects");
            anchor = EditorGUILayout.ObjectField("Anchor Object", anchor, typeof(Transform), true) as Transform;
            if (GUILayout.Button("Set Anchor Override"))
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.probeAnchor = anchor;
                    }
                }
            }
        
            if (GUILayout.Button("Clear Anchor Override"))
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.probeAnchor = null;
                    }
                }
            }
            
            MeshWithVertexColors = EditorGUILayout.ObjectField("Mesh Object", MeshWithVertexColors, typeof(Mesh), true) as Mesh;
            if (GUILayout.Button("Remove Vertex Colors"))
            {
                Color32[] vertexColors32 = null;
                MeshWithVertexColors.colors32 = vertexColors32;
                Debug.Log(MeshWithVertexColors.colors32.Length );
                
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
            
            GUILayout.Label("\n(Re)Generates UV2 on the selected meshes");
            GUILayout.Label("Do not use if you have custom UV2s on these meshes!", EditorStyles.boldLabel);
            if(GUILayout.Button("Generate UV2"))
            {
                foreach(GameObject obj in Selection.gameObjects)
                {
                    MeshFilter filter = obj.GetComponent<MeshFilter>();
                    if (filter == null || filter.sharedMesh == null) continue;
                    Unwrapping.GenerateSecondaryUVSet(filter.sharedMesh);
                }
            }
            
            GUILayout.Label("Additional tools are to be added with time.");
        }
    }
}
