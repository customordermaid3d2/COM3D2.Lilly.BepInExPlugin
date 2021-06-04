using com.workman.cm3d2.scene.dailyEtc;
using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchBase
{
    class BasePanelMgrPatch
    {
        // BasePanelMgr


        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "BasePanelMgrPatch"
        );

        public static BasePanelMgr instance;
        public static SceneMgr sceneMgr;

        [HarmonyPatch(typeof(BasePanelMgr), "Start")]
        [HarmonyPostfix]
        public static void Init(BasePanelMgr __instance, SceneMgr ___sceneMgr)
        {
            instance = __instance;
            sceneMgr = ___sceneMgr;
            if (configEntryUtill["Init"])
                MyLog.LogMessage("SaveAndLoadMgr.Init");

        }

        /// <summary>
        /// new Action(base.GetManager<SaveAndLoadMgr>().OpenLoadPanel)
        /// new Action(base.GetManager<SaveAndLoadMgr>().OpenSavePanel)
        /// </summary>
        public static void OpenLoadPanel()
        {
            sceneMgr.GetManager<SaveAndLoadMgr>().OpenLoadPanel();
        }       
        
        public static void OpenSavePanel()
        {
            sceneMgr.GetManager<SaveAndLoadMgr>().OpenSavePanel();
        }
       









    }
}
