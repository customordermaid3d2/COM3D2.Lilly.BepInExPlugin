using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    /// <summary>
    /// 바디 파라미터값 출력 관련
    /// </summary>
    class TBodyPatch// : AwakeUtill
    {
        //TBody
        public static ConfigEntryUtill config = ConfigEntryUtill.Create(
            "TBodyPatch"
            , "BoneMorph_FromProcItem"
            , "VertexMorph_FromProcItem"
            , "LoadBody_R"
        );

        public void init()
        {
            MyLog.LogDebug("TBodyPatch.init");
        }

        public void Awake()
        {

        }

        [HarmonyPostfix, HarmonyPatch(typeof(TBody), "BoneMorph_FromProcItem")]
        public static void BoneMorph_FromProcItem(string tag, float f)
        {
            if (config["BoneMorph_FromProcItem",false])
                MyLog.LogMessage("TBody.BoneMorph_FromProcItem"
                    , tag
                    , f
                    );
        }
        [HarmonyPostfix, HarmonyPatch(typeof(TBody), "VertexMorph_FromProcItem")]
        public static void VertexMorph_FromProcItem(string tag, float f)
        {
            if (config["VertexMorph_FromProcItem", false])
                MyLog.LogMessage("TBody.VertexMorph_FromProcItem"
                , tag
                , f
                );
        }

        [HarmonyPostfix, HarmonyPatch(typeof(TBody), "LoadBody_R")]
        public static void LoadBody_R(string f_strModelFileName, Maid f_maid)
        {
            if (config["LoadBody_R", false])
                MyLog.LogMessage("TBody.LoadBody_R"
                , f_strModelFileName
                , MyUtill.GetMaidFullName(f_maid)
                );

        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(TBody), "CrossFade",typeof(string),typeof(AFileSystemBase),typeof(bool),typeof(bool),typeof(bool),typeof(float),typeof(float))]
        public static void CrossFade(string filename, AFileSystemBase fileSystem, bool additive = false, bool loop = false, bool boAddQue = false, float fade = 0.5f, float weight = 1f)
        {
            if (config["CrossFade", false])
                MyLog.LogMessage("TBody.CrossFade1"
                , filename
                , additive
                , loop
                , boAddQue
                , fade
                , weight
                );
        }
                
        [HarmonyPostfix, HarmonyPatch(typeof(TBody), "CrossFade",typeof(string),typeof(byte[]),typeof(bool),typeof(bool),typeof(bool),typeof(float),typeof(float))]
        public static void CrossFade(string tag, byte[] byte_data, bool additive = false, bool loop = false, bool boAddQue = false, float fade = 0.5f, float weight = 1f)
        {
            if (config["CrossFade", false])
                MyLog.LogMessage("TBody.CrossFade2"
                , tag
                , additive
                , loop
                , boAddQue
                , fade
                , weight
                );
        }

#if COM3D2_157
        [HarmonyPostfix,HarmonyPatch(typeof(TBody), "UpdateMyBoneMorph")]        
        public static void UpdateMyBoneMorph(string name, float val)
        {
            MyLog.LogMessage("TBody.UpdateMyBoneMorph"
                , name
                , val
                );
        }
#endif

    }
}
