using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schedule;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// ScheduleTaskCtrl
    /// ScheduleScene ScheduleApi
    /// ScheduleMgr ScheduleMgr
    /// ScheduleCtrl ScheduleCtrl
    /// ScheduleTaskViewer TaskViewer
    /// int CurrentActiveSlotNo
    /// </summary>
    class ScheduleTaskCtrlPatch
    {
        public static ScheduleTaskCtrl instance;

        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleTaskCtrl), MethodType.Constructor)]
        public static void ScheduleTaskCtrlCtor(ScheduleTaskCtrl __instance)
        {
            MyLog.LogMessage(
            "ScheduleTaskCtrl.Ctor"
            );
            instance = __instance;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(ScheduleTaskCtrl), "SetWorkId")]
        public static void SetWorkId(ScheduleTaskCtrl __instance
            , ScheduleMgr.ScheduleTime workTime
            , int taskId
            , ScheduleCtrl ___m_scheduleCtrl
        )
        {
            if (workTime == ScheduleMgr.ScheduleTime.DayTime)
            {
                MyLog.LogMessage(
                "ScheduleTaskCtrl.SetWorkId"
                , workTime
                , taskId
                , ___m_scheduleCtrl.SelectedMaid.status.noonWorkId
                );
            }
            else if (workTime == ScheduleMgr.ScheduleTime.Night)
            {
                MyLog.LogMessage(
                "ScheduleTaskCtrl.SetWorkId"
                , workTime
                , taskId
                 , ___m_scheduleCtrl.SelectedMaid.status.nightWorkId
                );
            }
        }
        /*
        // private void SetWorkId(ScheduleMgr.ScheduleTime workTime, int taskId)
        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleTaskCtrl), "SetWorkId")]
        public static void SetWorkId(ScheduleTaskCtrl __instance
            , ScheduleMgr.ScheduleTime workTime
            , int taskId
            )
        {
            MyLog.LogMessage(
            "ScheduleTaskCtrl.SetWorkId"
            , workTime
            , taskId
            );
        }
        */

    }
}
