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
        public static Dictionary<string, AIKCtrl> StrIKCtrlPair;// = new Dictionary<string, AIKCtrl>();

        //[HarmonyPrefix, HarmonyPatch(typeof(FullBodyIKMgr), MethodType.Constructor)]
        public static void FullBodyIKMgrC(Dictionary<string, AIKCtrl> ___StrIKCtrlPair)
        {
            StrIKCtrlPair = ___StrIKCtrlPair;
            MyLog.Log("FullBodyIKMgrC"
            );
        }

        public static void GetStrIKCtrlPairInfo()
        {
            Maid maid = GameMain.Instance.CharacterMgr.GetMaid(0);
            FullBodyIKMgr fullBodyIKMgr= maid.fullBodyIK;
            foreach (var item in fullBodyIKMgr.strIKCtrlPair)
            {
                MyLog.Log("GetStrIKCtrlPairInfo"
                    , item.Key
                    , item.Value.effectorType
                    );
            }

            // GetStrIKCtrlPairInfo , 体全体 , Body
            // GetStrIKCtrlPairInfo , 左肩 , UpperArm_L
            // GetStrIKCtrlPairInfo , 左肘 , Forearm_L
            // GetStrIKCtrlPairInfo , 左手 , Hand_L
            // GetStrIKCtrlPairInfo , 右肩 , UpperArm_R
            // GetStrIKCtrlPairInfo , 右肘 , Forearm_R
            // GetStrIKCtrlPairInfo , 右手 , Hand_R
            // GetStrIKCtrlPairInfo , 左腿 , Thigh_L
            // GetStrIKCtrlPairInfo , 左膝 , Calf_L
            // GetStrIKCtrlPairInfo , 左足 , Foot_L
            // GetStrIKCtrlPairInfo , 右腿 , Thigh_R
            // GetStrIKCtrlPairInfo , 右膝 , Calf_R
            // GetStrIKCtrlPairInfo , 右足 , Foot_R

        }

        //[HarmonyPrefix, HarmonyPatch(typeof(FullBodyIKMgr), "IKAttach", typeof(string), typeof(IKAttachParam))]
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
