using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class ScenarioSelectMgrPatch
    {
        // ScenarioSelectMgr

        public static Dictionary<int, ScenarioData> m_AllScenarioData;
        public static List<ScenarioData> m_ImportantScenarioList;
        public static ScenarioData[] m_AddedScenerio; // GameMain.Instance.ScenarioSelectMgr.AddedScenario;

        // 시나리오 데이터별 플레이 정보?
        //public readonly Dictionary<ScenarioData.PlayableCondition, ScenarioData.PlayableData> m_PlayableData = new Dictionary<ScenarioData.PlayableCondition, ScenarioData.PlayableData>();

        [HarmonyPatch(typeof(ScenarioSelectMgr), MethodType.Constructor)]
        [HarmonyPostfix]

        internal static void ScenarioSelectMgrCtor(
            ref Dictionary<int, ScenarioData> ___m_AllScenarioData
            , ref List<ScenarioData> ___m_ImportantScenarioList
            , ref ScenarioData[] ___m_AddedScenerio
            )
        {
            m_AllScenarioData = ___m_AllScenarioData;
            m_ImportantScenarioList = ___m_ImportantScenarioList;
            m_AddedScenerio = ___m_AddedScenerio;
            //m_AllScenarioData = GameMain.Instance.ScenarioSelectMgr.GetAllScenarioData();
            //m_AddedScenerio = GameMain.Instance.ScenarioSelectMgr.AddedScenario;
            /*
			this.m_ImportantScenarioList = (from e in this.m_AllScenarioData.Values
			where e.IsPlayable && e.IsImportant
			select e).ToList<ScenarioData>();
             */

        }

        /// <summary>
        /// 실제로 nei 파일을 읽어와 데이터를 담음
        /// </summary>
        [HarmonyPatch(typeof(ScenarioSelectMgr), "InitScenarioData")]
        [HarmonyPostfix]
        internal static void InitScenarioData(Dictionary<int, ScenarioData> ___m_AllScenarioData)
        {
            //m_AllScenarioData=___m_AllScenarioData;
        }


        /// <summary>
        /// 분석용
        /// </summary>
        /// <returns></returns>
        public bool IsEventAdded()
        {
            List<ScenarioData> list = (from e in m_AllScenarioData.Values
                                       where e.ImportantPlayable
                                       select e).ToList<ScenarioData>();
            ScenarioData[] array = (from e in list
                                    where !m_ImportantScenarioList.Contains(e)
                                    select e).ToArray<ScenarioData>();
            m_AddedScenerio = array;
            m_ImportantScenarioList = list;
            return array.Length > 0;
        }


        internal static void print()
        {
            try
            {

                //AccessTools.Field
                MyLog.LogDarkBlue("m_AllScenarioData. start");
                if (m_AllScenarioData != null)
                {

                    foreach (var item in m_AllScenarioData)
                    {
                        if (item.Value == null)
                        {
                            continue;
                        }
                        ScenarioData scenarioData = item.Value;
                        MyLog.LogMessage("ScenarioSelectMgr1"
                        , scenarioData.ID
                        , scenarioData.NotLineTitle
                        //, scenarioData.Title
                        , scenarioData.ScenarioScript
                        , scenarioData.EventContents
                        , scenarioData.IsImportant
                        , scenarioData.IsPlayable
                        , scenarioData.IsOncePlayed
                        , scenarioData.ScriptLabel
                        , scenarioData.NotPlayAgain
                        , scenarioData.EventMaidCount
                        , scenarioData.ConditionCount
                        , MyUtill.Join("/", scenarioData.ConditionText)
                        );
                    }
                }

                MyLog.LogDarkBlue("m_ImportantScenarioList. start");
                if (m_ImportantScenarioList != null)
                    foreach (ScenarioData scenarioData in m_ImportantScenarioList)
                    {
                        MyLog.LogMessage("ScenarioSelectMgr2"
                        , scenarioData.ID
                        , scenarioData.NotLineTitle
                        //, scenarioData.Title
                        , scenarioData.ScenarioScript
                        , scenarioData.EventContents
                        , scenarioData.IsImportant
                        , scenarioData.IsPlayable
                        , scenarioData.IsOncePlayed
                        , scenarioData.ScriptLabel
                        , scenarioData.NotPlayAgain
                        , scenarioData.EventMaidCount
                        , scenarioData.ConditionCount
                        , MyUtill.Join("/", scenarioData.ConditionText)
                        );
                    }

                MyLog.LogMessage("ScenarioSelectMgr3"
                    , m_AllScenarioData.Count
                    , m_ImportantScenarioList.Count
                    , m_AddedScenerio.Length
                );

            }
            catch (Exception e)
            {
                MyLog.LogWarning ("ScenarioSelectMgr4",e.ToString()
);
            }
        }



    }
}
