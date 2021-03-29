using BepInEx;
using COM3D2API;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin
{
    [BepInPlugin("COM3D2.Lilly.Plugin", "Lilly", "21.3.28")]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임

    public class Lilly : BaseUnityPlugin
    {
        public static bool isLogOn = true;
        public static void SetLogOnOff()
        {
            isLogOn = !isLogOn;
        }

        public static bool isGuiOn = false;
        public static void SetGuiOnOff()
        {
            isGuiOn = !isGuiOn;
        }

        public static HarmonyUtill? harmonyUtill;

        public Lilly()
        {
            harmonyUtill=new HarmonyUtill();
        }

        /// <summary>
        /// 한번만 실행됨
        /// </summary>
        public void Awake()
        {
            MyLog.LogMessage("Awake");

            HarmonyUtill.harmonyUtill.SetHarmonyListAll();
        }

        public void OnEnable()
        {
            MyLog.LogMessage("OnEnable");

            SceneManager.sceneLoaded += this.OnSceneLoaded;

            HarmonyUtill.harmonyUtill.SetHarmonyPatchAll();
        }      

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GearMenu.SetButton();
        }

        public void OnGUI()
        {
            /*
            if (!Lilly.isGuiOn)
            {
                return;
            }
            */
            HarmonyUtill.OnGui();
            InfoUtill.OnGui();
            CheatUtill.OnGui();
        }

        public void OnDisable()
        {
            MyLog.LogMessage("OnDisable");

            SceneManager.sceneLoaded -= this.OnSceneLoaded;

            HarmonyUtill.harmonyUtill.SetHarmonyUnPatchAll();
        }



    }
}
