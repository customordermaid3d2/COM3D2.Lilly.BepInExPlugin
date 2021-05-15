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
using COM3D2.Lilly.Plugin.MyGUI;
using COM3D2.Lilly.Plugin.InfoPatch;
using COM3D2.Lilly.Plugin.ToolPatch;
using COM3D2.Lilly.Plugin.Utill;

namespace COM3D2.Lilly.Plugin.MyGUI
{
    public class EasyUtill : GUIVirtual
    {
        //public static Scene scene;       


        public EasyUtill()
        {

            name = "EasyUtill";
        }

        public void SetScene()
        {
            //scene=SceneManager.GetSceneByName("SceneDaily");

        }


        public static string scenario_str = string.Empty;
        public static string label_name = string.Empty;

        public override void SetButtonList()
        {
            GUILayout.Label("now scene.name : " + Lilly.scene.name);
            if (GUILayout.Button("mod reflash2")) modreflash2();

            if (GUILayout.Button("SetRandomCommu")) { ScheduleAPIPatch.SetRandomCommu(true); ScheduleAPIPatch.SetRandomCommu(false); };

            GUILayout.Label("Schedule 진입 필요.");
            GUI.enabled = ScheduleMgrPatch.m_scheduleApi != null;
            if (GUILayout.Button("슬롯에 메이드 자동 배치")) ScheduleMgrPatch.SetSlotAllMaid();
            if (GUILayout.Button("슬롯의 메이드들 제거")) ScheduleMgrPatch.SetSlotAllDel();
            GUI.enabled = true;
            if (GUILayout.Button("시설에 메이드 자동 배치 - 주간")) CheatUtill.SetFacilityAllMaid(ScheduleMgr.ScheduleTime.DayTime);
            if (GUILayout.Button("시설에 메이드 자동 배치 - 야간")) CheatUtill.SetFacilityAllMaid(ScheduleMgr.ScheduleTime.Night);



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
