using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Yotogis;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    // 이게 7개 스킬 슬롯에 스킬 등록 해줌
    class YotogiSkillContainerViewerPatch
    {
        // YotogiSkillContainerViewer

        public static YotogiSkillContainerViewer instance;
        public static Maid maid;

        [HarmonyPatch(typeof(YotogiSkillContainerViewer), MethodType.Constructor, new Type[] { typeof(GameObject), typeof(MonoBehaviour) })]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void Constructor(GameObject root_obj, MonoBehaviour parent, YotogiSkillContainerViewer __instance)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillContainerViewer.Constructor"
                    , root_obj.name
                    , parent.name
                    );
            }

            instance = __instance;
        }
        /*

        */
        [HarmonyPatch(typeof(YotogiSkillContainerViewer), "Init", MethodType.Normal)]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void Init(Maid maid)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillContainerViewer.Init"
                    , MyUtill.GetMaidFullName(maid)
                    );
            }
            YotogiSkillContainerViewerPatch.maid = maid;
        }
        /*

        */
        [HarmonyPatch(typeof(YotogiSkillContainerViewer), "AddSkill")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void AddSkill(Skill.Data data, bool lockSkillExp)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillContainerViewer.AddSkill"
                    , data.name
                    , lockSkillExp
                    );
            }
        }

        public static List<Skill.Data> skillList = new List<Skill.Data>();
        //public static List<Skill.Old.Data> skillOldList = new List<Skill.Old.Data>();

        /// <summary>
        /// fail
        /// </summary>
        public static void AddSkill(bool listClear = true)
        {
            if (instance == null)
            {
                MyLog.LogMessage("YotogiSkillContainerViewer.AddSkill instance==null"
                );
                return;
            }          
            
            if (listClear)
            {
                YotogiStage.Data setting_stage_data_;

                if (YotogiStageSelectManager.SelectedStage != null)
                {
                    setting_stage_data_ = YotogiStageSelectManager.SelectedStage;
                }
                else
                {
                    setting_stage_data_ = YotogiStage.GetAllDatas(true)[0];
                }

                foreach (Skill.Data.SpecialConditionType type in Enum.GetValues(typeof(Skill.Data.SpecialConditionType)))
                {
                    //bool enabled = false;
                    Dictionary<int, YotogiSkillListManager.Data> dictionary = YotogiSkillListManager.CreateDatas(maid.status, true, type);
                    foreach (KeyValuePair<int, YotogiSkillListManager.Data> keyValuePair in dictionary)
                    {
                        YotogiSkillListManager.Data value = keyValuePair.Value;
                        if (value.skillData.IsExecStage(setting_stage_data_))
                        {
                            MyLog.LogMessage("AddSkill"
                            , value.skillData.category
                            , value.skillData.id
                            , value.skillData.name
                            );
                            skillList.Add(value.skillData);
                            //skillOldList.Add(value.skillDataOld);
                        }
                    }
                }
            }

            if (skillList.Count > 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    instance.AddSkill(skillList.ElementAt(UnityEngine.Random.Range(0, skillList.Count)), false);
                }
            }
        }

    }
}
