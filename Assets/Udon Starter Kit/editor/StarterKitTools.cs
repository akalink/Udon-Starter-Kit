using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class StarterKitTools : EditorWindow
{

    [MenuItem("Window/Starer Kit Tools")]
    public static void SetWindow()
    {
        StarterKitTools window = (StarterKitTools) GetWindow(typeof(StarterKitTools));
        window.minSize = new Vector2(400, 500);
        window.Show();
    }
}
