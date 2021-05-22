using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class BgMgrPatch
    {
        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
            "BgMgrPatch"
        );

        // public void ChangeBg(string f_strPrefubName)
        /// <summary>
        /// 배경 변경시?
        /// </summary>
        /// <param name="f_strPrefubName"></param>
        // public void ChangeBg(string f_strPrefubName)
        [HarmonyPatch(typeof(BgMgr), "ChangeBg")]
        [HarmonyPostfix]
        private static void ChangeBgPost(string f_strPrefubName)
        {
            if (configEntryUtill["ChangeBg", false])
                MyLog.LogMessage("BgMgr.ChangeBgPost : " + f_strPrefubName);
        }


    }
}
