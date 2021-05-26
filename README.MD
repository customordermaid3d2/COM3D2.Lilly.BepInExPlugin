# 뭐지 이건

연습용으로 만드는 플러그인  
내가 만들어놓고 내가 모르기도 함  
기어 아이콘에 내 아이콘이 여러개 생김  
그외에 게임 기능 작동시 자동으로 적용되는것도 있음  
하모니 유틸에서 끄고 키는게 가능  
설정한 값 대부분은 설정파일에 저장됨  
너무 많아서 언제 다 소개하지...  

https://youtu.be/CHb02i-M418


# 필요한거

- BepInEx https://github.com/BepInEx/BepInEx  
- SybarisLoader https://github.com/BepInEx/BepInEx.SybarisLoader.Patcher  
- UnityInjectorLoader https://github.com/BepInEx/BepInEx.UnityInjectorLoader  

- COM3D2.API.dll  https://github.com/DeathWeasel1337/COM3D2_Plugins/releases/download/v3/COM3D2.API.v1.0.zip
- System.Threading.dll  어지간하면 탐색기에서 발견 가능. https://github.com/customordermaid3d2/COM3D2.Lilly.BepInExPlugin/releases/download/210409/System.Threading.dll


# 설치 위치

- COM3D2\BepInEx\plugins


# 구현해본것

## 자동 적용

- 메이드 고용시 프리셋 자동 적용 


# 기어 메뉴 기능


## GUIInfo

정보성 데이터가 로그창에 출력됨  

- 게임 정보 얻기 : 게임 버전 등을 표시
- Facility 정보 얻기 : 아마 시설 정보였을거. 현제 버그 터졌는데 사용을 잘 안하니 뭐  
- Scene 정보 얻기 : 현제 장면 정보. 원래 전체 장면 목록들 출력하는 목표로 만들려고 했던거 같은데..  
- 정보 얻기 바디 관련 : 본 목록이랑 아이템 슬롯명 출력용?  
- 정보 얻기 플레이어 관련 : 오픈된 드레스 목록? 플레이어 플레그 목록, 스테이지 목록, 룸 데이터는 뭐지, 트로피 목록
- 정보 얻기 메이드 관련 : 어우쉬 미친듯 많네, 이거 콘솔창에 다 밀려서 올라가버리니 로그 파일 열어서 봐야할듯  메이드의 성격  
  메이드의 특징  
  메이드의 성벽  
  메이드 관리 - 클래스  
  메이드 관리 - 밤시중 정보  
  스킬 목록이랑 해당 스킬의 명령 목록  
  회상모드의 라이프에서 사용되는 시나리오 포함  
- ScenarioSelectMgrPatch 관련 : 버그로 실행 불가.  시나리오 데이터 관련정보, 플레이 가능한지 등등 표시하기 위한건데 개선해야겠네  

- 정보 얻기 메이드 플레그 관련 : 메이드 관리에서 선택한 메이드의 플레그 목록 출력


## GUIMaidEdit


- 메이드 추가(성격 랜덤, 프리셋 폴더안의 프리셋 자동 적용, 수치값 최대)  
  Personal Rand, Contract , Heroine 으로 랜덤을 끄고 직접 선택도 가능  
  

- 메이드 고용 진입시 자동 적용 
  기존 메이드를 에딧 진입후 완료시키면, 신규 고용 이벤이나 이적 이벤트 뜸. SceneEditPatch 필요   
  MaidManagementMainPatch.newMaidSetting SceneEditPatch.OnCompleteFadeIn 필요
  
- GP01FBFaceEyeRandomOnOff : 이건 작동 안하는거엿나, GP-01FB 사용시 눈 랜덤 지정 가능  
- GP-01FB Face Eye Random : 메이드 에딧에서 GP-01FB 사용시 눈 랜덤 지정 가능  


## GUIRndPreset  

프리셋 랜덤 적용 관련 기능.  
저장할때 옷이나 바디, 둘다 정용하도록 설정했더라도  
프리셋이 옷과 바디 둘다 저장되있기에  
해당 메이드에게 현재 가진 프리셋중 월하는 설정값을 설정하여 적용


## GUIFlagMaid

메이드 관리에서  
해당 메이드의 플레그를 편집할수 있음  


## GUIFlagPlayer

플레이어 플레그 관리  


## GUIEasy

- 모드 파일 다시 읽기. 불러올때마다 중복출력되는게 발생해서 개선이좀 필요함. 원래 다른 플러그인 개발자거  

- 스케줄 관리의 커뮤니티 랜덤 선택  
  하모니에서 SceneMgrPatch기능으로 낮 장면 진입시마다 랜덤 선택되는기능도 있음   


- 슬롯에 메이드 자동 배치  
- 시설에 메이드 자동 배치  
  https://youtu.be/CHb02i-M418  


## GUICheat

- 일상 관련 플레그 모두 획득(스토리 꼬일수 있음)  

- 시설 자동 배치 : 시설 관리에 설치 가능한 시설들을 하나식 모두 변경.  
  돈도 안들고, 단지 기존 시설에 배치한 메이드들 다 해제됨...아 젠장  
  https://youtu.be/WNl3sTpzCK0  

- 시나리오 획득 : 모든 메이드에게 획득 가능한 시나리오 획득  

- 라이프 클리어 처리 :  라이프 모드에서 사용하는 메이드 스토리 전부 획득 처리  

중간에 메이드 관리에서 보여줬던거 다 넘어가고  

- 플레그 제거 처리 : 이건 첨 사용시 아무런 반영이 안되고  
  BepInEx\config 안에 eventEndFlags.csv,flags.csv,oldflags.csv 파일이 생김  
  파일 열어보면 "//#"는 주석으로 인식, 칼럼갯수는 2개로 맞춰야 하고 형식 맞출것  
  뒤 int숫자나 bool값은 현제 쓰지 않지만 적어줘야함  
  지금은 플레그 제거만 가능  

- 플레이어 플레그 획득 처리  
- 플레이어 치트 처리  

- 메이드 관리에서 해당 메이드만 치트 처리  
- 해당메이드를 이적메이드인지, 단순히 신규생성한 메이드인지 변경 처리  

## GUIPlugin

베핀 플러그인 껏다 킬수 있게 만든거긴 한데 적용이 잘 안됨  


## GUIOnOff

콘솔창에 로그 기록되는거 켜고.끄기 위한거  
하지만 기능 그고 키는것도 적용중  
각 클래스마다 천천히 적용중  


### ScheduleAPIPatch

스케줄 관리 관련

- VisibleNightWork : 모든 밤시중 보여주기 가능. 버그 발생하니 그냥 끄자.
- EnableNightWork : 모든 밤시중 선택 가능. 버그 발생하니 그냥 끄자.
- EnableNoonWork : 모든 업무 선택 가능. 버그 발생하니 그냥 끄자.


## HarmonyUtill

특정 장면이나 기능 사용시 자동으로 적용  
설정파일로도 저장됨 : COM3D2\BepInEx\config\COM3D2.Lilly.Plugin.cfg  
각각의 최상단 "온오프" 버튼 눌르면 일괄 꺼지고 켜짐  
GUI 일부 기능은 겜 구동시부터 켜져 있어야 할수도 있음  
지금부턴 하모니 적용시킨거 소스 봐가며 설명할거


### BasePatch

자동 적용할거 모음  

- CharacterMgrPatchBase : 프리셋 로딩시 인셉션 오류뜨면 로딩 안되는 현상 제거  

- EmpireLifeModeManagerPatch  
  회상모드 스토리 치트 적용시키기위해 필요.  
  겜 구동시 켜져있어야함  

- GameUtyPatch : 뭐였지 현제 미적용  

- NDebugPatch : 망할 메세지 박스 안뜨게 해줌  

- ProfileCtrlPatch  
  성격값 가져오는용인데 필요 없어서 꺼야할듯  

- ScheduleCtrlPatch  
  스케줄에 메이드들 설정한거 일괄 해제 구현하려다 실패한거  

- ScoutManagerPatch  
  스카웃 모드 바로 사용 가능. 원래 다른 플러그인거 그대로 가져온거  


### InfoPatch

정보성 출력 모음  

- AudioSourceMgrPatch : 재생한 소리파일 출력  

- BgMgrPatch : 배경 출력?  

- BoneMorph_Patch : 본 테이터 분셕용인데 미사용  

- CameraMainPatch : 페이드 여부 출력  

- CharacterMgrPatch : 아이템 / 프리셋 선택시 바뀌는 프리셋 및 모드 파일명 출력  

- FacilityManagerToolPatch  
  시설관리 치트용으로 만든건데, 굳이 하모니로 할 필요가 없어서 필용 없음  
  현제는 로그 출력용  

- GameObjectPatch : 오브젝트 이름 출력용. 모든 오브젝트가 다 나오니 그냥 끌것  

- KagScriptPatch : 스크립트 읽고 어느 명령어 부분으로 이동했는지 정보 출력용  
  해당 클레스에 코딩으로 수동 이동하는 방법 대충 적혀 있음  

- MaidPatch : 메이드 슬롯 정보 관련

- ScheduleMgrPatch : 스케줄 관리 관련 출력용  

- ScenarioSelectMgrPatch : 시나리오 정보 출력용. 사실상 작동안하는 경우라 개선 필요  

- StatusPatch : 플레그 정보 출력  

- TBodyPatch : TBody의 모양이 변경되는등의 수치값 표시  


### ToolPatch

치트 관련 모음  

- AbstractFreeModeItemPatch : 프리 모드에서 모든 이벤트 열기 위한용
  
- ClassDataPatch : 클래스 정보 불러올때마다 밤시중과 업무 클래스 레벨 최대치 처리.  
  이거 쓰는것보단 easyUtill 에 있는걸  쓰는게 성능에 좋음  

- EmpireLifeModeManagerToolPatch : 회상 모드에서 라이프 선택 가능. 메이드 피들러에서 가져옴  

- GameMainPatch : 오류있는 세이브 파일 로딩시 겜이 멈추면 자동으로 타이틀로 이동  

- MaidManagementMainPatch : 메이드 관리 화면에서 모든 버튼 활성화  

- SceneEditPatch : 메이드 에딧 진입시 모든 스텟 적용 (SetMaidStatus)  

- ScenarioDataPatch : 회상모드 시나리오 처리용. 이거 자신 성격 상관없이 다 뜨던걸로 기억  

- SceneFreeModeSelectManagerPatch : 회상모드에서 H이벤트 활성?  

- ScheduleAPIPatch : 스케줄 관리에서 모든 스케줄 사용 가능.  
  사용 못하는것도 선택 가능해지니 사용하지 말것  

- SceneMgrPatch : 장면 전환 관련인데. 여기다라 커뮤니티 랜덤 자동적용    


# 컴파일 방법 (직접 수정해보고 싶을경우)

https://cafe.naver.com/kisscm/book5100636/92271


