using HarmonyLib;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    // ScheduleCtrl
    //[MyHarmony(MyHarmonyType.Base)]
    class ScheduleCtrlPatch
    {
        public static MethodInfo hGetMaidName;
        //public static MethodInfo hGetMaidBySlotNo;
        //public static MethodInfo hSetViewerActive;
        //public static MethodInfo hSetSelectedRowActive;
        public static Dictionary<string, ScheduleCtrl.MaidStatusAndTaskUnit> m_dicMaidStatusAndTask;
        public static string m_deleteSlotNo;
        public static ScheduleMgr m_scheduleMgr;
        public static ScheduleScene m_scheduleApi;
        public static ScheduleCtrl instance;
        public static CharacterSelectManager m_charSelMgr;
        public static ScheduleCtrl.ExclusiveViewer m_currentActiveViewer;



        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleCtrl), MethodType.Constructor)]
        public static void ScheduleCtrlPatchC(
            ScheduleCtrl __instance
            , ref Dictionary<string, ScheduleCtrl.MaidStatusAndTaskUnit> ___m_dicMaidStatusAndTask
            , ref string ___m_deleteSlotNo
            , ref ScheduleMgr ___m_scheduleMgr
            , ref ScheduleScene ___m_scheduleApi
            , ref CharacterSelectManager ___m_charSelMgr
            , ref ScheduleCtrl.ExclusiveViewer ___m_currentActiveViewer
            )
        {
            MyLog.LogMessage(
                "ScheduleCtrlPatchC"
                );

            instance = __instance;
            hGetMaidName = AccessTools.Method(typeof(ScheduleCtrl), "GetMaidName"); // 이건 잘됨
            //hGetMaidBySlotNo = AccessTools.Method(typeof(ScheduleCtrl), "GetMaidBySlotNo"); // 이건 잘됨
            //hSetViewerActive = AccessTools.Method(typeof(ScheduleCtrl), "SetViewerActive"); // 이건 잘됨
            //hSetSelectedRowActive = AccessTools.Method(typeof(ScheduleCtrl), "SetSelectedRowActive"); // 이건 잘됨
            m_dicMaidStatusAndTask = ___m_dicMaidStatusAndTask;
            m_deleteSlotNo = ___m_deleteSlotNo;
            m_scheduleMgr = ___m_scheduleMgr;
            m_scheduleApi = ___m_scheduleApi;
            m_charSelMgr = ___m_charSelMgr;
            m_currentActiveViewer = ___m_currentActiveViewer;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_deleteSlotNo">slot_0_MaidStatus</param>
        public static void DeleteMaidAndReDraw(string m_deleteSlotNo)
        {
            string text = null;
            if (m_dicMaidStatusAndTask.ContainsKey(m_deleteSlotNo))
            {
                text = m_scheduleMgr.CurrentActiveButton;
                int slotId = ScheduleCtrl.ToIntSlotNo(m_deleteSlotNo);
                m_scheduleApi.SetSlot_Safe(slotId, null, true, true);
                if (instance.DicExclusiveViewer[ScheduleCtrl.ExclusiveViewer.MaidStatusList].activeSelf)
                {
                    //m_charSelMgr.MoveGridArea((Maid)hGetMaidBySlotNo.Invoke(instance, new object[] { m_deleteSlotNo }));
                    m_charSelMgr.MoveGridArea(instance.GetMaidBySlotNo( m_deleteSlotNo ));
                }
                m_scheduleMgr.UpdateMaidStatus();
                MyLog.LogMessage(
                "DeleteMaidAndReDraw1"
                , m_deleteSlotNo
                , slotId
                , text
                );
            }
            //GameMain.Instance.SysDlg.Close();
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Contains(m_deleteSlotNo))
                {
                    //hSetViewerActive.Invoke(instance, new object[] { ScheduleCtrl.ExclusiveViewer.None });
                    instance.SetViewerActive(ScheduleCtrl.ExclusiveViewer.None );
                }
                else
                {
                    instance.SetSelectedRowActive( text );
                    m_scheduleMgr.CurrentActiveButton = text;
                    //hSetViewerActive.Invoke(instance, new object[] { m_currentActiveViewer });
                    instance.SetViewerActive( m_currentActiveViewer );
                    if (instance.DicExclusiveViewer[ScheduleCtrl.ExclusiveViewer.Task].activeSelf)
                    {
                        m_scheduleMgr.UpdateTask("-1");
                    }
                }
                MyLog.LogMessage(
                "DeleteMaidAndReDraw2"
                , m_deleteSlotNo
                , text
                );
            }
            //m_deleteSlotNo = null;
            m_scheduleApi = null;
        }

    }
}
