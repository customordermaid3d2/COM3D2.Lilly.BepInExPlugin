using COM3D2.Lilly.Plugin.ToolPatch;
using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class DailyMgrPatch
    {
        // DailyMgr


        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "DailyMgrPatch"
        );

        public static bool isSetSlotAllMaid {
            get { return configEntryUtill["슬롯에_메이드_자동_배치", false]; }
            set { configEntryUtill["슬롯에_메이드_자동_배치", false] = value; }
            }
        
        public static bool isSetScheduleAllMaid {
            get { return configEntryUtill["메이드_스케줄_자동_배치", false]; }
            set { configEntryUtill["메이드_스케줄_자동_배치", false] = value; }
            }
        
        public static bool isSetRandomCommu {
            get { return configEntryUtill["커뮤니티_자동_적용", false]; }
            set { configEntryUtill["커뮤니티_자동_적용", false] = value; }
            }

        /// <summary>
        /// public void SceneStart(bool f_bIsDay, MonoBehaviour f_parent, DailyAPI.dgOnSceneStartCallBack f_dgLoadedFinish)
        /// </summary>
        /// <param name="___m_sceneName"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(DailyMgr), "OpenDaytimePanel")]
        public static void OpenDaytimePanel()
        {
            if (configEntryUtill["OpenDaytimePanel", false])
            {
                MyLog.LogMessage("DailyMgr.OpenDaytimePanel"
                );
            }
            
            if (isSetSlotAllMaid)
            {
                ScheduleMgrPatch.SetSlotAllMaid();
            }

            if (isSetScheduleAllMaid)
            {
                ScheduleUtill.SetScheduleAllMaid(ScheduleMgr.ScheduleTime.DayTime);
                ScheduleUtill.SetScheduleAllMaid(ScheduleMgr.ScheduleTime.Night);
            }

            if (isSetRandomCommu)
            {
                ScheduleAPIPatch.SetRandomCommu(true);
                ScheduleAPIPatch.SetRandomCommu(false);
            }
        }

        public static bool IsLegacy;
        [HarmonyPostfix, HarmonyPatch(typeof(DailyMgr), "IsLegacy",MethodType.Getter)]
        public static void GetIsLegacy(bool __result)
        {
            if (configEntryUtill["IsLegacy", false])
            {
                MyLog.LogMessage("DailyMgr.IsLegacy"
                    ,__result
                );
            }
            IsLegacy = __result;
        }
    }
}
