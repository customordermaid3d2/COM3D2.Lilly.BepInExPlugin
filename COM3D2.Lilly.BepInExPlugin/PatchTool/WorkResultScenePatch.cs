using HarmonyLib;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    class WorkResultScenePatch
    {
        // WorkResultScene

        /// <summary>
        /// public void Calc(WorkResultSceneMode mode = WorkResultSceneMode.Noon, bool intervene = true)
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(WorkResultScene), "Calc",new Type[] { typeof(WorkResultSceneMode),typeof(bool)})]
        public static void Calc(WorkResultSceneMode mode = WorkResultSceneMode.Noon, bool intervene = true)
        {
            MyLog.LogMessage(
                "WorkResultScene.Calc"
                , mode
                , intervene
            );

        }


    }
}
