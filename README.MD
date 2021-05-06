# 뭐지 이건

- 연습용으로 만드는 플러그인
- 내가 만들어놓고 내가 모르기도 함


# 필요한거

- COM3D2.API.dll  https://github.com/DeathWeasel1337/COM3D2_Plugins
- System.Threading.dll  어지간하면 탐색기에서 발견 가능. https://github.com/customordermaid3d2/COM3D2.Lilly.BepInExPlugin/releases/download/210409/System.Threading.dll


# 설치 위치

- COM3D2\BepInEx\plugins


# 구현해본것

## 자동 적용

- 메이드 고용시 프리셋 자동 적용 

# 기어 메뉴 기능

## MaidEditGui

- 기존 메이드를 에딧 진입후 완료시키면, 신규 고용 이벤이나 이적 이벤트 뜸. SceneEditPatch 필요   


## easyUtill

- 모드 파일 다시 읽기
- 메이드 추가(성격 랜덤, 프리셋 폴더안의 프리셋 자동 적용, 수치값 최대)
#### 프리셋 적용
#### 메이드 고용 진입시 자동 적용 
MaidManagementMainPatch.newMaidSetting SceneEditPatch.OnCompleteFadeIn 필요
- GP01FBFaceEyeRandomOnOff : GP-01FB 사용시 눈 랜덤 지정 가능  
- SetMaidStatus : 스텟 최대치


## CheatUtill

작성중  

## HarmonyUtill

특정 장면이나 기능 사용시 자동으로 적용  
설정파일로도 저장됨 : COM3D2\BepInEx\config\COM3D2.Lilly.Plugin.cfg  


### BasePatch

자동 적용할거 모음  

- CharacterMgrPatchBase : 프리셋 로딩시 인셉션 오류뜨면 로딩 안되는 현상 제거  
- EmpireLifeModeManagerPatch : 뭐였지  
- GameUtyPatch : 뭐였지 현제 미적용  
- NDebugPatch : 망할 메세지 박스 안뜨게 해줌  
- ScheduleCtrlPatch : 뭔가 하려다 만듯?  
- ScoutManagerPatch : 스카웃 모드 바로 사용 가능. 원래 다른 플러그인거 그대로 가져온거  


### InfoPatch

정보성 출력 모음  

- AudioSourceMgrPatch : 재생한 소리파일 출력  
- BgMgrPatch : 배경 출력?  
- BoneMorph_Patch : 본 테이터 분셕용인데 미사용  
- CameraMainPatch : 페이드 여부 출력  
- CharacterMgrPatch : 아이템 / 프리셋 선택시 바뀌는 프리셋 및 모드 파일명 출력  
- FullBodyIKMgrPatch : 미사용?  
- MaidPatch : 메이드 파츠 정보 관련?  
- ScheduleMgrPatch : 스케줄 관리 관련?  
- TBodyPatch : 작동 안하는듯?  


### ToolPatch

치트 관련 모음  

- ClassDataPatch : 클래스 정보 불러올때마다 밤시중과 업무 클래스 레벨 최대치 처리.  
  이거 쓰는것보단 easyUtill 에 있는걸  쓰는게 성능에 좋음  
- EmpireLifeModeManagerToolPatch : 회상 모드에서 라이프 선택 가능. 메이드 피들러에서 가져옴  
- GameMainPatch : 오류있는 세이브 파일 로딩시 겜이 멈추면 자동으로 타이틀로 이동
- MaidManagementMainPatch : 메이드 관리 화면에서 모든 버튼 활성화  
- ScenarioDataPatch : 회상모드 에서 회상 가능?  메이드 피들러에서 가져옴  
- SceneEditPatch : 메이드 에딧 진입시 모든 스텟 적용 (SetMaidStatus)  
- SceneFreeModeSelectManagerPatch : 회상모드에서 H이벤트 활성?  
- ScheduleAPIPatch : 스케줄 관리에서 모든 스케줄 사용 가능? 비추천?  
- SceneMgrPatch : 커뮤니티할 메이드 랜덤 선택


## InfoUtill

작성중  

## 미정리

- 현제 보이는 메인 메이드 정보값 출력 (현재는 일반 플레그만)
- 게임 정보 얻기(쓸데가 있나? 분석용) 
- 라이프 모드 시나리오 클리어(모든 메이드와 플레이어 시나리오)
- 밤시중 직업? 클리어
- 모든 메이드 스텟 최대 , 특성 획득 , 스킬 획득 
- 모든 메이드 이벤트 클리어 완료 
- 현제 보이는 메인 메이드 이벤트 클리어 제거
- 모든 메이드 이벤트 클리어 제거
- modreflash 기능을 스레드로 처리 - EasyUtill
- 자동기능 온오프
- 로그 기록 온오프


# 컴파일 방법 (직접 수정해보고 싶을경우)

https://cafe.naver.com/kisscm/book5100636/92271


