# 뭐지 이건

- 연습용으로 만드는 플러그인
- 내가 만들어놓고 내가 모르기도 함


# 필요한거

- COM3D2.API.dll  https://github.com/JustAGuest4168/COM3D2.OutfitMatchHelper/releases
- System.Threading.dll  어지간하면 탐색기에서 발견 가능. 릴리즈에 첨부하긴 함


# 설치 위치

- COM3D2\BepInEx\plugins


# 구현해본것


## 자동 기능

- 메이드 관리 화면에서 모든 버튼 활성화 - MaidManagementMainPatch
- 메이드 에딧 진입시 모든스텟 적용 - SetMaidStatus
- 프리셋 선택,삭제시 콘솔창에 파일명 출력 - CharacterMgrPatch
- 아이템 / 프리셋 선택시 바뀌는 모드 파일명 출력 - CharacterMgrPatch - PresetSetPostfix2
- 프리셋 로딩시 인셉션 오류뜨면 로딩 안되는 현상 제거 - CharacterMgrPatchBase


## 기어 메뉴 기능

- 현제 보이는 메인 메이드 정보값 출력 (현재는 일반 플레그만)
- 게임 정보 얻기(쓸데가 있나? 분석용) 
- 라이프 모드 시나리오 클리어(모든 메이드와 플레이어 시나리오)
- 밤시중 직업? 클리어
- 모든 메이드 스텟 최대 , 특성 획득 , 스킬 획득 
- 모든 메이드 이벤트 클리어 완료 
- 현제 보이는 메인 메이드 이벤트 클리어 제거
- 모든 메이드 이벤트 클리어 제거
- 자동기능 온오프
- 로그 기록 온오프


# 컴파일 방법

https://cafe.naver.com/kisscm/book5100636/92271


# 구성 

## BasePatch

기본적으로 자동 적용  
망할 메세지박스 안뜨고 로그로 기록  
스카웃모드 버튼 뜸  


## InfoPatch

개발자용 정보성 로그 출력용  
볼려면 콘솔창 필요  


## ToolPatch

치트 관련
내가 만들어놓고 정확한 효과는 나도 모름  
gui버튼 누르면 나오는 버튼에 대충 설명  


## Utill



