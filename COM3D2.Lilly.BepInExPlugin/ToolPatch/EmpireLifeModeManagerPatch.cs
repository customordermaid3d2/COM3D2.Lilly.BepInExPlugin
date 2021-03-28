using FacilityFlag;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wf;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 회상 모드에서 라이프 관련
    /// </summary>
    class EmpireLifeModeManagerToolPatch 
    {
        // EmpireLifeModeManager

        [HarmonyPatch(typeof(EmpireLifeModeManager), "GetScenarioExecuteCount")]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void GetScenarioExecuteCount(out int __result)
        {
            __result = 9999;/*
            if (Lilly.isLogOnOffAll)
                MyLog.LogMessage("GetScenarioExecuteCount.");*/
        }

    }
}
