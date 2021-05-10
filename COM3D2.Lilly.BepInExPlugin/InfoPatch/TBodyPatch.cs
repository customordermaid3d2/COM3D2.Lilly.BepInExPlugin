﻿using COM3D2.Lilly.Plugin.Utill;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class TBodyPatch// : AwakeUtill
    {
        //TBody
        public static ConfigEntryUtill config = ConfigEntryUtill.Create(
            "TBodyPatch"
            , "BoneMorph_FromProcItem"
            , "VertexMorph_FromProcItem"
        );

        public  void init()
        {
            MyLog.LogDebug("TBodyPatch.init");
        }

        public  void Awake()
        {
            
        }

        [HarmonyPostfix,HarmonyPatch(typeof(TBody), "BoneMorph_FromProcItem")]        
        public static void BoneMorph_FromProcItem(string tag, float f)
        {
            if (config["BoneMorph_FromProcItem"])
            MyLog.LogMessage("TBody.BoneMorph_FromProcItem"
                , tag
                ,f
                );
        }
        [HarmonyPostfix,HarmonyPatch(typeof(TBody), "VertexMorph_FromProcItem")]        
        public static void VertexMorph_FromProcItem(string tag, float f)
        {
            if (config["VertexMorph_FromProcItem"])
                MyLog.LogMessage("TBody.VertexMorph_FromProcItem"
                ,tag
                ,f
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
