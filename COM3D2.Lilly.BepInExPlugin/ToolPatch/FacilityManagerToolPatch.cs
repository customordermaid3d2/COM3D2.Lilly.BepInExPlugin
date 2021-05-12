using FacilityFlag;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    class FacilityManagerToolPatch
    {
        // FacilityManager

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
            if (m_FacilityDefaultDataArray!=null)
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

        /// <summary>
        /// 시설 자동 배치
        /// </summary>
        public static void SetFacilityAll()
        {
            SetData();

            MyLog.LogMessage(
                "FacilityManagerToolPatch.SetFacilityAll1"
                , GameMain.Instance.FacilityMgr.FacilityCountMax
                , m_NextDayFacilityArray.Count
                , m_FacilityArray.Length
            );

            List<Facility.FacilityStatus> list = FacilityDataTable.GetFacilityStatusArray(true).ToList();
            for (int i = 2; i < GameMain.Instance.FacilityMgr.FacilityCountMax; i++)
            {
                var item = list[UnityEngine.Random.Range(0, list.Count)];
                Facility facility = GameMain.Instance.FacilityMgr.CreateNewFacility(item.typeID);

                GameMain.Instance.FacilityMgr.SetFacility(i, facility);

                list.Remove(item);
                if (list.Count == 0)
                {
                    break;
                }
            }
        }

        /*
         
                MyLog.LogMessage(
                    "FacilityManagerToolPatch.SetFacilityAll2"
                    , item.name
                    , item.typeID
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
         
         
         */

    }
}
