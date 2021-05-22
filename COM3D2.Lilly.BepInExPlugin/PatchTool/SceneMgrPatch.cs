using com.workman.cm3d2.scene.dailyEtc;
using COM3D2.Lilly.Plugin.PatchInfo;
using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    /// <summary>
    /// SceneDaily 장면 로딩시에 작동
    /// </summary>
    class SceneMgrPatch
    {
        // SceneMgr

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "SceneMgrPatch"
        , "커뮤니티_자동_적용"
        , "슬롯에_메이드_자동_배치"
        , "시설에_메이드_자동_배치"
        );

        //private string m_sceneName;

        // private void DispatcherSceneDaily(string openType)

        [HarmonyPostfix, HarmonyPatch(typeof(SceneMgr), "Start")]
        public static void Start(string ___m_sceneName)
        {
            MyLog.LogMessage("SceneMgr.Start"
                , ___m_sceneName
            );
        }


        // private void DispatcherSceneDaily(string openType)
        [HarmonyPostfix, HarmonyPatch(typeof(SceneMgr), "DispatcherSceneDaily")]
        public static void DispatcherSceneDaily(string openType)
        {
            MyLog.LogMessage("SceneMgr.DispatcherSceneDaily"
                , openType
            );
            if (openType == "Daytime")
            {
                if (configEntryUtill["슬롯에_메이드_자동_배치"])
                {
                    ScheduleMgrPatch.SetSlotAllMaid();
                }

                if (configEntryUtill["시설에_메이드_자동_배치"])
                {
                    CheatUtill.SetFacilityAllMaid(ScheduleMgr.ScheduleTime.DayTime);
                    CheatUtill.SetFacilityAllMaid(ScheduleMgr.ScheduleTime.Night);
                }

                if (configEntryUtill["커뮤니티_자동_적용"])
                {
                    ScheduleAPIPatch.SetRandomCommu(true);
                    ScheduleAPIPatch.SetRandomCommu(false);
                }
            }


        }
    }
}
