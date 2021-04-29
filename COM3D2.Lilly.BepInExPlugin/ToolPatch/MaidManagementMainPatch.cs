﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 메이드 관리 화면
    /// SceneMaidManagement 장면과 관련된 클래스 찿아야 하는데
    /// </summary>
    class MaidManagementMainPatch
    {


        public static bool newMaid=false;
        public static Maid ___select_maid_;
        /// <summary>
        /// 메이드 관리에서 모든 버튼 활성화
        /// </summary>
        /// <param name="___select_maid_"></param>
        /// <param name="___button_dic_"></param>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(MaidManagementMain), "OnSelectChara")]
        public static void OnSelectChara(Maid ___select_maid_, Dictionary<string, UIButton> ___button_dic_, MaidManagementMain __instance)
        {
            // 현제 선택한 메이드 표시
            MyLog.LogMessage("MaidManagementMain.OnSelectChara:" + ___select_maid_.status.charaName.name1 + " , " + ___select_maid_.status.charaName.name2);
            MaidManagementMainPatch.___select_maid_ = ___select_maid_;

            // MaidStatusUtill.SetMaidStatus(___select_maid_);
            //___m_maid.status.base = 9999;
            //___m_maid.status.base = 9999;

            foreach (var item in ___button_dic_)
            {
                item.Value.isEnabled = true;
            }
        }

        /// <summary>
        /// 고용 버튼 클릭시
        /// </summary>
        /// <param name="___select_maid_"></param>
        /// <param name="___button_dic_"></param>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(MaidManagementMain), "OnClickEmploymentButton")]
        public static void OnClickEmploymentButton()
        {
            MyLog.LogMessage("MaidManagementMain.OnClickEmploymentButton:");
        }

        
        /// <summary>
        /// 고용 ok 누를시
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(MaidManagementMain), "Employment")]
        public static void Employment(string ___new_edit_label_)
        {
            newMaid = true;
            MyLog.LogMessage("MaidManagementMain.Employment"
                //, EasyUtill._GP01FBFaceEyeRandomOnOff.Value
                //, EasyUtill._SetMaidStatusOnOff.Value
                , newMaid
                , ___new_edit_label_
                );

            // GameMain.Instance.SysDlg.Close();
            // int num = this.employmentPrice;
            // GameMain.Instance.CharacterMgr.status.money += (long)(num * -1);
            // Maid maid = this.chara_mgr_.AddStockMaid();
            // MaidManagementMain.BackUpSelectMaidGUID = maid.status.guid;
            // MaidManagementMain.BackUpBarValue = 1f;
            // MaidManagementMain.BackUpRightPanelVisible = this.chara_select_mgr_.IsRightVisible();
            // this.chara_mgr_.SetActiveMaid(maid, 0);
            // this.maid_management_.move_screen.SetNextLabel(this.new_edit_label_);
            // this.Finish();

        }

        public static void newMaidSetting()
        {
            if (!newMaid)
            {
                return;
            }
            Maid maid = GameMain.Instance.CharacterMgr.GetMaid(0);

            PersonalUtill.SetPersonalRandom(maid);

            if (EasyUtill._SetMaidStatusOnOff.Value)
                CheatGUI.SetMaidStatus(maid);

            PresetUtill.RandPreset(PresetUtill.ListType.All, PresetUtill.PresetType.All, maid);

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
                        ,e.ToString()
                    );
                }

            }
            newMaid = false;
        }
    }
}
