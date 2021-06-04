using com.workman.cm3d2.scene.dailyEtc;
using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class DailyAPIPatch
    {
        // DailyAPI

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "SceneMgrPatch"
        );

        /// <summary>
        /// public void SceneStart(bool f_bIsDay, MonoBehaviour f_parent, DailyAPI.dgOnSceneStartCallBack f_dgLoadedFinish)
        /// </summary>
        /// <param name="___m_sceneName"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(DailyAPI), "SceneStart")]
        public static void SceneStart(bool f_bIsDay, MonoBehaviour f_parent, DailyAPI.dgOnSceneStartCallBack f_dgLoadedFinish)
        {
            if (configEntryUtill["SceneStart", false])
            {
                MyLog.LogMessage("DailyAPI.SceneStart"
                    , f_bIsDay
                     , f_parent.name
                );
            }
        }
    }
}
