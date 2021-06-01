using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Yotogis;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    /// <summary>
    /// 캐릭터 설정 관련
    /// </summary>
    public static class CharacterMgrPatch // 이름은 마음대로 지어도 되긴 한데 나같은 경우 정리를 위해서 해킹 대상 클래스 이름에다가 접미사를 붇임
    {
        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
            "CharacterMgrPatch"
            , "SetActive"
            , "Deactivate"
            , "PresetSave"
            , "PresetSet"
            );

        // https://github.com/BepInEx/HarmonyX/wiki/Prefix-changes
        // https://github.com/BepInEx/HarmonyX/wiki/Patch-parameters
        // 
        // 매개 변수의 순서는 중요하지 않지만 이름은 중요합니다. 원본 메소드의 이름과 똑같아야함
        // 모든 매개 변수를 전달할 필요는 없습니다.

        // ------------------------------------

        // public CharacterMgr.Preset PresetLoad(BinaryReader brRead, string f_strFileName)

        // 정상
        //[HarmonyPatch(typeof(CharacterMgr), "PresetLoad", new Type[]
        //{
        //            typeof(string)
        //})]
        //[HarmonyPrefix]
        //public static void PresetLoadPrefix(CharacterMgr __instance, string f_strFileName)
        //{
        //    //MyLog.Log("PresetLoadPrefix():" + f_strFileName);
        //}

        // ------------------------------------

        public static Maid[] m_gcActiveMaid;
        public static string[] namesMaid = new string[18];

        [HarmonyPatch(typeof(CharacterMgr), MethodType.Constructor)]
        [HarmonyPostfix]
        public static void CharacterMgrConstructor(Maid[] ___m_gcActiveMaid)
        {
            m_gcActiveMaid = ___m_gcActiveMaid;
            MyLog.LogMessage("CharacterMgr.Constructor ");
        }

        /// <summary>
        /// 프리셋 선택해서 메이드에게 입힐때 작동
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="f_maid"></param>
        /// <param name="f_prest"></param>
        //[HarmonyPatch(typeof(CharacterMgr), "PresetSet")]
        //[HarmonyPrefix]
        public static void PresetSet(CharacterMgr __instance, Maid f_maid, CharacterMgr.Preset f_prest)
        {
            MyLog.LogMessage("PresetSetPretfix1.f_prest.strFileName:"
                + f_prest.strFileName
                );
            //MaidProp[] array = PresetUtill.getMaidProp(f_prest);
            //foreach (MaidProp maidProp in array)
            //{
            //    MyLog.LogMessageS("PresetSetPretfix1: " + maidProp.idx + " , " + maidProp.strFileName);
            //}
        }

        // private void SetActive(Maid f_maid, int f_nActiveSlotNo, bool f_bMan)
        [HarmonyPatch(typeof(CharacterMgr), "SetActive")]
        [HarmonyPostfix]
        public static void SetActive(Maid f_maid, int f_nActiveSlotNo, bool f_bMan)
        {
            if (configEntryUtill["SetActive", false])
                MyLog.LogMessage("CharacterMgr.SetActive", f_nActiveSlotNo, MyUtill.GetMaidFullName(f_maid));
            if (!f_bMan)
                namesMaid[f_nActiveSlotNo] = MyUtill.GetMaidFullName(f_maid);
        }

        // private void SetActive(Maid f_maid, int f_nActiveSlotNo, bool f_bMan)
        [HarmonyPatch(typeof(CharacterMgr), "Deactivate")]
        [HarmonyPrefix]
        public static void Deactivate(int f_nActiveSlotNo, bool f_bMan)
        {
            if (configEntryUtill["Deactivate",false])
                MyLog.LogMessage("CharacterMgr.Deactivate", f_nActiveSlotNo);// HarmonyPrefix로 했는데도 m_gcActiveMaid 에선 제거되있네
            if (!f_bMan)
                namesMaid[f_nActiveSlotNo] = string.Empty;

        }

        // public CharacterMgr.Preset PresetSave(Maid f_maid, CharacterMgr.PresetType f_type)
        [HarmonyPatch(typeof(CharacterMgr), "PresetSave")]
        [HarmonyPostfix]
        public static void PresetSave(Maid f_maid, CharacterMgr.PresetType f_type, CharacterMgr.Preset __result)
        {
            if (configEntryUtill["PresetSave"])
                MyLog.LogMessage("CharacterMgr.PresetSavePost0: " + MyUtill.GetMaidFullName(f_maid) + " , " + __result.strFileName + " , " + __result.ePreType);
        }

        // public void PresetSet(Maid f_maid, CharacterMgr.Preset f_prest, bool forceBody = false) // 157
        // public void PresetSet(Maid f_maid, CharacterMgr.Preset f_prest) // 155
        // 테스팅 완료
        [HarmonyPatch(typeof(CharacterMgr), "PresetSet", new Type[] { typeof(Maid), typeof(CharacterMgr.Preset) })]
        [HarmonyPostfix]
        public static void PresetSet(Maid f_maid, CharacterMgr.Preset f_prest)
        {



            if (configEntryUtill["PresetSet",false])
            {
                return;
            }
            MyLog.LogMessage("CharacterMgr.PresetSet.f_prest.strFileName:" + f_prest.strFileName + " , " + f_prest.ePreType);
            MaidProp[] array = MyUtill.getMaidProp(f_prest);
            foreach (MaidProp maidProp in array)
            {
                if (maidProp.strFileName.Length > 0)// 값 없는거 출력 방지
                {
                    MyLog.LogMessage("PresetSetPostfix2: " + maidProp.idx.ToString().PadLeft(3) + " , " + maidProp.strFileName);
                }
            }
        }

        // ------------------------------------

        // public void SetActiveMaid(Maid f_maid, int f_nActiveSlotNo)

        //[HarmonyPatch(typeof(CharacterMgr), "SetActiveMaid", new Type[]{ typeof(Maid) ,typeof(int)        })]
        // [HarmonyPostfix]
        public static void SetActiveMaidPost0(Maid f_maid, int f_nActiveSlotNo)
        {
            if (!Lilly.isLogOn)
            {
                return;
            }
            MyLog.LogMessage("CharacterMgr.SetActiveMaidPost0: " + f_maid.status.firstName + " , " + f_maid.status.lastName);
        }


    }
}
