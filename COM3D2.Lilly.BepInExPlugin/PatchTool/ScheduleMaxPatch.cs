using HarmonyLib;
using MonoMod.Cil;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 스케줄 max값 늘리기 위한 여정. 현제 실패
    /// fail
    /// </summary>
    class ScheduleMaxPatch
    {
        public const int maxSlot = 100;

        // protected override void SetDataForViewer()
        [HarmonyILManipulator, HarmonyPatch(typeof(ScheduleCtrl), "SetDataForViewer")]//, MethodType.Constructor
        public static void SetDataForViewer(ILContext ctx, MethodBase orig)
        {
            MyLog.LogMessage(
            "ScheduleCtrl.SetDataForViewer"
            );

            ILCursor c = new ILCursor(ctx);

            if (c.TryGotoNext(MoveType.Before,
                x => x.MatchLdcI4(40)
            ))
            {
                c.Remove();
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, maxSlot);
            }
            else
            {
                MyLog.LogWarning(
                    "ScheduleCtrl.SetDataForViewer"
                    , c.ToString()
                );
            }
        }

        //private void SetWorkId(ScheduleMgr.ScheduleTime workTime, int taskId)
        [HarmonyILManipulator, HarmonyPatch(typeof(ScheduleTaskCtrl), "SetWorkId")]//, MethodType.Constructor
        public static void SetWorkId(ILContext ctx, MethodBase orig)
        {
            MyLog.LogMessage(
            "ScheduleTaskCtrl.SetWorkId"
            );

            ILCursor c = new ILCursor(ctx);

            if (c.TryGotoNext(MoveType.Before,
                x => x.MatchLdcI4(40)
            ))
            {
                c.Remove();
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, maxSlot);
            }
            else
            {
                MyLog.LogWarning(
                    "ScheduleTaskCtrl.SetWorkId"
                    , c.ToString()
                );
            }
        }


        // public void SetSlot_Safe(int slotId, Maid maid, bool slotUpdate = true, bool updateAll = true)
        // public void SetNoonWorkSlot_Safe(ScheduleMgr.ScheduleTime workTime, int slotId, int workId)

        [HarmonyILManipulator]
        [HarmonyPatch(typeof(ScheduleScene), MethodType.Constructor)]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleScene), "SetSlot_Safe")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleScene), "SetNoonWorkSlot_Safe")]//, MethodType.Constructor
        public static void ScheduleScenePatch(ILContext ctx, MethodBase orig)
        {
            MyLog.LogMessage(
            "ScheduleScenePatch"
            );

            ILCursor c = new ILCursor(ctx);

            for (int i = 0; i < 4; i++)
            {
                if (c.TryGotoNext(MoveType.Before,
                    x => x.MatchLdcI4(40)
                ))
                {
                    c.Remove();
                    c.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, maxSlot);
                }
                else
                {
                    MyLog.LogWarning(
                        "ScheduleScenePatch"
                        , orig.Name
                        , i
                        , c.ToString()
                    );
                    break;
                }
            }
        }

        // public void SetNoonWorkSlot_Safe(ScheduleMgr.ScheduleTime workTime, int slotId, int workId)
        [HarmonyILManipulator, HarmonyPatch(typeof(WorkResultScene), "Calc")]//, MethodType.Constructor
        public static void Calc(ILContext ctx, MethodBase orig)
        {
            MyLog.LogMessage(
            "WorkResultScene.Calc"
            );

            ILCursor c = new ILCursor(ctx);

            for (int i = 0; i < 2; i++)
            {
                if (c.TryGotoNext(MoveType.Before,
                    x => x.MatchLdcI4(40)
                ))
                {
                    c.Remove();
                    c.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, maxSlot);
                }
                else
                {
                    MyLog.LogWarning(
                          "WorkResultScene.Calc"
                         , i
                        , c.ToString()
                    );
                    break;
                }
            }
        }

        // public void SetNoonWorkSlot_Safe(ScheduleMgr.ScheduleTime workTime, int slotId, int workId)
        [HarmonyILManipulator]
        [HarmonyPatch(typeof(ScheduleAPI), "BackupParam")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetNoonTrainerMaid")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetNoonTraineeMaid")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetNightTrainerMaid")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetNightTraineeMaid")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetTravelMaid")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetTravelBgData")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetCommunicationMaid")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "CanCommunicationMaids")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetEntertainMaids")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetVipWorkMaids")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetYotogiMaids")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "GetNewYotogiMaids")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "OneDayParamDiff")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "EnableNoonWork")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "SlotEmptyCheck")]//, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "CheckTraining")]// 4 , MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "CheckYotogi")]// 1 , MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "ScheduleErrorAllCheck")]// 1 , MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "DayStartManage")]// 1 , MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "ThroughNoonResult")]// 1 , MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "IsSlotInMaid")]// 1, MethodType.Constructor
        [HarmonyPatch(typeof(ScheduleAPI), "CanTrainee")]// 1 , MethodType.Constructor
        public static void ScheduleAPIPatch(ILContext ctx, MethodBase orig)
        {
            MyLog.LogMessage(
            "ScheduleAPIPatch"
            );

            ILCursor c = new ILCursor(ctx);

            for (int i = 0; i < 4; i++)
            {
                if (c.TryGotoNext(MoveType.Before,
                    x => x.MatchLdcI4(40)
                ))
                {
                    c.Remove();
                    c.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, maxSlot);
                }
                else
                {
                    MyLog.LogWarning(
                        //,"WorkResultScene.Calc"
                        "ScheduleAPIPatch"
                        , orig.Name                        
                        , i
                        , c.ToString()
                    );
                    break;
                }
            }
        }

        // public void SetNoonWorkSlot_Safe(ScheduleMgr.ScheduleTime workTime, int slotId, int workId)
        [HarmonyILManipulator]
        [HarmonyPatch(typeof(ScheduleAPI), "MaidWorkIdErrorCheck",new Type[]{typeof(bool)})]//, MethodType.Constructor
        public static void ScheduleAPIPatch2(ILContext ctx, MethodBase orig)
        {
            MyLog.LogMessage(
            "ScheduleAPIPatch2"
            );

            ILCursor c = new ILCursor(ctx);

            for (int i = 0; i < 4; i++)
            {
                if (c.TryGotoNext(MoveType.Before,
                    x => x.MatchLdcI4(40)
                ))
                {
                    c.Remove();
                    c.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, maxSlot);
                }
                else
                {
                    MyLog.LogWarning(
                        //,"WorkResultScene.Calc"
                        "ScheduleAPIPatch2"
                        , orig.Name                        
                        , i
                        , c.ToString()
                    );
                    break;
                }
            }
        }

        [HarmonyILManipulator]
        [HarmonyPatch(typeof(PlayerStatus.Status), MethodType.Constructor)]//, MethodType.Constructor
        [HarmonyPatch(typeof(PlayerStatus.Status), "GetScheduleSlot")]//, MethodType.Constructor
        [HarmonyPatch(typeof(PlayerStatus.Status), "Deserialize")]//, MethodType.Constructor
        public static void StatusPatch1(ILContext ctx, MethodBase orig)
        {
            MyLog.LogMessage(
            "StatusPatch1"
            );

            ILCursor c = new ILCursor(ctx);

            for (int i = 0; i < 2; i++)
            {
                if (c.TryGotoNext(MoveType.Before,
                    x => x.MatchLdcI4(40)
                ))
                {
                    c.Remove();
                    c.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, maxSlot);
                }
                else
                {
                    MyLog.LogWarning(
                          "StatusPatch1"
                         , i
                        , c.ToString()
                    );
                    break;
                }
            }
        }

        //[HarmonyILManipulator]
        public static void StatusPatch2(ILContext ctx, MethodBase orig)
        {
            MyLog.LogMessage(
            "StatusPatch2"
            );

            ILCursor c = new ILCursor(ctx);

            for (int i = 0; i < 2; i++)
            {
                if (c.TryGotoNext(MoveType.Before,
                    x => x.MatchLdcI4(40)
                ))
                {
                    c.Remove();
                    c.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, maxSlot);
                }
                else
                {
                    MyLog.LogWarning(
                          "StatusPatch2"
                         , i
                        , c.ToString()
                    );
                    break;
                }
            }
        }
    }
}
