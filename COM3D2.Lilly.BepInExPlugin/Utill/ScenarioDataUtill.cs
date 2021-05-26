using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM3D2.Lilly.Plugin.Utill
{
    class ScenarioDataUtill
    {
        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
    "ScenarioDataUtill"
    );

        static bool isRunSetScenarioDataAll = false;

        internal static void SetScenarioDataAll()
        {

            if (isRunSetScenarioDataAll)
                return;

            Task.Factory.StartNew(() =>
            {
                isRunSetScenarioDataAll = true;

                MyLog.LogDarkBlue("SetScenarioDataAll. start");

                // 병렬 처리
                foreach (var scenarioData in GameMain.Instance.ScenarioSelectMgr.GetAllScenarioData())
                {

                    // MyLog.LogMessageS(".SetScenarioDataAll:" + scenarioData.ID + " , " + scenarioData.ScenarioScript + " , " + scenarioData.IsPlayable + " , " + scenarioData.Title); ;
                    if (scenarioData.IsPlayable)
                    {
                        //MyLog.LogMessageS(".m_EventMaid");
                        if (configEntryUtill["SetScenarioDataAll", false])
                            MyLog.LogMessage("SetScenarioDataAll1"
                        , scenarioData.ID
                        , scenarioData.Title
                        , scenarioData.ScenarioScript
                        , scenarioData.ScenarioScript.Contains("_Marriage")
                        );
                        foreach (var maid in scenarioData.GetEventMaidList())
                        {
                            if (maid.status.heroineType == HeroineType.Sub)
                            {
                                if (configEntryUtill["SetScenarioDataAll", false])
                                    MyLog.LogMessage("SetScenarioDataAll2"
                                , MyUtill.GetMaidFullName(maid)
                                , maid.status.heroineType
                                );
                                continue;
                            }

                            //    try
                            //{
                            bool b = maid.status.GetEventEndFlag(scenarioData.ID);
                            if (!b)
                            {
                                if (configEntryUtill["SetScenarioDataAll", false])
                                    MyLog.LogMessage("SetScenarioDataAll3"
                                    , MyUtill.GetMaidFullName(maid)
                                    , b
                                    );
                                maid.status.SetEventEndFlag(scenarioData.ID, true);
                                if (scenarioData.ScenarioScript.Contains("_Marriage"))
                                {
                                    maid.status.specialRelation = SpecialRelation.Married;
                                    maid.status.relation = Relation.Lover;
                                    maid.status.OldStatus.isMarriage = true;
                                    maid.status.OldStatus.isNewWife = true;
                                    //SetMaidCondition(0, '嫁');
                                }
                            }
                            //}
                            //catch (Exception e)
                            //{
                            //    MyLog.LogError("ScenarioDataUtill.SetScenarioDataAll3 : " + e.ToString());
                            //}
                        }
                    }
                    try
                    {
                    }
                    catch (Exception e)
                    {
                        MyLog.LogError("ScenarioDataUtill.SetScenarioDataAll2 : " + e.ToString());
                    }
                }
                try
                {
                }
                catch (Exception e)
                {
                    MyLog.LogError("ScenarioDataUtill.SetScenarioDataAll1 : " + e.ToString());
                }
                MyLog.LogDarkBlue("SetScenarioDataAll. end");
                isRunSetScenarioDataAll = false;

                //FlagUtill.RemoveErrFlagAll();

            });
        }


    }
}
