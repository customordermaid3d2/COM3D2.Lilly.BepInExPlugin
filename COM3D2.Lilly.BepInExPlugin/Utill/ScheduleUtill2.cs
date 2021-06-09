using COM3D2.Lilly.Plugin.PatchInfo;
using MaidStatus;
using PlayerStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Utill
{
    class ScheduleUtill
    {
        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
            "ScheduleUtill"
        );

        public static void SetScheduleAllMaid(ScheduleMgr.ScheduleTime scheduleTime,bool isYotogi = true,bool isTraining = true, bool isSetFacility = true)
        {
            if (ScheduleMgrPatch.m_scheduleApi == null)
            {
                MyLog.LogMessage("SetYotogiAllMaid"
                , "스케줄 관리 접속 한번 필요"
                );
                return;
            }

            // 사용 가능한 메이드 슬롯 목록
            List<int> slots = new();
            List<int> slotsn = new();

            ScheduleData[] scheduleDatas = GameMain.Instance.CharacterMgr.status.scheduleSlot;

            SetSlots(scheduleDatas, slots);

            int c1=40, c2=40 ,c3,c4;
            if(isYotogi && isTraining&& isSetFacility)
            {
                c3= UnityEngine.Random.Range(0, 40);
                c4= UnityEngine.Random.Range(0, 40);
                if (c3< c4)
                {
                    c1 = c3;
                    c2 -= c4;
                }
                else
                {
                    c1 -= c3;
                    c2 = c4;
                }
            }

            if (isYotogi)
                SetSchedule(scheduleTime, ScheduleType.Yotogi, slots, slotsn,c1);

            if (isTraining)
                SetSchedule(scheduleTime, ScheduleType.Training, slots, slotsn,c2);

            if (isSetFacility)
                SetWorkFacility(scheduleTime, slots);

            SetFinal();
        }

        public static void SetWorkFacility(ScheduleMgr.ScheduleTime scheduleTime, List<int> slots)
        {
            if (slots.Count==0)
            {
                return;
            }

            Facility facility;
            FacilityDataTable.FacilityDefaultData defaultData;
            ScheduleCSVData.Work workData;
            Maid maid;
            int fn;
            int sn;

            var facilitys = GameMain.Instance.FacilityMgr.GetFacilityArray().ToList();

            while (facilitys.Count > 1)
            {
                fn = UnityEngine.Random.Range(1, facilitys.Count);
                if (facilitys[fn] == null)
                {
                    facilitys.RemoveAt(fn);
                    if (configEntryUtill["SetWorkFacility"])
                        MyLog.LogMessage(
                        "facilitys null"
                        , fn
                        );
                    continue;
                }

                facility = facilitys[fn];
                defaultData = facility.defaultData;
                workData = defaultData.workData;

                if (facility.minMaidCount <= slots.Count && workData.id != 0)
                {
                    for (int k = 0; k < facility.minMaidCount; k++)
                    {
                        sn = UnityEngine.Random.Range(0, slots.Count);
                        maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[sn]);

                        SetWorkId(scheduleTime, workData.id, slots[sn]);

                        facility.AllocationMaid(maid, scheduleTime);

                        slots.Remove(slots[sn]);
                    }
                    if (slots.Count == 0)
                    {
                        return;
                    }
                }

                facilitys.RemoveAt(fn);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleTime"></param>
        /// <param name="scheduleType">not use Work</param>
        /// <param name="slots"></param>
        /// <param name="slotsn"></param>
        /// <param name="cnt"></param>
        public static void SetSchedule(ScheduleMgr.ScheduleTime scheduleTime, ScheduleType scheduleType, List<int> slots,List<int> slotsn, int cnt = 40)
        {
            if (scheduleType == ScheduleType.Work||cnt==0)
            {
                return;
            }

            slots.AddRange(slotsn);
            slotsn.Clear();

            if (slots.Count == 0)
            {
                return;
            }

            Maid maid;
            List<ScheduleBase> list = new List<ScheduleBase>();
            List<ScheduleBase> scheduleData;
            int sc;
            int wc;

            for (int i = 0; i < cnt; i++)
            {
                sc = UnityEngine.Random.Range(0, slots.Count);

                maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[sc]);
                if (scheduleType==ScheduleType.Yotogi && maid.status.heroineType == HeroineType.Sub)
                {
                    slotsn.Add(slots[sc]);
                    slots.Remove(slots[sc]);
                    continue;
                }

                scheduleData = ScheduleMgrPatch.m_scheduleApi.slot[slots[sc]].scheduleData.Where(
                    x =>
                    x.workType == scheduleType
                    && x.enabled
                    ).ToList();

                list.Clear();

                foreach (ScheduleBase scheduleBase in scheduleData)
                {
                    if (DailyMgr.IsLegacy && ScheduleCSVData.WorkLegacyDisableId.Contains(scheduleBase.id))
                        continue;

                    if (!PersonalEventBlocker.IsEnabledScheduleTask(maid.status.personal, scheduleBase.id))
                        continue;

                    list.Add(scheduleBase);
                }
                if (list.Count > 0)
                {
                    wc = UnityEngine.Random.Range(0, list.Count);
                    SetWorkId(scheduleTime, list[wc].id, slots[sc]);
                }
                slots.Remove(slots[sc]);
                if (slots.Count == 0)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 서브 성격 제외
        /// </summary>
        /// <param name="scheduleDatas"></param>
        /// <param name="slots"></param>
        public static void SetSlots(ScheduleData[] scheduleDatas, List<int> slots)
        {
            for (int i = 0; i < scheduleDatas.Length; i++)
            {
                if (scheduleDatas[i].maid_guid == string.Empty)
                    continue;

                //if (GameMain.Instance.CharacterMgr.status.GetScheduleSlot(i).status.heroineType == HeroineType.Sub)
                //    continue;

                slots.Add(i);
            }
        }


        public static void SetWorkId(ScheduleMgr.ScheduleTime workTime, int taskId, int slotId)
        {
            if (ScheduleCSVData.AllData.ContainsKey(taskId))
            {
                ScheduleCSVData.ScheduleBase scheduleBase = ScheduleCSVData.AllData[taskId];
                //int slotId = 0;
                //for (int i = 0; i < 40; i++)
                //{
                //    Maid scheduleSlot = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(i);
                //    if (scheduleSlot != null && scheduleSlot == this.m_scheduleCtrl.SelectedMaid)
                //    {
                //        slotId = i;
                //    }
                //}
                ScheduleTaskCtrl.TaskType type = scheduleBase.type;
                if (type != ScheduleTaskCtrl.TaskType.Training && type != ScheduleTaskCtrl.TaskType.Work)
                {
                    if (type == ScheduleTaskCtrl.TaskType.Yotogi)
                    {
                        ScheduleMgrPatch.m_scheduleApi.SetNightWorkSlot_Safe(workTime, slotId, taskId);
                    }
                }
                else
                {
                    ScheduleMgrPatch.m_scheduleApi.SetNoonWorkSlot_Safe(workTime, slotId, taskId);
                }
            }
        }

        public static void SetFinal()
        {
            if (!DailyMgr.IsLegacy)
            {
                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
            }
            ScheduleAPI.MaidWorkIdErrorCheck(true);
        }

    }
}
