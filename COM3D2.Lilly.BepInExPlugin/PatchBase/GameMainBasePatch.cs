using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.BasePatch
{
    class GameMainBasePatch
    {
        // GameMain

        [HarmonyPostfix, HarmonyPatch(typeof(GameMain), "OnInitialize")]
        public static void OnInitialize()
        {

        }

    }
}
