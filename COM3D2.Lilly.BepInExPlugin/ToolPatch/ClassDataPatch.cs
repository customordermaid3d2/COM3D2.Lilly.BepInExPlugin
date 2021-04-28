using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 실시간으로 밤시중과 업무 클래스 레벨 최대치 처리
    /// 성능 너무 않조으니 이건 쓰지 말자
    /// </summary>
    class ClassDataPatch
    {

        // ClassData

        [HarmonyPostfix, HarmonyPatch(typeof(ClassData<JobClass.Data>), "level", MethodType.Getter)]
        private static void JobClass_Data_level(ClassData<JobClass.Data> __instance, ref int __result) // string __m_BGMName 못가져옴
        {
            int l = __instance.expSystem.GetMaxLevel();
            __instance.expSystem.SetLevel(l);
            MyLog.LogMessage("ClassData.JobClass.level"
                , __result
                , l
                );
            __result = l;
        }

        /// <summary>
        /// 스케줄 관리에서 훈련이랑 업무
        /// 메이드 관리에서 클래스 등이 해당
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(ClassData<YotogiClass.Data>), "level", MethodType.Getter)]
        private static void YotogiClass_Data_level(ClassData<YotogiClass.Data> __instance, ref int __result) // string __m_BGMName 못가져옴
        {
            int l = __instance.expSystem.GetMaxLevel();
            __instance.expSystem.SetLevel(l);
            MyLog.LogMessage("ClassData.YotogiClass.level"
                , __result
                ,l
                );
            __result = l;
        }
    }
}
