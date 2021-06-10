using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
#if ScheduleUtill

    class YotogiOldSkillSelectManagerPatch
    {
        // YotogiOldSkillSelectManager

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "YotogiOldSkillSelectManagerPatch"
        );

        public static bool isAddSkill {
            get => configEntryUtill["AddSkill"];
            set => configEntryUtill["AddSkill"] = value;
        }

        [HarmonyPatch(typeof(YotogiOldSkillSelectManager), "OnCall")]//, new Type[] { typeof(int), typeof(int), typeof(bool) }
        [HarmonyPostfix]
        public static void OnCall()
        {
            //if (configEntryUtill["SetResolution"])
            {
                MyLog.LogMessage("YotogiOldSkillSelectManagerPatch.OnCall");
            }


            if (isAddSkill)
            {
                YotogiOldSkillContainerViewerPatch.AddSkill(true);
                //AddSkill();
            }
        }
    } 
#endif
}
