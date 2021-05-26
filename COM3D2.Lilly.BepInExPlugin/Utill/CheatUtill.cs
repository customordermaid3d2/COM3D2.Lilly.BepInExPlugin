using COM3D2.Lilly.Plugin.PatchInfo;
using COM3D2.Lilly.Plugin.ToolPatch;
using MaidStatus;
using PlayerStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wf;

namespace COM3D2.Lilly.Plugin.Utill
{
    class CheatUtill
    {
        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
            "CheatUtill"
            , "SetFacilityAllMaid"
        );


        public static void SetMaidAll(Maid maid)
        {
            MyLog.LogMessage(
                "CheatUtill.SetMaidAll st"
                );
            StatusUtill.SetMaidStatus(maid);
            SkillClassUtill.SetMaidYotogiClass(maid);
            SkillClassUtill.SetMaidJobClass(maid);
            SkillClassUtill.SetMaidSkill(maid);
            MyLog.LogMessage(
            "CheatUtill.SetMaidAll end"
            );
        }

        internal static void SetHeroineType(HeroineType transfer)
        {
            MyLog.LogMessage(
            "CheatUtill.SetHeroineType"
            );
            MaidManagementMainPatch.select_maid.status.heroineType = transfer;
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

            if (configEntryUtill["SetFacilityAllMaid",false])
                MyLog.LogMessage(
                "SetFacilityAllMaid1"
                );
            //List<KeyValuePair<int, ScheduleCSVData.Work>> works = ScheduleCSVData.WorkData.ToList();

            // 스케줄의 슬롯 정보
            // public Maid GetScheduleSlot(int slotNo)
            // if (string.IsNullOrEmpty(this.scheduleSlot[slotNo].maid_guid))
            ScheduleData[] scheduleDatas = GameMain.Instance.CharacterMgr.status.scheduleSlot;

            // 사용 가능한 목록
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
            Maid maid;
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
                            slots.RemoveAt(n1);
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

        static bool isSetAllWorkRun = false;

        internal static void SetWorkAll()
        {

            if (isSetAllWorkRun)
                return;

            Task.Factory.StartNew(() =>
            {
                isSetAllWorkRun = true;
                MyLog.LogDarkBlue("ScheduleAPIPatch.SetAllWork. start");

                ReadOnlyDictionary<int, NightWorkState> night_works_state_dic = GameMain.Instance.CharacterMgr.status.night_works_state_dic;
                MyLog.LogMessage("ScheduleAPIPatch.SetAllWork.night_works_state_dic:" + night_works_state_dic.Count);

                foreach (var item in night_works_state_dic)
                {
                    NightWorkState nightWorkState = item.Value;
                    nightWorkState.finish = true;
                }

                MyLog.LogMessage("ScheduleAPIPatch.SetAllWork.YotogiData:" + ScheduleCSVData.YotogiData.Values.Count);
                foreach (Maid maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
                {
                    MyLog.LogMessage(".SetAllWork.Yotogi:" + MyUtill.GetMaidFullName(maid), ScheduleCSVData.YotogiData.Values.Count);
                    if (maid.status.heroineType == HeroineType.Sub)
                        continue;


                    foreach (ScheduleCSVData.Yotogi yotogi in ScheduleCSVData.YotogiData.Values)
                    {
#if DEBUG
                            if (Lilly.isLogOn)
                                MyLog.LogInfo(".SetAllWork:"
                                    + yotogi.id
                                    , yotogi.name
                                    , yotogi.type
                                    , yotogi.yotogiType
                                );
#endif
                        if (DailyMgr.IsLegacy)
                        {
                            maid.status.OldStatus.SetFlag("_PlayedNightWorkId" + yotogi.id, 1);
                        }
                        else
                        {
                            maid.status.SetFlag("_PlayedNightWorkId" + yotogi.id, 1);
                        }
                        if (yotogi.condFlag1.Count > 0)
                        {
                            for (int n = 0; n < yotogi.condFlag1.Count; n++)
                            {
                                maid.status.SetFlag(yotogi.condFlag1[n], 1);
                            }
                        }
                        if (yotogi.condFlag0.Count > 0)
                        {
                            for (int num = 0; num < yotogi.condFlag0.Count; num++)
                            {
                                maid.status.SetFlag(yotogi.condFlag0[num], 0);
                            }
                        }
                    }
                    if (DailyMgr.IsLegacy)
                    {
                        maid.status.OldStatus.SetFlag("_PlayedNightWorkVip", 1);
                    }
                    else
                    {
                        maid.status.SetFlag("_PlayedNightWorkVip", 1);
                    }
                }

                MyLog.LogDarkBlue("ScheduleAPIPatch.SetAllWork. end");
                isSetAllWorkRun = false;
            });

        }

    }
}
