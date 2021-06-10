using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchInfo
{

#if ScheduleUtill
    class YotogiOldStageSelectManagerPatch
    {
        // YotogiOldStageSelectManager
        public static List<YotogiOldStageUnit> yotogiStageUnits = new List<YotogiOldStageUnit>();

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
    "YotogiStageSelectManagerPatch"
    );

        public static YotogiOldStageSelectManager instance;

        public static bool isSelect {
            get => configEntryUtill["isSelect"];
            set => configEntryUtill["isSelect"] = value;
        }

        [HarmonyPatch(typeof(YotogiOldStageSelectManager), "OnCall")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void OnCallPre()//YotogiStageSelectManager __instance
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiOldStageSelectManager.OnCall");
            }
            //instance = __instance;

            if (isSelect)
            {
                yotogiStageUnits.Clear();
            }
        }

        [HarmonyPatch(typeof(YotogiOldStageSelectManager), "OnCall")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void OnCallPost(YotogiOldStageSelectManager __instance)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiStageSelectManager.OnCall");
            }
            instance = __instance;

            if (isSelect)
            {
                Select();
            }
        }

        //public void SetStageData(YotogiOld.StageData stage_data, bool enabled)
        [HarmonyPatch(typeof(YotogiOldStageUnit), "SetStageData", new Type[] { typeof(YotogiOld.StageData), typeof(bool) })]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void SetStageData(YotogiOld.StageData stage_data, bool enabled, YotogiOldStageUnit __instance, UILabel ___name_label_)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiOldSkillUnit.SetStageData"
                    , stage_data.sort_id
                    , stage_data.draw_name
                    , stage_data.stage_name
                    , enabled
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
                ___name_label_.text = stage_data.draw_name;
                //this.name_label_.text = "??????";
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

            MyLog.LogMessage("YotogiOldStageSelectManagerPatch.Select", yotogiStageUnits.Count);

            if (yotogiStageUnits.Count > 0)
            {
                // 배경을 바꾸기만 함. 실제로도 적용 되긴 함
                YotogiOldStageUnit yotogiStageUnit = yotogiStageUnits[UnityEngine.Random.Range(0, yotogiStageUnits.Count)];
                yotogiStageUnit.UpdateBG();

                YotogiOld.StageData stage_data = yotogiStageUnit.stage_data;
                YotogiOldStageSelectManager.SelectStage(stage_data);
                GameMain.Instance.MainCamera.SetFromScriptOnTarget(stage_data.camera_data.stage_select_camera_center, stage_data.camera_data.stage_select_camera_radius, stage_data.camera_data.stage_select_camera_rotate);
                GameMain.Instance.SoundMgr.PlayBGM(yotogiStageUnit.stage_data.bgm_file, 1f, true);
            }
        }
    } 
#endif
}
