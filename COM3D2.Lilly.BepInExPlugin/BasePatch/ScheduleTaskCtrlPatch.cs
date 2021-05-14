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


    }
}
