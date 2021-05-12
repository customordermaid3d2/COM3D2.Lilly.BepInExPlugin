using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin
{


    /// <summary>
    /// 메이드 설정 관련
    /// </summary>
    public static class MaidPatch
    {
        // https://github.com/BepInEx/HarmonyX/wiki/Prefix-changes
        // https://github.com/BepInEx/HarmonyX/wiki/Patch-parameters
        // 
        // 매개 변수의 순서는 중요하지 않지만 이름은 중요합니다.
        // 모든 매개 변수를 전달할 필요는 없습니다.

        // public static MaidProp[] m_aryMaidProp;

        public static ConfigEntryUtill configEntryUtill = new ConfigEntryUtill(
        "MaidPatch"
        , "SetProp"
        , "DelProp"
        );

        /// <summary>
        /// 메이드 만들어질때
        /// </summary>
        /// <param name="f_strTypeName"></param>
        /// <param name="f_bMan"></param>
        /// <param name="___m_aryMaidProp"></param>
        //[HarmonyPatch(typeof(Maid), "Initialize", new Type[] { typeof(MPN), typeof(int), typeof(bool) })]
        //[HarmonyPostfix]
        // public void Initialize(string f_strTypeName, bool f_bMan)
        public static void Initialize(string f_strTypeName, bool f_bMan, MaidProp[] ___m_aryMaidProp)
        {
        }

        //[HarmonyPatch(typeof(Maid), "SetProp", new Type[] { typeof(MaidProp) })]
        //[HarmonyPostfix]
        // public void SetProp(MaidProp mps) 
        public static void SetProp(MaidProp mps)
        {
            print("SetProp1",mps);
        }

        // public void SetProp(string tag, int val, bool f_bTemp = false)
        // public void SetProp(MPN idx, int val, bool f_bTemp = false)
        [HarmonyPatch(typeof(Maid), "SetProp", new Type[] { typeof(MPN), typeof(int), typeof(bool) })]
        [HarmonyPostfix]
        public static void SetProp(Maid __instance, MPN idx, int val, bool f_bTemp, MaidProp[] ___m_aryMaidProp)
        {
            if(configEntryUtill["SetProp"])
            if (__instance.Visible)
                print("SetProp2",___m_aryMaidProp[(int)idx]);
        }

        // public void SetProp(string tag, string filename, int f_nFileNameRID, bool f_bTemp = false, bool f_bNoScale = false)
        // public void SetProp(MPN idx, string filename, int f_nFileNameRID, bool f_bTemp = false, bool f_bNoScale = false)
        // private void SetProp(MaidProp mp, string filename, int f_nFileNameRID, bool f_bTemp, bool f_bNoScale = false)
        [HarmonyPatch(typeof(Maid), "SetProp", new Type[] { typeof(MaidProp), typeof(string), typeof(int), typeof(bool) , typeof(bool) })]
        [HarmonyPostfix]
        public static void SetProp(Maid __instance, MaidProp mp, string filename, int f_nFileNameRID, bool f_bTemp, bool f_bNoScale )
        {
            if (configEntryUtill["SetProp"])
                if (__instance.Visible)
            {
                print("SetProp3",mp);
            }
        }

        // public void SetSubProp(MPN idx, int subno, string filename, int f_nFileNameRID = 0)

        [HarmonyPatch(typeof(Maid), "DelProp")]
        [HarmonyPrefix]
        public static void DelProp(Maid __instance, MPN idx, bool f_bTemp, MaidProp[] ___m_aryMaidProp)
        {
            if (configEntryUtill["DelProp"])
                if (__instance.Visible)
                print("DelProp",___m_aryMaidProp[(int)idx]);
        }

        public static void print(string s,MaidProp maidProp)
        {
            if (maidProp==null)
            {
                MyLog.LogMessage(s);
                return;
            }
            if (maidProp.strFileName.Length !=0)
            {
                MyLog.LogMessage(s+ ": " + (MPN)maidProp.idx , maidProp.strFileName);
            }
        }

        // public void SetUpModel(string f_strPresetMenuFileName)

        /*
        public FullBodyIKMgr fullBodyIK
        {
            get
            {
                return this.body0.fullBodyIK;
            }
        }
        */

    }
}


/*
 public static List<MaidProp> CreateInitMaidPropList()
	{
		return new List<MaidProp>
		{
			Maid.CreateProp(0, int.MaxValue, 0, MPN.null_mpn, 0),
			Maid.CreateProp(0, 130, 10, MPN.MuneL, 1),
			Maid.CreateProp(0, 100, 0, MPN.MuneS, 1),
			Maid.CreateProp(0, 130, 10, MPN.MuneTare, 1),
			Maid.CreateProp(0, 100, 40, MPN.RegFat, 1),
			Maid.CreateProp(0, 100, 20, MPN.ArmL, 1),
			Maid.CreateProp(0, 100, 20, MPN.Hara, 1),
			Maid.CreateProp(0, 100, 40, MPN.RegMeet, 1),
			Maid.CreateProp(20, 80, 50, MPN.KubiScl, 2),
			Maid.CreateProp(0, 100, 50, MPN.UdeScl, 2),
			Maid.CreateProp(0, 100, 50, MPN.EyeScl, 2),
			Maid.CreateProp(0, 100, 50, MPN.EyeSclX, 2),
			Maid.CreateProp(0, 100, 50, MPN.EyeSclY, 2),
			Maid.CreateProp(0, 100, 50, MPN.EyePosX, 2),
			Maid.CreateProp(0, 100, 50, MPN.EyePosY, 2),
			Maid.CreateProp(0, 100, 0, MPN.EyeClose, 2),
			Maid.CreateProp(0, 100, 50, MPN.EyeBallPosX, 2),
			Maid.CreateProp(0, 100, 50, MPN.EyeBallPosY, 2),
			Maid.CreateProp(0, 100, 50, MPN.EyeBallSclX, 2),
			Maid.CreateProp(0, 100, 50, MPN.EyeBallSclY, 2),
			Maid.CreateProp(0, 1, 0, MPN.EarNone, 2),
			Maid.CreateProp(0, 100, 0, MPN.EarElf, 2),
			Maid.CreateProp(0, 100, 50, MPN.EarRot, 2),
			Maid.CreateProp(0, 100, 50, MPN.EarScl, 2),
			Maid.CreateProp(0, 100, 50, MPN.NosePos, 2),
			Maid.CreateProp(0, 100, 50, MPN.NoseScl, 2),
			Maid.CreateProp(0, 100, 0, MPN.FaceShape, 2),
			Maid.CreateProp(0, 100, 0, MPN.FaceShapeSlim, 2),
			Maid.CreateProp(0, 100, 50, MPN.MayuShapeIn, 2),
			Maid.CreateProp(0, 100, 50, MPN.MayuShapeOut, 2),
			Maid.CreateProp(0, 100, 50, MPN.MayuX, 2),
			Maid.CreateProp(0, 100, 50, MPN.MayuY, 2),
			Maid.CreateProp(0, 100, 50, MPN.MayuRot, 2),
			Maid.CreateProp(0, 100, 50, MPN.HeadX, 2),
			Maid.CreateProp(0, 100, 50, MPN.HeadY, 2),
			Maid.CreateProp(20, 80, 50, MPN.DouPer, 2),
			Maid.CreateProp(20, 80, 50, MPN.sintyou, 2),
			Maid.CreateProp(0, 100, 50, MPN.koshi, 2),
			Maid.CreateProp(0, 100, 50, MPN.kata, 2),
			Maid.CreateProp(0, 100, 50, MPN.west, 2),
			Maid.CreateProp(0, 100, 10, MPN.MuneUpDown, 2),
			Maid.CreateProp(0, 100, 40, MPN.MuneYori, 2),
			Maid.CreateProp(0, 100, 50, MPN.MuneYawaraka, 2),
			Maid.CreateProp(0, 100, 50, MPN.MayuThick, 2),
			Maid.CreateProp(0, 100, 50, MPN.MayuLong, 2),
			Maid.CreateProp(0, 100, 50, MPN.Yorime, 2),
			Maid.CreateProp(0, 100, 50, MPN.MabutaUpIn, 1),
			Maid.CreateProp(0, 100, 50, MPN.MabutaUpIn2, 1),
			Maid.CreateProp(0, 100, 50, MPN.MabutaUpMiddle, 1),
			Maid.CreateProp(0, 100, 50, MPN.MabutaUpOut, 1),
			Maid.CreateProp(0, 100, 50, MPN.MabutaUpOut2, 1),
			Maid.CreateProp(0, 100, 50, MPN.MabutaLowIn, 1),
			Maid.CreateProp(0, 100, 50, MPN.MabutaLowUpMiddle, 1),
			Maid.CreateProp(0, 100, 50, MPN.MabutaLowUpOut, 1),
			Maid.CreateProp(string.Empty, MPN.body, 3),
			Maid.CreateProp(string.Empty, MPN.head, 3),
			Maid.CreateProp(string.Empty, MPN.hairf, 3),
			Maid.CreateProp(string.Empty, MPN.hairr, 3),
			Maid.CreateProp(string.Empty, MPN.hairt, 3),
			Maid.CreateProp(string.Empty, MPN.hairs, 3),
			Maid.CreateProp(string.Empty, MPN.wear, 3),
			Maid.CreateProp(string.Empty, MPN.skirt, 3),
			Maid.CreateProp(string.Empty, MPN.mizugi, 3),
			Maid.CreateProp(string.Empty, MPN.bra, 3),
			Maid.CreateProp(string.Empty, MPN.panz, 3),
			Maid.CreateProp(string.Empty, MPN.stkg, 3),
			Maid.CreateProp(string.Empty, MPN.shoes, 3),
			Maid.CreateProp(string.Empty, MPN.headset, 3),
			Maid.CreateProp(string.Empty, MPN.glove, 3),
			Maid.CreateProp(string.Empty, MPN.acchead, 3),
			Maid.CreateProp(string.Empty, MPN.hairaho, 3),
			Maid.CreateProp(string.Empty, MPN.accha, 3),
			Maid.CreateProp(string.Empty, MPN.acchana, 3),
			Maid.CreateProp(string.Empty, MPN.acckamisub, 3),
			Maid.CreateProp(string.Empty, MPN.acckami, 3),
			Maid.CreateProp(string.Empty, MPN.accmimi, 3),
			Maid.CreateProp(string.Empty, MPN.accnip, 3),
			Maid.CreateProp(string.Empty, MPN.acckubi, 3),
			Maid.CreateProp(string.Empty, MPN.acckubiwa, 3),
			Maid.CreateProp(string.Empty, MPN.accheso, 3),
			Maid.CreateProp(string.Empty, MPN.accude, 3),
			Maid.CreateProp(string.Empty, MPN.accashi, 3),
			Maid.CreateProp(string.Empty, MPN.accsenaka, 3),
			Maid.CreateProp(string.Empty, MPN.accshippo, 3),
			Maid.CreateProp(string.Empty, MPN.accanl, 3),
			Maid.CreateProp(string.Empty, MPN.accvag, 3),
			Maid.CreateProp(string.Empty, MPN.megane, 3),
			Maid.CreateProp(string.Empty, MPN.accxxx, 3),
			Maid.CreateProp(string.Empty, MPN.handitem, 3),
			Maid.CreateProp(string.Empty, MPN.acchat, 3),
			Maid.CreateProp(string.Empty, MPN.haircolor, 3),
			Maid.CreateProp(string.Empty, MPN.skin, 3),
			Maid.CreateProp(string.Empty, MPN.acctatoo, 3),
			Maid.CreateProp(string.Empty, MPN.accnail, 3),
			Maid.CreateProp(string.Empty, MPN.underhair, 3),
			Maid.CreateProp(string.Empty, MPN.hokuro, 3),
			Maid.CreateProp(string.Empty, MPN.mayu, 3),
			Maid.CreateProp(string.Empty, MPN.lip, 3),
			Maid.CreateProp(string.Empty, MPN.eye, 3),
			Maid.CreateProp(string.Empty, MPN.eye_hi, 3),
			Maid.CreateProp(string.Empty, MPN.eye_hi_r, 3),
			Maid.CreateProp(string.Empty, MPN.chikubi, 3),
			Maid.CreateProp(string.Empty, MPN.chikubicolor, 3),
			Maid.CreateProp(string.Empty, MPN.eyewhite, 3),
			Maid.CreateProp(string.Empty, MPN.nose, 3),
			Maid.CreateProp(string.Empty, MPN.facegloss, 3),
			Maid.CreateProp(string.Empty, MPN.matsuge_up, 3),
			Maid.CreateProp(string.Empty, MPN.matsuge_low, 3),
			Maid.CreateProp(string.Empty, MPN.futae, 3),
			Maid.CreateProp(string.Empty, MPN.moza, 3),
			Maid.CreateProp(string.Empty, MPN.onepiece, 3),
			Maid.CreateProp(string.Empty, MPN.set_maidwear, 3),
			Maid.CreateProp(string.Empty, MPN.set_mywear, 3),
			Maid.CreateProp(string.Empty, MPN.set_underwear, 3),
			Maid.CreateProp(string.Empty, MPN.set_body, 3),
			Maid.CreateProp(string.Empty, MPN.folder_eye, 3),
			Maid.CreateProp(string.Empty, MPN.folder_mayu, 3),
			Maid.CreateProp(string.Empty, MPN.folder_underhair, 3),
			Maid.CreateProp(string.Empty, MPN.folder_skin, 3),
			Maid.CreateProp(string.Empty, MPN.folder_eyewhite, 3),
			Maid.CreateProp(string.Empty, MPN.folder_matsuge_up, 3),
			Maid.CreateProp(string.Empty, MPN.folder_matsuge_low, 3),
			Maid.CreateProp(string.Empty, MPN.folder_futae, 3),
			Maid.CreateProp(string.Empty, MPN.kousoku_upper, 3),
			Maid.CreateProp(string.Empty, MPN.kousoku_lower, 3),
			Maid.CreateProp(string.Empty, MPN.seieki_naka, 3),
			Maid.CreateProp(string.Empty, MPN.seieki_hara, 3),
			Maid.CreateProp(string.Empty, MPN.seieki_face, 3),
			Maid.CreateProp(string.Empty, MPN.seieki_mune, 3),
			Maid.CreateProp(string.Empty, MPN.seieki_hip, 3),
			Maid.CreateProp(string.Empty, MPN.seieki_ude, 3),
			Maid.CreateProp(string.Empty, MPN.seieki_ashi, 3)
		};
	}
 
 */