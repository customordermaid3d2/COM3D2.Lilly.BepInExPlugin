using FacilityFlag;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.InfoPatch
{
    class FacilityManagerPatch
    {
        // FacilityManager

        // FacilityManager facilityManager = GameMain.Instance.FacilityMgr;

        public static FacilityManager facilityManager;

        //private static FacilityManager facilityManager;// 사용 못함
        public static Dictionary<int, Facility> m_NextDayFacilityArray;
        public static Facility[] m_FacilityArray;// 현재

        private static DataArray<int, SimpleExperienceSystem> facilityExpArray;

        public static Dictionary<int, FacilityDataTable.FacilityDefaultData> m_FacilityDefaultDataArray;
        public static DataArray<string, string> m_FacilityAchievementList;

        public static DataArray<int, SimpleExperienceSystem> FacilityExpArray {
            get
            {
                if (facilityExpArray == null)
                {
                    facilityExpArray = Traverse.Create(GameMain.Instance.FacilityMgr).Field("m_FacilityExpArray").GetValue<DataArray<int, SimpleExperienceSystem>>();
                }
                return facilityExpArray;
            }
            set => facilityExpArray = value; }

        /// <summary>
        /// 겜 실행시 한번만 실행됨
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(FacilityManager), MethodType.Constructor)]
        public static void FacilityManagerConstructor(FacilityManager __instance)
        {
            facilityManager = __instance;
        }
        

        [HarmonyPostfix, HarmonyPatch(typeof(FacilityManager), "ResetData")]
        public static void ResetData(
            Dictionary<int, FacilityDataTable.FacilityDefaultData> ___m_FacilityDefaultDataArray
            , DataArray<int, SimpleExperienceSystem> ___m_FacilityExpArray
            , DataArray<string, string> ___m_FacilityAchievementList
            )
        {
            SetData();

            MyLog.LogMessage(
                "FacilityManagerToolPatch.ResetData"
                , m_NextDayFacilityArray.Count
                , m_FacilityArray.Length
            );
            m_FacilityDefaultDataArray = ___m_FacilityDefaultDataArray;
            FacilityExpArray = ___m_FacilityExpArray;
            m_FacilityAchievementList = ___m_FacilityAchievementList;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(FacilityDataTable), "ReadCSVFacilityDefaultData")]
        public static void ReadCSVFacilityDefaultData(Dictionary<int, FacilityDataTable.FacilityDefaultData> ___m_FacilityDefaultDataArray)
        {
            MyLog.LogMessage(
                "FacilityManagerToolPatch.ReadCSVFacilityDefaultData"

            );
            m_FacilityDefaultDataArray = ___m_FacilityDefaultDataArray;
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
            FacilityManagerPatch.SetData();

            Facility facility;

            foreach (var item in FacilityManagerPatch.m_FacilityArray)
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
            foreach (var item in FacilityManagerPatch.m_NextDayFacilityArray)
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
            if (FacilityManagerPatch.m_FacilityDefaultDataArray != null)
                foreach (var item in FacilityManagerPatch.m_FacilityDefaultDataArray)
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

    }
}
