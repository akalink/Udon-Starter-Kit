
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class MirrorWithToggle : UdonSharpBehaviour
    {
        [Header("A set of mirrors that can be toggled on and off.")]
        [Header("By design this system is meant to be local and will not with 'network' toggled on")]
        public TextMeshProUGUI debugger;
        
        private Transform[] mirrors;
        

        void Start()
        {
            mirrors = GetComponentsInChildren<Transform>(true);
            Debug.Log("length of array is " + mirrors.Length);
            for (int i = 1; i < mirrors.Length; i++)
            {
                mirrors[i].gameObject.SetActive(false);
            }
        }

        public void _Mirror1()
        {
            ToggleThisNumber(1);
        } /////TODO visual button and HandCollider logic

        public void _Mirror2()
        {
            ToggleThisNumber(2);
        }
        
        public void _Mirror3()
        {
            ToggleThisNumber(3);
        }
        private void ToggleThisNumber(int mirrorInt)
        {
            Debug.Log("has not crashed yet, null check");
            if (mirrorInt < mirrors.Length)
            {
                Debug.Log("has not crashed yet, toggle main");
                mirrors[mirrorInt].gameObject.SetActive(!mirrors[mirrorInt].gameObject.activeSelf);
                Debug.Log("has not crashed yet, toggle off others");
                if (mirrors[mirrorInt].gameObject.activeSelf)
                {
                    for (int i = 1; i < mirrors.Length; i++)
                    {
                        if (i != mirrorInt)
                        {
                            mirrors[i].gameObject.SetActive(false);
                        }
                    }
                }
                
            }
        }
    }
}

