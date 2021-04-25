using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 메이드 에딧 모드로 들어갈시
    /// </summary>
    class SceneEditPatch
    {
        // SceneEdit

        //private List<SceneEdit.SliderItemSet> m_listSliderItem;

        /// <summary>
        /// 오류나서 제거
        /// 다음 두개 이후에 작동
        /// TBody.BoneMorph_FromProcItem
        /// ExternalSaveData
        /// </summary>
        /// <param name="___m_maid"></param>
        /// <param name="__instance"></param>
        //private void Start()
        //private Maid m_maid;
        [HarmonyPatch(typeof(SceneEdit), "Start")]
        [HarmonyPostfix]
        public static void Start() // Maid ___m_maid,SceneEdit __instance
        {
            Maid ___m_maid = SceneEdit.Instance.maid;
            if (___m_maid == null)
            {
                MyLog.LogError("SceneEdit.Start:null");
                return;
            }
            MyLog.LogMessage("SceneEdit.Start:" + ___m_maid.status.charaName.name1 + " , " + ___m_maid.status.charaName.name2);

        }

        //[HarmonyPatch(typeof(SceneEdit.MenuItemSet), "Start")]
        //[HarmonyPostfix]
        //public static void StartPos() // Maid ___m_maid,SceneEdit __instance
        //{            
        //    Maid ___m_maid = SceneEdit.Instance.maid;
        //    if (___m_maid == null)
        //    {
        //        MyLog.LogErrorS("SceneEdit.Start:null");
        //        return;
        //    }
        //    MyLog.LogMessageS("SceneEdit.Start:" + ___m_maid.status.charaName.name1 + " , " + ___m_maid.status.charaName.name2);
        //    MaidStatusUtill.SetMaidStatus(___m_maid);
        //}

        // public void OnCharaLoadCompleted()
        [HarmonyPostfix, HarmonyPatch(typeof(SceneEdit), "OnCharaLoadCompleted")]
        public static void OnCharaLoadCompleted()
        {
            MyLog.LogMessage("SceneEdit.OnCharaLoadCompleted"
                , EasyUtill._GP01FBFaceEyeRandomOnOff.Value
                , EasyUtill._SetMaidStatusOnOff.Value
                );

            Maid maid = GameMain.Instance.CharacterMgr.GetMaid(0);

            if (EasyUtill._GP01FBFaceEyeRandomOnOff.Value)
                EasyUtill.GP01FBFaceEyeRandom(1, maid);

            if (EasyUtill._SetMaidStatusOnOff.Value)
                CheatUtill.SetMaidStatus(maid);

            PersonalUtill.SetPersonalRandom(maid);
            PresetUtill.RandPreset(PresetUtill.ListType.All, PresetUtill.PresetType.All, maid);
        }

        // private void OnCompleteFadeIn()
        [HarmonyPostfix, HarmonyPatch(typeof(SceneEdit), "OnCompleteFadeIn")]
        public static void OnCompleteFadeIn()
        {
            MyLog.LogMessage("SceneEdit.OnCompleteFadeIn");
        }

        // private void OnEndScene()
        [HarmonyPostfix, HarmonyPatch(typeof(SceneEdit), "OnEndScene")]
        public static void OnEndScene()
        {
            MyLog.LogMessage("SceneEdit.OnEndScene");
        }
    }
}
