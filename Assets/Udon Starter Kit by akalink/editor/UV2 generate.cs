
using UnityEngine;
using UnityEditor;

public class UV2generate : EditorWindow
{
    public Mesh meshToFix;
    [MenuItem("Window/UV Generate")]
    static void OpenWindow()
    {
        UV2generate window = (UV2generate)GetWindow(typeof(UV2generate));
        window.minSize = new Vector2(600, 300);
        window.Show();
        
    }
    private void OnGUI()
    {
        GUILayout.Label("Generates a new UV2 on selected objects");
        if(GUILayout.Button("Generate UV2"))
        {
            foreach(GameObject obj in Selection.gameObjects)
            {
                MeshFilter filter = obj.GetComponent<MeshFilter>();
                if (filter == null || filter.sharedMesh == null) continue;
                Unwrapping.GenerateSecondaryUVSet(filter.sharedMesh);
            }
        }
    }

    //public static void Unwrapping.GenerateSecondaryUVSet(Mesh src, UnwrapParam settings); //must declare a body???

}
