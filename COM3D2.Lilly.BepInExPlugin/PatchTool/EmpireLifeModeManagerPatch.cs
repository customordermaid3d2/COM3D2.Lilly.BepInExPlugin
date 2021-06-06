using FacilityFlag;
using HarmonyLib;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wf;

namespace COM3D2.Lilly.Plugin.BasePatch
{
    //[MyHarmony(MyHarmonyType.Base)]
    /// <summary>
    /// 회상 모드에서 라이프 관련
    /// </summary>
    class EmpireLifeModeManagerPatch
    {
        // EmpireLifeModeManager

        [HarmonyPatch(typeof(EmpireLifeModeManager), "GetScenarioExecuteCount")]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void GetScenarioExecuteCount(out int __result)
        {
            __result = 9999;/*
            if (Lilly.isLogOnOffAll)
                MyLog.LogMessage("GetScenarioExecuteCount.");*/
        }

        public static Dictionary<string, DataArray<int, byte>> m_SaveDataMaidScenarioExecuteCountArray;//= new Dictionary<string, DataArray<int, byte>>();
        public static DataArray<int, byte> m_SaveDataScenarioExecuteCountArray;// = new DataArray<int, byte>();

        [HarmonyPatch(typeof(EmpireLifeModeManager), MethodType.Constructor)]
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void Constructor(
            DataArray<int, byte> ___m_SaveDataScenarioExecuteCountArray
            , Dictionary<string, DataArray<int, byte>> ___m_SaveDataMaidScenarioExecuteCountArray
            )
        {
            m_SaveDataScenarioExecuteCountArray = ___m_SaveDataScenarioExecuteCountArray;
            m_SaveDataMaidScenarioExecuteCountArray = ___m_SaveDataMaidScenarioExecuteCountArray;
        }

        static bool isScenarioExecuteCountAllRun = false;

        /// <summary>
        /// 피들러 참고. 이숫자 대체 어디서 들고오는거야
        /// </summary>
        public static void SetEmpireLifeModeDataAll()
        {

            if (!isScenarioExecuteCountAllRun)
            {
                Task.Factory.StartNew(() =>
                {
                    MyLog.LogDarkBlue("SetScenarioExecuteCountAll. start");
                    isScenarioExecuteCountAllRun = true;

                    foreach (Maid maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
                    {
                        if (maid.status.heroineType == HeroineType.Sub)
                            continue;

                        foreach (var data in EmpireLifeModeData.GetAllDatas(true))
                        {
                            //try
                            //{
                            int cnt = GameMain.Instance.LifeModeMgr.GetScenarioExecuteCount(data.ID);
                            if (cnt >= 255)
                                continue;

                            IncrementMaidScenarioExecuteCount(data.ID, maid);
                            MyLog.LogMessage("SetScenarioExecuteCountAll:"
                                + cnt
                                + MyUtill.GetMaidFullName(maid)
                                , data.ID
                                , data.strUniqueName
                                , data.dataScenarioFileName
                                , data.dataScenarioFileLabel
                            );
                            if (data.dataFlagMaid != null)
                                MyLog.LogMessage("SetScenarioExecuteCountAll.Maid:"
                                    + MyUtill.Join(" | ", data.dataFlagMaid.Keys.ToArray())
                                );
                            if (data.dataFlagPlayer != null)
                                MyLog.LogMessage("SetScenarioExecuteCountAll.Player:"
                                + MyUtill.Join(" | ", data.dataFlagPlayer.Keys.ToArray())
                            );



                            //m_SaveDataScenarioExecuteCountArray.Add(key, 255, true);
                            //}
                            //catch (Exception e)
                            //{
                            //    MyLog.LogMessage("SetScenarioExecuteCountAll:" + e.ToString());
                            //}

                        }
                    }

                    isScenarioExecuteCountAllRun = false;
                    MyLog.LogDarkBlue("SetScenarioExecuteCountAll. end");
                });
            }
        }

        public static void IncrementMaidScenarioExecuteCount(int eventID, Maid maid)
        {
            if (maid == null)
            {
                return;
            }
            //NDebug.AssertNull(maid);
            //string guid = maid.status.guid;
            DataArray<int, byte> dataArray = CreateMaidDataArray(maid);
            byte b = dataArray.Get(eventID, false);
            b = 255;
            //if (b < 255)
            //{
            //    b += 1;
            //}
            dataArray.Add(eventID, b, true);
            m_SaveDataScenarioExecuteCountArray.Add(eventID, b, true);
        }

        public static DataArray<int, byte> CreateMaidDataArray(Maid maid)
        {
            NDebug.AssertNull(maid);
            string guid = maid.status.guid;
            if (m_SaveDataMaidScenarioExecuteCountArray.ContainsKey(guid))
            {
                return m_SaveDataMaidScenarioExecuteCountArray[guid];
            }
            DataArray<int, byte> dataArray = new DataArray<int, byte>();
            m_SaveDataMaidScenarioExecuteCountArray.Add(guid, dataArray);
            /*
            Debug.LogFormat("メイド「{0}」の情報を作成しました。", new object[]
            {
            maid.status.fullNameJpStyle
            });*/
            return dataArray;
        }
    }
}
