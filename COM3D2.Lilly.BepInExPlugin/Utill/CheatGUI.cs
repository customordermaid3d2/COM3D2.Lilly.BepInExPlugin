using FacilityFlag;
using MaidStatus;
using MaidStatus.CsvData;
using PlayerStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using wf;
using Yotogis;

namespace COM3D2.Lilly.Plugin
{
    public class CheatGUI : GUIVirtual
    {
        public CheatGUI() : base("CheatUtill")
        {

        }

        public override void SetButtonList()
        {
            if (GUILayout.Button("시나리오 처리 처리")) CheatGUI.SetScenarioDataAll();
            if (GUILayout.Button("프리 모드 플레그 처리")) CheatGUI.SetFreeModeItemEverydayAll();
            if (GUILayout.Button("밤시중 플레그 처리")) CheatGUI.SetYotogiAll();
            if (GUILayout.Button("일상 플레그 처리")) CheatGUI.SetWorkAll();
            if (GUILayout.Button("라이프 클리어 처리 ")) EmpireLifeModeManagerPatch.SetEmpireLifeModeDataAll();
            if (GUILayout.Button("플레이어 치트 처리")) CheatGUI.SetAllPlayerStatus();
            if (GUILayout.Button("스텟, 스킬, 잡, 클래스 처리")) CheatGUI.SetMaidStatusAll();
            if (GUILayout.Button("모든 메이드 메이드 플레그 제거")) CheatGUI.RemoveEventEndFlagAll();

            GUILayout.Label("메이드 관리에서 사용 SceneMaidManagement");
            GUI.enabled  = Lilly.scene.name == "SceneMaidManagement";
            if (GUILayout.Button("선택 메이드 스텟, 스킬, 잡, 클래스 처리")) CheatGUI.SetMaidStatus();
            if (GUILayout.Button("선택 메이드 플레그 제거")) CheatGUI.RemoveEventEndFlag();
            GUI.enabled = true;
        }

        public static CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;

        public static void RemoveEventEndFlagAll()
        {
            Action<Maid> action = delegate (Maid maid) {
                maid.status.RemoveEventEndFlagAll();
            };
            for (int j = 0; j < characterMgr.GetStockMaidCount(); j++)
            {
                Maid stockMaid = characterMgr.GetStockMaid(j);
                MyLog.LogMessage(".RemoveEventEndFlagAll:" + stockMaid.status.fullNameJpStyle); ;
                action(stockMaid);
            }
        }

        public static void RemoveEventEndFlag()
        {
            /*
            if (Lilly.scene.name != "SceneMaidManagement")
            {
                MyLog.LogDarkBlue("메이드 관리에서 사용하세요"); ;
                return;
            }
            */
            if (HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMainPatch)))
                RemoveEventEndFlag(MaidManagementMainPatch.___select_maid_);
            else
                MyLog.LogDarkBlue("HarmonyUtill에서 MaidManagementMainPatch 를 켜주시고 메이드를 선택하세요");
            //RemoveEventEndFlag(SceneEdit.Instance.maid);
        }

        public static void RemoveEventEndFlag(Maid maid)
        {
            if (maid != null)
            {
                MyLog.LogDarkBlue(".RemoveEventEndFlag:" + maid.status.firstName + " , " + maid.status.lastName); ;
                maid.status.RemoveEventEndFlagAll();
            }

        }

        public static void SetMaidStatus()
        {
            MyLog.LogDarkBlue("MaidStatusUtill.SetMaidStatusAll. start");

                SetMaidStatus(MaidManagementMainPatch.___select_maid_);
            
            /*
            Parallel.ForEach(GameMain.Instance.CharacterMgr.GetStockMaidList(), maid =>
            {
                //MyLog.LogMessageS("MaidStatusUtill.ActiveManSloatCount: " + maid.status.firstName +" , "+ maid.status.lastName);

                SetMaidStatus(maid);
            });
            */
            MyLog.LogDarkBlue("MaidStatusUtill.SetMaidStatusAll. end");
        }

        public static void SetMaidStatusAll()
        {
            MyLog.LogDarkBlue("MaidStatusUtill.SetMaidStatusAll. start");


            foreach (var maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
            {
                SetMaidStatus(maid);
            }
            /*
            Parallel.ForEach(GameMain.Instance.CharacterMgr.GetStockMaidList(), maid =>
            {
                //MyLog.LogMessageS("MaidStatusUtill.ActiveManSloatCount: " + maid.status.firstName +" , "+ maid.status.lastName);

                SetMaidStatus(maid);
            });
            */
            MyLog.LogDarkBlue("MaidStatusUtill.SetMaidStatusAll. end");
        }

        public static void SetMaidStatus(Maid maid)
        {
            if (maid == null)
            {
                MyLog.LogFatal("MaidStatusUtill.SetMaidStatus:null");
                return;
            }
            MyLog.LogMessage("SetMaidStatus : " + MyUtill.GetMaidFullName(maid));

            maid.status.employmentDay = 1;// 고용기간

            maid.status.baseAppealPoint = 9999;
            maid.status.baseCare = 9999;
            maid.status.baseCharm = 9999;
            maid.status.baseCooking = 9999;
            maid.status.baseDance = 9999;
            maid.status.baseElegance = 9999;
            maid.status.baseHentai = 9999;
            maid.status.baseHousi = 9999;
            maid.status.baseInyoku = 9999;
            maid.status.baseLovely = 9999;
            maid.status.baseMaxHp = 9999;
            maid.status.baseMaxMind = 9999;
            maid.status.baseMaxReason = 9999;
            maid.status.baseMvalue = 9999;
            maid.status.baseReception = 9999;
            maid.status.baseTeachRate = 9999;
            maid.status.baseVocal = 9999;

            maid.status.studyRate = 0;   // 습득율
            maid.status.likability = 999;// 호감도

            maid.status.heroineType = HeroineType.Transfer;// 기본, ? , 이전
            maid.status.relation = Relation.Lover;// 호감도
            maid.status.seikeiken = Seikeiken.Yes_Yes;// 


            // 특징
            MyLog.LogMessage("SetMaidStatus.AddFeature: " + MyUtill.GetMaidFullName(maid));
            foreach (Feature.Data data in Feature.GetAllDatas(true))            
                maid.status.AddFeature(data);
            

            // 성벽
            MyLog.LogMessage("SetMaidStatus.AddPropensity: " + MyUtill.GetMaidFullName(maid));
            foreach (Propensity.Data data in Propensity.GetAllDatas(true))            
                maid.status.AddPropensity(data);

            IEnumerable<SimpleExperienceSystem> expSystems;

            #region YotogiClass

            YotogiClassSystem yotogiClassSystem = maid.status.yotogiClass;
            List<YotogiClass.Data> learnPossibleYotogiClassDatas = yotogiClassSystem.GetLearnPossibleClassDatas(true, AbstractClassData.ClassType.Share | AbstractClassData.ClassType.New | AbstractClassData.ClassType.Old);

            MyLog.LogMessage("SetMaidStatus.YotogiClass learn", MyUtill.GetMaidFullName(maid), learnPossibleYotogiClassDatas.Count);
            foreach (YotogiClass.Data data in learnPossibleYotogiClassDatas)
                maid.status.yotogiClass.Add(data, true, true);            

            var yotogiClassSystems = yotogiClassSystem.GetAllDatas().Values;
            MyLog.LogMessage("SetMaidStatus.YotogiClass expSystem", MyUtill.GetMaidFullName(maid), yotogiClassSystems.Count);
            SetExpMax(yotogiClassSystems.Select(x => x.expSystem));

            #endregion

            #region JobClass

            JobClassSystem jobClassSystem = maid.status.jobClass;
            List<JobClass.Data> learnPossibleClassDatas = jobClassSystem.GetLearnPossibleClassDatas(true, AbstractClassData.ClassType.Share | AbstractClassData.ClassType.New | AbstractClassData.ClassType.Old);

            MyLog.LogMessage("SetMaidStatus.JobClass.learn: " + MyUtill.GetMaidFullName(maid), learnPossibleClassDatas.Count);
            foreach (JobClass.Data data in learnPossibleClassDatas)
                jobClassSystem.Add(data, true, true);

            var jobClassSystems = jobClassSystem.GetAllDatas().Values;// old 데이터 포함
            MyLog.LogMessage("SetMaidStatus.JobClass.expSystem: " + MyUtill.GetMaidFullName(maid), jobClassSystems.Count);
            SetExpMax(jobClassSystems.Select(x => x.expSystem));

            #endregion



            #region 스킬 영역

            List<Skill.Data> learnPossibleSkills = Skill.GetLearnPossibleSkills(maid.status);
            MyLog.LogMessage(".SetMaidStatus.Skill learn : " + MyUtill.GetMaidFullName(maid), learnPossibleSkills.Count);
            foreach (Skill.Data data in learnPossibleSkills)
                maid.status.yotogiSkill.Add(data);            

            MyLog.LogMessage(".SetMaidStatus.Skill expSystem : " + MyUtill.GetMaidFullName(maid), maid.status.yotogiSkill.datas.Count);
            SetExpMax(maid.status.yotogiSkill.datas.GetValueArray().Select(x => x.expSystem));


            List<Skill.Old.Data> learnPossibleOldSkills = Skill.Old.GetLearnPossibleSkills(maid.status);
            MyLog.LogMessage(".SetMaidStatus.Old.Skill learn : " + MyUtill.GetMaidFullName(maid), learnPossibleOldSkills.Count);
            foreach (Skill.Old.Data data in learnPossibleOldSkills)
                maid.status.yotogiSkill.Add(data);            

            MyLog.LogMessage(".SetMaidStatus.Old.Skill expSystem : " + MyUtill.GetMaidFullName(maid), maid.status.yotogiSkill.oldDatas.Count);
            SetExpMax(maid.status.yotogiSkill.oldDatas.GetValueArray().Select(x => x.expSystem));

            #endregion


            MyLog.LogMessage(".SetMaidStatus.WorkData max : " + MyUtill.GetMaidFullName(maid), maid.status.workDatas.Count);
            foreach (WorkData workData in maid.status.workDatas.GetValueArray())
            {
                workData.level = 10;
                workData.playCount = 9999U;
            }


        }

        private static void SetExpMax(IEnumerable<SimpleExperienceSystem> expSystems)
        {
            //int c = 0;
            foreach (var expSystem in expSystems)
            {
                expSystem.SetLevel(expSystem.GetMaxLevel());
             //   c++;
            }
            //MyLog.LogMessage("SetExpMax : " + c);
        }

        internal static void SetAllPlayerStatus()
        {
            MyLog.LogDarkBlue("SetAllPlayerStatus st");

            PlayerStatus.Status status =GameMain.Instance.CharacterMgr.status;
            status.casinoCoin = 999999L;
            status.clubGauge = 100;
            status.clubGrade = 5;
            status.money = 9999999999L;

            ScheduleCSVData.vipFullOpenDay = 0;

            try
            {
                foreach (Trophy.Data item in Trophy.GetAllDatas(false))
                {
                    if (GameMain.Instance.CharacterMgr.status.IsHaveTrophy(item.id))
                    {
                        continue;
                    }

                    MyLog.LogMessage("Trophy"
                    , item.id
                    , item.name
                    , item.type
                    , item.rarity
                    , item.maidPoint
                    , item.infoText
                    , item.bonusText
                    );
                    GameMain.Instance.CharacterMgr.status.AddHaveTrophy(item.id);
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("Trophy:" + e.ToString());
            }


            MyLog.LogDarkBlue("SetAllPlayerStatus ed");
        }

        static bool isRunSetScenarioDataAll = false;

        internal static void SetScenarioDataAll()
        {

            if (!isRunSetScenarioDataAll)
            {
                Task.Factory.StartNew(() =>
                {
                    isRunSetScenarioDataAll = true;
                    MyLog.LogDarkBlue("ScenarioDataUtill.SetScenarioDataAll. start");
                    try
                    {
                        // 병렬 처리
                        foreach (var scenarioData in GameMain.Instance.ScenarioSelectMgr.GetAllScenarioData())
                        {
                            try
                            {
                                // MyLog.LogMessageS(".SetScenarioDataAll:" + scenarioData.ID + " , " + scenarioData.ScenarioScript + " , " + scenarioData.IsPlayable + " , " + scenarioData.Title); ;
                                if (scenarioData.IsPlayable)
                                {
                                    //MyLog.LogMessageS(".m_EventMaid");
                                    foreach (var maid in scenarioData.GetEventMaidList())
                                    {
                                        try
                                        {
                                            bool b = maid.status.GetEventEndFlag(scenarioData.ID);
                                            if (!b)
                                            {
                                                MyLog.LogMessage(".SetEventEndFlagAll:" + scenarioData.ID + " , " + scenarioData.ScenarioScript + " , " + maid.status.firstName + " , " + maid.status.lastName + " , " + b + " , " + scenarioData.ScenarioScript.Contains("_Marriage") + " , " + scenarioData.Title); ;
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
                                        }
                                        catch (Exception e)
                                        {
                                            MyLog.LogError("ScenarioDataUtill.SetScenarioDataAll3 : " + e.ToString());
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                MyLog.LogError("ScenarioDataUtill.SetScenarioDataAll2 : " + e.ToString());
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MyLog.LogError("ScenarioDataUtill.SetScenarioDataAll1 : " + e.ToString());
                    }
                    MyLog.LogDarkBlue("ScenarioDataUtill.SetScenarioDataAll. end");
                    isRunSetScenarioDataAll = false;
                });
            }
        }

        internal static void SetFreeModeItemEverydayAll()
        {
            MyLog.LogDarkBlue("ScenarioDataUtill.SetScenarioAll. start");

            SetEveryday(FreeModeItemEveryday.ScnearioType.Nitijyou);
            MyLog.LogInfo("ScenarioDataUtill.SetScenarioAll. Nitijyou end");

            SetEveryday(FreeModeItemEveryday.ScnearioType.Story);
            MyLog.LogInfo("ScenarioDataUtill.SetScenarioAll. Story emd");

            MyLog.LogDarkBlue("ScenarioDataUtill.SetScenarioAll. end");
        }

        private static void SetEveryday(FreeModeItemEveryday.ScnearioType type)
        {
            string fileName = string.Empty;
            string fixingFlagText;
            if (type == FreeModeItemEveryday.ScnearioType.Nitijyou)
            {
                fileName = "recollection_normal2.nei";
                fixingFlagText = "シーン鑑賞_一般_フラグ_";
            }
            else
            {
                if (type != FreeModeItemEveryday.ScnearioType.Story)
                {
                    return;
                }
                fileName = "recollection_story.nei";
                fixingFlagText = "シーン鑑賞_メイン_フラグ_";
            }
            SetEverydaySub(type, fileName, AbstractFreeModeItem.GameMode.COM3D, fixingFlagText);
            if (GameUty.IsEnabledCompatibilityMode && type == FreeModeItemEveryday.ScnearioType.Nitijyou)
            {
                SetEverydaySub(type, fileName, AbstractFreeModeItem.GameMode.CM3D2, fixingFlagText);
            }
        }

        private static void SetEverydaySub(FreeModeItemEveryday.ScnearioType type, string fileName, AbstractFreeModeItem.GameMode gameMode, string fixingFlagText)
        {
            AFileBase afileBase;
            if (gameMode == AbstractFreeModeItem.GameMode.CM3D2)
            {
                afileBase = GameUty.FileSystemOld.FileOpen(fileName);
            }
            else
            {
                if (gameMode != AbstractFreeModeItem.GameMode.COM3D)
                {
                    return;
                }
                afileBase = GameUty.FileSystem.FileOpen(fileName);
            }
            using (afileBase)
            {
                using (CsvParser csvParser = new CsvParser())
                {
                    bool condition = csvParser.Open(afileBase);
                    NDebug.Assert(condition, fileName + "\nopen failed.");
                    for (int i = 1; i < csvParser.max_cell_y; i++)
                    {
                        if (csvParser.IsCellToExistData(0, i))
                        {
                            int cellAsInteger = csvParser.GetCellAsInteger(0, i);

                            int num = 1;
                            if (gameMode != AbstractFreeModeItem.GameMode.CM3D2 || type != FreeModeItemEveryday.ScnearioType.Nitijyou)
                            {
                                string name = csvParser.GetCellAsString(num++, i);
                                string call_file_name = csvParser.GetCellAsString(num++, i);
                                string check_flag_name = csvParser.GetCellAsString(num++, i);
                                if (gameMode == AbstractFreeModeItem.GameMode.COM3D)
                                {
                                    bool netorare = (csvParser.GetCellAsString(num++, i) == "○");
                                }
                                string info_text = csvParser.GetCellAsString(num++, i);
                                List<string> list = new List<string>();
                                for (int j = 0; j < 9; j++)
                                {
                                    if (csvParser.IsCellToExistData(num, i))
                                    {
                                        list.Add(csvParser.GetCellAsString(num, i));
                                    }
                                    num++;
                                }
                                int subHerionID = csvParser.GetCellAsInteger(num++, i);
                                while (csvParser.IsCellToExistData(num, 0))
                                {
                                    if (csvParser.GetCellAsString(num, i) == "○")
                                    {
                                        string cellAsString = csvParser.GetCellAsString(num, 0);
                                        //Personal.Data data = Personal.GetData(cellAsString);
                                    }
                                    num++;
                                }

                                if (GameMain.Instance.CharacterMgr.status.GetFlag(fixingFlagText + check_flag_name) == 0)
                                {
                                    MyLog.LogMessage("SetEverydaySub.Flag"
                                    , check_flag_name
                                    , call_file_name
                                    , cellAsInteger
                                    , name
                                    , info_text
                                    );
                                    GameMain.Instance.CharacterMgr.status.SetFlag(fixingFlagText + check_flag_name, 1);
                                }

                            }
                        }

                    }
                }
            }
        }

        internal static void SetYotogiAll()
        {
            MyLog.LogDarkBlue("SetAllYotogi START"
);

            foreach (var item in ScheduleCSVData.YotogiData)
            {
                ScheduleCSVData.Yotogi yotogi = item.Value;
                if (yotogi.condManVisibleFlag1.Count > 0)
                {
                    for (int j = 0; j < yotogi.condManVisibleFlag1.Count; j++)
                    {
                        if (GameMain.Instance.CharacterMgr.status.GetFlag(yotogi.condManVisibleFlag1[j]) == 0)
                        {
                            MyLog.LogMessage("SetScenarioAll.yotogi." + yotogi.condManVisibleFlag1[j]);
                            GameMain.Instance.CharacterMgr.status.SetFlag(yotogi.condManVisibleFlag1[j], 1);
                        }
                    }
                }
            }

            MyLog.LogDarkBlue("SetAllYotogi END"
            );
        }

        static bool isSetAllWorkRun = false;

        internal static void SetWorkAll()
        {

            if (!isSetAllWorkRun)
            {
                Task.Factory.StartNew(() =>
                {
                    isSetAllWorkRun = true;
                    MyLog.LogDarkBlue("ScheduleAPIPatch.SetAllWork. start");

                    ReadOnlyDictionary<int, NightWorkState> night_works_state_dic = GameMain.Instance.CharacterMgr.status.night_works_state_dic;
                    MyLog.LogMessage("ScheduleAPIPatch.SetAllWork.night_works_state_dic:" + night_works_state_dic.Count);

                    foreach (var item in night_works_state_dic)
                    {
                        NightWorkState nightWorkState = item.Value;
                        nightWorkState.finish = true;
                    }

                    MyLog.LogMessage("ScheduleAPIPatch.SetAllWork.YotogiData:" + ScheduleCSVData.YotogiData.Values.Count);
                    foreach (Maid maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
                    {
                        MyLog.LogMessage(".SetAllWork.Yotogi:" + MyUtill.GetMaidFullName(maid), ScheduleCSVData.YotogiData.Values.Count);

                        foreach (ScheduleCSVData.Yotogi yotogi in ScheduleCSVData.YotogiData.Values)
                        {
#if DEBUG
                            if (Lilly.isLogOn)
                                MyLog.LogInfo(".SetAllWork:"
                                    + yotogi.id
                                    , yotogi.name
                                    , yotogi.type
                                    , yotogi.yotogiType
                                );
#endif
                            if (DailyMgr.IsLegacy)
                            {
                                maid.status.OldStatus.SetFlag("_PlayedNightWorkId" + yotogi.id, 1);
                            }
                            else
                            {
                                maid.status.SetFlag("_PlayedNightWorkId" + yotogi.id, 1);
                            }
                            if (yotogi.condFlag1.Count > 0)
                            {
                                for (int n = 0; n < yotogi.condFlag1.Count; n++)
                                {
                                    maid.status.SetFlag(yotogi.condFlag1[n], 1);
                                }
                            }
                            if (yotogi.condFlag0.Count > 0)
                            {
                                for (int num = 0; num < yotogi.condFlag0.Count; num++)
                                {
                                    maid.status.SetFlag(yotogi.condFlag0[num], 0);
                                }
                            }
                        }
                        if (DailyMgr.IsLegacy)
                        {
                            maid.status.OldStatus.SetFlag("_PlayedNightWorkVip", 1);
                        }
                        else
                        {
                            maid.status.SetFlag("_PlayedNightWorkVip", 1);
                        }
                    }

                    MyLog.LogDarkBlue("ScheduleAPIPatch.SetAllWork. end");
                    isSetAllWorkRun = false;
                });
            }
        }

    }
}
