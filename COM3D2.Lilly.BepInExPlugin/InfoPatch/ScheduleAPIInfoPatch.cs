using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.InfoPatch
{
    class ScheduleAPIInfoPatch
    {
        // ScheduleAPI

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "ScheduleAPIInfoPatch"
        , "FacilitySlotActive"
        );

        // public static bool FacilitySlotActive(string maidGuid, Facility facility, ScheduleMgr.ScheduleTime time)
        [HarmonyPatch(typeof(ScheduleAPI), "FacilitySlotActive")]
        [HarmonyPrefix]
        public static void FacilitySlotActive(
            string maidGuid, Facility facility, ScheduleMgr.ScheduleTime time)
        {
            if(configEntryUtill["FacilitySlotActive"])
            MyLog.LogMessage("ScheduleAPI.FacilitySlotActive"
                , maidGuid
                , facility.facilityName
                , time
            );
            Maid maid = IsSlotInMaid(maidGuid);
            if (maid == null)
            {
                return ;
            }

            int key = 0;
            if (time == ScheduleMgr.ScheduleTime.DayTime)
            {
                key = maid.status.noonWorkId;
            }
            if (time == ScheduleMgr.ScheduleTime.Night)
            {
                key = maid.status.nightWorkId;
            }
            if (configEntryUtill["FacilitySlotActive"])
                MyLog.LogMessage("ScheduleAPI.FacilitySlotActive1"
                , MyUtill.GetMaidFullName(maid)
                , key
            );
            if (ScheduleCSVData.AllData.ContainsKey(key))
            {
                ScheduleCSVData.ScheduleBase scheduleBase = ScheduleCSVData.AllData[key];
                if (scheduleBase.type == ScheduleTaskCtrl.TaskType.Work)
                {
                    ScheduleCSVData.Work work = (ScheduleCSVData.Work)scheduleBase;
                    if (configEntryUtill["FacilitySlotActive"])
                        MyLog.LogMessage("ScheduleAPI.FacilitySlotActive2"
                        , work.facilityId
                        , work.facility.ID
                        , work.facility.name
                    );
                    if (configEntryUtill["FacilitySlotActive"])
                        MyLog.LogMessage("ScheduleAPI.FacilitySlotActive3"
                        , ScheduleCSVData.faclilityPowerUpWorkId
                        , facility.defaultData.ID
                        , facility.defaultData.name
                    );
                    if (work.facility == facility.defaultData && work.facilityId != ScheduleCSVData.faclilityPowerUpWorkId)
                    {
                        return ;
                    }
                }
                else if (scheduleBase.type == ScheduleTaskCtrl.TaskType.Training)
                {
                    ScheduleCSVData.Training training = (ScheduleCSVData.Training)scheduleBase;
                    if (training.facilityId == facility.defaultData.ID)
                    {
                        return ;
                    }
                }
            }

            //SetRandomCommu(isDaytime);
        }

        public static Maid IsSlotInMaid(string maidGuid)
        {
            for (int i = 0; i < 40; i++)
            {
                Maid scheduleSlot = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(i);
                if (scheduleSlot != null && scheduleSlot.status.guid == maidGuid)
                {
                    return scheduleSlot;
                }
            }
            return null;
        }
    }
}
