using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.PatchBase
{
    class ScreenPatch
    {
        // Screen

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "ScreenPatch"
        );

        /// <summary>
        /// public static void SetResolution(int width, int height, bool fullscreen)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPatch(typeof(Screen), "SetResolution",new Type[] { typeof(int) , typeof(int) , typeof(bool) })]
        [HarmonyPrefix]
        public static void SetResolution(int width, int height, bool fullscreen,CharacterMgr __instance)
        {
            if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("Screen.SetResolution", width, height, fullscreen);
            }
        }
    }
}
