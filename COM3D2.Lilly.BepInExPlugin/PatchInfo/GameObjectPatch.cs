﻿using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    /// <summary>
    /// 모든 오브젝트가 다 나오니 그냥 끌것
    /// </summary>
    class GameObjectPatch
    {
        // GameObject


        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
            "GameObjectPatch"
        );

        [HarmonyPostfix, HarmonyPatch(typeof(GameObject), "SetActive")]
        public static void SetActive(GameObject __instance, bool value)
        {            
            if (!configEntryUtill ["SetActive",false] ||
                "New Game Object" == __instance.name ||
                "keta0" == __instance.name || 
                "keta1" == __instance.name || 
                "Explanation" == __instance.name
                )
                return;
            if (value)
            {
                MyLog.LogMessage("GameObject.SetActive"
                , __instance.name
                , __instance.tag
                , __instance.scene.name
                , value
                );
            }

        }
    }
}
