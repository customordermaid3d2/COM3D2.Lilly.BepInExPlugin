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
using Schedule;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
    public class GUIEasy : GUIMgr
    {
        //public static Scene scene;       
        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "GUIEasy"
        );

        public GUIEasy()
        {
            nameGUI = "EasyUtill";
        }

        public void SetScene()
        {
            //scene=SceneManager.GetSceneByName("SceneDaily");

        }



        public override void SetBody()
        {
            GUILayout.Label("now scene.name : " + Lilly.scene.name);


            if (GUILayout.Button("mod reflash 0")) ModRefresh0();
            if (GUILayout.Button("mod reflash 1")) ModRefresh1();
            if (GUILayout.Button("mod reflash 2")) ModRefresh2();

            if (GUILayout.Button("SetRandomCommu")) { ScheduleAPIPatch.SetRandomCommu(true); ScheduleAPIPatch.SetRandomCommu(false); };

#if ScheduleUtill
            GUILayout.Label("Schedule 진입 필요.");
            GUI.enabled = ScheduleMgrPatch.m_scheduleApi != null;
            if (GUILayout.Button("슬롯에 메이드 자동 배치")) ScheduleMgrPatch.SetSlotAllMaid();
            if (GUILayout.Button("슬롯의 메이드들 제거")) ScheduleMgrPatch.SetSlotAllDel();
            if (GUILayout.Button("메이드 전체 자동 배치 - 주간")) ScheduleUtill.SetScheduleAllMaid(ScheduleMgr.ScheduleTime.DayTime);
            if (GUILayout.Button("메이드 전체 자동 배치 - 야간")) ScheduleUtill.SetScheduleAllMaid(ScheduleMgr.ScheduleTime.Night);
            if (GUILayout.Button("메이드 밤시중 자동 배치 - 주간")) ScheduleUtill.SetScheduleAllMaid(ScheduleMgr.ScheduleTime.DayTime, true, false, false);
            if (GUILayout.Button("메이드 밤시중 자동 배치 - 야간")) ScheduleUtill.SetScheduleAllMaid(ScheduleMgr.ScheduleTime.Night, true, false, false);
            if (GUILayout.Button("메이드 훈련 자동 배치 - 주간")) ScheduleUtill.SetScheduleAllMaid(ScheduleMgr.ScheduleTime.DayTime, false, true, false);
            if (GUILayout.Button("메이드 훈련 자동 배치 - 야간")) ScheduleUtill.SetScheduleAllMaid(ScheduleMgr.ScheduleTime.Night, false, true, false);
            if (GUILayout.Button("메이드 시설에 자동 배치 - 주간")) ScheduleUtill.SetScheduleAllMaid(ScheduleMgr.ScheduleTime.DayTime, false, false, true);
            if (GUILayout.Button("메이드 시설에 자동 배치 - 야간")) ScheduleUtill.SetScheduleAllMaid(ScheduleMgr.ScheduleTime.Night, false, false, true);
            //GUI.enabled = !DailyMgrPatch.IsLegacy;

            GUI.enabled = true;
            GUILayout.Label("매일 자동 적용.");
            if (GUILayout.Button("슬롯에_메이드_자동_배치 " + DailyMgrPatch.isSetSlotAllMaid)) DailyMgrPatch.isSetSlotAllMaid = !DailyMgrPatch.isSetSlotAllMaid;
            if (GUILayout.Button("메이드_스케줄_자동_배치 " + DailyMgrPatch.isSetScheduleAllMaid)) DailyMgrPatch.isSetScheduleAllMaid = !DailyMgrPatch.isSetScheduleAllMaid;
            if (GUILayout.Button("커뮤니티_자동_적용 " + DailyMgrPatch.isSetRandomCommu)) DailyMgrPatch.isSetRandomCommu = !DailyMgrPatch.isSetRandomCommu;

            GUI.enabled = true;
            GUILayout.Label("밤시중");
            if (GUILayout.Button("스테이지 자동 선택 " + YotogiStageSelectManagerPatch.isSelect)) YotogiStageSelectManagerPatch.isSelect = !YotogiStageSelectManagerPatch.isSelect;
            if (GUILayout.Button("스테이지 자동 선택 ")) YotogiStageSelectManagerPatch.Select();
            if (GUILayout.Button("스킬 자동 선택 " + YotogiSkillSelectManagerPatch.isAddSkill)) YotogiSkillSelectManagerPatch.isAddSkill = !YotogiSkillSelectManagerPatch.isAddSkill;
            if (GUILayout.Button("스킬 자동 선택 ")) YotogiSkillContainerViewerPatch.AddSkill(false);

            GUI.enabled = DailyMgrPatch.IsLegacy;
            if (GUILayout.Button("스킬 자동 선택 old " + YotogiOldSkillSelectManagerPatch.isAddSkill)) YotogiOldSkillSelectManagerPatch.isAddSkill = !YotogiOldSkillSelectManagerPatch.isAddSkill;
            if (GUILayout.Button("스킬 자동 선택 old ")) YotogiOldSkillContainerViewerPatch.AddSkill(false);
#endif


            GUI.enabled = true;
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
