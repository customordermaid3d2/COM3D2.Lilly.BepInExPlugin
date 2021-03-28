using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    class SceneFreeModeSelectManagerPatch
    {
        /// <summary>
        /// 피들러 참고 
        /// 회상모드
        /// 모든 버튼 활성화
        /// </summary>
        /// TypeDefinition type12 = CS$<>8__locals1.ass.MainModule.GetType("SceneFreeModeSelectManager");
        /// type12.GetMethod("Start").InjectWith(type.GetMethod("OnSceneFreeModeSelectAwake"), -1, null, InjectFlags.PassInvokingInstance, InjectDirection.Before, null, null);
        [HarmonyPatch(typeof(SceneFreeModeSelectManager), "Start")]
        [HarmonyPostfix]
        public static void Start(SceneFreeModeSelectManager __instance)
        {
            //Maid maid = GameMain.Instance.CharacterMgr.GetMaid(0);
            //GameObject gameObject = base.gameObject.transform.parent.gameObject;
            //UIGrid component = UTY.GetChildObject(gameObject, "MenuSelect/Menu/FreeModeMenuButton", false).GetComponent<UIGrid>();
            //GameObject childObject = UTY.GetChildObject(gameObject, "MenuSelect/Menu/FreeModeMenuButton/ストーリー", false);
            //GameObject childObject2 = UTY.GetChildObject(gameObject, "MenuSelect/Menu/FreeModeMenuButton/日常", false);
            //GameObject childObject3 = UTY.GetChildObject(gameObject, "MenuSelect/Menu/FreeModeMenuButton/夜伽", false);
            //GameObject childObject4 = UTY.GetChildObject(gameObject, "MenuSelect/Menu/FreeModeMenuButton/VIP", false);
            //GameObject childObject5 = UTY.GetChildObject(gameObject, "MenuSelect/Menu/FreeModeMenuButton/VIP_HEvent", false);
            //GameObject childObject6 = UTY.GetChildObject(gameObject, "MenuSelect/Menu/FreeModeMenuButton/ライフモード", false);

            // FreeModeSelect.Awake()

            // 강제 활성화시 오류
            //SetMethod(__instance, "MenuSelect/Menu/FreeModeMenuButton/ストーリー");
            //SetMethod(__instance, "MenuSelect/Menu/FreeModeMenuButton/日常");
            SetMethod(__instance, "MenuSelect/Menu/FreeModeMenuButton/夜伽");
            //if (DailyMgr.IsLegacy)
            //{
            //    SetMethod(__instance, "MenuSelect/Menu/FreeModeMenuButton/VIP");
            //}
            //else
            //{
            //    SetMethod(__instance, "MenuSelect/Menu/FreeModeMenuButton/VIP_HEvent");
            //}
            //SetMethod(__instance, "MenuSelect/Menu/FreeModeMenuButton/ライフモード");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="test"></param>
        private static void SetMethod(SceneFreeModeSelectManager __instance, string test)
        {
            MyLog.LogMessage("CreateItemEverydayList:" + test);
            try
            {
                GameObject childObject = UTY.GetChildObject(__instance.gameObject.transform.parent.gameObject, test, false);
                childObject.SetActive(true);
                childObject.GetComponent<UIButton>().isEnabled = true;
            }
            catch (Exception)
            {
                MyLog.LogError("CreateItemEverydayList:" + test);
            }
        }
    }
}
