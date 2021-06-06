using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class YotogiSkillSelectManagerPatch
    {
        // YotogiSkillSelectManager

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "YotogiSkillSelectManagerPatch"
        );

        [HarmonyPatch(typeof(YotogiSkillSelectManager), "OnCall")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void OnCall()
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.OnCall");
            }
            if (configEntryUtill["AddSkill"])
            {
                YotogiSkillContainerViewerPatch.AddSkill();
            }
        }
        
        [HarmonyPatch(typeof(YotogiSkillSelectManager), "OnClickAllReset")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void OnClickAllReset()
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.OnClickAllReset");
            }
        }

        [HarmonyPatch(typeof(YotogiSkillSelectManager), "OnClickEdit")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void OnClickEdit()
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.OnClickEdit");
            }
        }

        [HarmonyPatch(typeof(YotogiSkillSelectManager), "OnClickFromCategoryButton")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void OnClickFromCategoryButton(GameObject obj, Yotogi.Category category)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.OnClickFromCategoryButton"
                    , obj.name
                    , category
                    );
            }
        }

        [HarmonyPatch(typeof(YotogiSkillSelectManager), "OnClickFromSkillUnit")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void OnClickFromSkillUnit(GameObject game_obj)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.OnClickFromSkillUnit"
                    , game_obj
                    );
            }
        }
        [HarmonyPatch(typeof(YotogiSkillSelectManager), "CreateNewSkillUnit")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void CreateNewSkillUnit(GameObject root)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.CreateNewSkillUnit"
                    , root
                    );
            }
        }

        List<GameObject> skill_unit_list_ = new List<GameObject>();

        [HarmonyPatch(typeof(YotogiSkillSelectManager), "SetCategorySkill")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void SetCategorySkill(Yotogi.Category category)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.SetCategorySkill"
                    , category
                    );
            }
        }

        /*
        public static void test()
        {
            List<YotogiSkillListManager.Data> skill_list = this.category_data_array_[(int)category].skill_list;
            for (int j = 0; j < skill_list.Count; j++)
            {
                GameObject gameObject = this.CreateNewSkillUnit(childObject2);
                YotogiSkillLockUnit componentInChildren = gameObject.GetComponentInChildren<YotogiSkillLockUnit>();
                YotogiSkillUnit componentInChildren2 = gameObject.GetComponentInChildren<YotogiSkillUnit>();
                componentInChildren2.SetSkillData(this.maid_, skill_list[j].skillData, skill_list[j].maidStatusSkillData, componentInChildren, skill_list[j].conditionDatas);
                componentInChildren2.SetConditionsPanel(childObject3);
                this.skill_unit_list_.Add(componentInChildren2.gameObject);
            }
        }
        */
    }
}
