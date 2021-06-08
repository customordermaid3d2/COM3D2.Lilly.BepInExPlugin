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
            "class ScheduleUtill"
        );

        public static Dictionary<int, ScheduleCSVData.Yotogi> yotogiDataCm;

        public static Dictionary<int, ScheduleCSVData.Yotogi> YotogiData {
            get
            {
                if (DailyMgrPatch.IsLegacy)
                {
                    if (yotogiDataCm == null)
                    {
                        yotogiDataCm = ScheduleCSVData.YotogiData.Where(x => x.Value.mode != ScheduleCSVData.ScheduleBase.Mode.COM3D).ToDictionary(x => x.Key, x => x.Value);
                    }
                    return yotogiDataCm;
                }
                else
                {
                    return ScheduleCSVData.YotogiData;
                }
            }
        }

        /// <summary>
        /// 분석용
        /// </summary>
        /// <param name="maid"></param>
        /// <param name="taskId"></param>
        /// <param name="time"></param>
        /// <returns>true 일경우 사용 불가</returns>
        public static bool CheckYotogi(Maid maid, int taskId, ScheduleMgr.ScheduleTime time)
        {
            bool flag = false;
            if (ScheduleCSVData.YotogiData.ContainsKey(taskId))
            {
                ScheduleCSVData.Yotogi yotogi = ScheduleCSVData.YotogiData[taskId];
                if (!flag && (yotogi.yotogiType == ScheduleCSVData.YotogiType.Vip || yotogi.yotogiType == ScheduleCSVData.YotogiType.VipCall))
                {
                    if (maid != null)
                    {
                        if (!ScheduleAPI.VisibleNightWork(taskId, maid, true))
                        {
                            flag = true;
                        }
                    }
                    else if (!ScheduleAPI.VisibleNightWork(taskId, null, true))
                    {
                        flag = true;
                    }
                }
                if (!flag && (yotogi.yotogiType == ScheduleCSVData.YotogiType.Vip || yotogi.yotogiType == ScheduleCSVData.YotogiType.VipCall))
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Maid scheduleSlot = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(i);
                        if (scheduleSlot != null && scheduleSlot != maid && (taskId == scheduleSlot.status.noonWorkId || taskId == scheduleSlot.status.nightWorkId))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag && (yotogi.yotogiType == ScheduleCSVData.YotogiType.Vip || yotogi.yotogiType == ScheduleCSVData.YotogiType.VipCall || yotogi.yotogiType == ScheduleCSVData.YotogiType.Entertain || yotogi.yotogiType == ScheduleCSVData.YotogiType.Travel) && !ScheduleAPI.EnableNightWork(taskId, maid, true, true))
                {
                    flag = true;
                }
                if (flag)
                {
                    WorkIdReset(maid.status, time);
                }
            }
            else
            {
                WorkIdReset(maid.status, time);
            }
            return flag;
        }

        private static void WorkIdReset(MaidStatus.Status maidStatus, ScheduleMgr.ScheduleTime time)
        {
            if (time == ScheduleMgr.ScheduleTime.DayTime)
            {
                maidStatus.noonWorkId = 0;
            }
            else if (time == ScheduleMgr.ScheduleTime.Night)
            {
                maidStatus.nightWorkId = 0;
            }
        }




        internal static void SetYotogiAllMaid(ScheduleMgr.ScheduleTime scheduleTime)
        {
            if (ScheduleMgrPatch.m_scheduleApi == null)
            {
                MyLog.LogMessage("SetSlotAllDel"
                , "스케줄 관리 접속 한번 필요"
                );
                return;
            }

            if (DailyMgrPatch.IsLegacy && scheduleTime == ScheduleMgr.ScheduleTime.DayTime)
            {
                MyLog.LogMessage("SetSlotAllDel"
                , "DayTime & Legacy 에선 사용 불가"
                );
                return;
            }

            ScheduleData[] scheduleDatas = GameMain.Instance.CharacterMgr.status.scheduleSlot;
           // Maid maid;

            // 사용 가능한 메이드 슬롯 목록
            List<int> slots = new();
            SetSlots(scheduleDatas, slots);

            SetNightWork(scheduleTime, slots);

        }

        // 밤시중 스킬 선택
        private static void SetNightWork(ScheduleMgr.ScheduleTime scheduleTime, List<int> slots)
        {
            Maid maid;

            int ic = UnityEngine.Random.Range(0, 5);

            //밤시중용 처리
            for (int i = 0; i < ic; i++)
            {
                int n1 = UnityEngine.Random.Range(0, slots.Count);
                maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[n1]);
                if (maid.status.heroineType == MaidStatus.HeroineType.Sub)
                    continue;

                ScheduleMgrPatch.m_scheduleApi.SetNightWorkSlot_Safe(scheduleTime, slots[n1], 10000);
                slots.Remove(slots[n1]);
                if (slots.Count <= 4 || slots.Count == 0)
                {
                    break;
                }
            }

            ic = UnityEngine.Random.Range(0, 40);

            // //밤시중용 처리
            for (int j = 0; j < ic; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    int dn = UnityEngine.Random.Range(0, ScheduleCSVData.YotogiData.Count);
                    if (ScheduleCSVData.YotogiData.ElementAt(dn).Value.mode == ScheduleCSVData.ScheduleBase.Mode.CM3D2)
                    {

                        continue;
                    }

                    int id = ScheduleCSVData.YotogiData.ElementAt(dn).Key;

                    int sn = UnityEngine.Random.Range(0, slots.Count);
                    maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[sn]);
                    if (maid.status.heroineType == MaidStatus.HeroineType.Sub)
                        break;

                    if(!PersonalEventBlocker.IsEnabledScheduleTask(maid.status.personal, id))
                    {
                        break;
                    }

                    if (!ScheduleUtill.CheckYotogi(maid, id, scheduleTime))
                    {
                        ScheduleMgrPatch.m_scheduleApi.SetNightWorkSlot_Safe(scheduleTime, slots[sn], id);
                        slots.Remove(slots[sn]);
                        break;
                    }
                }
                if (slots.Count == 0)
                {
                    break;
                }
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
            if (!DailyMgr.IsLegacy)
            {
                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
            }
            ScheduleAPI.MaidWorkIdErrorCheck(true);
        }

        public static void SetScheduleAllMaid2(ScheduleMgr.ScheduleTime scheduleTime)
        {
            if (ScheduleMgrPatch.m_scheduleApi == null)
            {
                MyLog.LogMessage("SetSlotAllDel"
                , "스케줄 관리 접속 한번 필요"
                );
                return;
            }

            if (configEntryUtill["SetScheduleAllMaid", false])
                MyLog.LogMessage(
                "SetScheduleAllMaid"
                );

            // 스케줄의 슬롯 정보
            // public Maid GetScheduleSlot(int slotNo)
            // if (string.IsNullOrEmpty(this.scheduleSlot[slotNo].maid_guid))
            ScheduleData[] scheduleDatas = GameMain.Instance.CharacterMgr.status.scheduleSlot;


            // 사용 가능한 메이드 슬롯 목록
            List<int> slots = new();
            SetSlots(scheduleDatas, slots);


            Maid maid;
            List<ScheduleBase> scheduleData;
            List<ScheduleBase> ids = new();

            int ic = UnityEngine.Random.Range(0, 40);
            //밤시중용 처리
            for (int i = 0; i < ic; i++)
            {
                try
                {
                    if (slots.Count == 0 || slots.Count < 3)
                    {
                        break;
                    }

                    int sn = UnityEngine.Random.Range(0, slots.Count);
                    scheduleData = ScheduleMgrPatch.m_scheduleApi.slot[sn].scheduleData;
                    maid = ScheduleMgrPatch.m_scheduleApi.slot[sn].maid;
                    ids.Clear();



                    foreach (ScheduleBase scheduleBase in scheduleData)
                    {
                        if (scheduleBase.workType != ScheduleType.Work)
                            if (PersonalEventBlocker.IsEnabledScheduleTask(maid.status.personal, scheduleBase.id)&& scheduleBase.enabled)
                            {
                                if (configEntryUtill["SetScheduleAllMaid_2", false])
                                    MyLog.LogMessage("scheduleBase",
                                    scheduleBase.id,
                                    scheduleBase.name,
                                    scheduleBase.workType,
                                    scheduleBase.enabled
                                );

                                //ScheduleCSVData.AllData[scheduleBase.id]
                                ids.Add(scheduleBase);
                            }
                    }
                    int idsc = UnityEngine.Random.Range(0, ids.Count);
                    if (configEntryUtill["SetScheduleAllMaid_2", false])
                        MyLog.LogMessage("maid"
                            , MyUtill.GetMaidFullName(maid)
                            , ids[idsc].id
                            , ids[idsc].name
                            , ids[idsc].workType
                            , ids[idsc].enabled
                        );

                    //ScheduleMgrPatch.m_scheduleApi.SetWorkId(scheduleTime, slots[sn], ids[UnityEngine.Random.Range(0, ids.Count)]);
                    SetWorkId(scheduleTime, slots[sn], ids[idsc].id);
                    slots.Remove(sn);

                }
                catch (Exception e)
                {
                    MyLog.Log("scheduleBase", e.ToString());
                }
            }




            //var facilitys = GameMain.Instance.FacilityMgr.GetFacilityArray().Where(x=>x).ToList();
            var facilitys = GameMain.Instance.FacilityMgr.GetFacilityArray().ToList();
            if (configEntryUtill["SetFacilityAllMaid"])
                MyLog.LogMessage(
                "SetFacilityAllMaid3"
                , scheduleDatas.Length
                , slots.Count
                , facilitys.Count
                );

            // 구현부
            Facility facility;
            FacilityDataTable.FacilityDefaultData defaultData;
            ScheduleCSVData.Work workData;

            while (facilitys.Count > 2)
            {
                int n2 = UnityEngine.Random.Range(2, facilitys.Count);
                if (facilitys[n2] == null)
                {
                    if (configEntryUtill["SetFacilityAllMaid"])
                        MyLog.LogMessage(
                        "SetFacilityAllMaid null"
                        , n2
                        );
                }
                else
                {
                    facility = facilitys[n2];
                    defaultData = facility.defaultData;
                    workData = defaultData.workData;

                    if (facility.minMaidCount <= slots.Count && workData.id != 0)
                    {
                        for (int k = 0; k < facility.minMaidCount; k++)
                        {
                            int n1 = UnityEngine.Random.Range(0, slots.Count);
                            try
                            {
                                maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[n1]);
                                if (configEntryUtill["SetFacilityAllMaid"])
                                    MyLog.LogMessage(
                                    "SetFacilityAllMaid4"
                                    , n2
                                    , n1
                                    , MyUtill.GetMaidFullName(maid)
                                    , facility.defaultName

                                    );

                                //if (ScheduleMgrPatch.m_scheduleApi != null)
                                ScheduleMgrPatch.m_scheduleApi.SetNoonWorkSlot_Safe(scheduleTime, slots[n1], workData.id);

                                facility.AllocationMaid(maid, scheduleTime);
                            }
                            catch (Exception e)
                            {
                                MyLog.LogWarning(
                                "SetFacilityAllMaid4"
                                , n2
                                , e.ToString()
                                );
                            }
                            slots.Remove(slots[n1]);
                        }
                        if (slots.Count == 0)
                        {
                            if (!DailyMgrPatch.IsLegacy)
                            {
                                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
                            }
                            ScheduleAPI.MaidWorkIdErrorCheck(true);
                            return;
                        }
                    }

                }
                facilitys.RemoveAt(n2);
            }

            facility = facilitys[1];
            defaultData = facility.defaultData;
            workData = defaultData.workData;
            while (slots.Count > 0)
            {
                int n1 = 0;
                maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[n1]);

                //if (ScheduleMgrPatch.m_scheduleApi != null)
                ScheduleMgrPatch.m_scheduleApi.SetNoonWorkSlot_Safe(scheduleTime, slots[n1], workData.id);

                facility.AllocationMaid(maid, scheduleTime);

                slots.RemoveAt(n1);
            }

            if (!DailyMgrPatch.IsLegacy)
            {
                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
            }
            ScheduleAPI.MaidWorkIdErrorCheck(true);

        }

        public static void SetScheduleAllMaid(ScheduleMgr.ScheduleTime scheduleTime)
        {
            if (ScheduleMgrPatch.m_scheduleApi == null)
            {
                MyLog.LogMessage("SetSlotAllDel"
                , "스케줄 관리 접속 한번 필요"
                );
                return;
            }

            if (configEntryUtill["SetFacilityAllMaid", false])
                MyLog.LogMessage(
                "SetFacilityAllMaid1"
                );
            //List<KeyValuePair<int, ScheduleCSVData.Work>> works = ScheduleCSVData.WorkData.ToList();

            // 스케줄의 슬롯 정보
            // public Maid GetScheduleSlot(int slotNo)
            // if (string.IsNullOrEmpty(this.scheduleSlot[slotNo].maid_guid))
            ScheduleData[] scheduleDatas = GameMain.Instance.CharacterMgr.status.scheduleSlot;
            Maid maid;

            // 사용 가능한 메이드 슬롯 목록
            List<int> slots = new();
            SetSlots(scheduleDatas, slots);

            if (!DailyMgrPatch.IsLegacy || scheduleTime == ScheduleMgr.ScheduleTime.Night)
            {

                SetNightWork(scheduleTime, slots);

            }

            //var facilitys = GameMain.Instance.FacilityMgr.GetFacilityArray().Where(x=>x).ToList();
            var facilitys = GameMain.Instance.FacilityMgr.GetFacilityArray().ToList();
            if (configEntryUtill["SetFacilityAllMaid"])
                MyLog.LogMessage(
                "SetFacilityAllMaid3"
                , scheduleDatas.Length
                , slots.Count
                , facilitys.Count
                );

            // 구현부
            Facility facility;
            FacilityDataTable.FacilityDefaultData defaultData;
            ScheduleCSVData.Work workData;

            while (facilitys.Count > 2)
            {
                int n2 = UnityEngine.Random.Range(2, facilitys.Count);
                if (facilitys[n2] == null)
                {
                    if (configEntryUtill["SetFacilityAllMaid"])
                        MyLog.LogMessage(
                        "SetFacilityAllMaid null"
                        , n2
                        );
                }
                else
                {
                    facility = facilitys[n2];
                    defaultData = facility.defaultData;
                    workData = defaultData.workData;

                    if (facility.minMaidCount <= slots.Count && workData.id != 0)
                    {
                        for (int k = 0; k < facility.minMaidCount; k++)
                        {
                            int n1 = UnityEngine.Random.Range(0, slots.Count);
                            try
                            {
                                maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[n1]);
                                if (configEntryUtill["SetFacilityAllMaid"])
                                    MyLog.LogMessage(
                                    "SetFacilityAllMaid4"
                                    , n2
                                    , n1
                                    , MyUtill.GetMaidFullName(maid)
                                    , facility.defaultName

                                    );

                                //if (ScheduleMgrPatch.m_scheduleApi != null)
                                ScheduleMgrPatch.m_scheduleApi.SetNoonWorkSlot_Safe(scheduleTime, slots[n1], workData.id);

                                facility.AllocationMaid(maid, scheduleTime);
                            }
                            catch (Exception e)
                            {
                                MyLog.LogWarning(
                                "SetFacilityAllMaid4"
                                , n2
                                , e.ToString()
                                );
                            }
                            slots.Remove(slots[n1]);
                        }
                        if (slots.Count == 0)
                        {
                            if (!DailyMgrPatch.IsLegacy)
                            {
                                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
                            }
                            ScheduleAPI.MaidWorkIdErrorCheck(true);
                            return;
                        }
                    }

                }
                facilitys.RemoveAt(n2);
            }

            facility = facilitys[1];
            defaultData = facility.defaultData;
            workData = defaultData.workData;
            while (slots.Count > 0)
            {
                int n1 = 0;
                maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[n1]);

                //if (ScheduleMgrPatch.m_scheduleApi != null)
                ScheduleMgrPatch.m_scheduleApi.SetNoonWorkSlot_Safe(scheduleTime, slots[n1], workData.id);

                facility.AllocationMaid(maid, scheduleTime);

                slots.RemoveAt(n1);
            }

            if (!DailyMgrPatch.IsLegacy)
            {
                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
            }
            ScheduleAPI.MaidWorkIdErrorCheck(true);






        }
        

        private static void SetSlots(ScheduleData[] scheduleDatas, List<int> slots)
        {
            for (int i = 0; i < scheduleDatas.Length; i++)
            {
                if (scheduleDatas[i].maid_guid == string.Empty)
                    continue;
                slots.Add(i);
            }
        }

        public static void SetFacilityAllMaid(ScheduleMgr.ScheduleTime scheduleTime)
        {
            if (ScheduleMgrPatch.m_scheduleApi == null)
            {
                MyLog.LogMessage("SetSlotAllDel"
                , "스케줄 관리 접속 한번 필요"
                );
                return;
            }

            if (configEntryUtill["SetFacilityAllMaid", false])
                MyLog.LogMessage(
                "SetFacilityAllMaid1"
                );
            //List<KeyValuePair<int, ScheduleCSVData.Work>> works = ScheduleCSVData.WorkData.ToList();

            // 스케줄의 슬롯 정보
            // public Maid GetScheduleSlot(int slotNo)
            // if (string.IsNullOrEmpty(this.scheduleSlot[slotNo].maid_guid))
            ScheduleData[] scheduleDatas = GameMain.Instance.CharacterMgr.status.scheduleSlot;
            Maid maid;

            // 사용 가능한 메이드 슬롯 목록
            List<int> slots = new();
            for (int i = 0; i < scheduleDatas.Length; i++)
            {
                if (scheduleDatas[i].maid_guid == string.Empty)
                    continue;
                slots.Add(i);
            }


            //var facilitys = GameMain.Instance.FacilityMgr.GetFacilityArray().Where(x=>x).ToList();
            var facilitys = GameMain.Instance.FacilityMgr.GetFacilityArray().ToList();
            if (configEntryUtill["SetFacilityAllMaid"])
                MyLog.LogMessage(
                "SetFacilityAllMaid3"
                , scheduleDatas.Length
                , slots.Count
                , facilitys.Count
                );

            // 구현부
            Facility facility;
            FacilityDataTable.FacilityDefaultData defaultData;
            ScheduleCSVData.Work workData;

            while (facilitys.Count > 2)
            {
                int n2 = UnityEngine.Random.Range(2, facilitys.Count);
                if (facilitys[n2] == null)
                {
                    if (configEntryUtill["SetFacilityAllMaid"])
                        MyLog.LogMessage(
                        "SetFacilityAllMaid null"
                        , n2
                        );
                }
                else
                {
                    facility = facilitys[n2];
                    defaultData = facility.defaultData;
                    workData = defaultData.workData;

                    if (facility.minMaidCount <= slots.Count && workData.id != 0)
                    {
                        for (int k = 0; k < facility.minMaidCount; k++)
                        {
                            int n1 = UnityEngine.Random.Range(0, slots.Count);
                            try
                            {
                                maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[n1]);
                                if (configEntryUtill["SetFacilityAllMaid"])
                                    MyLog.LogMessage(
                                    "SetFacilityAllMaid4"
                                    , n2
                                    , n1
                                    , MyUtill.GetMaidFullName(maid)
                                    , facility.defaultName

                                    );

                                //if (ScheduleMgrPatch.m_scheduleApi != null)
                                ScheduleMgrPatch.m_scheduleApi.SetNoonWorkSlot_Safe(scheduleTime, slots[n1], workData.id);

                                facility.AllocationMaid(maid, scheduleTime);
                            }
                            catch (Exception e)
                            {
                                MyLog.LogWarning(
                                "SetFacilityAllMaid4"
                                , n2
                                , e.ToString()
                                );
                            }
                            slots.Remove(slots[n1]);
                        }
                        if (slots.Count == 0)
                        {
                            if (!DailyMgrPatch.IsLegacy)
                            {
                                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
                            }
                            ScheduleAPI.MaidWorkIdErrorCheck(true);
                            return;
                        }
                    }

                }
                facilitys.RemoveAt(n2);
            }

            facility = facilitys[1];
            defaultData = facility.defaultData;
            workData = defaultData.workData;
            while (slots.Count > 0)
            {
                int n1 = 0;
                maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[n1]);

                //if (ScheduleMgrPatch.m_scheduleApi != null)
                ScheduleMgrPatch.m_scheduleApi.SetNoonWorkSlot_Safe(scheduleTime, slots[n1], workData.id);

                facility.AllocationMaid(maid, scheduleTime);

                slots.RemoveAt(n1);
            }

            if (!DailyMgrPatch.IsLegacy)
            {
                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
            }
            ScheduleAPI.MaidWorkIdErrorCheck(true);
        }

    }
}
