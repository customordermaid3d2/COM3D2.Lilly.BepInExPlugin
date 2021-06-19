using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using Kasizuki;
using PrivateMaidMode;
using scoutmode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static Kasizuki.KasizukiManager;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class DeserializePatch
    {
        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "DeserializePatch"
            , "Deserialize"
        );

        [HarmonyPatch(typeof(GameMain), "MakeSavePathFileName")]
        [HarmonyPrefix]
        public static void MakeSavePathFileName(int f_nSaveNo)
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("GameMain.MakeSavePathFileName", f_nSaveNo);
        }

        [HarmonyPatch(typeof(GameMain), "DeserializeReadHeader")]
        [HarmonyPrefix]
        public static void DeserializeReadHeader(BinaryReader brRead, int gameVersion)
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("GameMain.DeserializeReadHeader", gameVersion);
        }

        [HarmonyPatch(typeof(CharacterMgr), "Deserialize")]
        [HarmonyPrefix]
        public static void CharacterMgr_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("CharacterMgr.Deserialize");
        }

        [HarmonyPatch(typeof(ScriptManager), "Deserialize")]
        [HarmonyPrefix]
        public static void ScriptManager_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("ScriptManager.Deserialize");
        }
        
        [HarmonyPatch(typeof(ScenarioSelectMgr), "Deserialize")]
        [HarmonyPrefix]
        public static void ScenarioSelectMgr_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("ScenarioSelectMgr.Deserialize");
        }
        
        [HarmonyPatch(typeof(FacilityManager), "Deserialize",typeof(BinaryReader))]
        [HarmonyPrefix]
        public static void FacilityManager_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("FacilityManager.Deserialize");
        }
        
        [HarmonyPatch(typeof(EmpireLifeModeManager), "Deserialize")]
        [HarmonyPrefix]
        public static void EmpireLifeModeManager_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("EmpireLifeModeManager.Deserialize");
        }
        
        [HarmonyPatch(typeof(KasizukiManager), "Deserialize")]
        [HarmonyPrefix]
        public static void KasizukiManager_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("KasizukiManager.Deserialize");
        }
        [HarmonyPatch(typeof(CasinoDataMgr), "Deserialize")]
        [HarmonyPrefix]
        public static void CasinoDataMgr_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("CasinoDataMgr.Deserialize");
        }
        
        [HarmonyPatch(typeof(VsDanceDataMgr), "Deserialize")]
        [HarmonyPrefix]
        public static void VsDanceDataMgr_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("VsDanceDataMgr.Deserialize");
        }
        
        [HarmonyPatch(typeof(SaveData), "Deserialize")]
        [HarmonyPrefix]
        public static void SaveData_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("SaveData.Deserialize");
        }
        
        [HarmonyPatch(typeof(PrivateModeMgr), "Deserialize")]
        [HarmonyPrefix]
        public static void PrivateModeMgr_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("PrivateModeMgr.Deserialize");
        }
        
        [HarmonyPatch(typeof(ScoutManager), "DeSerialize")]
        [HarmonyPrefix]
        public static void ScoutManager_Deserialize()
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("ScoutManager.DeSerialize");
        }

        [HarmonyPatch(typeof(NDebug), "Assert",typeof(bool),typeof(string))]
        [HarmonyPrefix]
        public static void Assert(bool condition, string message)
        {
            if (!condition)
                if (configEntryUtill["Assert"])
                    MyLog.LogMessage("NDebug.Assert", message);

        }

    }
}
