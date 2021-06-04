using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class DailyCtrlPatch
    {
        // DailyCtrl

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "DailyCtrlPatch"
        );

        [HarmonyPostfix, HarmonyPatch(typeof(DailyCtrl), "DisplayViewer")]
        public static void DisplayViewer(DailyMgr.Daily daily)
        {
            if (configEntryUtill["DisplayViewer", false])
                MyLog.LogMessage("DailyCtrl.DisplayViewer"
                    , daily
                );
        }
    }
}
