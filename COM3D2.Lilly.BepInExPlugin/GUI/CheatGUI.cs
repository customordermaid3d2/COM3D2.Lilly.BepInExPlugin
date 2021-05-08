using COM3D2.Lilly.Plugin.BasePatch;
using COM3D2.Lilly.Plugin.Utill;
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
            if (GUILayout.Button("일상 플레그 처리")) CheatGUI.SetWorkAll();
            
            GUILayout.Label("모든 메이드 대상");
            if (GUILayout.Button("시나리오 처리 처리")) ScenarioDataUtill.SetScenarioDataAll();
            if (GUILayout.Button("라이프 클리어 처리 ")) EmpireLifeModeManagerBasePatch.SetEmpireLifeModeDataAll();
            if (GUILayout.Button("JobClass 처리")) SkillClassUtill.SetMaidJobClassAll();
            if (GUILayout.Button("YotogiClass 처리")) SkillClassUtill.SetMaidYotogiClassAll();
            if (GUILayout.Button("Skill 처리")) SkillClassUtill.SetMaidSkillAll();
            if (GUILayout.Button("스텟,성텩,특성,업무 처리")) StatusUtill.SetMaidStatusAll();
            if (GUILayout.Button("모든 메이드 이벤트 플레그 제거")) FlagUtill.RemoveEventEndFlagAll();
            
            GUILayout.Label("플레이어 대상");
            if (GUILayout.Button("프리 모드 플레그 처리")) PlayerUtill.SetFreeModeItemEverydayAll();
            if (GUILayout.Button("밤시중 플레그 처리")) PlayerUtill.SetYotogiAll(); // player
            if (GUILayout.Button("플레이어 치트 처리")) PlayerUtill.SetAllPlayerStatus();

            GUILayout.Label("메이드 관리에서 사용 SceneMaidManagement");
            GUI.enabled = Lilly.scene.name == "SceneMaidManagement";
            if (GUILayout.Button("선택 메이드 스텟, 스킬, 잡, 클래스 처리")) CheatUtill.SetMaidAll(MaidManagementMainPatch.___select_maid_);
            if (GUILayout.Button("선택 메이드 플레그 제거")) FlagUtill.RemoveEventEndFlag(true);
            GUI.enabled = true;
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
