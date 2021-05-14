using COM3D2.Lilly.Plugin.MyGUI;
using COM3D2.Lilly.Plugin.ToolPatch;
using COM3D2.Lilly.Plugin.Utill;
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
    class SceneEditPatch//: AwakeUtill
    {
        // SceneEdit        

        //public static List<SceneEdit.SliderItemSet> m_listSliderItem;
        //public static Type sliderItemSet;
        //public static SceneEdit Instance;

        public SceneEditPatch(){
            //Lilly.actionsAwake += Awake;
            }

        public  void Awake()
        {
            /*
            try
            {
                sliderItemSet = AccessTools.TypeByName("SliderItemSet");
                sliderItemSet.GetProperty("m_listSliderItem");
            }
            catch (Exception e)
            {
                MyLog.LogError("SceneEdit.Awake", e.ToString());
            }
            */
        }

        [HarmonyPostfix, HarmonyPatch(typeof(SceneEdit), MethodType.Constructor)]
        public static void SceneEditCtor()//SceneEdit __Instance  // Constructor에서는 안먹힘 생성도 안됐기때문?
        {
            MyLog.LogMessage("SceneEdit.Constructor");
            //Instance = __Instance;
            //Instance = SceneEdit.Instance;
        }

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
        
        [HarmonyPatch(typeof(SceneEdit), "OnCompleteFadeIn")]
        [HarmonyPostfix]
        public static void OnCompleteFadeIn() // Maid ___m_maid,SceneEdit __instance
        {
            MyLog.LogMessage("SceneEdit.OnCompleteFadeIn", newMaid);
            newMaidSetting();
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

        /// <summary>
        /// 여기다 프리셋 로드 사용시 무한 루프 걸려버림
        /// </summary>
        // public void OnCharaLoadCompleted()
        [HarmonyPostfix, HarmonyPatch(typeof(SceneEdit), "OnCharaLoadCompleted")]
        public static void OnCharaLoadCompleted()
        {
            MyLog.LogMessage("SceneEdit.OnCharaLoadCompleted"
               //, EasyUtill._GP01FBFaceEyeRandomOnOff.Value
               //, EasyUtill._SetMaidStatusOnOff.Value
                );

        }

        // private string m_strScriptArg;
        [HarmonyPrefix, HarmonyPatch(typeof(SceneEdit), "OnEndScene")]
        public static void OnEndScene(string ___m_strScriptArg, Maid ___m_maid)
        {
            MyLog.LogMessage("SceneEdit.OnEndScene"
               , ___m_strScriptArg
            );
            if (MaidEditGui.newMaid.Value)
            {
                GameMain.Instance.CMSystem.SetTmpGenericFlag("新規雇用メイド", 1);
            }
            else if (MaidEditGui.movMaid.Value)
            {
                GameMain.Instance.CMSystem.SetTmpGenericFlag("移籍メイド", 1);
            }
        }

        public static bool newMaid = false;

        public static void newMaidSetting()
        {
            if (!newMaid)
            {
                return;
            }
            Maid maid = GameMain.Instance.CharacterMgr.GetMaid(0);

            PersonalUtill.SetPersonalRandom(maid);

            if (MaidEditGui._SetMaidStatusOnOff.Value)
                CheatUtill.SetMaidAll(maid);

            PresetUtill.RandPreset(maid);

            if (MaidEditGui._GP01FBFaceEyeRandomOnOff.Value)
            {
                try
                {
                    maid.SetProp(MPN.head, "face001_fb_i_.menu", "face001_fb_i_.menu".GetHashCode(), false, false);
                    MaidEditGui.GP01FBFaceEyeRandom(1, maid);
                }
                catch (Exception e)
                {
                    MyLog.LogMessage(
                        "newMaidSetting"
                        , e.ToString()
                    );
                }

            }
            newMaid = false;
        }

        // private void OnEndScene()

    }
}
