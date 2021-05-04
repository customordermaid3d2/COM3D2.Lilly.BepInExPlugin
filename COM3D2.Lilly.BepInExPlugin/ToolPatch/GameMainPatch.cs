using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    /// <summary>
    /// 아직 미구현
    /// </summary>
    class GameMainPatch
    {
        //GameMain

        // public bool Deserialize(int f_nSaveNo, bool scriptExec = true)

        [HarmonyPatch(typeof(GameMain), "Deserialize",new Type[] { typeof(int) , typeof(bool) })]
        [HarmonyPostfix]
        public static void Deserialize(ref bool __result)
        {
            MyLog.LogMessage("Deserialize.", __result);
            if (!__result)
            {
                //UICamera.InputEnable = true;
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
            MyLog.LogMessage("DeserializeFinalizer.");
        }
    }
}
