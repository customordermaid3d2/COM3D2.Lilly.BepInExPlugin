using FacilityFlag;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wf;

namespace COM3D2.Lilly.Plugin
{
    //[MyHarmony(MyHarmonyType.Base)]
    /// <summary>
    /// 회상 모드에서 라이프 관련
    /// </summary>
    class EmpireLifeModeManagerPatch 
    {
        // EmpireLifeModeManager

        public static Dictionary<string, DataArray<int, byte>>? m_SaveDataMaidScenarioExecuteCountArray;//= new Dictionary<string, DataArray<int, byte>>();
        public static DataArray<int, byte>? m_SaveDataScenarioExecuteCountArray;// = new DataArray<int, byte>();

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

        static bool isScenarioExecuteCountAllRun=false;

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
                        foreach (var data in EmpireLifeModeData.GetAllDatas(true))
                        {
                            try
                            {
                                int cnt = GameMain.Instance.LifeModeMgr.GetScenarioExecuteCount(data.ID);
                                if (cnt<255)
                                {
                                    IncrementMaidScenarioExecuteCount(data.ID, maid);
                                }

                                if (Lilly.isLogOn)
                                    continue;

                                MyLog.LogMessage("SetScenarioExecuteCountAll:" 
                                    + cnt
                                    + MyUtill.GetMaidFullNale(maid)
                                    , data.ID
                                    , data.strUniqueName
                                    , data.dataScenarioFileName
                                    , data.dataScenarioFileLabel
                                );
                                if(data.dataFlagMaid!=null)
                                    MyLog.LogMessage("SetScenarioExecuteCountAll.Maid:"
                                        + MyUtill.Join(" | ", data.dataFlagMaid.Keys.ToArray())
                                    );
                                if (data.dataFlagPlayer != null)
                                    MyLog.LogMessage("SetScenarioExecuteCountAll.Player:"
                                    + MyUtill.Join(" | ", data.dataFlagPlayer.Keys.ToArray())
                                );
                                //m_SaveDataScenarioExecuteCountArray.Add(key, 255, true);
                            }
                            catch (Exception e)
                            {
                                MyLog.LogMessage("SetScenarioExecuteCountAll:" + e.ToString());
                            }

                        }
                    }

                    isScenarioExecuteCountAllRun = false;
                    MyLog.LogDarkBlue("SetScenarioExecuteCountAll. end");
                });
            }
            /*
            CsvCommonIdManager commonIdManager = new CsvCommonIdManager("empire_life_mode", "エンパイアライフモード.csv", CsvCommonIdManager.Type.IdAndUniqueName, null);
            string[] array = new string[]
            {
                        "list"
            };
            KeyValuePair<AFileBase, CsvParser>[] array2 = new KeyValuePair<AFileBase, CsvParser>[array.Length];
            for (int i = 0; i < array2.Length; i++)
            {
                string text = "empire_life_mode_" + array[i] + ".nei";
                AFileBase afileBase = GameUty.FileSystem.FileOpen(text);
                CsvParser csvParser = new CsvParser();
                bool condition = csvParser.Open(afileBase);
                if (!condition)
                {
                    MyLog.LogMessage("SetScenarioExecuteCountAll open error:"+ text);
                    continue;
                }
                //NDebug.Assert(condition, text + "\nopen failed.");
                array2[i] = new KeyValuePair<AFileBase, CsvParser>(afileBase, csvParser);
            }
            foreach (KeyValuePair<int, KeyValuePair<string, string>> keyValuePair in commonIdManager.idMap)
            {
                int key = keyValuePair.Key;
                EmpireLifeModeData.Data value = new EmpireLifeModeData.Data(key, array2[0].Value);                
            }
            foreach (KeyValuePair<AFileBase, CsvParser> keyValuePair2 in array2)
            {
                keyValuePair2.Value.Dispose();
                keyValuePair2.Key.Dispose();
            }
            */


            // IsCorrectScenarioAnyNumberPlay(this EmpireLifeModeData.Data data)
            // GameMain.Instance.LifeModeMgr.GetScenarioExecuteCount(data.ID);
            // GetScenarioExecuteCount(int eventID)
            /*
            m_SaveDataScenarioExecuteCountArray.Loop(   
                new Action<int, int, byte>(
                    (int i, int key, byte value)=>{
                        MyLog.LogMessage("SetScenarioExecuteCountAll:"+i
                            ,key
                            ,value
                            );
                        m_SaveDataScenarioExecuteCountArray.Add(key, 255, true);
                    }
                )
            );
            */
        }
        /*
        /// <summary>
        /// 분석용
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public int GetScenarioExecuteCount(int eventID)
        {
            if (!m_SaveDataScenarioExecuteCountArray.Contains(eventID))
            {
                return 0;
            }
            return (int)m_SaveDataScenarioExecuteCountArray.Get(eventID, false);
        }
        */
        /*
        /// <summary>
        /// 분석용
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsCorrectScenarioAnyNumberPlay(this EmpireLifeModeData.Data data)
        {
            //GameMain.Instance.LifeModeMgr.
            return data.dataScenarioAnyNumberPlay || 0 >= GameMain.Instance.LifeModeMgr.GetScenarioExecuteCount(data.ID);
        }
        */
        public static void IncrementMaidScenarioExecuteCount(int eventID, Maid maid)
        {
            if (maid ==null)
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
