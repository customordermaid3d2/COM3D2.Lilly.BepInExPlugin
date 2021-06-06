using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yotogis;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class YotogiSkillSelectManagerPatch
    {
        // YotogiSkillSelectManager

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "YotogiSkillSelectManagerPatch"
        );

        public static YotogiSkillSelectManager instance;

        [HarmonyPatch(typeof(YotogiSkillSelectManager), "Awake")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void Awake(YotogiSkillSelectManager __instance)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.Awake");
            }
            instance = __instance;
        }

        /// <summary>
        /// public void CreateSkillButtons(Skill.Data.SpecialConditionType type)
        /// </summary>
        /// <param name="___category_data_array_"></param>
        [HarmonyPatch(typeof(YotogiSkillSelectManager), "CreateSkillButtons")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void CreateSkillButtons(Skill.Data.SpecialConditionType type)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.CreateSkillButtons", type);
            }

        }

        public static bool isAddSkill {
            get => configEntryUtill["AddSkill"];
            set => configEntryUtill["AddSkill"] = value;
        }

        [HarmonyPatch(typeof(YotogiSkillSelectManager), "OnCall")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void OnCall()
        {
            if (configEntryUtill["OnCall"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.OnCall");
            }
            if (isAddSkill)
            {
                YotogiSkillContainerViewerPatch.AddSkill(true);
                //AddSkill();
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
            if (configEntryUtill["OnClickEdit"])
            {
                MyLog.LogMessage("YotogiSkillSelectManager.OnClickEdit");
            }
        }

        [HarmonyPatch(typeof(YotogiSkillSelectManager), "OnClickFromCategoryButton")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void OnClickFromCategoryButton(GameObject obj, Yotogi.Category category)
        {
            if (configEntryUtill["OnClickFromCategoryButton"])
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
            if (configEntryUtill["OnClickFromSkillUnit"])
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
            if (configEntryUtill["CreateNewSkillUnit"])
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

    }
}
