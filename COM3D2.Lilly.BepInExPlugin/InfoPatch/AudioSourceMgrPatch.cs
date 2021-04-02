using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 사운드 출력 관련
    /// </summary>
    public static class AudioSourceMgrPatch
    {
        // https://github.com/BepInEx/HarmonyX/wiki/Prefix-changes
        // https://github.com/BepInEx/HarmonyX/wiki/Patch-parameters
        // 
        // 매개 변수의 순서는 중요하지 않지만 이름은 중요합니다.
        // 모든 매개 변수를 전달할 필요는 없습니다.

        // public void LoadPlay(string f_strFileName, float f_fFadeTime, bool f_bStreaming, bool f_bLoop = false)

        // 정상
       /// [HarmonyPrefix,HarmonyPatch(typeof(AudioSourceMgr), "LoadPlay")]
        public static void LoadPlayPrefix1(string f_strFileName)
        {
            //MyLog.Log("LoadPlayPrefix1:" + f_strFileName);
        }

        /// <summary>
        /// 출력되는 소리
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="f_strFileName"></param>
        [HarmonyPostfix,HarmonyPatch(typeof(AudioSourceMgr), "LoadPlay")]        
        public static void LoadPlayPostfix2(AudioSourceMgr __instance, string f_strFileName)
        {
            MyLog.LogMessage("LoadPlayPostfix2:" + f_strFileName);
        }
    }
}
