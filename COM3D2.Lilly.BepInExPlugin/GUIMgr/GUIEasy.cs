using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using UnityEngine;
using COM3D2.Lilly.Plugin.PatchInfo;
using COM3D2.Lilly.Plugin.ToolPatch;
using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
    public class GUIEasy : GUIMgr
    {
        //public static Scene scene;       


        public GUIEasy()
        {
            nameGUI = "EasyUtill";
        }

        public void SetScene()
        {
            //scene=SceneManager.GetSceneByName("SceneDaily");

        }

        public static string scenario_str = string.Empty;
        public static string label_name = string.Empty;

        public override void SetBody()
        {
            GUILayout.Label("now scene.name : " + Lilly.scene.name);


            if (GUILayout.Button("mod reflash 0")) ModRefresh0();
            if (GUILayout.Button("mod reflash 1")) ModRefresh1();
            if (GUILayout.Button("mod reflash 2")) ModRefresh2();

            if (GUILayout.Button("SetRandomCommu")) { ScheduleAPIPatch.SetRandomCommu(true); ScheduleAPIPatch.SetRandomCommu(false); };

            GUILayout.Label("Schedule 진입 필요.");
            GUI.enabled = ScheduleMgrPatch.m_scheduleApi != null;
            if (GUILayout.Button("슬롯에 메이드 자동 배치")) ScheduleMgrPatch.SetSlotAllMaid();
            if (GUILayout.Button("슬롯의 메이드들 제거")) ScheduleMgrPatch.SetSlotAllDel();
            if (GUILayout.Button("시설에 메이드 자동 배치 - 주간")) CheatUtill.SetFacilityAllMaid(ScheduleMgr.ScheduleTime.DayTime);
            if (GUILayout.Button("시설에 메이드 자동 배치 - 야간")) CheatUtill.SetFacilityAllMaid(ScheduleMgr.ScheduleTime.Night);
            GUI.enabled = true;



            GUILayout.Label("scenario_str ");
            scenario_str = GUILayout.TextField(scenario_str);
            GUILayout.Label("label_name ");
            label_name = GUILayout.TextField(label_name);
            if (GUILayout.Button("Exec run1"))
            {
                if (scenario_str != "" && label_name != "")
                    KagScriptPatch.run1(scenario_str, label_name);
            }
            if (GUILayout.Button("Exec run2"))
            {
                if (scenario_str != "" && label_name != "")
                    KagScriptPatch.run2(scenario_str, label_name);
            }

            /*
            GUILayout.Label("ScheduleTaskCtrlPatch");
            GUI.enabled = ScheduleTaskCtrlPatch.instance != null;
            if (GUILayout.Button("스케줄 자동 채우기")) ScheduleTaskCtrlPatch.SetScheduleSlot();
            GUI.enabled = true;
            */

            GUI.enabled = true;
            //if (GUILayout.Button("mod reflash")) modreflash();
        }

        private void ModRefresh1()
        {
            if (isRunModreflash)
                return;

            isRunModreflash = true;
            Task.Factory.StartNew(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                MyLog.LogDarkBlue("modreflash1. start ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));

                FileSystemWindows m_ModFileSystem = (FileSystemWindows)GameUty.FileSystemMod;
                if (Directory.Exists(UTY.gameProjectPath + "\\Mod"))
                {
                    string[] list2 = m_ModFileSystem.GetList(string.Empty, AFileSystemBase.ListType.AllFolder);
                    foreach (string text31 in list2)
                    {
                        if (m_ModFileSystem.AddAutoPath(text31))
                        {
                            UnityEngine.Debug.Log(text31 + " 파일 추가");
                        }
                    }
                }
                if (m_ModFileSystem != null)
                {
                    //string[] list3 = m_ModFileSystem.GetList(string.Empty, AFileSystemBase.ListType.AllFile);
                    //string[] m_aryModOnlysMenuFiles = GameUty.ModOnlysMenuFiles;
                    //m_aryModOnlysMenuFiles = Array.FindAll<string>(list3, (string i) => new Regex(".*\\.menu$").IsMatch(i));
                    typeof(GameUty).GetField("m_aryModOnlysMenuFiles").SetValue(null, Array.FindAll<string>(GameUty.FileSystemMod.GetList(string.Empty, AFileSystemBase.ListType.AllFile), (string i) => new Regex(".*\\.menu$").IsMatch(i)));

                }

                MyLog.LogDarkBlue("modreflash1. end ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));
                isRunModreflash = false;
            }
            );
        }

        /// <summary>
        /// 중복 현상 발생
        /// </summary>
        private void ModRefresh2()
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

        /// <summary>
        /// 잘못 만든듯
        /// </summary>
        private void ModRefresh0()
        {
            if (isRunModreflash)
                return;

            isRunModreflash = true;
            Task.Factory.StartNew(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                MyLog.LogDarkBlue("modreflash0. start ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));

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

                MyLog.LogDarkBlue("modreflash0. end ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));
                isRunModreflash = false;
            }
            );
        }   /*
        */



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
