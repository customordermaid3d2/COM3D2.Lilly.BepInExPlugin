using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    /// <summary>
    /// BoneMorph_
    /// </summary>
    class BoneMorph_Patch
    {
        /// <summary>
        /// 157 임시조치용
        /// </summary>
        /// <param name="t"></param>
        /// <param name="f_mpn"></param>
        /// <param name="f_slot"></param>
        /// <param name="tbskin"></param>
        // public void InitBoneMorphEdit(Transform t, MPN f_mpn, TBody.SlotID f_slot, TBodySkin tbskin)
        //[HarmonyPatch(typeof(BoneMorph_), "InitBoneMorphEdit",typeof(Transform),typeof(MPN),typeof(TBody.SlotID),typeof(TBodySkin)), HarmonyPrefix]
        public static void InitBoneMorphEdit(Transform t, MPN f_mpn, TBody.SlotID f_slot, TBodySkin tbskin)
        {
            MyLog.LogMessage(
                "InitBoneMorphEdit"
                // , t.name// 의미 없음
                , f_mpn // MPN 가 SlotID 보다 많음
                , f_slot
                , tbskin.obj_tr.name
                );
            //tbskin.obj_tr = t;
            /*
            if (this.IsCrcBody)
            {
                this.bonemorph = new TMorphBone();
                this.bonemorph.Init();
            }
            else
            {
                this.bonemorph = new BoneMorph_();
                this.bonemorph.Init();
            }
            */
        }
    }
}
