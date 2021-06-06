using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class UTYPatch
    {
        // UTY

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "UTYPatch"
        );

        /// <summary>
        /// public static GameObject GetChildObject(GameObject f_goParent, string f_strObjName, bool f_bNoError = false)
        /// </summary>
        [HarmonyPatch(typeof(YotogiStageSelectManager), "GetChildObject", new Type[] { typeof(GameObject), typeof(string), typeof(bool) })]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void GetChildObject(GameObject f_goParent, string f_strObjName, bool f_bNoError )
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("UTY.GetChildObject"
                    , f_goParent.name
                    , f_strObjName
                    , f_bNoError
                    );
            }
        }
    }
}
