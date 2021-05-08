using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Utill
{
    class FlagUtill
    {

        public static void RemoveEventEndFlagAll()
        {
            for (int j = 0; j < GameMain.Instance.CharacterMgr.GetStockMaidCount(); j++)
            {
                Maid maid = GameMain.Instance.CharacterMgr.GetStockMaid(j);
                MyLog.LogMessage("RemoveEventEndFlagAll:" + MyUtill.GetMaidFullName(maid)); ;
                maid.status.RemoveEventEndFlagAll();
            }
        }

        public static void RemoveEventEndFlag(bool logon = false)
        {
            /*
            if (Lilly.scene.name != "SceneMaidManagement")
            {
                MyLog.LogDarkBlue("메이드 관리에서 사용하세요"); ;
                return;
            }
            */
            if (HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMainPatch)))
                RemoveEventEndFlag(MaidManagementMainPatch.___select_maid_);
            else
                MyLog.LogDarkBlue("HarmonyUtill에서 MaidManagementMainPatch 를 켜주시고 메이드를 선택하세요");
            //RemoveEventEndFlag(SceneEdit.Instance.maid);
        }

        public static void RemoveEventEndFlag(Maid maid)
        {
            if (maid != null)
            {
                MyLog.LogDarkBlue("RemoveEventEndFlag" + MyUtill.GetMaidFullName(maid));
                maid.status.RemoveEventEndFlagAll();
            }

        }

    }
}
