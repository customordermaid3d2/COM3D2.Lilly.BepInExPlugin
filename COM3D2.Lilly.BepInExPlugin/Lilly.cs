using BepInEx;
using BepInEx.Configuration;
using COM3D2.Lilly.Plugin.InfoPatch;
using COM3D2.Lilly.Plugin.MyGUI;
using COM3D2.Lilly.Plugin.Utill;
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
    [BepInPlugin("COM3D2.Lilly.Plugin", "COM3D2.Lilly.Plugin", "21.5.16")]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임
    [BepInProcess("COM3D2x64.exe")]
    public class Lilly : BaseUnityPlugin 
    {
        public static ConfigFile customFile;// = new ConfigFile(Path.Combine(Paths.ConfigPath, "COM3D2.Lilly.Plugin.cfg"), true);

        Stopwatch stopwatch = new Stopwatch(); //객체 선언
        
        public static System.Random rand = new System.Random();

        public static bool isLogOn = true;

        public static ConfigEntryUtill configEntryUtill;

        public static event Action actionsAwake;
        public static event Action actionsInit;

        public static void SetLogOnOff()
        {
            isLogOn = !isLogOn;
        }

        public static bool isGuiOn = false;

        public static void SetGuiOnOff()
        {
            isGuiOn = !isGuiOn;
        }

        public static HarmonyUtill harmonyUtill;
        public static InfoUtill infoUtill;
        public static CheatGUI cheatUtill;
        public static EasyUtill easyUtill;
        public static MaidEditGui maidEditGui;
        public static OnOffGUI OnOffGUI;
        public static PresetGUI presetGUI;

        public Lilly()
        {
            MyLog.LogDebug("https://github.com/customordermaid3d2/COM3D2.Lilly.BepInExPlugin");
            stopwatch.Start(); // 시간측정 시작
            MyLog.LogDarkBlue("Lilly", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));

            customFile = Config;
            AwakeUtill.customFile = Lilly.customFile;
            GUIVirtual.customFile = Lilly.customFile;
            ConfigEntryUtill.customFile = Lilly.customFile;
            configEntryUtill = ConfigEntryUtill.Create(
            "Lilly"
            , "OnSceneLoaded"
            );

            MyLog.LogMessage("ConfigFilePath", customFile.ConfigFilePath);

            harmonyUtill = new HarmonyUtill();
            infoUtill = new InfoUtill();
            cheatUtill = new CheatGUI();
            easyUtill = new EasyUtill();
            maidEditGui = new MaidEditGui();
            OnOffGUI = new OnOffGUI();
            presetGUI = new PresetGUI();

            GearMenu.SetButton();
            PresetUtill.init();

            // 성능이 너무 나쁨. 하모니가 괜히 클래스 지정한게 아닌듯
            //InvokeInit.Invoke();
            //InvokeAwake.Invoke();


            if (actionsInit.GetLength()>0)
            actionsInit();
        }



        /// <summary>
        /// 한번만 실행됨
        /// 단순 참조는 위에서 처리하고
        /// 초기화 같은건 이걸 이용하자
        /// </summary>

        public void Awake()
        {
            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime dateTime = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            MyLog.LogMessage("Awake",dateTime.ToString("u"));

            if (actionsAwake.GetLength() > 0)
                actionsAwake();
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
            GUIVirtual.ActionsStart();
        }

        public static Scene scene;


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // SceneManager.GetActiveScene().name 
            Lilly.scene = scene;
            if (configEntryUtill["OnSceneLoaded"])
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
