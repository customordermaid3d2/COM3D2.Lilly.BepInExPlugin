using BepInEx;
using BepInEx.Configuration;
using COM3D2API;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin
{
    // https://github.com/customordermaid3d2/COM3D2.Lilly.BepInExPlugin
    [BepInPlugin("COM3D2.Lilly.Plugin", "COM3D2.Lilly.Plugin", "21.4.2")]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임
    [BepInProcess("COM3D2x64.exe")]
    public class Lilly : BaseUnityPlugin
    {
        public static ConfigFile customFile;// = new ConfigFile(Path.Combine(Paths.ConfigPath, "COM3D2.Lilly.Plugin.cfg"), true);

        Stopwatch stopwatch = new Stopwatch(); //객체 선언
        
        public static System.Random rand = new System.Random();

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
        public static CheatGUI? cheatUtill;
        public static EasyUtill? easyUtill;
        public static MaidEditGui? maidEditGui;

        public Lilly()
        {
            stopwatch.Start(); // 시간측정 시작
            MyLog.LogDarkBlue("Lilly", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));

            customFile = Config;
            GUIVirtual.customFile = Lilly.customFile;

            harmonyUtill =new HarmonyUtill();
            infoUtill = new InfoUtill();
            cheatUtill = new CheatGUI();
            easyUtill = new EasyUtill();
            maidEditGui = new MaidEditGui();

            GearMenu.SetButton();
        }

        /// <summary>
        /// 한번만 실행됨
        /// </summary>

        public void Awake()
        {
            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime dateTime = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            MyLog.LogMessage("Awake",dateTime.ToString("u"));
            MyLog.LogDarkBlue("https://github.com/customordermaid3d2/COM3D2.Lilly.BepInExPlugin");

            SceneEditPatch.Awake();
            harmonyUtill.Awake();

            
        }

        public void OnEnable()
        {
            MyLog.LogMessage("OnEnable");

            SceneManager.sceneLoaded += this.OnSceneLoaded;

            HarmonyUtill.SetHarmonyPatchAll();
            
        }

        public void Start()
        {
            MyLog.LogMessage("Start");
        }

        public static Scene scene;

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // SceneManager.GetActiveScene().name 
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
                , string.Format("{0:0.000} ", stopwatch.Elapsed.ToString())
                );
            
        }

        public void OnGUI()
        {
            /*
            if (!Lilly.isGuiOn)
            {
                return;
            }
            */
            GUIVirtual.ActionsOnGui();
        }

        public void OnDisable()
        {
            MyLog.LogMessage("OnDisable");

            GUIVirtual.SetGuiOffAll();

            SceneManager.sceneLoaded -= this.OnSceneLoaded;

            HarmonyUtill.SetHarmonyUnPatchAll();
        }



    }
}
