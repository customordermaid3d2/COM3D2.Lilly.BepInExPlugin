using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    public class PersonalUtill
    {
        public static List<Personal.Data> personalDataAll;
        public static List<Personal.Data> personalDataEnable;

        public static List<Personal.Data> GetPersonalData(bool onlyEnabled)
        {
            if (personalDataAll==null)
            {
                personalDataAll = Personal.GetAllDatas(false);
                personalDataEnable = Personal.GetAllDatas(true);
            }
            if (onlyEnabled)
            {
                return personalDataEnable;
            }
                return personalDataAll;
        }

        public static void SetPersonalRandom(Maid maid)
        {
            MyLog.LogMessage(
                "MaidManagementMain.Employment"
                ,MyUtill.GetMaidFullName(maid)
            );
            maid.status.SetPersonal(GetPersonalData(true)[UnityEngine.Random.Range(0, GetPersonalData(true).Count)]);
        }

    }
}
