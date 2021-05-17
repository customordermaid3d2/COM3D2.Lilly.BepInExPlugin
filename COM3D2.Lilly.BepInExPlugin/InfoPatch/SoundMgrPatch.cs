using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.InfoPatch
{
    class SoundMgrPatch
    {
        // SoundMgr

        /// <summary>
        /// public void PlayBGM(string f_strFileName, float f_fTime, bool f_fLoop = true)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "PlayBGM", new Type[] { typeof(string),typeof(float), typeof(bool)  })]
        public static void PlayBGM(SoundMgr __instance, string f_strFileName, float f_fTime, bool f_fLoop = true)
        {
            MyLog.LogMessage("PlayBGM"
                , f_strFileName
                , f_fTime
                , f_fLoop
                );
        }
        /// <summary>
        /// public void PlayBGMLegacy(string f_strFileName, float f_fTime, bool f_fLoop = true)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "PlayBGMLegacy", new Type[] { typeof(string),typeof(float), typeof(bool)  })]
        public static void PlayBGMLegacy(SoundMgr __instance, string f_strFileName, float f_fTime, bool f_fLoop = true)
        {
            MyLog.LogMessage("PlayBGMLegacy"
                , f_strFileName
                , f_fTime
                , f_fLoop
                );
        }

        /// <summary>
        /// public void PlayDanceBGM(string f_strFileName, float f_fTime, bool f_fLoop = true)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "PlayDanceBGM", new Type[] { typeof(string),typeof(float), typeof(bool)  })]
        public static void PlayDanceBGM(SoundMgr __instance, string f_strFileName, float f_fTime, bool f_fLoop = true)
        {
            MyLog.LogMessage("PlayDanceBGM"
                , f_strFileName
                , f_fTime
                , f_fLoop
                );
        }

        /// <summary>
        /// public void PlayEnv(string f_strFileName, float f_fTime)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "PlayEnv", new Type[] { typeof(string),typeof(float)  })]
        public static void PlayEnv(SoundMgr __instance, string f_strFileName, float f_fTime)
        {
            MyLog.LogMessage("PlayEnv"
                , f_strFileName
                , f_fTime
                );
        }
        /// <summary>
        /// public void PlaySe(string f_strFileName, bool f_bLoop)
        /// 오류 발생
        /// </summary>
        /// <param name="__instance"></param>
        //[HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "PlaySe", new Type[] { typeof(string),typeof(float)  })]
        public static void PlaySe(SoundMgr __instance, string f_strFileName, bool f_bLoop)
        {
            MyLog.LogMessage("PlaySe"
                , f_strFileName
                , f_bLoop
                );
        }

        /// <summary>
        /// public void SetVolume(AudioSourceMgr.Type f_eType, int f_nVol)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "SetVolume", new Type[] { typeof(AudioSourceMgr.Type),typeof(int)  })]
        public static void SetVolume(SoundMgr __instance, AudioSourceMgr.Type f_eType, int f_nVol)
        {
            MyLog.LogMessage("SetVolume"
                , f_eType
                , f_nVol
                );
        }

        /// <summary>
        /// public void SetVolumeAll(int f_nVol)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "SetVolumeAll", new Type[] { typeof(int)  })]
        public static void SetVolumeAll(SoundMgr __instance,  int f_nVol)
        {
            MyLog.LogMessage("SetVolumeAll"
                , f_nVol
                );
        }

        /// <summary>
        /// public void SetVolumeDance(int f_nVol)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "SetVolumeDance", new Type[] { typeof(int)  })]
        public static void SetVolumeDance(SoundMgr __instance,  int f_nVol)
        {
            MyLog.LogMessage("SetVolumeDance"
                , f_nVol
                );
        }

        /// <summary>
        /// public void StopBGM(float f_fTime)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "StopBGM", new Type[] { typeof(float)  })]
        public static void StopBGM(SoundMgr __instance, float f_fTime)
        {
            MyLog.LogMessage("StopBGM"
                , f_fTime
                );
        }

        /// <summary>
        /// public void StopEnv(float f_fTime)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "StopEnv", new Type[] { typeof(float)  })]
        public static void StopEnv(SoundMgr __instance, float f_fTime)
        {
            MyLog.LogMessage("StopEnv"
                , f_fTime
                );
        }

        /// <summary>
        /// public void StopSe(string f_strFileName)
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(SoundMgr), "StopSe", new Type[] { typeof(string)  })]
        public static void StopSe(SoundMgr __instance, string f_strFileName)
        {
            MyLog.LogMessage("StopSe"
                , f_strFileName
                );
        }


    }
}
