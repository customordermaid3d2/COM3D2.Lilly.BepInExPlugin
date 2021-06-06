using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchBase
{
    class SaveAndLoadMgrPatch
    {
        // SaveAndLoadMgr

        //public static SaveAndLoadMgr instance;

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "SaveAndLoadMgrPatch"
        );

        /// <summary>
        /// 이거론 안됨. 자꾸 파괴되서 BasePanelMgr를 참조해야할듯
        /// </summary>
        /// <param name="__instance"></param>
        //[HarmonyPatch(typeof(SaveAndLoadMgr), "Init")]
        //[HarmonyPostfix]
        //public static void Init(SaveAndLoadMgr __instance)
        //{
        //    instance = __instance;
        //    if (configEntryUtill["Init"])
        //        MyLog.LogMessage("SaveAndLoadMgr.Init");
        //}
    }
}
