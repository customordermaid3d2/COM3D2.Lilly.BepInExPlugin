using com.workman.cm3d2.scene.dailyEtc;
using COM3D2.Lilly.Plugin.PatchInfo;
using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    /// <summary>
    /// SceneDaily 장면 로딩시에 작동
    /// </summary>
    class SceneMgrPatch
    {
        // SceneMgr

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "SceneMgrPatch"
        );

        //private string m_sceneName;

        // private void DispatcherSceneDaily(string openType)

        [HarmonyPostfix, HarmonyPatch(typeof(SceneMgr), "Start")]
        public static void Start(string ___m_sceneName)
        {
            MyLog.LogMessage("SceneMgr.Start"
                , ___m_sceneName
            );
        }


        // private void DispatcherSceneDaily(string openType)
        [HarmonyPostfix, HarmonyPatch(typeof(SceneMgr), "DispatcherSceneDaily")]
        public static void DispatcherSceneDaily(string openType, Dictionary<string, string> ___m_tagBackup)
        {
            MyLog.LogMessage("SceneMgr.DispatcherSceneDaily"
                , openType
            );
            if (!DailyMgr.IsLegacy && openType == "Daytime")
            {
                if (configEntryUtill["___m_tagBackup", false])
                {
                    foreach (var item in ___m_tagBackup)
                    {
                        MyLog.LogMessage("SceneMgr.DispatcherSceneDaily"
                            , item.Key
                            , item.Value
                        );
                    }
                }

            }


        }
    }
}
