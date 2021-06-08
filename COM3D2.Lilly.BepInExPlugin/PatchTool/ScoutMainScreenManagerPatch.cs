using HarmonyLib;
using scoutmode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    /// <summary>
    /// 스카웃 메이드 목록 화면
    /// </summary>
    class ScoutMainScreenManagerPatch
    {
        // ScoutMainScreenManager

        // private void AddScoutMaid()

        [HarmonyPostfix, HarmonyPatch(typeof(ScoutMainScreenManager), "AddScoutMaid")]
        public static void AddScoutMaid(ScoutMainScreenManager __instance)
        {
            //SceneEditPatch.newMaid = true;

            MyLog.LogMessage("ScoutMainScreenManagerPatch.AddScoutMaid"
                , __instance.parent_mgr.moveScreen.next_label
                );

            // this.onFinishEvent = delegate ()
            // {
            //     Maid maid = GameMain.Instance.CharacterMgr.AddStockMaid();
            //     maid.Visible = true;
            //     GameMain.Instance.CharacterMgr.SetActiveMaid(maid, 0);
            // };
            // this.parent_mgr.CallAddScoutCharacter(this);

        }
    }
}
