using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schedule;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// ScheduleTaskCtrl
    /// ScheduleScene ScheduleApi
    /// ScheduleMgr ScheduleMgr
    /// ScheduleCtrl ScheduleCtrl
    /// ScheduleTaskViewer TaskViewer
    /// int CurrentActiveSlotNo
    /// </summary>
    class ScheduleTaskCtrlPatch
    {
        public static ScheduleTaskCtrl instance;

        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleTaskCtrl), MethodType.Constructor)]
        public static void ScheduleTaskCtrlCtor(ScheduleTaskCtrl __instance)
        {
            MyLog.LogMessage(
            "ScheduleTaskCtrlCtor"
            );
            instance = __instance;
        }

        /// <summary>
        /// 일단 실패
        /// </summary>
        public static void SetScheduleSlot()
        {
            
            List<Maid> l = GameMain.Instance.CharacterMgr.GetStockMaidList();
            MyLog.LogMessage(
            "SetScheduleSlot"
            , l.Count
            );
            ScheduleMgr sm=instance.ScheduleMgr;
            //sm.


            ScheduleScene ss = instance.ScheduleApi;
            // public void SetSlot_Safe(int slotId, Maid maid, bool slotUpdate = true, bool updateAll = true)
            //s.SetSlot_Safe();
            //for (int i = 0; i < s.slot.Length; i++)
            //{
            //    s.SetSlot_Safe(i, null, true, true);
            //}
            if (ss.slot == null)
            {
                MyLog.LogFatal(
                "SetScheduleSlot null"
                );
                return;
            }
            else
            {
                for (int i = 0; i < ss.slot.Length; i++)
                {
                    MyLog.LogFatal(
                    "SetScheduleSlot"
                    , MyUtill.GetMaidFullName(ss.slot[i].Maid)
                    , ss.slot[i].nightWorkId
                    , ss.slot[i].noonWorkId
                    , ss.slot[i].personal_name
                    , ss.slot[i].popular_rank
                    );
                    if (l.Count == 0)
                    {
                        break;
                    }
                    try
                    {
                        int m = Lilly.rand.Next(i);
                        ss.SetSlot_Safe(i, l[m], true, true);
                        l.RemoveAt(m);
                    }
                    catch (Exception e)
                    {
                        MyLog.LogFatal(
                        "SetScheduleSlot"
                        , e.ToString()
                        );
                    }
                }
                MyLog.LogMessage(
                "SetScheduleSlot"
                , "end"
                );
            }
        }
    }
}
