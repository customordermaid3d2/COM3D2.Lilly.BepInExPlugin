using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.InfoPatch
{
    /// <summary>
    /// GameMain.Instance.ScriptMgr.adv_kag.kag.LoadScenarioString(sceneMenuLoadScript);
    /// GameMain.Instance.ScriptMgr.adv_kag.kag.Exec();    /// 
    /// GameMain.Instance.LoadScene("SceneToTitle");
    /// 스크립트 읽고 어느 명령어 부분으로 이동했는지 정보 출력용  
    /// </summary>
    class KagScriptPatch
    {
        // KagScript

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "KagScriptPatch"
        , "LoadScenarioString"
        , "GoToLabel"
        , "LoadScenario"
        );

        public static void run1(string scenario_str, string label_name)
        {
            GameMain.Instance.ScriptMgr.LoadAdvScenarioScript(scenario_str, label_name);        
            GameMain.Instance.ScriptMgr.adv_kag.Exec();
            /*
            GameMain.Instance.ScriptMgr.adv_kag.kag.LoadScenarioString(scenario_str);
            GameMain.Instance.ScriptMgr.adv_kag.kag.GoToLabel(label_name);
            GameMain.Instance.ScriptMgr.adv_kag.kag.Exec();
            */
        }

        public static void run2(string scenario_str, string label_name)
        {
            /*
            GameMain.Instance.ScriptMgr.LoadAdvScenarioScript(scenario_str, label_name);        
            GameMain.Instance.ScriptMgr.adv_kag.Exec();
            */
            GameMain.Instance.ScriptMgr.adv_kag.kag.LoadScenarioString(scenario_str);
            GameMain.Instance.ScriptMgr.adv_kag.kag.GoToLabel(label_name);
            GameMain.Instance.ScriptMgr.adv_kag.kag.Exec();
        }

        // public void LoadScenarioString(string scenario_str);
        [HarmonyPatch(typeof(KagScript), "LoadScenarioString")]
        [HarmonyPostfix]
        public static void LoadScenarioString(string scenario_str)
        {
            if (configEntryUtill["LoadScenarioString"])
                MyLog.LogMessage("LoadScenarioString.", scenario_str);
        }

        // public void GoToLabel(string label_name)
        [HarmonyPatch(typeof(KagScript), "GoToLabel")]
        [HarmonyPostfix]
        public static void GoToLabel(string label_name)
        {
            if (configEntryUtill["GoToLabel"])
                MyLog.LogMessage("GoToLabel.", label_name);
        }

        [HarmonyPatch(typeof(KagScript), "LoadScenario")]
        [HarmonyPostfix]
        public static void LoadScenario(string file_name)
        {
            if (configEntryUtill["LoadScenario"])
                MyLog.LogMessage("LoadScenario.", file_name);
        }
    }
}
