using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using wf;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
#if ScheduleUtill
    class YotogiStageSelectManagerPatch
    {
        // YotogiStageSelectManager

        public static List<YotogiStageUnit> yotogiStageUnits = new List<YotogiStageUnit>();

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
    "YotogiStageSelectManagerPatch"
    );

        public static YotogiStageSelectManager instance;

        public static bool isSelect {
            get => configEntryUtill["isSelect"];
            set => configEntryUtill["isSelect"] = value;
        }

        [HarmonyPatch(typeof(YotogiStageSelectManager), "OnCall")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void OnCallPre()//YotogiStageSelectManager __instance
        {
            if (configEntryUtill["OnCallPre"])
            {
                MyLog.LogMessage("YotogiStageSelectManager.OnCall");
            }
            //instance = __instance;

            if (isSelect)
            {
                yotogiStageUnits.Clear();
            }
        }

        [HarmonyPatch(typeof(YotogiStageSelectManager), "OnCall")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void OnCallPost(YotogiStageSelectManager __instance)
        {
            if (configEntryUtill["OnCallPost"])
            {
                MyLog.LogMessage("YotogiStageSelectManager.OnCall");
            }
            instance = __instance;

            if (isSelect)
            {
                Select();
            }
        }

        [HarmonyPatch(typeof(YotogiStageSelectManager), "OnClickOK")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void OnClickOK()
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiStageSelectManager.OnClickOK");
            }

        }

        /// <summary>
        /// 분석용
        /// </summary>
        [HarmonyPatch(typeof(YotogiStageUnit), "SetStageData")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void SetStageData(YotogiStage.Data stage_data, bool enabled, bool isDaytime, YotogiStageUnit __instance, UILabel ___name_label_)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiStageUnit.SetStageData"
                    , stage_data.id
                    , stage_data.drawName
                    , stage_data.uniqueName
                    , enabled
                    , isDaytime
                    );
            }
            if (enabled)
            {
                //___name_label_.text = stage_data.drawName;
                //Utility.SetLocalizeTerm(__instance.name_label_, stage_data.termName, false);
                if (isSelect)
                {
                    yotogiStageUnits.Add(__instance);
                }
            }
            else
            {
                ___name_label_.text = stage_data.drawName;
                //this.name_label_.text = "??????";
            }

        }

        /// <summary>
        /// 분석용
        /// </summary>
        [HarmonyPatch(typeof(Utility), "CreatePrefab")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void CreatePrefab(GameObject parent, string path, bool trans_reset = true)
        {
            if (configEntryUtill["CreatePrefab", false])
            {
                MyLog.LogMessage("Utility.CreatePrefab"
                    , parent?.name
                    , path
                    , trans_reset
                    );
            }

        }


        public static void Select()
        {

            /*
[Message:     Lilly] UTY.GetChildObject , SystemShortcut , Base/Grid/Help , False
[Message:     Lilly] UTY.GetChildObject , StageSelectPanel , StageSelectViewer/StageViewer/Contents/StageUnitParent , False
[Message:     Lilly] UTY.GetChildObject , StageSelectPanel , Ok , False
[Message:     Lilly] UTY.GetChildObject , StageUnit(Clone) , Parent , False
[Message:     Lilly] UTY.GetChildObject , Parent , Icon , False
[Message:     Lilly] UTY.GetChildObject , Parent , Name , False
[Message:     Lilly] UTY.GetChildObject , Parent , StarGroup , False
[Message:     Lilly] UTY.GetChildObject , Parent , StarMarkGroup , False
[Message:     Lilly] UTY.GetChildObject , Parent , StarGroup , False
[Message:     Lilly] YotogiStageUnit.SetStageData
             * */

            MyLog.LogMessage("YotogiStageSelectManager.Select", yotogiStageUnits.Count);

            if (yotogiStageUnits.Count > 0)
            {
                // 배경을 바꾸기만 함. 실제로도 적용 되긴 함
                var yotogiStageUnit = yotogiStageUnits[UnityEngine.Random.Range(0, yotogiStageUnits.Count)];
                yotogiStageUnit.UpdateBG();

                YotogiStage.Data stage_data = yotogiStageUnit.stage_data;
                YotogiStageSelectManager.SelectStage(stage_data, GameMain.Instance.CharacterMgr.status.isDaytime);
                stage_data.stageSelectCameraData.Apply();
                GameMain.Instance.SoundMgr.PlayBGM(stage_data.bgmFileName, 1f, true);
            }

        }
    } 
#endif
}
