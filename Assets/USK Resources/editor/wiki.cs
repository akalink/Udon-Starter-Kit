﻿
using UnityEngine;
using UnityEditor;

namespace StarterKit
{

    public class wiki : MonoBehaviour
    {
        [MenuItem("Udon Starter Kit/Wiki", false, 41)]
        private static void WikiLink()
        {
            Application.OpenURL("https://garrett-mcpherson.gitbook.io/udon-starter-kit-wiki/");
        }
    
    }
}