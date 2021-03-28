using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    class CameraMainPatch
    {
        //public void FadeIn(float f_fTime = 0.5f, bool f_bUIEnable = false, CameraMain.dgOnCompleteFade f_dg = null, bool f_bSkipable = true, bool f_bColorIsSameOut = true, Color f_color = default(Color))
        [HarmonyPatch(typeof(CameraMain), "FadeIn")]
        [HarmonyPostfix]
        private static void FadeIn(CameraMain __instance) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("FadeIn:" + __instance.GetFadeState());

        }        
        
        [HarmonyPatch(typeof(CameraMain), "FadeOut")]
        [HarmonyPostfix]
        private static void FadeOut(CameraMain __instance) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("FadeOut:" + __instance.GetFadeState());

        }        

        [HarmonyPatch(typeof(CameraMain), "FadeInNoUI")]
        [HarmonyPostfix]
        private static void FadeInNoUI(CameraMain __instance) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("FadeOut:" + __instance.GetFadeState());

        }
        
        [HarmonyPatch(typeof(CameraMain), "FadeOutNoUI")]
        [HarmonyPostfix]
        private static void FadeOutNoUI(CameraMain __instance) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("FadeOut:" + __instance.GetFadeState());

        }

        /// <summary>
        /// 
        /// </summary>
        public enum FadeState
        {
            Non,
            ProcIn,
            ProcOut,
            Out
        }


        //		CameraMain.FadeOut(float, bool, CameraMain.dgOnCompleteFade, bool, Color) : void @060018DD
    }
}
