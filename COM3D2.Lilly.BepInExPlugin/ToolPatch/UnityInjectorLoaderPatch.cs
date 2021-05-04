using BepInEx.UnityInjectorLoader;
using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

namespace COM3D2.Lilly.Plugin.ToolPatch
{
    /// <summary>
    /// 결과적으로 실패
    /// </summary>
    class UnityInjectorLoaderPatch
    {
        // UnityInjectorLoader

        public static Harmony harmony;

        [HarmonyILManipulator, HarmonyPatch(typeof(UnityInjectorLoader), "Init")]//, MethodType.Constructor
        public static void Init(ILContext ctx, System.Reflection.MethodBase orig)
        {
            MyLog.LogMessage(
            "UnityInjectorLoader.Init"
            );

            ILCursor c = new ILCursor(ctx);

            if (c.TryGotoNext(MoveType.After,
                x => x.MatchRet(),
                x => x.MatchLdloc(1)
            ))
            {
                // 
                c.Emit(OpCodes.Call, AccessTools.Method(typeof(UnityInjectorLoaderPatch), "SetHarmony"));
            }
            else
            {
                MyLog.LogFatal(
                    "ScheduleCtrl.SetDataForViewer"
                    , c.ToString()
                );
            }
        }

        private static void SetHarmony()
        {
            if (harmony == null)
            {
                harmony = Harmony.CreateAndPatchAll(typeof(NPRShaderPatch));
            }
        }
    }
}

