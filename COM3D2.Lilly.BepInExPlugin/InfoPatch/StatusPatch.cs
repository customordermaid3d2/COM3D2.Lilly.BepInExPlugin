using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class StatusPatch
    {
        // Status

        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerStatus.Status), "SetFlag")]
        public static void PlayerStatusSetFlag(string flagName, int value)
        {
            MyLog.LogMessage("PlayerStatusSetFlag"
                , flagName
                , value
            );  
        }

        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerStatus.Status), "AddFlag")]
        public static void PlayerStatusAddFlag(string flagName, int value)
        {
            MyLog.LogMessage("PlayerStatusAddFlag"
                , flagName
                , value
            );
        }

        // public int GetFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerStatus.Status), "GetFlag")]
        public static void PlayerStatusGetFlag(string flagName, int __result)
        {
            MyLog.LogMessage("PlayerStatusGetFlag"
                , flagName
                , __result
            );
        }

        // public bool RemoveFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerStatus.Status), "RemoveFlag")]
        public static void PlayerStatusRemoveFlag(string flagName, bool __result)
        {
            MyLog.LogMessage("PlayerStatusRemoveFlag"
                , flagName
                , __result
            );
        }

        // =================================
        
        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "SetEventEndFlag")]
        public static void SetEventEndFlag(int id, bool value)
        {
            MyLog.LogMessage("SetEventEndFlag"
                , id
                , value
            );
        }

        // public bool GetEventEndFlag(int id)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "GetEventEndFlag")]
        public static void GetEventEndFlag(int id, bool __result)
        {
            MyLog.LogMessage("MaidStatusGetFlag"
                , id
                , __result
            );
        }

        // public void RemoveEventEndFlag(int id)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "RemoveEventEndFlag")]
        public static void RemoveEventEndFlag(int id)
        {
            MyLog.LogMessage("MaidStatusRemoveFlag"
                , id
            );
        }
        
        // =================================
        
        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "SetFlag")]
        public static void MaidStatusSetFlag(string flagName, int value)
        {
            MyLog.LogMessage("MaidStatusSetFlag"
                , flagName
                , value
            );
        }

        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "AddFlag")]
        public static void MaidStatusAddFlag(string flagName, int value)
        {
            MyLog.LogMessage("MaidStatusAddFlag"
                , flagName
                , value
            );
        }

        // public int GetFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "GetFlag")]
        public static void MaidStatusGetFlag(string flagName, int __result)
        {
            MyLog.LogMessage("MaidStatusGetFlag"
                , flagName
                , __result
            );
        }

        // public bool RemoveFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "RemoveFlag")]
        public static void MaidStatusRemoveFlag(string flagName, bool __result)
        {
            MyLog.LogMessage("MaidStatusRemoveFlag"
                , flagName
                , __result
            );
        }

        

        // =================================
        
        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Old.Status), "SetFlag")]
        public static void MaidStatusOldSetFlag(string flagName, int value)
        {
            MyLog.LogMessage("MaidStatusOldSetFlag"
                , flagName
                , value
            );
        }

        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Old.Status), "AddFlag")]
        public static void MaidStatusOldAddFlag(string flagName, int value)
        {
            MyLog.LogMessage("MaidStatusOldAddFlag"
                , flagName
                , value
            );
        }

        // public int GetFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Old.Status), "GetFlag")]
        public static void MaidStatusOldGetFlag(string flagName, int __result)
        {
            MyLog.LogMessage("MaidStatusOldGetFlag"
                , flagName
                , __result
            );
        }

        // public bool RemoveFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Old.Status), "RemoveFlag")]
        public static void MaidStatusOldRemoveFlag(string flagName, bool __result)
        {
            MyLog.LogMessage("MaidStatusOldRemoveFlag"
                , flagName
                , __result
            );
        }

        // CMSystem

        //public Dictionary<string, int> m_GenericTmpFlag = new Dictionary<string, int>();
        //public Dictionary<string, string> m_SystemVers = new Dictionary<string, string>();

        // public void SetTmpGenericFlag(string flag_name, int val)
        [HarmonyPostfix, HarmonyPatch(typeof(CMSystem), "SetTmpGenericFlag")]
        public static void SetTmpGenericFlag(string flag_name, int val)
        {
            MyLog.LogMessage("SetTmpGenericFlag"
                , flag_name
                , val
            );
        }

        // GameMain.Instance.CMSystem.SetTmpGenericFlag("ダンスOVRカメラタイプ", 0);
        // public int GetTmpGenericFlag(string flag_name)
        [HarmonyPostfix, HarmonyPatch(typeof(CMSystem), "GetTmpGenericFlag")]
        public static void GetTmpGenericFlag(string flag_name, int __result)// Dictionary<string, int> ___m_GenericTmpFlag
        {            
            MyLog.LogMessage("GetTmpGenericFlag"
                , flag_name
                , __result
            );
        }


    }
}
