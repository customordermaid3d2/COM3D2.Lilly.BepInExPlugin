using COM3D2.Lilly.Plugin.ToolPatch;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Utill
{
    class CheatUtill
    {
        public static void SetMaidAll(Maid maid)
        {
            MyLog.LogMessage(
                "CheatUtill.SetMaidAll"
                );
            StatusUtill.SetMaidStatus(maid);
            SkillClassUtill.SetMaidYotogiClass(maid);
            SkillClassUtill.SetMaidJobClass(maid);
            SkillClassUtill.SetMaidSkill(maid);
        }

        internal static void SetHeroineType(HeroineType transfer)
        {
            MyLog.LogMessage(
            "CheatUtill.SetHeroineType"
            );
            MaidManagementMainPatch.___select_maid_.status.heroineType = transfer;
        }
    }
}
