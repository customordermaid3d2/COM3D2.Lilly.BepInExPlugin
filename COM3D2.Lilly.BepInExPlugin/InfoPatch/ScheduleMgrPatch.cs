using HarmonyLib;
using Schedule;
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

        public static ScheduleScene m_scheduleApi;

        /// <summary>
        /// 스케줄에 들어가야 로딩됨
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="___m_scheduleApi"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleMgr), "LoadData")]
        // private Dictionary<string, ScheduleCtrl.MaidStatusAndTaskUnit> LoadData()
        private static void LoadData(
            ScheduleMgr __instance
            , ScheduleScene ___m_scheduleApi
            ) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("LoadData"
                );
            m_scheduleApi = ___m_scheduleApi;
        }
        
        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleMgr), "ClickMaidStatus")]
        private static void ClickMaidStatus(
            ScheduleMgr __instance
            , ScheduleCtrl ___m_Ctrl
            ) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("ClickMaidStatus"
                , UIButton.current.name
                , MyUtill.GetMaidFullName(___m_Ctrl.SelectedMaid)
                );
        }

        [HarmonyPostfix,HarmonyPatch(typeof(ScheduleMgr), "ClickTask")]
        private static void ClickTask(
            ScheduleMgr __instance
            , ScheduleCtrl ___m_Ctrl
            ) // string __m_BGMName 못가져옴
        {
            MyLog.LogMessage("ClickTask"
                , UIButton.current.name
                , MyUtill.GetMaidFullName(___m_Ctrl.SelectedMaid)
                );
        }

        public static void SetSlotAllMaid()
        {
            List<Maid> maids = new List<Maid>();
            maids.AddRange(GameMain.Instance.CharacterMgr.GetStockMaidList());
            MyLog.LogMessage("SetSlotAllMaid"
            , maids.Count
            , m_scheduleApi.slot.Length
            );
            for (int i = 0; i < m_scheduleApi.slot.Length; i++)
            {
                if (maids.Count==0)
                {
                    return;
                }
                Maid maid = maids[UnityEngine.Random.Range(0, maids.Count)];
                //m_scheduleApi.slot[i] 
                m_scheduleApi.SetSlot_Safe(i, maid, true, false);
                maids.Remove(maid);
            }
        }
        
        public static void SetSlotAllDel()
        {
            for (int i = 0; i < m_scheduleApi.slot.Length; i++)
            {
                m_scheduleApi.SetSlot_Safe(i, null, true, false);
            }
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
