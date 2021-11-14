
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

        public Animator allMonthsAnimator;
        public string animatorIntName;
        #region Month variables

        public GameObject januaryObject;
        public GameObject februaryObject;
        public GameObject marchObject;
        public GameObject aprilObject;
        public GameObject mayObject;
        public GameObject juneObject;
        public GameObject julyObject;
        public GameObject augustObject;
        public GameObject septemberObject;
        public GameObject octoberObject;
        public GameObject novemberObject;
        public GameObject decemberObject;
        

        #endregion
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
            if (allMonthsAnimator != null)
            {
                allMonthsAnimator.SetInteger(animatorIntName, monthInt);
            }
            switch (monthInt)
            { //I know I could have done this with a gameObject array, but this is IMHO opinion well be easier for the end user to understand. In case you were wondering. 
                case 1: if (januaryObject != null) { januaryObject.SetActive(true);} break;
                case 2: if (februaryObject != null) { februaryObject.SetActive(true);}break;
                case 3: if (marchObject != null) { marchObject.SetActive(true);}break;
                case 4: if (aprilObject != null) { aprilObject.SetActive(true);}break;
                case 5: if (mayObject != null) { mayObject.SetActive(true);}break;
                case 6: if (juneObject != null) { juneObject.SetActive(true);}break;
                case 7: if (julyObject != null) { julyObject.SetActive(true);}break;
                case 8: if (augustObject != null) { augustObject.SetActive(true);}break;
                case 9: if (septemberObject != null) { septemberObject.SetActive(true);}break;
                case 10: if (octoberObject != null) { octoberObject.SetActive(true);}break;
                case 11: if (novemberObject != null) { novemberObject.SetActive(true);} break;
                case 12: if (decemberObject != null) { decemberObject.SetActive(true);}break;
            }
        }

        public override void OnDeserialization()
        {
            MonthSelect();
        }
    }
}