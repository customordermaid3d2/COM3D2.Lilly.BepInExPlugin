using Schedule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using BepInEx.Configuration;
using BepInEx;
using MaidStatus;
using System.Reflection;

namespace COM3D2.Lilly.Plugin
{
    public class EasyUtill : GUIVirtual
    {
        //public static Scene scene;


        public static ConfigFile customFile;//= new ConfigFile(Path.Combine(Paths.ConfigPath, "COM3D2.Lilly.Plugin.EasyUtill.cfg"), true);

        public static ConfigEntry<bool> _GP01FBFaceEyeRandomOnOff;
        public static ConfigEntry<bool> _SetMaidStatusOnOff;

        public EasyUtill()
        {
            name = "EasyUtill";
            
        }


        internal void Awake()
        {
            customFile = Lilly.customFile;

            _GP01FBFaceEyeRandomOnOff = customFile.Bind(
              "EasyUtill",
              "_GP01FBFaceEyeRandomOnOff",
              true
              );
            _SetMaidStatusOnOff = customFile.Bind(
                "EasyUtill",
                "_SetMaidStatusOnOff",
                true
                );
        }

        public void Start()
        {               
        }

        public void SetScene()
        {
            //scene=SceneManager.GetSceneByName("SceneDaily");

        }




        public override void SetButtonList()
        {
            if (GUILayout.Button("mod reflash2")) modreflash2();
            if (GUILayout.Button("Maid add")) AddStockMaid();

            PresetUtill.SetButtonList();

            GUILayout.Label("MaidManagementMain Harmony 필요 : "+ HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMainPatch)));
            GUILayout.Label("메이드 에딧 진입시 자동 적용  ");
            //GUI.enabled = HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMain));
            if (GUILayout.Button("GP01FBFaceEyeRandomOnOff " + _GP01FBFaceEyeRandomOnOff.Value)) _GP01FBFaceEyeRandomOnOff.Value = !_GP01FBFaceEyeRandomOnOff.Value;
            if (GUILayout.Button("SetMaidStatus " + _SetMaidStatusOnOff.Value)) _SetMaidStatusOnOff.Value = !_SetMaidStatusOnOff.Value;

            GUILayout.Label("now scene.name : " + Lilly.scene.name);
            
            GUILayout.Label("SceneDaily");
            GUI.enabled = Lilly.scene.name == "SceneDaily";
            if (GUILayout.Button("DeleteMaidStatusAll")) DeleteMaidStatusAll();
            
            GUILayout.Label("SceneEdit");
            GUI.enabled = Lilly.scene.name == "SceneEdit";
            if (GUILayout.Button("GP-01FB Face Eye Random")) GP01FBFaceEyeRandom(1);
            if (GUILayout.Button("GP-01FB Face Eye Random UP")) GP01FBFaceEyeRandom(2);
            if (GUILayout.Button("GP-01FB Face Eye Random DOWN")) GP01FBFaceEyeRandom(3);
            

            GUI.enabled = true;
            //if (GUILayout.Button("mod reflash")) modreflash();
        }



        private void AddStockMaid()
        {
            MyLog.LogMessage("EasyUtill.AddStockMaid");

            Maid maid = GameMain.Instance.CharacterMgr.AddStockMaid();

            PersonalUtill.SetPersonalRandom(maid);

            if (EasyUtill._SetMaidStatusOnOff.Value)
                CheatUtill.SetMaidStatus(maid);

            //GP01FBFaceEyeRandom(1, maid);
            PresetUtill.RandPreset(PresetUtill.ListType.All, PresetUtill.PresetType.All, maid);

            MyLog.LogMessage("EasyUtill.AddStockMaid",MyUtill.GetMaidFullName(maid));
            //private void OnEndDlgOk()
            {
                //BaseMgr<ProfileMgr>.Instance.UpdateProfileData(true);
                ////GameMain.Instance.SysDlg.Close();
                ////UICamera.InputEnable = false;
                //maid.ThumShotCamMove();
                //maid.body0.trsLookTarget = GameMain.Instance.ThumCamera.transform;
                //maid.boMabataki = false;
                ////if (this.modeType == SceneEdit.ModeType.CostumeEdit)
                //{
                //    for (int i = 81; i <= 80; i++)
                //    {
                //        maid.ResetProp((MPN)i, true);
                //    }
                //}
                //Lilly.StartCoroutine(this.CoWaitPutCloth());
            }
        }


        public static void GP01FBFaceEyeRandom(int v, Maid m_maid=null)
        {
            if (m_maid==null)
            {
                m_maid = GameMain.Instance.CharacterMgr.GetMaid(0);
            }
            MyLog.LogMessage("GP01FBFaceEyeRandom", MyUtill.GetMaidFullName(m_maid));
            if (v == 1 || v == 2)
                GP01FBFaceEyeRandomUp(m_maid);
            if (v == 1 || v == 3)
                GP01FBFaceEyeRandomDown(m_maid);
            m_maid.AllProcProp();
            SceneEdit.Instance.UpdateSliders();
        }

        public static void GP01FBFaceEyeRandomDown(Maid m_maid)
        {
            m_maid.body0.VertexMorph_FromProcItem("mabutalowin"         , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutalowupmiddle"   , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutalowupout"      , UnityEngine.Random.Range(0f, 1f));
        }

        public static void GP01FBFaceEyeRandomUp(Maid m_maid)
        {
            m_maid.body0.VertexMorph_FromProcItem("mabutaupin"      , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutaupin2"     , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutaupmiddle"  , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutaupout"     , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutaupout2"    , UnityEngine.Random.Range(0f, 1f));
        }

        private void modreflash2()
        {
            if (isRunModreflash)
                return;

            isRunModreflash = true;
            Task.Factory.StartNew(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                MyLog.LogDarkBlue("modreflash2. start ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));

                bool flag = Directory.Exists(UTY.gameProjectPath + "\\Mod\\");
                if (flag)
                {
                    GameUty.UpdateFileSystemPath();
                    GameUty.UpdateFileSystemPathOld();
                }
                bool flag3 = GameUty.FileSystemMod != null;
                if (flag3)
                {
                    typeof(GameUty).GetField("m_aryModOnlysMenuFiles").SetValue(null, Array.FindAll<string>(GameUty.FileSystemMod.GetList(string.Empty, AFileSystemBase.ListType.AllFile), (string i) => new Regex(".*\\.menu$").IsMatch(i)));
                }

                MyLog.LogDarkBlue("modreflash2. end ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));
                isRunModreflash = false;
            }
            );

        }

        static bool isRunModreflash = false;

        private void modreflash()
        {
            if (isRunModreflash)
                return;

            isRunModreflash = true;
            Task.Factory.StartNew(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                MyLog.LogDarkBlue("modreflash. start ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));

                FileSystemWindows m_ModFileSystem = null;
                string text = UTY.gameProjectPath + "\\";
                if (Directory.Exists(text + "Mod"))
                {
                    m_ModFileSystem = new FileSystemWindows();
                    m_ModFileSystem.SetBaseDirectory(text);
                    m_ModFileSystem.AddFolder("Mod");

                    string[] list2 = m_ModFileSystem.GetList(string.Empty, AFileSystemBase.ListType.AllFolder);
                    foreach (string text31 in list2)
                    {
                        if (!m_ModFileSystem.AddAutoPath(text31))
                        {
                            UnityEngine.Debug.Log("m_ModFileSystemのAddAutoPathには既に " + text31 + " がありました。");
                        }
                    }
                }
                //typeof(GameUty).GetField("m_ModFileSystem").SetValue(null, m_ModFileSystem);
                // 메이드 에딧에서 목록만 갱신함
                if (m_ModFileSystem != null)
                {
                    string[] list3 = m_ModFileSystem.GetList(string.Empty, AFileSystemBase.ListType.AllFile);
                    //GameUty.m_aryModOnlysMenuFiles = Array.FindAll<string>(list3, (string i) => new Regex(".*\\.menu$").IsMatch(i));
                    string[] list4 = Array.FindAll<string>(list3, (string i) => new Regex(".*\\.menu$").IsMatch(i));
                    typeof(GameUty).GetField("m_aryModOnlysMenuFiles").SetValue(null, list4);

                }
                if (m_ModFileSystem != null)
                {
                    m_ModFileSystem.Dispose();
                    m_ModFileSystem = null;
                }

                MyLog.LogDarkBlue("modreflash. end ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));
                isRunModreflash = false;
            }
            );
        }   /*
        */
        private void DeleteMaidStatusAll()
        {
            for (int i = 0; i < 40; i++)//SetDataForViewer 에서 하드 코딩됨
            {
                ScheduleCtrlPatch.DeleteMaidAndReDraw("slot_" + i + "_MaidStatus");
            }
        }


        /*
        public void ClickMaidStatus()
        {
            string name = UIButton.current.name;
            if (UICamera.currentTouchID == -1)
            {
                if (this.CurrentActiveButton == name)
                {
                    return;
                }
                Debug.Log(string.Format("{0}ボタンがクリックされました。", name));
                this.m_MaidStatusListCtrl.CreateTaskViewer(name);
                this.CurrentActiveButton = name;
            }
            else if (UICamera.currentTouchID == -2)
            {
                Debug.Log(string.Format("{0}ボタンが右クリックされました。", name));
                if (this.m_Ctrl.CanDeleteData(name))
                {
                    this.m_Ctrl.DeleteMaidStatus(this.m_scheduleApi, name);
                }
            }
        }
        */
    }
}
