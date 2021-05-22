using HarmonyLib;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    class SlotBasePatch
    {

        [HarmonyPostfix, HarmonyPatch(typeof(SlotBase), "noonCommuFlag",MethodType.Getter)]
        private static void noonCommuFlagGetter(bool __result) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("noonCommuFlagGetter"
                , __result
                );
        }

        [HarmonyPostfix, HarmonyPatch(typeof(SlotBase), "nightCommuFlag", MethodType.Getter)]
        private static void nightCommuFlagGetter(bool __result) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("nightCommuFlagGetter"
                , __result
                );
        }

        [HarmonyPostfix, HarmonyPatch(typeof(SlotBase), "slotId", MethodType.Getter)]
        private static void slotIdGetter(int __result) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("slotIdGetter"
                , __result
                );
        }
        [HarmonyPostfix, HarmonyPatch(typeof(SlotBase), "popular_rank", MethodType.Getter)]
        private static void popular_rankGetter(int __result) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("popular_rankGetter"
                , __result
                );
        }
    }
}
