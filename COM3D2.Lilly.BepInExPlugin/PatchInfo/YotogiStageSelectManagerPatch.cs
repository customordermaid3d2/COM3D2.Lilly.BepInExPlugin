using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class YotogiStageSelectManagerPatch
    {
        // YotogiStageSelectManager

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "YotogiStageSelectManagerPatch"
        );

        [HarmonyPatch(typeof(YotogiStageSelectManager), "OnCall")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void OnCall()
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiStageSelectManager.OnCall");
            }
        }

        [HarmonyPatch(typeof(YotogiStageSelectManager), "OnClickOK")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void OnClickOK()
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiStageSelectManager.OnClickOK");
            }
        }
    }
}
