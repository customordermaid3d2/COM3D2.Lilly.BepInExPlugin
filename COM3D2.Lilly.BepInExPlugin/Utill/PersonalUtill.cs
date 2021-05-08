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

        public static List<Personal.Data> GetPersonalData(bool onlyEnabled=true)
        {
            CreateData();
            if (onlyEnabled)
            {
                return personalDataEnable;
            }
            return personalDataAll;
        }

        public static Personal.Data GetPersonalData(int  index, bool onlyEnabled = true)
        {
            CreateData();
            if (onlyEnabled)
            {
                return personalDataEnable[index];
            }
            return personalDataAll[index];
        }

        private static void CreateData()
        {
            if (personalDataAll == null)
            {
                personalDataAll = Personal.GetAllDatas(false);
                personalDataEnable = Personal.GetAllDatas(true);
            }
        }

        public static int SetPersonalRandom(Maid maid)
        {
            int a = UnityEngine.Random.Range(0, GetPersonalData().Count);
            Personal.Data data=GetPersonalData(a);
            maid.status.SetPersonal(data);
            maid.status.firstName = data.uniqueName;
            MyLog.LogMessage(
                "MaidManagementMain.Employment"
                ,MyUtill.GetMaidFullName(maid)
            );
            return a;
        }
        
        public static Personal.Data SetPersonal(Maid maid,int index)
        {            
            Personal.Data data=GetPersonalData(index);
            maid.status.SetPersonal(data);
            maid.status.firstName = data.uniqueName;
            MyLog.LogMessage(
                "MaidManagementMain.Employment"
                ,MyUtill.GetMaidFullName(maid)
            );
            return data;
        }

    }
}
