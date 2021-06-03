using COM3D2.Lilly.Plugin.PatchInfo;
using COM3D2.Lilly.Plugin.ToolPatch;
using MaidStatus;
using PlayerStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wf;

namespace COM3D2.Lilly.Plugin.Utill
{
    class CheatUtill
    {
        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
            "CheatUtill"
            , "SetFacilityAllMaid"
        );


        public static void SetMaidAll(Maid maid)
        {
            MyLog.LogMessage(
                "CheatUtill.SetMaidAll st"
                );
            StatusUtill.SetMaidStatus(maid);
            SkillClassUtill.SetMaidYotogiClass(maid);
            SkillClassUtill.SetMaidJobClass(maid);
            SkillClassUtill.SetMaidSkill(maid);
            MyLog.LogMessage(
            "CheatUtill.SetMaidAll end"
            );
        }

        internal static void SetHeroineType(HeroineType transfer)
        {
            MyLog.LogMessage(
            "CheatUtill.SetHeroineType"
            );
            MaidManagementMainPatch.select_maid.status.heroineType = transfer;
        }

        static bool isSetAllWorkRun = false;

        internal static void SetWorkAll()
        {

            if (isSetAllWorkRun)
                return;

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
                    if (maid.status.heroineType == HeroineType.Sub)
                        continue;


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
