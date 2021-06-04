using BepInEx;
using BepInEx.Configuration;
using COM3D2.Lilly.Plugin.BasePatch;
using COM3D2.Lilly.Plugin.PatchInfo;
using COM3D2.Lilly.Plugin.GUIMgr;
using COM3D2.Lilly.Plugin.ToolPatch;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using COM3D2.Lilly.Plugin.PatchTool;
using COM3D2.Lilly.Plugin.PatchBase;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
    public class GUIHarmony : GUIMgr
    {
        public struct SHarmony
        {
            public Type type;
            public bool patch;

            public SHarmony(Type type, bool patch=true)
            {
                this.type = type;
                this.patch = patch;
            }
        }

        // 하모니 적용되면 여기에 추가할것
        public static Dictionary<Type, Harmony> harmonys = new Dictionary<Type, Harmony>();

        public static List<SHarmony> infoList = new List<SHarmony>();
        public static List<SHarmony> baseList = new List<SHarmony>();
        public static List<SHarmony> toolList = new List<SHarmony>();

        public static bool isToolPatch = true;
        public static bool isInfoPatch = true;
        public static bool isBasePatch = true;

        public static GUIHarmony Instance;

        public GUIHarmony() : base("HarmonyUtill")
        {
            Instance = this;                       
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
        new internal static void init()
        {

            SetHarmonyInfoList();
            SetHarmonyBaseList();
            SetHarmonyToolList();
        }

        private static void SetHarmonyInfoList()
        {
            infoList.Add(new(typeof(AudioSourceMgrPatch)));
            infoList.Add(new(typeof(BgMgrPatch)));
            //infoList.Add(typeof(BoneMorph_Patch));//157 임시조치용 메이드 보이스 피치
            infoList.Add(new(typeof(CameraMainPatch)));// 페이드 인 아웃 확인용
            infoList.Add(new(typeof(CharacterMgrPatch)));// 프리셋값 출력용
            infoList.Add(new(typeof(FacilityManagerPatch)));// 회상 모드에서 버튼 활성화용            
            infoList.Add(new(typeof(GameObjectPatch),false));// 
            infoList.Add(new(typeof(KagPatch)));// 
            infoList.Add(new(typeof(MaidPatch)));// 아이템 장착 확인용
            infoList.Add(new(typeof(ScreenPatch)));//
            infoList.Add(new(typeof(SoundMgrPatch)));// 스케줄 관리
            infoList.Add(new(typeof(ScheduleAPIInfoPatch)));// 스케줄 관리
            infoList.Add(new(typeof(ScheduleMgrPatch)));// 스케줄 관리
            infoList.Add(new(typeof(ScenarioSelectMgrPatch)));// 시나리오 정보 출력용 
            infoList.Add(new(typeof(StatusPatch),false));// 플레그 관리
            infoList.Add(new(typeof(TBodyPatch),false));// 바디 파라미터값 출력 관련
            //infoList.Add(typeof(FullBodyIKMgrPatch));// 뼈 관련. 안뜨는거 같음
        }

        public static void SetHarmonyToolList()
        {

            toolList.Add(new(typeof(AbstractFreeModeItemPatch)));// 프리 모드에서 모든 이벤트 열기 위한용 오버 플로우
            toolList.Add(new(typeof(ClassDataPatch),false));// 실시간 클래스 경험치 최대값 설정. 성능 나쁨
            toolList.Add(new(typeof(EmpireLifeModeManagerToolPatch)));// 회상모드 시나리오 처리용?            
            toolList.Add(new(typeof(GameMainPatch)));// 세이브 파일 로딩시 버전 차이 등으로 로딩 못하고 멈출경우 자동으로 타이틀로 돌아감
            toolList.Add(new(typeof(MaidManagementMainPatch)));//메이드 관리에서 모든 버튼 활성화
            toolList.Add(new(typeof(SceneEditPatch))); //메이드 에딧 진입시 모든 스텟 적용
            toolList.Add(new(typeof(ScenarioDataPatch)));// 회상모드 시나리오 처리용?
            toolList.Add(new(typeof(SceneFreeModeSelectManagerPatch)));// 회상 모드에서 버튼 활성화용
            toolList.Add(new(typeof(ScheduleCalcAPIPatch)));// 커뮤니티 자동적용 포함되있음
            toolList.Add(new(typeof(ScheduleAPIPatch)));// 회상모드 시나리오 처리용?
            toolList.Add(new(typeof(SceneMgrPatch)));// 커뮤니티 자동적용 포함되있음
            toolList.Add(new(typeof(StatusToolPatch)));// 
                                                //toolList.Add(typeof(NPRShaderPatch));// 회상모드 시나리오 처리용?
                                                //toolList.Add(typeof(UnityInjectorLoaderPatch));// 회상모드 시나리오 처리용?
                                                //toolList.Add(typeof(ScheduleMaxPatch));// 슬롯 최대 늘리기 실패
            toolList.Add(new(typeof(WorkResultScenePatch)));// 커뮤니티 자동적용 포함되있음
        }

        private static void SetHarmonyBaseList()
        {
            baseList.Add(new(typeof(CharacterMgrPatchBase)));// 스카우트 모드의 필요사항 (메이드 수 등등)을 해제.
            baseList.Add(new(typeof(EmpireLifeModeManagerBasePatch)));// 회상모드 시나리오 처리용?
            //baseList.Add(typeof(GameUtyPatch));// mod reflash. 필요 없음
            baseList.Add(new(typeof(NDebugPatch)));// 망할 메세지 박스
            baseList.Add(new(typeof(ProfileCtrlPatch)));// 스케줄 관련
            baseList.Add(new(typeof(SaveAndLoadMgrPatch)));// 망할 메세지 박스
            baseList.Add(new(typeof(BasePanelMgrPatch)));// 망할 메세지 박스
            baseList.Add(new(typeof(ScheduleCtrlPatch)));// 스케줄 관련
            baseList.Add(new(typeof(ScheduleScenePatch)));// 스케줄 관련
            baseList.Add(new(typeof(ScoutManagerPatch)));// 스카우트 모드의 필요사항 (메이드 수 등등)을 해제.
            baseList.Add(new(typeof(ScheduleTaskCtrlPatch),false));// 스카우트 모드의 필요사항 (메이드 수 등등)을 해제.
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

        public static void SetHarmonyPatchAll(List<SHarmony> list)
        {
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            // Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),null);// 이건 사용법 모르겠음
            MyLog.LogDarkYellow();
            foreach (SHarmony item in list) // 인셉션 나면 중단되는 현상 제거
            {
                ConfigEntry<bool> t = customFile.Bind("HarmonyUtill",
                    item.type.Name,
                    item.patch);                
                MyLog.LogDarkMagenta("SetHarmonyPatch"
                    , item.type.Name
                    , t.Value
                    );
                if (t.Value)
                {
                    SetHarmonyPatch(item);
                }
            }
            MyLog.LogDarkYellow();
        }

        public static void SetHarmonyUnPatchAll(List<SHarmony> list)
        {
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            // Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),null);// 이건 사용법 모르겠음

            foreach (SHarmony item in list) // 인셉션 나면 중단되는 현상 제거
            {
                SetHarmonyUnPatchAll(item);
            }
        }

        public static void SetHarmonyUnPatchAll(SHarmony item)
        {
            try
            {
                Harmony harmony;
                if (harmonys.TryGetValue(item.type, out harmony))
                {
                    harmonys.Remove(item.type);
                    harmony.UnpatchSelf();
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("SetHarmonyUnPatch:" + e.ToString());
            }
        }

        public static void SetHarmonyPatch(ref bool isPatch, List<SHarmony> list)
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

        public static void SetHarmonyPatch(List<SHarmony> list)
        {
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            // Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),null);// 이건 사용법 모르겠음
            MyLog.LogLine();
            foreach (SHarmony item in list) // 인셉션 나면 중단되는 현상 제거
            {
                SetHarmonyPatch(item);
            }
            MyLog.LogLine();
        }
        
        public static void SetHarmonyPatch(SHarmony item)
        {
            ConfigEntry<bool> t = customFile.Bind(
                Instance.nameGUI,
                item.type.Name,
                true
                );
            t.Value = true;
            MyLog.LogDarkMagenta("SetHarmonyPatch"
                , item.type.Name
                , t.Value
                );
            try
            {
                if (!harmonys.ContainsKey(item.type))
                {
                    harmonys.Add(item.type, Harmony.CreateAndPatchAll(item.type, null));
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("SetHarmonyPatch:" + e.ToString());
            }
        }

        public static void SetHarmonyUnPatch(List<SHarmony> list)
        {
            // https://github.com/BepInEx/HarmonyX/wiki/Patching-with-Harmony
            // 이거로 원본 메소드에 연결시켜줌. 이게 일종의 해킹

            // Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(),null);// 이건 사용법 모르겠음

            foreach (SHarmony item in list) // 인셉션 나면 중단되는 현상 제거
            {
                SetHarmonyUnPatch(item);
            }
        }

        public static void SetHarmonyUnPatch(SHarmony item)
        {
            ConfigEntry<bool> t = customFile.Bind(
                Instance.nameGUI,
                item.type.Name,
                item.patch);
                t.Value = false;
            MyLog.LogDarkMagenta("SetHarmonyUnPatch"
                , item.type.Name
                , t.Value
                );
            try
            {
                Harmony harmony;
                if (harmonys.TryGetValue(item.type, out harmony))
                {
                    harmonys.Remove(item.type);
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

        public override void SetBody()
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

        private static void SetButtonList1(string text, ref bool isPatch, List<SHarmony> list)
        {
            if (GUILayout.Button(text+ isPatch))
            {
                SetHarmonyPatch(ref isPatch, list);
            }

            foreach (var item in list)
            {
                bool b = GetHarmonyPatchCheck(item.type);
                if (GUILayout.Button(item.type.Name + " , " + b))
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
