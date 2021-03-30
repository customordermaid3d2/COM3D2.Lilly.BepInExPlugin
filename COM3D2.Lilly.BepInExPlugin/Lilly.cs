﻿using BepInEx;
using COM3D2API;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin
{
    // https://github.com/customordermaid3d2/COM3D2.Lilly.BepInExPlugin
    [BepInPlugin("COM3D2.Lilly.Plugin", "COM3D2.Lilly.Plugin", "21.3.28")]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임

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
        public static InfoUtill? infoUtill;
        public static CheatUtill? cheatUtill;
        public static EasyUtill? easyUtill;

        public Lilly()
        {
            harmonyUtill=new HarmonyUtill();
            infoUtill = new InfoUtill();
            cheatUtill = new CheatUtill();
            easyUtill = new EasyUtill();
        }

        /// <summary>
        /// 한번만 실행됨
        /// </summary>
        public void Awake()
        {
            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime dateTime = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            MyLog.LogMessage("Awake",dateTime.ToString("u"));

            HarmonyUtill.SetHarmonyListAll();
            EasyUtill.SetScene();
            SetOnGUIlist();
        }

        public void OnEnable()
        {
            MyLog.LogMessage("OnEnable");

            SceneManager.sceneLoaded += this.OnSceneLoaded;

            HarmonyUtill.SetHarmonyPatchAll();
            
        }

        public static Scene scene;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Lilly.scene = scene;
            MyLog.LogMessage("OnSceneLoaded"
                , scene.buildIndex
                , scene.rootCount
                , scene.name
                , scene.isLoaded
                , scene.isDirty
                , scene.IsValid()
                , scene.path
                , mode
                );
            GearMenu.SetButton();
        }

        public static event Action actions ;

        public void SetOnGUIlist()
        {
            actions+= harmonyUtill.OnGui;
            actions+= infoUtill.OnGui;
            actions+= cheatUtill.OnGui;
            actions+= easyUtill.OnGui;
        }

        public void OnGUI()
        {
            /*
            if (!Lilly.isGuiOn)
            {
                return;
            }
            */
            actions();
        }

        public void OnDisable()
        {
            MyLog.LogMessage("OnDisable");

            SceneManager.sceneLoaded -= this.OnSceneLoaded;

            HarmonyUtill.SetHarmonyUnPatchAll();
        }



    }
}