using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchTool
{
    class StatusToolPatch
    {

        public static Dictionary<string, string> flagsPlayer = new Dictionary<string, string>();

        // public bool RemoveFlag(string flagName)
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerStatus.Status), "Deserialize")]
        public static void Deserialize()
        {
            StatusToolPatch.flagsPlayer = GameMain.Instance.CharacterMgr.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
        }

    }
}
