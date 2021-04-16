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

        // 정상
        /// <summary>
        /// 주의. 에딧 모드에서 프리셋 창 뜰때 이 함수를 이용해서 파일들을 다 읽어옴
        /// 프리셋 인셉션 오류 방지
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="__result"></param>
        /// <param name="f_strFileName"></param>
        [HarmonyPatch(typeof(CharacterMgr), "PresetLoad", new Type[] { typeof(BinaryReader), typeof(string) })]
        [HarmonyFinalizer]
        public static void PresetLoadPostfix(ref Exception __exception, CharacterMgr __instance, string f_strFileName)
        // public CharacterMgr.Preset PresetLoad(BinaryReader brRead, string f_strFileName)
        {
            if (__exception == null)
            {
                return;
            }
            MyLog.Log("PresetLoad() "
                , f_strFileName
                , __exception.ToString()
                );
            __exception = null;
        }

    }
}
