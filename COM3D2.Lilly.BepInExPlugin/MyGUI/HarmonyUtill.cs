using BepInEx;
using BepInEx.Configuration;
using COM3D2.Lilly.Plugin.BasePatch;
using COM3D2.Lilly.Plugin.InfoPatch;
using COM3D2.Lilly.Plugin.MyGUI;
using COM3D2.Lilly.Plugin.ToolPatch;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.MyGUI
{
    public class HarmonyUtill : GUIVirtual
    {
        // 하모니 적용되면 여기에 추가할것
        public static Dictionary<Type, Harmony> harmonys = new Dictionary<Type, Harmony>();

        public static List<Type> infoList = new List<Type>();
        public static List<Type> baseList = new List<Type>();
        public static List<Type> toolList = new List<Type>();

        public static bool isToolPatch = true;
        public static bool isInfoPatch = true;
        public static bool isBasePatch = true;

        public static HarmonyUtill Instance;

        public HarmonyUtill() : base("HarmonyUtill")
        {
            Instance = this;            
            SetHarmonyListAll();
        }
        /*
        public  void init()
        {
            MyLog.LogDebug("HarmonyUtill.init");
        }

        public  void Awake()
        {
        }
        */
        public static void SetHarmonyListAll()
        {

            SetHarmonyInfoList();
            SetHarmonyBaseList();
            SetHarmonyToolList();
        }

        private static void SetHarmonyInfoList()
        {
            infoList.Add(typeof(AudioSourceMgrPatch));
            infoList.Add(typeof(BgMgrPatch));
            //infoList.Add(typeof(BoneMorph_Patch));//157 임시조치용 메이드 보이스 피치
            infoList.Add(typeof(CameraMainPatch));// 페이드 인 아웃 확인용
            infoList.Add(typeof(CharacterMgrPatch));// 프리셋값 출력용
            infoList.Add(typeof(FacilityManagerToolPatch));// 회상 모드에서 버튼 활성화용
            infoList.Add(typeof(GameObjectPatch));// 
            infoList.Add(typeof(KagScriptPatch));// 
            infoList.Add(typeof(MaidPatch));// 아이템 장착 확인용
            infoList.Add(typeof(ScheduleMgrPatch));// 스케줄 관리

            infoList.Add(typeof(ScenarioSelectMgrPatch));// 시나리오 정보 출력용 
            infoList.Add(typeof(StatusPatch));// 플레그 관리
            infoList.Add(typeof(TBodyPatch));// 스케줄 관리
            //infoList.Add(typeof(FullBodyIKMgrPatch));// 뼈 관련. 안뜨는거 같음
        }

        public static void SetHarmonyToolList()
        {

            toolList.Add(typeof(AbstractFreeModeItemPatch));// 프리 모드에서 모든 이벤트 열기 위한용 오버 플로우
            //toolList.Add(typeof(ClassDataPatch));// 실시간 클래스 경험치 최대값 설정. 성능 나쁨
            toolList.Add(typeof(EmpireLifeModeManagerToolPatch));// 회상모드 시나리오 처리용?            
            toolList.Add(typeof(GameMainPatch));// 세이브 파일 로딩시 버전 차이 등으로 로딩 못하고 멈출경우 자동으로 타이틀로 돌아감
            toolList.Add(typeof(MaidManagementMainPatch));//메이드 관리에서 모든 버튼 활성화
            toolList.Add(typeof(SceneEditPatch)); //메이드 에딧 진입시 모든 스텟 적용
            toolList.Add(typeof(ScenarioDataPatch));// 회상모드 시나리오 처리용?
            toolList.Add(typeof(SceneFreeModeSelectManagerPatch));// 회상 모드에서 버튼 활성화용
            toolList.Add(typeof(ScheduleAPIPatch));// 회상모드 시나리오 처리용?
            toolList.Add(typeof(SceneMgrPatch));// 커뮤니티 자동적용 포함되있음
                                                //toolList.Add(typeof(NPRShaderPatch));// 회상모드 시나리오 처리용?
                                                //toolList.Add(typeof(UnityInjectorLoaderPatch));// 회상모드 시나리오 처리용?
                                                //toolList.Add(typeof(ScheduleMaxPatch));// 슬롯 최대 늘리기 실패
        }

        private static void SetHarmonyBaseList()
        {
            baseList.Add(typeof(CharacterMgrPatchBase));// 스카우트 모드의 필요사항 (메이드 수 등등)을 해제.
            baseList.Add(typeof(EmpireLifeModeManagerBasePatch));// 회상모드 시나리오 처리용?
            //baseList.Add(typeof(GameUtyPatch));// mod reflash. 필요 없음
            baseList.Add(typeof(NDebugPatch));// 망할 메세지 박스
            baseList.Add(typeof(ProfileCtrlPatch));// 스케줄 관련
            baseList.Add(typeof(ScheduleCtrlPatch));// 스케줄 관련
            baseList.Add(typeof(ScoutManagerPatch));// 스카우트 모드의 필요사항 (메이드 수 등등)을 해제.
            //baseList.Add(typeof(ScheduleTaskCtrlPatch));// 스카우트 모드의 필요사항 (메이드 수 등등)을 해제.
        }


        public static void SetHarmonyPatchAll()
        {
            SetHarmonyPatchAll(infoList);
            SetHarmonyPatchAll(baseList);
            SetHarmonyPatchAll(toolList);
        }

        public static void SetHarmonyUnPatchAll()
        {
            SetHarmonyUnPatchAll(infoList);
            SetHarmonyUnPatchAll(baseList);
            SetHarmonyUnPatchAll(toolList);
        }

        public static void SetHarmonyPatchAll(List<Type> list)
        {
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            // Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),null);// 이건 사용법 모르겠음
            MyLog.LogDarkYellow();
            foreach (Type item in list) // 인셉션 나면 중단되는 현상 제거
            {
                ConfigEntry<bool> t = customFile.Bind("HarmonyUtill",
                    item.Name,
                    true);                
                MyLog.LogDarkMagenta("SetHarmonyPatch"
                    , item.Name
                    , t.Value
                    );
                if (t.Value)
                {
                    SetHarmonyPatch(item);
                }
            }
            MyLog.LogDarkYellow();
        }

        public static void SetHarmonyUnPatchAll(List<Type> list)
        {
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            // Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),null);// 이건 사용법 모르겠음

            foreach (Type item in list) // 인셉션 나면 중단되는 현상 제거
            {
                SetHarmonyUnPatchAll(item);
            }
        }

        public static void SetHarmonyUnPatchAll(Type item)
        {
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

        public static void SetHarmonyPatch(ref bool isPatch, List<Type> list)
        {
            if (isPatch)
            {
                SetHarmonyUnPatch(list);
            }
            else
            {
                SetHarmonyPatch(list);
            }
            isPatch = !isPatch;
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
            ConfigEntry<bool> t = customFile.Bind(
                Instance.name,
                item.Name,
                true
                );
            t.Value = true;
            MyLog.LogDarkMagenta("SetHarmonyPatch"
                , item.Name
                , t.Value
                );
            try
            {
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
            ConfigEntry<bool> t = customFile.Bind(
                Instance.name,
                item.Name,
                false);
                t.Value = false;
            MyLog.LogDarkMagenta("SetHarmonyUnPatch"
                , item.Name
                , t.Value
                );
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

        private Vector2 scrollPos = Vector2.zero;

        public override void SetButtonList()
        {

            scrollPos = GUILayout.BeginScrollView(scrollPos);

            GUILayout.Label("toolList 대상");
            SetButtonList1("toolList 온오프 ",ref isToolPatch, toolList);

            GUILayout.Label("infoList 대상");
            SetButtonList1("infoList 온오프 ", ref isInfoPatch, infoList);

            GUILayout.Label("baseList 대상");
            SetButtonList1("baseList 온오프 ", ref isBasePatch, baseList);

            GUILayout.EndScrollView();


        }

        private static void SetButtonList1(string text, ref bool isPatch, List<Type> list)
        {
            if (GUILayout.Button(text+ isPatch))
            {
                SetHarmonyPatch(ref isPatch, list);
            }

            foreach (var item in list)
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
