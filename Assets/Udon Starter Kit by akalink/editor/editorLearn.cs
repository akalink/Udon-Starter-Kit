using System;
using UnityEditor;
using UnityEngine;

public class editorLearn : EditorWindow
{
    
    public Transform anchor;
    
    [MenuItem("Window/Example")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<editorLearn>("Example");
    }
    
    
    private void OnGUI()
    {
        GUILayout.Label("Generates a new UV2 on selected objects");

        if (GUILayout.Button("Set Static"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.isStatic = true;
            }
        }
        
        if (GUILayout.Button("Renderer Details"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                Renderer renderer = obj.GetComponent<Renderer>();

                if (renderer != null)
                {
                    string temp = obj.name + ": bounds of " + renderer.bounds;
                    temp += " Static batching is " + renderer.isPartOfStaticBatch;
                    temp += " light prope interpolation is: " + renderer.lightProbeUsage;
                    Debug.Log(temp);
                }
                else
                {
                    Debug.Log("There is no renderer on" + obj.name);
                }
            }
        }

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

        if (GUILayout.Button("Normalize Parent Scale & Position"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                Transform parent = obj.GetComponent<Transform>();
                Transform[] children = obj.GetComponentsInChildren<Transform>();
                if (children.Length == 1)
                {
                    return;
                }
                Vector3 scale = parent.localScale;
                Vector3 position = parent.position;
                /*foreach (Transform child in children)
                {
                    child.localScale = new Vector3(scale.x * child.localScale.x, scale.y * child.localScale.y, scale.z * child.localScale.z);
                    child.position = new Vector3(position.x + child.position.x, position.y + child.position.y, position.z + child.position.z);
                }*/
                for(int i = 1; i < children.Length; i++)
                {
                    Debug.Log(scale + children[i].localScale);
                    children[i].localScale = Vector3.Scale(children[i].localScale, scale);
                    children[i].position = children[i].position + position;
                }

                parent.localScale = Vector3.one;
                parent.position = Vector3.zero;
            }
            
        }
    }
}
