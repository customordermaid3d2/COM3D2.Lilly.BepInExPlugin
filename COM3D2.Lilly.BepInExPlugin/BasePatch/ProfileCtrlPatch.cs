using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 성격 정보
    /// </summary>
    class ProfileCtrlPatch
    {
        // ProfileCtrl

        public static Dictionary<string, Personal.Data> m_dicPersonal;
        public static List<Personal.Data> personals;

        // private void Start()
        [HarmonyPostfix, HarmonyPatch(typeof(ProfileCtrl), "Init")]
        public static void Init(ProfileCtrl __instance, Dictionary<string, Personal.Data> ___m_dicPersonal)
        {
            MyLog.LogMessage("ProfileCtrl.Init", ___m_dicPersonal.Count);
            m_dicPersonal = ___m_dicPersonal;
            personals = ___m_dicPersonal.Values.ToList();
            foreach (KeyValuePair<string, Personal.Data> keyValuePair in ProfileCtrlPatch.m_dicPersonal)
            {
                MyLog.LogMessage("ProfileCtrl.Init", keyValuePair.Key, keyValuePair.Value.id);
            }
        }


    }
}
