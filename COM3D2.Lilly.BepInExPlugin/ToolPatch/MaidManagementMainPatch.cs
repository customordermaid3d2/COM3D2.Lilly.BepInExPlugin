using HarmonyLib;
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

        public static Maid ___select_maid_;
        /// <summary>
        /// 메이드 관리에서 모든 버튼 활성화
        /// </summary>
        /// <param name="___select_maid_"></param>
        /// <param name="___button_dic_"></param>
        /// <param name="__instance"></param>
        [HarmonyPatch(typeof(MaidManagementMain), "OnSelectChara")]
        [HarmonyPostfix]
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

    }
}
