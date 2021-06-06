using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    /// <summary>
    /// 세이브 파일 로딩시 버전 차이 등으로 로딩 못하고 멈출경우 자동으로 타이틀로 돌아감
    /// </summary>
    class GameMainPatch
    {
        //GameMain

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "GameMain"
        );

        // public void LoadScene(string f_strSceneName)
        [HarmonyPatch(typeof(GameMain), "LoadScene")]
        [HarmonyPostfix]
        public static void LoadScene(string f_strSceneName)
        {
            if (configEntryUtill["LoadScene"])
                MyLog.LogMessage("GameMain.LoadScene", f_strSceneName);
        }

        // public bool Deserialize(int f_nSaveNo, bool scriptExec = true)

        [HarmonyPatch(typeof(GameMain), "Deserialize", new Type[] { typeof(int), typeof(bool) })]
        [HarmonyPostfix]
        public static void Deserialize(ref bool __result)
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("Deserialize", __result);
            if (!__result)
            {
                //UICamera.InputEnable = true;
                if (configEntryUtill["Deserialize"])
                    GameMain.Instance.LoadScene("SceneToTitle");
            }
        }

        //static readonly string sceneMenuLoadScript = "@AllProcPropSeqStart maid=0 sync\n" +
        //                                            "@eval exp=\"SetTmpFlag('新規雇用メイド',1)\"\n" +
        //                                            "@SceneCall name=SceneEdit qasm original";
        //static readonly string editDoneScript = "@jump file=\"DaytimeMain\" label=\"*昼メニュー\"";
        //

        [HarmonyPatch(typeof(GameMain), "Deserialize", new Type[] { typeof(int), typeof(bool) })]
        [HarmonyFinalizer]
        public static void DeserializeFinalizer(ref Exception __exception)
        {
            if (configEntryUtill["Deserialize"])
                MyLog.LogMessage("DeserializeFinalizer");
        }
    }
}
