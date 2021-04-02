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
                MyLog.LogMessage(s+ ": " + maidProp.idx.ToString().PadLeft(3) , maidProp.strFileName);
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
