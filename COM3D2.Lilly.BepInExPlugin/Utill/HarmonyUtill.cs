using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    public class HarmonyUtill : GUIVirtual
    {
        public static Dictionary<Type, Harmony> harmonys = new Dictionary<Type, Harmony>();

        public static List<Type> infoList = new List<Type>();
        public static List<Type> baseList = new List<Type>();
        public static List<Type> toolList = new List<Type>();

        public static bool isToolPatch = true;

        public static HarmonyUtill Instance;

        public HarmonyUtill() : base()
        {
           Instance = this;
        }

        public static void SetHarmonyListAll()
        {
            SetHarmonyInfoList();
            SetHarmonyBaseList();
            SetHarmonyToolList();
        }

        public static void SetHarmonyToolList()
        {
            toolList.Add(typeof(AbstractFreeModeItemPatch));// 프리 모드에서 모든 이벤트 열기 위한용 오버 플로우
            toolList.Add(typeof(EmpireLifeModeManagerToolPatch));// 회상모드 시나리오 처리용?
            toolList.Add(typeof(MaidManagementMainPatch));//메이드 관리에서 모든 버튼 활성화
            toolList.Add(typeof(SceneEditPatch)); //메이드 에딧 진입시 모든 스텟 적용
            toolList.Add(typeof(ScenarioDataPatch));// 회상모드 시나리오 처리용?
            toolList.Add(typeof(SceneFreeModeSelectManagerPatch));// 회상 모드에서 버튼 활성화용
            toolList.Add(typeof(ScheduleAPIPatch));// 회상모드 시나리오 처리용?
        }

        private static void SetHarmonyBaseList()
        {
            baseList.Add(typeof(EmpireLifeModeManagerPatch));// 회상모드 시나리오 처리용?
            baseList.Add(typeof(NDebugPatch));// 망할 메세지 박스
            toolList.Add(typeof(ScheduleCtrlPatch));// 스케줄 관련
            baseList.Add(typeof(ScoutManagerPatch));// 스카우트 모드의 필요사항 (메이드 수 등등)을 해제.
        }

        private static void SetHarmonyInfoList()
        {
            infoList.Add(typeof(AudioSourceMgrPatch));
            infoList.Add(typeof(BgMgrPatch));
            infoList.Add(typeof(BoneMorph_Patch));//157 임시조치용 메이드 보이스 피치
            infoList.Add(typeof(CameraMainPatch));// 페이드 인 아웃 확인용
            infoList.Add(typeof(CharacterMgrPatch));// 프리셋값 출력용
            infoList.Add(typeof(MaidPatch));// 아이템 장착 확인용
            infoList.Add(typeof(ScheduleMgrPatch));// 스케줄 관리
        }

        public static void SetHarmonyPatchTool()
        {
            if (isToolPatch)
            {
                SetHarmonyUnPatch(toolList);
            }
            else
            {
                SetHarmonyPatch(toolList);
            }
            isToolPatch = !isToolPatch;
        }

        public static void SetHarmonyPatchAll()
        {
            SetHarmonyPatch(infoList);
            SetHarmonyPatch(baseList);
            SetHarmonyPatch(toolList);
        }

        public static void SetHarmonyUnPatchAll()
        {
            SetHarmonyUnPatch(infoList);
            SetHarmonyUnPatch(baseList);
            SetHarmonyUnPatch(toolList);
        }

        public static void SetHarmonyPatch(List<Type> list)
        {
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            // Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),null);// 이건 사용법 모르겠음
            MyLog.LogLine();
            foreach (Type item in list) // 인셉션 나면 중단되는 현상 제거
            {
                SetHarmonyPatch(item);
            }
            MyLog.LogLine();

        }
        
        public static void SetHarmonyPatch(Type item)
        {
            try
            {
                MyLog.LogDarkMagenta("SetHarmonyPatch:" + item.Name);
                if (!harmonys.ContainsKey(item))
                {
                    harmonys.Add(item, Harmony.CreateAndPatchAll(item, null));
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("SetHarmonyPatch:" + e.ToString());
            }
        }

        public static void SetHarmonyUnPatch(List<Type> list)
        {
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            // Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),null);// 이건 사용법 모르겠음

            foreach (Type item in list) // 인셉션 나면 중단되는 현상 제거
            {
                SetHarmonyUnPatch(item);
            }
        }

        public static void SetHarmonyUnPatch(Type item)
        {
            MyLog.LogDarkMagenta("SetHarmonyUnPatch:" + item.Name);
            try
            {
                Harmony harmony;
                if (harmonys.TryGetValue(item, out harmony))
                {
                    harmonys.Remove(item);
                    harmony.UnpatchSelf();
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("SetHarmonyUnPatch:" + e.ToString());
            }
        }

        public static bool GetHarmonyPatchCheck(Type item)
        {
            return harmonys.ContainsKey(item);
        }


        public override void SetButtonList()
        {
            if (GUILayout.Button("SetHarmonyPatchTool 온오프"))
            {
                SetHarmonyPatchTool();
            }

            foreach (var item in toolList)
            {
                bool b = GetHarmonyPatchCheck(item);
                if (GUILayout.Button(item.Name + " , " + b))
                {
                    if (b)
                    {
                        SetHarmonyUnPatch(item);
                    }
                    else
                    {
                        SetHarmonyPatch(item);
                    }
                }
            }
        }
    }
}
