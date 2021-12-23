
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
        [Header("By design this system is meant to be local and will not work with 'network' toggled on")]
        public TextMeshProUGUI logger;
        
        private Transform[] mirrors;
        

        private void LoggerPrint(string text)
        {
            if (logger != null)
            {
                logger.text += "-" + this.name + "-" + text + "\n";
            }
        }
        void Start()
        {
            mirrors = GetComponentsInChildren<Transform>(true);
            for (int i = 1; i < mirrors.Length; i++)
            {
                mirrors[i].gameObject.SetActive(false);
            }
            LoggerPrint("There are " + mirrors.Length + " mirrors found");
        }

        public void _Mirror1()
        {
            ToggleThisNumber(1);
        } 

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
            if (mirrorInt < mirrors.Length)
            {
                mirrors[mirrorInt].gameObject.SetActive(!mirrors[mirrorInt].gameObject.activeSelf);
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
                LoggerPrint("Mirror " + mirrorInt + " has been toggled to be " + mirrors[mirrorInt].gameObject.activeSelf);
            }
        }
    }
}

