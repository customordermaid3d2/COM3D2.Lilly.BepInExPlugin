using COM3D2.Lilly.Plugin.Utill;
using FacilityFlag;
using HarmonyLib;
using PlayerStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wf;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    class FacilityManagerToolPatch
    {
        // FacilityManager      

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "FacilityManagerToolPatch"
        , "SetFacilityAllMaid"
        , "SetFacilityAll"
        );

        //private static FacilityManager facilityManager;// 사용 못함
        public static Dictionary<int, Facility> m_NextDayFacilityArray;
        public static Facility[] m_FacilityArray;// 현재

        public static Dictionary<int, FacilityDataTable.FacilityDefaultData> m_FacilityDefaultDataArray;

        //public static FacilityManager FacilityManager { 
        //    get {
        //        if (facilityManager==null)
        //        {
        //            facilityManager = GameMain.Instance.FacilityMgr;
        //        }
        //        return facilityManager; 
        //    }
        //    set => facilityManager = value; 
        //}

        // 작동 안함
        //[HarmonyPostfix, HarmonyPatch(typeof(GameMain), "OnInitialize")]
        //public static void OnInitialize()
        //{
        //    MyLog.LogMessage(
        //        "FacilityManagerToolPatch.OnInitialize"
        //        ,  FacilityManager.FacilityCountMax
        //    );
        //    FacilityManager = GameMain.Instance.FacilityMgr;
        //}

        [HarmonyPostfix, HarmonyPatch(typeof(FacilityDataTable), "ReadCSVFacilityDefaultData")]
        public static void ReadCSVFacilityDefaultData(Dictionary<int, FacilityDataTable.FacilityDefaultData> ___m_FacilityDefaultDataArray)
        {
            MyLog.LogMessage(
                "FacilityManagerToolPatch.ReadCSVFacilityDefaultData"

            );
            m_FacilityDefaultDataArray = ___m_FacilityDefaultDataArray;
        }

        /// <summary>
        /// 겜 실행시 한번만 실행됨
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(FacilityManager), "ResetData")]
        public static void ResetData()
        {
            SetData();

            MyLog.LogMessage(
                "FacilityManagerToolPatch.ResetData"
                , m_NextDayFacilityArray.Count
                , m_FacilityArray.Length
            );
        }

        private static void SetData()
        {
            if (m_NextDayFacilityArray == null)
                m_NextDayFacilityArray = GameMain.Instance.FacilityMgr.GetNextDayFacilityArray();
            if (m_FacilityArray == null)
                m_FacilityArray = GameMain.Instance.FacilityMgr.GetFacilityArray();
        }

        public static void GetGameInfo()
        {
            SetData();

            Facility facility;

            foreach (var item in m_FacilityArray)
            {
                facility = item;
                MyLog.LogMessage(
                    "FacilityManagerToolPatch.print1"
                    , facility.defaultName
                    , facility.facilityName
                    , facility.name
                    , facility.minMaidCount
                    , facility.maxMaidCount
                    , facility.nowDayTimeMaidCount
                    , facility.staffRankDayTime
                    , facility.staffRankNight
                    , facility.tag
                    , facility.typeCostume
                    , facility.hideFlags
                    , facility.enabled
                );
            }
            foreach (var item in m_NextDayFacilityArray)
            {
                facility = item.Value;
                MyLog.LogMessage(
                    "FacilityManagerToolPatch.print2"
                    , item.Key
                    , facility.defaultName
                    , facility.facilityName
                    , facility.name
                    , facility.minMaidCount
                    , facility.maxMaidCount
                    , facility.nowDayTimeMaidCount
                    , facility.staffRankDayTime
                    , facility.staffRankNight
                    , facility.tag
                    , facility.typeCostume
                    , facility.hideFlags
                    , facility.enabled
                );
            }
            if (m_FacilityDefaultDataArray != null)
                foreach (var item in m_FacilityDefaultDataArray)
                {
                    MyLog.LogMessage(
                        "FacilityManagerToolPatch.print3"
                        , item.Key
                        , item.Value.fileName
                        , item.Value.fileNameNight
                        , item.Value.ID
                        , item.Value.name
                        , item.Value.minMaidCount
                        , item.Value.maxMaidCount
                        , item.Value.rank
                        , item.Value.businessType
                        , item.Value.businessTypeID
                        , item.Value.businessTypeName
                    );
                }
            foreach (var item in FacilityDataTable.GetFacilityStatusArray(true))
            {
                MyLog.LogMessage(
                    "FacilityManagerToolPatch.print4"
                    , item.name
                    , item.typeID
                );
            }

        }

        public static List<Facility.FacilityStatus> listbak;
        public static List<Facility.FacilityStatus> list;

        private static void SetFacilityListInit()
        {
            if (listbak == null)
            {
                listbak = FacilityDataTable.GetFacilityStatusArray(true).ToList();
                foreach (var item in listbak)
                {
                    MyLog.LogMessage(
                        "FacilityManagerToolPatch.print4"
                        , item.typeID
                        , item.name
                    );
                    //if (!FacilityDataTable.GetFacilityCanBeDestroy(item.typeID, true))// 이게 제대로 안됨. 특히" 150 , 극장"이 제거 안됨
                    if (item.typeID==100 || item.typeID ==150)
                    {
                        listbak.Remove(item);
                    }
                }
            }
            if (list == null)
            {
                list = new List<Facility.FacilityStatus>();
            }
            list.Clear();
        }

        /// <summary>
        /// 시설 자동 배치
        /// </summary>
        public static void SetFacilityAll(bool random)
        {

            if (configEntryUtill["SetFacilityAll"])
                MyLog.LogMessage(
                "FacilityManagerToolPatch.SetFacilityAll1"
                , GameMain.Instance.FacilityMgr.FacilityCountMax
            );

            SetFacilityListInit();

            //List<Facility.FacilityStatus> list = new();

            for (int i = 2; i < GameMain.Instance.FacilityMgr.FacilityCountMax; i++)
            {
                if (list.Count == 0)
                {
                    list.AddRange(listbak);
                }

                Facility.FacilityStatus item;
                if (random)
                    item = list[UnityEngine.Random.Range(0, FacilityManagerToolPatch.list.Count)];                
                else
                    item = list[0];

                Facility facility = GameMain.Instance.FacilityMgr.CreateNewFacility(item.typeID);

                //[Message: Lilly] 3001 , 100 , トレーニングルーム , トレーニングルーム , -15756
                //[Message: Lilly] 3004 , 150 , 劇場 , 劇場 , -15762
                if (configEntryUtill["SetFacilityAll"])
                {
                    /*
[Message:     Lilly] 3004 , 劇場 , True , False , COM3D , Work , Basic , Basic
[Message:     Lilly] 150 , 劇場 , アイドル系 , 300 , アイドル系 , True , False , 2 , 40 , 1
[Message:     Lilly] Facility , 劇場 , 극장 , 1 , 3 , True , False , False , 2 , 40 , 0 , 1 , 1 , 150 , 극장
[Message:     Lilly] 150 , 극장
                     */
                    MyLog.LogMessage(
                          facility.defaultData.workData.id
                        , facility.defaultData.workData.name
                        , facility.defaultData.workData.isCommu
                        , facility.defaultData.workData.IsLegacy
                        , facility.defaultData.workData.mode
                        , facility.defaultData.workData.type
                        , facility.defaultData.workData.workTyp
                        , facility.defaultData.workData.trainingType
                        );
                    MyLog.LogMessage(
                          facility.defaultData.ID
                        , facility.defaultData.name
                        , facility.defaultData.businessType
                        , facility.defaultData.businessTypeID
                        , facility.defaultData.businessTypeName
                        , facility.defaultData.isRemoval
                        , facility.defaultData.isEnableData
                        , facility.defaultData.isEnableNTR
                        , facility.defaultData.isOnlyNTR
                        , facility.defaultData.isBusiness
                        , facility.defaultData.isDefaultPlace
                        , facility.defaultData.minMaidCount
                        , facility.defaultData.maxMaidCount
                        , facility.defaultData.rank
                        );
                    MyLog.LogMessage(
                          facility.name
                        , facility.defaultName
                        , facility.facilityName
                        , facility.facilityLevel
                        , facility.facilityValuation
                        , facility.isActiveAndEnabled
                        , facility.isOperationDayTime
                        , facility.isOperationNight
                        , facility.minMaidCount
                        , facility.maxMaidCount
                        , facility.nowNightMaidCount
                        , facility.staffRankDayTime
                        , facility.staffRankNight
                        , facility.param.typeID
                        , facility.param.name
                        );
                    MyLog.LogMessage(
                          facility.param.typeID
                        , facility.param.name
                        );
                }

                    GameMain.Instance.FacilityMgr.SetFacility(i, facility);
                
                list.Remove(item);

            }
        }

        /*
        public static void SetFacilityAllMaid(ScheduleMgr.ScheduleTime scheduleTime)
        {
            //if (ScheduleMgrPatch.m_scheduleApi == null)
            //{
            //    MyLog.LogMessage("SetSlotAllDel"
            //    , "스케줄 관리 접속 한번 필요"
            //    );
            //    return;
            //}

            if (configEntryUtill["SetFacilityAllMaid"])
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

                                if (ScheduleMgrPatch.m_scheduleApi == null)
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

                if (ScheduleMgrPatch.m_scheduleApi == null)
                    ScheduleMgrPatch.m_scheduleApi.SetNoonWorkSlot_Safe(scheduleTime, slots[n1], workData.id);

                facility.AllocationMaid(maid, scheduleTime);

                slots.RemoveAt(n1);
            }

            if (!DailyMgr.IsLegacy)
            {
                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
            }
            ScheduleAPI.MaidWorkIdErrorCheck(true);



            //ScheduleAPI.AddTrainingFacility(maid, workId, scheduleTime);





        }

        */
    }
}
/*
 *                     MyLog.LogMessage(
                        item.Key  // work id
                        , training.facilityId
                        , training.trainingType
                        , training.categoryID
                        , training.id
                        , training.name
                        , training.information
                        , training.mode
                        , training.type
                        , training.IsCommon
                        , training.isCommu
                        , training.IsLegacy
                        );
[Message:     Lilly] 3000 , 100 , Basic , 52 , 3000 , 施設強化 ,  , COM3D , Work , False , False , False
[Message:     Lilly] 3001 , 100 , Basic , 52 , 3001 , トレーニングルーム ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3002 , 100 , Basic , 52 , 3002 , オープンカフェ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3003 , 100 , Basic , 52 , 3003 , レストラン ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3004 , 100 , Basic , 52 , 3004 , 劇場 ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3005 , 100 , Basic , 52 , 3005 , バーラウンジ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3006 , 100 , Basic , 52 , 3006 , カジノ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3007 , 100 , Basic , 52 , 3007 , ソープ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3008 , 100 , Basic , 52 , 3008 , SMクラブ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3009 , 100 , Basic , 52 , 3009 , 宿泊部屋 ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3010 , 100 , Basic , 52 , 3010 , メイドリフレ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3011 , 100 , Basic , 52 , 3011 , 高級オープンカフェ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3012 , 100 , Basic , 52 , 3012 , 高級レストラン ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3013 , 100 , Basic , 52 , 3013 , 高級劇場 ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3014 , 100 , Basic , 52 , 3014 , 高級バーラウンジ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3015 , 100 , Basic , 52 , 3015 , 高級ソープ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3016 , 100 , Basic , 52 , 3016 , 高級SMクラブ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3017 , 100 , Basic , 52 , 3017 , 高級宿泊部屋 ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3018 , 100 , Basic , 52 , 3018 , 高級メイドリフレ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3019 , 100 , Basic , 52 , 3019 , 高級カジノ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3100 , 100 , Basic , 52 , 3100 , シーカフェ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3120 , 100 , Basic , 52 , 3120 , 神社 ,  , COM3D , Work , False , True , False
[Message:     Lilly] 4000 , 100 , Basic , 52 , 4000 , ポールダンス ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3260 , 100 , Basic , 52 , 3260 , ゲレンデ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3300 , 100 , Basic , 52 , 3300 , 春の庭園 ,  , COM3D , Work , False , True , False
 */