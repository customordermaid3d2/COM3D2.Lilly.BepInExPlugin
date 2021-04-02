using HarmonyLib;
using kt.ik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 안뜨는거 같음
    /// </summary>
    class FullBodyIKMgrPatch
    {
        // FullBodyIKMgr

        [HarmonyPrefix, HarmonyPatch(typeof(FullBodyIKMgr), "IKAttach", typeof(string), typeof(IKAttachParam))]
        // public void IKAttach(FullBodyIKMgr.IKEffectorType effector_type, IKAttachParam param)
        // public void IKAttach(string ik_name, IKAttachParam param)
        public static void IKAttach(string ik_name, IKAttachParam param)
        {
            MyLog.Log("IKAttach"
                + ik_name
                , param.targetChara.status.fullNameJpStyle
                , param.slotName
                , param.targetBoneName
                , param.attachIKName
                , param.attachPointName
                , param.attachType
                , param.axisBoneName
                , param.odoguName
                , param.odoguTgtName
                );
        }

    }
}
