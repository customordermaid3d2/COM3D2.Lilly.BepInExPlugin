using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class NDebugPatch
    {
        // NDebug

        //public static void MessageBox(string f_strTitle, string f_strMsg)

        [HarmonyPatch(typeof(NDebug), "MessageBox"), HarmonyPrefix]        
        private static bool FadeOut(string f_strTitle, string f_strMsg) // string __m_BGMName 못가져옴
        {
            MyLog.LogFatal(f_strTitle, f_strMsg);
            return false;
        }

    }
}
