using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using MaidStatus;
using PlayerStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wf;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    /// <summary>
    /// 모든 스케줄 버튼 클릭 가능
    /// 버그 발생 높으니 사용하지 말것
    /// </summary>
    class ScheduleAPIPatch
    {
        // ScheduleAPI

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
            "ScheduleAPIPatch"
        );

        public static void SetworkSuccessLvMax()
        {
            if (!configEntryUtill["GetCommunicationMaid_log"])
                MyLog.LogMessage("ScheduleAPI.SetworkSuccessLvMax"
                , ScheduleCSVData.workSuccessLvMissIncidence
                , ScheduleCSVData.workSuccessLvPerfectIncidence
            );
            // 효과 없음
            ScheduleCSVData.workSuccessLvMissIncidence = 0;
            ScheduleCSVData.workSuccessLvPerfectIncidence = 100;
        }


        /// <summary>
        /// 랜덤으로 커뮤니케이션 설정
        /// </summary>
        public static void SetRandomCommu(bool isDaytime)
        {
            List<Maid> list = ScheduleAPI.CanCommunicationMaids(isDaytime);
            if (list.Count > 0)
            {
                int i = UnityEngine.Random.Range(0, list.Count);
                if (isDaytime)
                {
                    foreach (Maid item in list)
                    {
                        item.status.noonCommu = false;
                    }
                    list[i].status.noonCommu = true;
                }
                else
                {
                    foreach (var item in list)
                    {
                        item.status.nightCommu = false;
                    }
                    list[i].status.nightCommu = true;
                }
                MyLog.LogMessage("ScheduleAPI.SetRandomCommu"
                    , MyUtill.GetMaidFullName(list[i])
                    , isDaytime
                );
            }
            else
            {
                MyLog.LogMessage("ScheduleAPI.SetRandomCommu count 0"
    , isDaytime
);
            }
        }

        /// <summary>
        /// 랜덤 커뮤니케이션 설정
        /// public static Maid GetCommunicationMaid(bool isDaytime)
        /// </summary>
        /// <param name="isDaytime"></param>        
        [HarmonyPatch(typeof(ScheduleAPI), "GetCommunicationMaid")]
        [HarmonyPrefix]
        public static void GetCommunicationMaid(bool isDaytime)//Maid __result,
        {
            if (!configEntryUtill["GetCommunicationMaid_log"])
                MyLog.LogMessage("ScheduleAPI.GetCommunicationMaid"
                , isDaytime
            );
            //SetRandomCommu(isDaytime);
        }

        /// <summary>
        /// 호출을 안함. 
        /// public static void DecideSuccess(WorkResultSceneMode sceneMode, int index, bool commu)
        /// </summary>
        /// <param name="isDaytime"></param>
        [HarmonyPatch(typeof(ScheduleAPI), "DecideSuccess")]
        [HarmonyPrefix]
        public static void DecideSuccessHarmonyPrefix(WorkResultSceneMode sceneMode, int index, bool commu)
        {
            if (!configEntryUtill["DecideSuccess_log"])
                MyLog.LogMessage("ScheduleAPI.DecideSuccess.HarmonyPrefix"
                , sceneMode
                , index
                , commu
            );

            if (!configEntryUtill["DecideSuccess_Perfect1"])
            {
                return;
            }

            SetworkSuccessLvMax();
        }

        /// <summary>
        /// 호출을 안함. 
        /// </summary>
        /// <param name="sceneMode"></param>
        /// <param name="index"></param>
        /// <param name="commu"></param>
        [HarmonyPatch(typeof(ScheduleAPI), "DecideSuccess")]
        [HarmonyPostfix]
        public static void DecideSuccessHarmonyPostfix(WorkResultSceneMode sceneMode, int index, bool commu)
        {
            if (!configEntryUtill["DecideSuccess_log"])
                MyLog.LogMessage("ScheduleAPI.DecideSuccess.HarmonyPostfix"
                , sceneMode
                , index
                , commu
            );

            if (!configEntryUtill["DecideSuccess_Perfect2"])
            {
                return;
            }

            ScheduleData scheduleData = GameMain.Instance.CharacterMgr.status.scheduleSlot[index];
            if (sceneMode == WorkResultSceneMode.Noon)
            {
                if (scheduleData.noon_success_level != ScheduleData.WorkSuccessLv.Unexecuted)
                    scheduleData.noon_success_level = ScheduleData.WorkSuccessLv.Perfect;
            }
            else if (sceneMode == WorkResultSceneMode.Night)
            {
                if (scheduleData.night_success_level != ScheduleData.WorkSuccessLv.Unexecuted)
                    scheduleData.night_success_level = ScheduleData.WorkSuccessLv.Perfect;
            }
        }
        // private static bool CheckCommu(Maid m, bool isDaytime, bool checkCommuState)
        //[HarmonyPatch(typeof(ScheduleAPI), "CheckCommu")]        
        //[HarmonyPostfix]
        public static void CheckCommu(bool __result, Maid m, bool isDaytime, bool checkCommuState)
        {
            if (!configEntryUtill["CheckCommu_log"])
                MyLog.LogMessage("ScheduleAPI.CheckCommu"
                , MyUtill.GetMaidFullName(m)
                , isDaytime
                , checkCommuState
                , __result
            );
        }


        // public static bool VisibleNightWork(int workId, Maid maid = null, bool checkFinish = true)
        // public static bool EnableNightWork(int workId, Maid maid = null, bool calledTargetCheck = true, bool withMaid = true)
        // public static bool EnableNoonWork(int workId, Maid maid = null)
        /// <summary>
        /// 스케즐 버튼 듸우기?
        /// 모든 밤시중이 활성화 되버려서 이방법 위험
        /// ScheduleCSVData.GetNightTravelWorkId:夜の旅行仕事IDが発見できませんでした。
        /// 발생
        /// </summary>
        /// <param name="__result"></param>
        /// <param name="workId"></param>
        /// <param name="maid"></param> null 잇을수 있음
        /// <returns></returns>
        [HarmonyPatch(typeof(ScheduleAPI), "VisibleNightWork")]
        //[HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void VisibleNightWork(ref bool __result, int workId, Maid maid)
        {
            if (configEntryUtill["VisibleNightWork", false])
            {
                MyLog.LogMessage("ScheduleAPI.VisibleNightWork"
                    , __result
                    , workId
                    , MyUtill.GetMaidFullName(maid)
                    , SceneFreeModeSelectManager.IsFreeMode
                );
            }
            if (configEntryUtill["VisibleNightWork_result", false])
                __result = true;
        }

        /// <summary>
        /// 스케줄 등록시
        /// </summary>
        /// <param name="__result"></param>
        /// <param name="workId"></param>
        /// <param name="maid"></param>
        /// <returns></returns>
        [HarmonyPatch(typeof(ScheduleAPI), "EnableNightWork")]
        //[HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void EnableNightWork(ref bool __result, int workId, Maid maid)
        {
            if (configEntryUtill["EnableNightWork", false])
            {
                MyLog.LogMessage("ScheduleAPI.EnableNightWork"
                    , __result
                    , workId
                    , MyUtill.GetMaidFullName(maid)
                    , SceneFreeModeSelectManager.IsFreeMode
                );

            }
            //if (configEntryUtill["EnableNightWork_result"])
                if (SceneFreeModeSelectManager.IsFreeMode)
                    __result = true;
        }

        [HarmonyPatch(typeof(ScheduleAPI), "EnableNoonWork")]
        [HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        public static void EnableNoonWork(ref bool __result, int workId, Maid maid)
        {
            if (configEntryUtill["EnableNoonWork", false])
            {
                MyLog.LogMessage("ScheduleAPI.EnableNoonWork"
                    , __result
                    , workId
                    , MyUtill.GetMaidFullName(maid)
                    , SceneFreeModeSelectManager.IsFreeMode
                );

            }
            if (configEntryUtill["EnableNoonWork_result"])
                if (SceneFreeModeSelectManager.IsFreeMode)
                __result = true;
            /*
			if (Lilly.isLogOnOffAll)
				MyLog.LogMessage("EnableNoonWork:" + SceneFreeModeSelectManager.IsFreeMode
				, workId
				, ScheduleCSVData.AllData[workId].name
				, maid != null ? MaidUtill.GetMaidFullNale(maid) : "" );
			*/
            //return false; // SceneFreeModeSelectManager.IsFreeMode;
        }

        //#if DEBUG

        /// <summary>
        /// 분석용
        /// </summary>
        /// <param name="workId"></param>
        /// <param name="maid"></param>
        /// <param name="checkFinish"></param>
        /// <returns></returns>
        public static bool VisibleNightWork(int workId, Maid maid, bool checkFinish)
        {
            ScheduleCSVData.Yotogi yotogi = ScheduleCSVData.YotogiData[workId];
            switch (yotogi.yotogiType)
            {
                case ScheduleCSVData.YotogiType.Vip:
                case ScheduleCSVData.YotogiType.VipCall:
                    {
                        NightWorkState nightWorksState = GameMain.Instance.CharacterMgr.status.GetNightWorksState(workId);
                        //if (nightWorksState == null)
                        {
                            //return false;
                        }
                        //nightWorksState.finish = true;
                        //if (checkFinish && nightWorksState.finish)
                        {
                            //if (DailyMgr.IsLegacy)
                            {
                                //return false;
                            }
                            //if (GameMain.Instance.CharacterMgr.status.clubGrade < 5)
                            {
                                //return false;
                            }
                            //ScheduleCSVData.vipFullOpenDay = 0;
                            //if (GameMain.Instance.CharacterMgr.status.days < ScheduleCSVData.vipFullOpenDay)
                            {
                                //return false;
                            }
                        }
                        break;
                    }
                case ScheduleCSVData.YotogiType.Travel:
                    //return false;
                    break;
                case ScheduleCSVData.YotogiType.EasyYotogi:
                    {
                        //if (yotogi.easyYotogi == null)
                        {
                            //return false;
                        }
                        //int trophyId = yotogi.easyYotogi.trophyId;
                        //if (!GameMain.Instance.CharacterMgr.status.IsHaveTrophy(trophyId))
                        {
                            //return false;
                        }
                        break;
                    }
            }
            if (yotogi.condPackage.Count > 0)
            {
                for (int i = 0; i < ScheduleCSVData.YotogiData[workId].condPackage.Count; i++)
                {
                    if (!PluginData.IsEnabled(ScheduleCSVData.YotogiData[workId].condPackage[i]))
                    {
                        return false;
                    }
                }
            }
            if (yotogi.condManVisibleFlag1.Count > 0)
            {
                for (int j = 0; j < yotogi.condManVisibleFlag1.Count; j++)
                {
                    if (GameMain.Instance.CharacterMgr.status.GetFlag(yotogi.condManVisibleFlag1[j]) < 1)
                    {
                        return false;
                    }
                }
            }
            if (maid != null)
            {
                if (ScheduleCSVData.YotogiData[workId].condMainChara && !maid.status.mainChara)
                {
                    return false;
                }
                if (yotogi.condPersonal.Count > 0)
                {
                    bool flag = false;
                    for (int k = 0; k < yotogi.condPersonal.Count; k++)
                    {
                        if (maid.status.personal.id == yotogi.condPersonal[k])
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        return false;
                    }
                }
                if (yotogi.subMaidUnipueName != string.Empty)
                {
                    if (maid.status.heroineType != HeroineType.Sub)
                    {
                        return false;
                    }
                    if (yotogi.subMaidUnipueName != maid.status.subCharaData.uniqueName)
                    {
                        return false;
                    }
                }
                else if (maid.status.heroineType == HeroineType.Sub)
                {
                    return false;
                }
            }
            return true;
        }

        //#endif

    }
}
