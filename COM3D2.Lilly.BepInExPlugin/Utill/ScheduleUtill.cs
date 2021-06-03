﻿using COM3D2.Lilly.Plugin.PatchInfo;
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

            int ic = UnityEngine.Random.Range(0, 5);

            //밤시중용 처리
            for (int i = 0; i < ic; i++)
            {
                int n1 = UnityEngine.Random.Range(0, slots.Count);
                ScheduleMgrPatch.m_scheduleApi.SetNightWorkSlot_Safe(scheduleTime, slots[n1], 10000);
                slots.Remove(slots[n1]);
                if (slots.Count <= 4)
                {
                    break;
                }
            }

            //ic = UnityEngine.Random.Range(0, 40);

            // //밤시중용 처리
            for (int j = 0; j < 40; j++)
            {
                int dn = UnityEngine.Random.Range(0, ScheduleCSVData.YotogiData.Count);
                int id = ScheduleCSVData.YotogiData.ElementAt(dn).Key;

                for (int i = 0; i < 10; i++)
                {
                    int sn = UnityEngine.Random.Range(0, slots.Count);
                    maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[sn]);

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
            for (int i = 0; i < scheduleDatas.Length; i++)
            {
                if (scheduleDatas[i].maid_guid == string.Empty)
                    continue;
                slots.Add(i);
            }

            int ic = UnityEngine.Random.Range(0, 5);

            //밤시중용 처리
            for (int i = 0; i < ic ; i++)
            {
                int n1 = UnityEngine.Random.Range(0, slots.Count);
                ScheduleMgrPatch.m_scheduleApi.SetNightWorkSlot_Safe(scheduleTime, slots[n1], 10000);
                slots.Remove(slots[n1]);
                if (slots.Count <= 4)
                {
                    break;
                }
            }

            ic = UnityEngine.Random.Range(0, 40);

            // //밤시중용 처리
            for (int j = 0; j < ic; j++)
            {
                int dn = UnityEngine.Random.Range(0, ScheduleCSVData.YotogiData.Count);
                int id = ScheduleCSVData.YotogiData.ElementAt(dn).Key;

                for (int i = 0; i < 10; i++)
                {
                    int sn = UnityEngine.Random.Range(0, slots.Count);
                    maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[sn]);

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
                            if (!DailyMgr.IsLegacy)
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

            if (!DailyMgr.IsLegacy)
            {
                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
            }
            ScheduleAPI.MaidWorkIdErrorCheck(true);
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
                            if (!DailyMgr.IsLegacy)
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

            if (!DailyMgr.IsLegacy)
            {
                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
            }
            ScheduleAPI.MaidWorkIdErrorCheck(true);
        }

    }
}