using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using wf;
using Yotogis;

namespace COM3D2.Lilly.Plugin.PatchInfo
{

#if ScheduleUtill
    class YotogiOldSkillContainerViewerPatch
    {
        // YotogiOldSkillContainerViewer

        public static YotogiOldSkillContainerViewer instance;
        public static Maid maid;

        [HarmonyPatch(typeof(YotogiOldSkillContainerViewer), MethodType.Constructor, new Type[] { typeof(GameObject), typeof(MonoBehaviour) })]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void Constructor(GameObject root_obj, MonoBehaviour parent, YotogiOldSkillContainerViewer __instance)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiOldSkillContainerViewer.Constructor"
                    , root_obj.name
                    , parent.name
                    );
            }

            instance = __instance;
        }

        [HarmonyPatch(typeof(YotogiOldSkillContainerViewer), "Init", MethodType.Normal)]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void Init(Maid maid)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiOldSkillContainerViewer.Init"
                    , MyUtill.GetMaidFullName(maid)
                    );
            }
            YotogiOldSkillContainerViewerPatch.maid = maid;
        }

        public static List<Skill.Old.Data> skillList = new List<Skill.Old.Data>();

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
                YotogiOld.Stage key = YotogiOld.Stage.プレイル\u30FCム;
                if (!string.IsNullOrEmpty(YotogiOldStageSelectManager.StageName))
                {
                    try
                    {
                        key = (YotogiOld.Stage)Enum.Parse(typeof(YotogiOld.Stage), YotogiOldStageSelectManager.StageName);
                    }
                    catch
                    {
                        MyLog.LogError("Yotogi.Stage enum convert error.\n" + YotogiOldStageSelectManager.StageName, false);
                        return;
                    }
                }

                YotogiOld.StageData setting_stage_data_ = YotogiOld.stage_data_list[key];

                ReadOnlySortedDictionary<int, YotogiSkillData> oldDatas = maid.status.yotogiSkill.oldDatas;
                foreach (YotogiSkillData yotogiSkillData in oldDatas.GetValueArray())
                {
                    Skill.Old.Data oldData = yotogiSkillData.oldData;
                    if (oldData.IsExecStage(setting_stage_data_.stage) && oldData.IsExecMaid(maid.status))
                    {
                        MyLog.LogMessage("AddSkillOld"
                            , oldData.category
                            , oldData.id
                            , oldData.name
                            );

                        skillList.Add(oldData);
                    }
                }
            }

            if (skillList.Count > 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    try
                    {
                        instance.AddSkill(skillList.ElementAt(UnityEngine.Random.Range(0, skillList.Count)));
                    }
                    catch (Exception e)
                    {
                        MyLog.LogError(e.ToString());
                    }
                }
            }
        }
    } 
#endif
}
