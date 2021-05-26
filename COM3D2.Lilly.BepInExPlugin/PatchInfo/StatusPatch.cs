using BepInEx.Configuration;
using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    /// <summary>
    /// 플레그 설정 정보 출력
    /// </summary>
    class StatusPatch //: AwakeUtill
    {

        /*
        public static ConfigEntry<bool> setFlag;
        public static ConfigEntry<bool> addFlag;
        public static ConfigEntry<bool> getFlag;
        public static ConfigEntry<bool> removeFlag;
        */
        public static ConfigEntryUtill config = ConfigEntryUtill.Create(
            "StatusPatch",
            "SetFlag",
            "AddFlag",
            "GetFlag",
            "RemoveFlag"
            );
        // Status

        /// <summary>
        /// 하모니 작동 전이라 안되는듯?
        /// </summary>
        public StatusPatch()
        {
            MyLog.LogMessage("StatusPatch"
            );
            //config = new ConfigEntryUtill("StatusPatch");
            //config.Add("SetFlag",true);
            //config.Add("AddFlag", true);
            //config.Add("GetFlag", true);
            //config.Add("RemoveFlag", true);
            /*
            setFlag = customFile.Bind(
              "StatusPatch",
              "SetFlag",
              true
              );
            addFlag = customFile.Bind(
              "StatusPatch",
              "AddFlag",
              true
              );
            getFlag = customFile.Bind(
              "StatusPatch",
              "GetFlag",
              true
              );
            removeFlag = customFile.Bind(
              "StatusPatch",
              "RemoveFlag",
              true
              );
            */
            //Lilly.actionsAwake += Awake;//하모니 작동 전이라 안되는듯?
        }

        public static void Initialize()
        {
            MyLog.LogDebug("StatusPatch.Initialize");
            
        }

        public  void init()
        {
            MyLog.LogDebug("StatusPatch.init");
            /*
            config = new ConfigEntryUtill(
            "StatusPatch",
            "SetFlag",
            "AddFlag",
            "GetFlag",
            "RemoveFlag"
            );
            */
        }


        public  void Awake()
        {
            MyLog.LogDebug("StatusPatch.Awake");
        }

        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerStatus.Status), "SetFlag")]
        public static void PlayerStatusSetFlag(string flagName, int value)
        {
            if(config["SetFlag"])
                MyLog.LogMessage("PlayerStatusSetFlag"
                    , flagName
                    , value
                );
        }

        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerStatus.Status), "AddFlag")]
        public static void PlayerStatusAddFlag(string flagName, int value)
        {
            if (config["AddFlag"])
                MyLog.LogMessage("PlayerStatusAddFlag"
                , flagName
                , value
            );
        }

        // public int GetFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerStatus.Status), "GetFlag")]
        public static void PlayerStatusGetFlag(string flagName, int __result)
        {
            if (config["GetFlag"])
                MyLog.LogMessage("PlayerStatusGetFlag"
                , flagName
                , __result
            );
        }

        // public bool RemoveFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerStatus.Status), "RemoveFlag")]
        public static void PlayerStatusRemoveFlag(string flagName, bool __result)
        {
            if (config["RemoveFlag"])
                MyLog.LogMessage("PlayerStatusRemoveFlag"
                , flagName
                , __result
            );
        }

        public static Dictionary<string, string> flagsPlayer=new Dictionary<string, string>();

        // public bool RemoveFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerStatus.Status), "Deserialize")]
        public static void Deserialize()
        {
            StatusPatch.flagsPlayer = GameMain.Instance.CharacterMgr.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
        }

        // =================================

        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "SetEventEndFlag")]
        public static void SetEventEndFlag(int id, bool value)
        {
            if (config["SetFlag"])
                MyLog.LogMessage("SetEventEndFlag"
                , id
                , value
            );
        }

        // public bool GetEventEndFlag(int id)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "GetEventEndFlag")]
        public static void GetEventEndFlag(int id, bool __result)
        {
            if (config["GetFlag"])
                MyLog.LogMessage("MaidStatusGetFlag"
                , id
                , __result
            );
        }

        // public void RemoveEventEndFlag(int id)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "RemoveEventEndFlag")]
        public static void RemoveEventEndFlag(int id)
        {
            if (config["RemoveFlag"])
                if (config["RemoveFlag"])
                MyLog.LogMessage("MaidStatusRemoveFlag"
                , id
            );
        }

        // =================================

        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "SetFlag")]
        public static void MaidStatusSetFlag(string flagName, int value)
        {
            if (config["SetFlag"])
                MyLog.LogMessage("MaidStatusSetFlag"
                , flagName
                , value
            );
        }

        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "AddFlag")]
        public static void MaidStatusAddFlag(string flagName, int value)
        {
            if (config["AddFlag"])
                MyLog.LogMessage("MaidStatusAddFlag"
                , flagName
                , value
            );
        }

        // public int GetFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "GetFlag")]
        public static void MaidStatusGetFlag(string flagName, int __result)
        {
            if (config["GetFlag"])
                MyLog.LogMessage("MaidStatusGetFlag"
                , flagName
                , __result
            );
        }

        // public bool RemoveFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Status), "RemoveFlag")]
        public static void MaidStatusRemoveFlag(string flagName, bool __result)
        {
            if (config["RemoveFlag"])
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
            if (config["SetFlag"])
                MyLog.LogMessage("MaidStatusOldSetFlag"
                , flagName
                , value
            );
        }

        // public void AddFlag(string flagName, int value)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Old.Status), "AddFlag")]
        public static void MaidStatusOldAddFlag(string flagName, int value)
        {
            if (config["AddFlag"])
                MyLog.LogMessage("MaidStatusOldAddFlag"
                , flagName
                , value
            );
        }

        // public int GetFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Old.Status), "GetFlag")]
        public static void MaidStatusOldGetFlag(string flagName, int __result)
        {
            if (config["GetFlag"])
                MyLog.LogMessage("MaidStatusOldGetFlag"
                , flagName
                , __result
            );
        }

        // public bool RemoveFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(MaidStatus.Old.Status), "RemoveFlag")]
        public static void MaidStatusOldRemoveFlag(string flagName, bool __result)
        {
            if (config["RemoveFlag"])
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
            if (config["SetFlag"])
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
            if (config["GetFlag"])
                MyLog.LogMessage("GetTmpGenericFlag"
                , flag_name
                , __result
            );
        }


    }
}
