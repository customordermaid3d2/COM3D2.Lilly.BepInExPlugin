using HarmonyLib;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.BasePatch
{
    /// <summary>
    /// 스케줄 관리에서 슬롯 선택 등
    /// </summary>
    class ScheduleScenePatch
    {
        // ScheduleScene

        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleScene), "SetCommuSlot_Safe")]
        // public void SetCommuSlot_Safe(ScheduleMgr.ScheduleTime workTime, int slotId, bool value)
        public static void SetCommuSlot_Safe(
            ScheduleScene __instance
            , ScheduleMgr.ScheduleTime workTime, int slotId, bool value
        )
        {
            MyLog.LogMessage(
            "SetCommuSlot_Safe"
            , workTime
            , slotId
            , value
            );
        }

        
        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleScene), "SetNightWorkId")]
        // private void SetNightWorkId(Maid maid, int setId)
        public static void SetCommuSlot_Safe(
            ScheduleScene __instance
            , Maid maid, int setId
        )
        {
            MyLog.LogMessage(
            "SetCommuSlot_Safe"
            , MyUtill.GetMaidFullName(maid)
            , setId
            );
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleScene), "SetNightWorkSlot_Safe")]
        // public void SetNightWorkSlot_Safe(ScheduleMgr.ScheduleTime workTime, int slotId, int workId)
        public static void SetNightWorkSlot_Safe(
            ScheduleScene __instance
            , ScheduleMgr.ScheduleTime workTime, int slotId, int workId)        
        {
            MyLog.LogMessage(
            "SetNightWorkSlot_Safe"
            , workTime
            , slotId
            , workId
            );
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleScene), "SetSlot_Safe")]
        // public void SetSlot_Safe(int slotId, Maid maid, bool slotUpdate = true, bool updateAll = true)
        public static void SetSlot_Safe(
            ScheduleScene __instance
            , int slotId, Maid maid, bool slotUpdate = true, bool updateAll = true)
        {
            MyLog.LogMessage(
            "SetSlot_Safe"
            , slotId
            , MyUtill.GetMaidFullName(maid)
            , slotUpdate
            , updateAll
            );
        }


    }
}
