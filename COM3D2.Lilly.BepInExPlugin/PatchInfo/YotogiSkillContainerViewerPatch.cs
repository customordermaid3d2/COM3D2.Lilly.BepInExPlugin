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

        [HarmonyPatch(typeof(YotogiSkillContainerViewer), MethodType.Constructor)]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void Init(GameObject root_obj, MonoBehaviour parent, YotogiSkillContainerViewer __instance)
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

        [HarmonyPatch(typeof(YotogiSkillContainerViewer), "Init")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPrefix]
        public static void Init(Maid maid)
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiSkillContainerViewer.Init"
                    ,  MyUtill.GetMaidFullName(maid)
                    );
            }
            YotogiSkillContainerViewerPatch.maid = maid;
        }
        
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

        public static void AddSkill()
        {
            if (instance==null)
            {
                MyLog.LogMessage("YotogiSkillContainerViewer.AddSkill instance==null"
                );
                return;
            }
            if (DailyMgrPatch.IsLegacy)
            {
                MyLog.LogMessage("YotogiSkillContainerViewer.AddSkill IsLegacy not run"
                );
                return;
            }
            var l = maid.status.yotogiSkill.datas.GetValueArray().Select(x => x.data).ToList();
            for (int i = 0; i < 7; i++)
            {
                instance.AddSkill(l.ElementAt(UnityEngine.Random.Range(0, l.Count)),false);
            }
        }

    }
}
