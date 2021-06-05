using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class SceneManagerPatch
    {
        // SceneManager

        //public static Scene scene;       
        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "SceneManagerPatch"
        );
        /*
        [HarmonyPatch(typeof(SceneManager), "LoadScene",new Type[] { typeof(string) }), HarmonyPostfix]
        public static void LoadScene(string sceneName)
        {
            if (configEntryUtill["LoadScene", false])
                MyLog.LogMessage("LoadScene"
                    , sceneName
                    );
        }
        */

        [HarmonyPatch(typeof(SceneManager), "LoadSceneAsyncNameIndexInternal"), HarmonyPostfix]//, new Type[] { typeof(string) }
        public static void LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, bool isAdditive, bool mustCompleteNextFrame)
        {
            if (configEntryUtill["LoadSceneAsyncNameIndexInternal", false])
                MyLog.LogMessage("LoadSceneAsyncNameIndexInternal"
                    , sceneName
                    , sceneBuildIndex
                    , isAdditive
                    , mustCompleteNextFrame
                    );
        }
    }
}
