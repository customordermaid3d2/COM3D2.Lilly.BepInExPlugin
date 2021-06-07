using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    //[MyHarmony(MyHarmonyType.Base)]
    class CharacterMgrPatchBase
    {

        public static PresetType presetType = PresetType.none;

        // public void PresetSet(Maid f_maid, CharacterMgr.Preset f_prest, bool forceBody = false) // 157
        // public void PresetSet(Maid f_maid, CharacterMgr.Preset f_prest) // 155
        // 테스팅 완료
        [HarmonyPatch(typeof(CharacterMgr), "PresetSet", new Type[] { typeof(Maid), typeof(CharacterMgr.Preset) })]
        [HarmonyPrefix]
        public static void PresetSet(Maid f_maid, CharacterMgr.Preset f_prest)
        {
            MyLog.Log("PresetSet.Prefix"
            , MyUtill.GetMaidFullName(f_maid)
            , f_prest.strFileName
            );
            switch (presetType)
            {
                case PresetType.Wear:
                    f_prest.ePreType = CharacterMgr.PresetType.Wear;
                    break;
                case PresetType.Body:
                    f_prest.ePreType = CharacterMgr.PresetType.Body;
                    break;
                case PresetType.All:
                    f_prest.ePreType = CharacterMgr.PresetType.All;
                    break;
                default:
                    break;
            }
        }

        public enum PresetType
        {
            none,
            Wear,
            Body,
            All
        }

    }
}
