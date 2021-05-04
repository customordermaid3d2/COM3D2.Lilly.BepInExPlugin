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
    /// </summary>
    class KagScriptPatch
    {
        // KagScript

        public static void run(string scenario_str)
        {
            GameMain.Instance.ScriptMgr.adv_kag.kag.LoadScenarioString(scenario_str);
            GameMain.Instance.ScriptMgr.adv_kag.kag.Exec();
        }

        // public void LoadScenarioString(string scenario_str);
        [HarmonyPatch(typeof(KagScript), "LoadScenarioString")]
        [HarmonyPostfix]
        public static void LoadScenarioString(string scenario_str)
        {
            MyLog.LogMessage("LoadScenarioString.", scenario_str);
        }

        // public void GoToLabel(string label_name)
        [HarmonyPatch(typeof(KagScript), "GoToLabel")]
        [HarmonyPostfix]
        public static void GoToLabel(string label_name)
        {
            MyLog.LogMessage("GoToLabel.", label_name);
        }

        [HarmonyPatch(typeof(KagScript), "LoadScenario")]
        [HarmonyPostfix]
        public static void LoadScenario(string file_name)
        {
            MyLog.LogMessage("LoadScenario.", file_name);
        }
    }
}
