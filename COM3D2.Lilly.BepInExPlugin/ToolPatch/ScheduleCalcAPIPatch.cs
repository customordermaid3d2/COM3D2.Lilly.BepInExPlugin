using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using PlayerStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    class ScheduleCalcAPIPatch
    {
        // ScheduleCalcAPI

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "ScheduleCalcAPI"
        , "SimulateMaidStatusResult"
        );

        /// <summary>
        /// 효과 없는듯 하다
        /// public static ScheduleCalcAPI.ResultSimulateParam SimulateMaidStatusResult(Maid maid, int workId, ScheduleData.WorkSuccessLv successLv = ScheduleData.WorkSuccessLv.Success, bool commu = false)
        /// </summary>
        [HarmonyPrefix, HarmonyPatch(typeof(ScheduleCalcAPI), "SimulateMaidStatusResult", new Type[] { typeof(Maid), typeof(int) , typeof(ScheduleData.WorkSuccessLv) , typeof(bool) })]
        public static void SimulateMaidStatusResult(Maid maid, int workId,ref ScheduleData.WorkSuccessLv successLv , bool commu = false)
        {
            if (configEntryUtill["SimulateMaidStatusResult"])
            MyLog.LogMessage(
                "ScheduleCalcAPI.SimulateMaidStatusResult"
                , MyUtill.GetMaidFullName(maid)
                , workId
                , successLv
                , commu
            );
            successLv = ScheduleData.WorkSuccessLv.Perfect;
        }

        /// <summary>
        /// 원인불명 오류 발생
        /// public static ScheduleCalcAPI.ResultSimulateParam SimulateIncomeResult(Maid maid, int workId, Facility facility, ScheduleMgr.ScheduleTime time, ScheduleData.WorkSuccessLv successLv = ScheduleData.WorkSuccessLv.Success, bool commu = false)
        /// </summary>
        // [HarmonyPrefix, HarmonyPatch(typeof(ScheduleCalcAPI), "SimulateIncomeResult", new Type[] { typeof(Maid), typeof(int) , typeof(Facility) , typeof(ScheduleMgr.ScheduleTime) , typeof(ScheduleData.WorkSuccessLv) , typeof(bool) })]
        public static void SimulateIncomeResult(Maid maid, int workId, Facility facility, ScheduleMgr.ScheduleTime time, ScheduleData.WorkSuccessLv successLv = ScheduleData.WorkSuccessLv.Success, bool commu = false)
        {
            MyLog.LogMessage(
                "ScheduleCalcAPI.SimulateIncomeResult"
                , MyUtill.GetMaidFullName(maid)
                , workId
                //, facility.defaultName
                , time
                , successLv
                , commu
            );
            successLv = ScheduleData.WorkSuccessLv.Perfect;
        }
    }
}
