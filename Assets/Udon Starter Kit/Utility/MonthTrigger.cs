
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace StarterKit
{
    public class MonthTrigger : UdonSharpBehaviour
    {
        [Header("Gets the month of the year as enable/triggers based on that data")]
        [UdonSynced()] private int monthInt;
        private DateTime masterTime;
        void Start()
        {
            if (Networking.LocalPlayer.IsOwner(gameObject))
            {
                masterTime = DateTime.Now;
                switch (masterTime.Month)
                {
                    case 1:
                        monthInt = 1; break;
                    case 2:
                        monthInt = 2; break;
                    case 3:
                        monthInt = 3; break;
                    case 4:
                        monthInt = 4; break;
                    case 5:
                        monthInt = 5; break;
                    case 6:
                        monthInt = 6; break;
                    case 7:
                        monthInt =  7; break;
                    case 8:
                        monthInt = 8; break;
                    case 9:
                        monthInt = 9; break;
                    case 10:
                        monthInt = 10; break;
                    case 11: 
                        monthInt = 11; break;
                    case 12:
                        monthInt = 12; break;
                }
                RequestSerialization();
                MonthSelect();
            }
        }

        private void MonthSelect()
        {
            switch (monthInt)
            {
                case 1: break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 5: break;
                case 6: break;
                case 7: break;
                case 8: break;
                case 9: break;
                case 10: break;
                case 11: Debug.Log("returned november"); break;
                case 12: break;
            }
        }

        public override void OnDeserialization()
        {
            MonthSelect();
        }
    }
}