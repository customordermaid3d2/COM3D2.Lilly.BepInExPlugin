﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
	/// <summary>
	/// 메인 화면의 스케줄 관련
	/// </summary>
    class ScheduleMgrPatch
    {
        // ScheduleMgr

        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleMgr), "ClickMaidStatus")]
        private static void ClickMaidStatus(
            ScheduleMgr __instance
            , ScheduleCtrl m_Ctrl
            ) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("ClickMaidStatus"
                , UIButton.current.name
                , MyUtill.GetMaidFullName(m_Ctrl.SelectedMaid)
                );
        }

        [HarmonyPostfix,HarmonyPatch(typeof(ScheduleMgr), "ClickTask")]
        private static void ClickTask(
            ScheduleMgr __instance
            , ScheduleCtrl m_Ctrl
            ) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("ClickTask"
                , UIButton.current.name
                , MyUtill.GetMaidFullName(m_Ctrl.SelectedMaid)
                );
        }
		/*
		public void ClickMaidStatus()
		{
			string name = UIButton.current.name;
			if (UICamera.currentTouchID == -1)
			{
				if (this.CurrentActiveButton == name)
				{
					return;
				}
				Debug.Log(string.Format("{0}ボタンがクリックされました。", name));
				this.m_MaidStatusListCtrl.CreateTaskViewer(name);
				this.CurrentActiveButton = name;
			}
			else if (UICamera.currentTouchID == -2)
			{
				Debug.Log(string.Format("{0}ボタンが右クリックされました。", name));
				if (this.m_Ctrl.CanDeleteData(name))
				{
					this.m_Ctrl.DeleteMaidStatus(this.m_scheduleApi, name);
				}
			}
		}
		*/
	}
}
