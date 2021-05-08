using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Utill
{
    class PlayerUtill
    {
        internal static void SetAllPlayerStatus()
        {
            MyLog.LogDarkBlue("SetAllPlayerStatus st");

            PlayerStatus.Status status = GameMain.Instance.CharacterMgr.status;
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


        internal static void SetYotogiAll()
        {
            MyLog.LogDarkBlue("SetAllYotogi START"
);

            foreach (var item in ScheduleCSVData.YotogiData)
            {
                ScheduleCSVData.Yotogi yotogi = item.Value;
                MyLog.LogMessage("SetScenarioAll.YotogiData", item.Key, yotogi.yotogiType);
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

    }
}
