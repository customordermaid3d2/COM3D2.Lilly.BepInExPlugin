using COM3D2.Lilly.Plugin.BasePatch;
using COM3D2.Lilly.Plugin.MyGUI;
using COM3D2.Lilly.Plugin.ToolPatch;
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

namespace COM3D2.Lilly.Plugin.MyGUI
{
    public class CheatGUI : GUIVirtualMgr
    {
        public CheatGUI() : base("CheatUtill")
        {

        }

        public override void SetButtonList()
        {
            if (GUILayout.Button("일상 플레그 처리")) CheatUtill.SetWorkAll();
            if (GUILayout.Button("workSuccessLvMax")) ScheduleAPIPatch.SetworkSuccessLvMax();

            GUILayout.Label("시설 관리 관련");
            if (GUILayout.Button("시설 자동 생성 - 랜덤")) FacilityUtill.SetFacilityAll(true);
            if (GUILayout.Button("시설 자동 생성 - 순차")) FacilityUtill.SetFacilityAll(false);
            if (GUILayout.Button("시설 경험치 최대")) FacilityUtill.SetMaxExp();

            GUILayout.Label("모든 메이드 대상");
            if (GUILayout.Button("시나리오 처리 처리")) ScenarioDataUtill.SetScenarioDataAll();
            if (GUILayout.Button("라이프 클리어 처리 ")) EmpireLifeModeManagerBasePatch.SetEmpireLifeModeDataAll();
            if (GUILayout.Button("JobClass 처리")) SkillClassUtill.SetMaidJobClassAll();
            if (GUILayout.Button("YotogiClass 처리")) SkillClassUtill.SetMaidYotogiClassAll();
            if (GUILayout.Button("Skill 처리")) SkillClassUtill.SetMaidSkillAll();
            if (GUILayout.Button("스텟,성텩,특성,업무 처리")) StatusUtill.SetMaidStatusAll();
            if (GUILayout.Button("모든 메이드 에러 플레그 제거")) FlagUtill.RemoveErrFlagAll();
            if (GUILayout.Button("모든 메이드 일반 플레그 제거")) FlagUtill.RemoveFlagAll();
            if (GUILayout.Button("모든 메이드 이벤트 플레그 제거")) FlagUtill.RemoveEventEndFlagAll();
            
            GUILayout.Label("플레이어 대상");
            if (GUILayout.Button("프리 모드 플레그 처리")) PlayerUtill.SetFreeModeItemEverydayAll();
            if (GUILayout.Button("밤시중 플레그 처리")) PlayerUtill.SetYotogiAll(); // player
            if (GUILayout.Button("플레이어 치트 처리")) PlayerUtill.SetAllPlayerStatus();

            GUILayout.Label("메이드 관리에서 사용 SceneMaidManagement");
            GUI.enabled = Lilly.scene.name == "SceneMaidManagement";
            if (GUILayout.Button("선택 메이드 스텟, 스킬, 잡, 클래스 처리")) CheatUtill.SetMaidAll(MaidManagementMainPatch.___select_maid_);
            if (GUILayout.Button("선택 메이드 플레그 제거")) FlagUtill.RemoveEventEndFlag(true);
            if (GUILayout.Button("HeroineType.Original")) CheatUtill.SetHeroineType(HeroineType.Original);
            if (GUILayout.Button("HeroineType.Transfer")) CheatUtill.SetHeroineType(HeroineType.Transfer);
            GUI.enabled = true;
        }




    }
}
