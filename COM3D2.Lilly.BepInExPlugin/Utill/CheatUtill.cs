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
            StatusUtill.SetMaidStatus(maid);
            SkillClassUtill.SetMaidYotogiClass(maid);
            SkillClassUtill.SetMaidJobClass(maid);
            SkillClassUtill.SetMaidSkill(maid);
        }

    }
}
