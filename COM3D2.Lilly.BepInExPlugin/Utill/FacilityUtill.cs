using COM3D2.Lilly.Plugin.PatchInfo;
using COM3D2.Lilly.Plugin.Utill;
using FacilityFlag;
using HarmonyLib;
using PlayerStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wf;

namespace COM3D2.Lilly.Plugin.Utill
{
    class FacilityUtill
    {
        // FacilityManager      

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "FacilityUtill"
        , "SetFacilityAllMaid"
        , "SetFacilityAll"
        );

        // 작동 안함
        //[HarmonyPostfix, HarmonyPatch(typeof(GameMain), "OnInitialize")]
        //public static void OnInitialize()
        //{
        //    MyLog.LogMessage(
        //        "FacilityManagerToolPatch.OnInitialize"
        //        ,  FacilityManager.FacilityCountMax
        //    );
        //    FacilityManager = GameMain.Instance.FacilityMgr;
        //}

        public static List<Facility.FacilityStatus> listbak;
        public static List<Facility.FacilityStatus> list;

        public static void GetAchievement()
        {
            //m_FacilityAchievementList
        }

        public static void SetMaxExp()
        {
            if (FacilityManagerPatch.FacilityExpArray == null)
            {               
                //FacilityManagerPatch.m_FacilityExpArray= AccessTools.Field(typeof(FacilityManager), "m_FacilityAchievementList");
                MyLog.LogWarning(
                    "FacilityManagerPatch.m_FacilityExpArray null"
                    , "게임 실행전 FacilityManagerPatch가 이미 켜져 있어야함"
                );
                return;
            }
            foreach (KeyValuePair<int, SimpleExperienceSystem> item in FacilityManagerPatch.FacilityExpArray.Copy())
            {
                SimpleExperienceSystem experienceSystem =item.Value;
                MyLog.LogMessage(
                    "SetMaxExp"
                    , item.Key                    
                    , experienceSystem.GetCurrentExp()
                    , experienceSystem.GetCurrentLevel()
                    , experienceSystem.GetTotalExp()                    
                    , experienceSystem.GetMaxLevel()                    
                    , experienceSystem.GetMaxLevelNeedExp()                    
                    , experienceSystem.GetNextLevelExp(experienceSystem.GetMaxLevel())                    
                    , experienceSystem.GetNextLevelRestExp()                    
                );
                experienceSystem.AddExp(experienceSystem.GetMaxLevelNeedExp());
            }

        }

        private static void SetFacilityListInit()
        {
            if (listbak == null)
            {
                listbak = FacilityDataTable.GetFacilityStatusArray(true).ToList();
                List<Facility.FacilityStatus> listbak2 = new(listbak);
                //listbak2.AddRange(listbak);
                foreach (var item in listbak2)
                {
                    MyLog.LogMessage(
                        "FacilityManagerToolPatch.print4"
                        , item.typeID
                        , item.name
                    );
                    if (!FacilityDataTable.GetFacilityCanBeDestroy(item.typeID, true))
                    //if (item.typeID==100 || item.typeID ==150)
                    {
                        // InvalidOperationException: Collection was modified; enumeration operation may not execute
                        listbak.Remove(item);// foreach 에서 Remove 실행시 배열이 바뀜. 즉 Remove 실행하기 위한 다른 조치 방안 필요
                    }
                }
            }
            if (list == null)
            {
                list = new List<Facility.FacilityStatus>();
            }
            list.Clear();
        }

        private static void GetFacilityMgr()
        {
            FacilityManager facilityManager = GameMain.Instance.FacilityMgr;
            //facilityManager.

        }


        /// <summary>
        /// 시설 자동 배치
        /// </summary>
        public static void SetFacilityAll(bool random)
        {

            if (configEntryUtill["SetFacilityAll"])
                MyLog.LogMessage(
                "FacilityManagerToolPatch.SetFacilityAll1"
                , GameMain.Instance.FacilityMgr.FacilityCountMax
            );

            SetFacilityListInit();

            //List<Facility.FacilityStatus> list = new();

            for (int i = 2; i < GameMain.Instance.FacilityMgr.FacilityCountMax; i++)
            {
                if (list.Count == 0)
                {
                    list.AddRange(listbak);
                }

                Facility.FacilityStatus item;
                if (random)
                    item = list[UnityEngine.Random.Range(0, FacilityUtill.list.Count)];                
                else
                    item = list[0];

                Facility facility = GameMain.Instance.FacilityMgr.CreateNewFacility(item.typeID);

                //[Message: Lilly] 3001 , 100 , トレーニングルーム , トレーニングルーム , -15756
                //[Message: Lilly] 3004 , 150 , 劇場 , 劇場 , -15762
                if (configEntryUtill["SetFacilityAll"])
                {
                    /*
[Message:     Lilly] 3004 , 劇場 , True , False , COM3D , Work , Basic , Basic
[Message:     Lilly] 150 , 劇場 , アイドル系 , 300 , アイドル系 , True , False , 2 , 40 , 1
[Message:     Lilly] Facility , 劇場 , 극장 , 1 , 3 , True , False , False , 2 , 40 , 0 , 1 , 1 , 150 , 극장
[Message:     Lilly] 150 , 극장
                     */
                    MyLog.LogMessage(
                          facility.defaultData.workData.id
                        , facility.defaultData.workData.name
                        , facility.defaultData.workData.isCommu
                        , facility.defaultData.workData.IsLegacy
                        , facility.defaultData.workData.mode
                        , facility.defaultData.workData.type
                        , facility.defaultData.workData.workTyp
                        , facility.defaultData.workData.trainingType
                        );
                    MyLog.LogMessage(
                          facility.defaultData.ID
                        , facility.defaultData.name
                        , facility.defaultData.businessType
                        , facility.defaultData.businessTypeID
                        , facility.defaultData.businessTypeName
                        , facility.defaultData.isRemoval
                        , facility.defaultData.isEnableData
                        , facility.defaultData.isEnableNTR
                        , facility.defaultData.isOnlyNTR
                        , facility.defaultData.isBusiness
                        , facility.defaultData.isDefaultPlace
                        , facility.defaultData.minMaidCount
                        , facility.defaultData.maxMaidCount
                        , facility.defaultData.rank
                        );
                    MyLog.LogMessage(
                          facility.name
                        , facility.defaultName
                        , facility.facilityName
                        , facility.facilityLevel
                        , facility.facilityValuation
                        , facility.isActiveAndEnabled
                        , facility.isOperationDayTime
                        , facility.isOperationNight
                        , facility.minMaidCount
                        , facility.maxMaidCount
                        , facility.nowNightMaidCount
                        , facility.staffRankDayTime
                        , facility.staffRankNight
                        , facility.param.typeID
                        , facility.param.name
                        );
                    MyLog.LogMessage(
                          facility.param.typeID
                        , facility.param.name
                        );
                }

                    GameMain.Instance.FacilityMgr.SetFacility(i, facility);
                
                list.Remove(item);

            }

            FacilityUtill.SetMaxExp();
        }


        public static void GetFacilityStatus()
        {
            List<Facility.FacilityStatus> list = FacilityDataTable.GetFacilityStatusArray(true).ToList();
            MyLog.LogMessage(
                "GetFacilityStatus"
                , list.Count
            );

            foreach (var item in list)
            {
                MyLog.LogMessage(
                    item.name
                    , item.typeID
                );
            }
        }

        public static void GetFacilityArray()
        {
            Facility[] v = GameMain.Instance.FacilityMgr.GetFacilityArray();
            MyLog.LogMessage(
                "GetFacilityArray"
                , v.Length
            );
            foreach (var item in v)
            {
                if (item == null)
                {
                    continue;
                }
                MyLog.LogMessage(
                    item.defaultData.workData.id
                    , item.defaultData.ID
                    , item.defaultData.isRemoval
                    , item.defaultName
                    , item.facilityName
                    , item.param.typeID
                    , item.param.name


                    //,item.GetInstanceID()

                    );
            }
        }

        /*
[Message:     Lilly] 3001 , 100 , トレーニングルーム , 트레이닝 룸 , -16922
[Message:     Lilly] 3004 , 150 , 劇場 , 극장 , -16928
[Message:     Lilly] 3001 , 100 , トレーニングルーム , 트레이닝 룸 , -59518
[Message:     Lilly] 3002 , 120 , オープンカフェ , 오픈 카페 , -59524
[Message:     Lilly] 3003 , 130 , レストラン , 레스토랑 , -59530
[Message:     Lilly] 3004 , 150 , 劇場 , 극장 , -59536
[Message:     Lilly] 3005 , 160 , バーラウンジ , 바라운지 , -59542
[Message:     Lilly] 3006 , 170 , カジノ , 카지노 , -59548
[Message:     Lilly] 3007 , 300 , ソープ , 소프 , -59554
[Message:     Lilly] 3008 , 310 , SMクラブ , SM클럽 , -59560
[Message:     Lilly] 3009 , 320 , ホテル , 호텔 , -59566
[Message:     Lilly] 3010 , 330 , リフレ , 리프레 , -59572
[Message:     Lilly] 3011 , 1010 , 高級オープンカフェ , 고급 오픈 카페 , -59578
[Message:     Lilly] 3012 , 1020 , 高級レストラン , 고급 레스토랑 , -59584
[Message:     Lilly] 3013 , 1040 , 高級劇場 , 고급 극장 , -59590
[Message:     Lilly] 3014 , 1050 , 高級バーラウンジ , 고급 바라운지 , -59596
[Message:     Lilly] 3015 , 1300 , 高級ソープ , 고급 소프 , -59602
[Message:     Lilly] 3016 , 1310 , 高級SMクラブ , 고급 SM클럽 , -59608
[Message:     Lilly] 3017 , 1320 , 高級ホテル , 고급 호텔 , -59614
[Message:     Lilly] 3018 , 1330 , 高級リフレ , 고급 리프레 , -59620
[Message:     Lilly] 3019 , 1340 , 高級カジノ , 고급 카지노 , -59626
[Message:     Lilly] 0 , 2000 , ご主人様専用ソープランド , 주인님 전용 소프랜드 , -59632
[Message:     Lilly] 0 , 2010 , ご主人様専用SMクラブ , 주인님 전용 SM클럽 , -59638
[Message:     Lilly] 3200 , 3000 , ランス10-決戦-コラボカフェ , 란스10 -결전- 콜라보 카페 , -59644
[Message:     Lilly] 3100 , 3010 , シーカフェ , 해변 카페 , -59650
[Message:     Lilly] 3210 , 3020 , RIDDLE JOKERコラボカフェ , RIDDLE JOKER 콜라보 카페 , -59656
[Message:     Lilly] 3110 , 3030 , プール , 수영장 , -59662
[Message:     Lilly] 3220 , 3040 , わんこの嫁入りコラボカフェ , 강아지의 시집가기 콜라보 카페 , -59668
[Message:     Lilly] 3120 , 3050 , 神社 , 신사 , -59674
[Message:     Lilly] 3230 , 3060 , ラズベリーキューブコラボカフェ , 라즈베리 큐브 콜라보 카페 , -59680
[Message:     Lilly] 3240 , 3070 , みにくいモジカの子コラボカフェ , 추한 모지카의 아이 콜라보 카페 , -59686
[Message:     Lilly] 4000 , 4000 , ポールダンスステージ , 폴댄스 스테이지 , -59692
[Message:     Lilly] 3250 , 3080 , 未来ラジオと人工鳩コラボカフェ , 미래 라디오와 인공 비둘기 콜라보 카페 , -59698
[Message:     Lilly] 3260 , 3090 , ゲレンデ , 슬로프 , -59704
[Message:     Lilly] 3270 , 3100 , 冒険者の酒場 , 모험가의 주점 , -59710
[Message:     Lilly] 3280 , 3110 , 金色ラブリッチェGTコラボカフェ , 금색 러브리체 GT 콜라보 카페 , -59716
[Message:     Lilly] 3290 , 3120 , 絆きらめく恋いろはコラボカフェ , 인연이 반짝이는 사랑의 첫걸음 콜라보 카페 , -59722
[Message:     Lilly] 3300 , 3130 , 春の庭園 , 봄의 공원 , -59728
[Message:     Lilly] 3310 , 3140 , イブニクル２コラボカフェ , 이브니클2 콜라보 카페 , -59734
[Message:     Lilly] 3320 , 3150 , コロラム！コラボカフェ , 코로람! 콜라보 카페 , -59740
[Message:     Lilly] 3330 , 3160 , コロラム！コラボカフェ2 , 코로람! 콜라보 카페 2 , -59746
[Message:     Lilly] 3340 , 3170 , わんこの嫁入りコラボカフェ2 , 강아지의 시집가기 콜라보 카페 2 , -59752
[Message:     Lilly] 3350 , 3180 , あいミス！コラボカフェ , 아이마스! 콜라보 카페 , -59758
[Message:     Lilly] 3360 , 3190 , ネコぱらコラボカフェ , 네코파라 콜라보 카페 , -59764
[Message:     Lilly] 3370 , 3200 , アイキスコラボカフェ , 아이키스 콜라보 카페 , -59770
[Message:     Lilly] 3001 , 100 , トレーニングルーム , 트레이닝 룸 , -59776
[Message:     Lilly] 3002 , 120 , オープンカフェ , 오픈 카페 , -59782
[Message:     Lilly] 3003 , 130 , レストラン , 레스토랑 , -59788
[Message:     Lilly] 3004 , 150 , 劇場 , 극장 , -59794
[Message:     Lilly] 3005 , 160 , バーラウンジ , 바라운지 , -59800
[Message:     Lilly] 3006 , 170 , カジノ , 카지노 , -59806
[Message:     Lilly] 3007 , 300 , ソープ , 소프 , -59812
[Message:     Lilly] 3008 , 310 , SMクラブ , SM클럽 , -59818
[Message:     Lilly] 3009 , 320 , ホテル , 호텔 , -59824
[Message:     Lilly] 3010 , 330 , リフレ , 리프레 , -59830
[Message:     Lilly] 3011 , 1010 , 高級オープンカフェ , 고급 오픈 카페 , -59836
[Message:     Lilly] 3012 , 1020 , 高級レストラン , 고급 레스토랑 , -59842
[Message:     Lilly] 3013 , 1040 , 高級劇場 , 고급 극장 , -59848
[Message:     Lilly] 3014 , 1050 , 高級バーラウンジ , 고급 바라운지 , -59854
[Message:     Lilly] 3015 , 1300 , 高級ソープ , 고급 소프 , -59860

         */


        /*
        public static void SetFacilityAllMaid(ScheduleMgr.ScheduleTime scheduleTime)
        {
            //if (ScheduleMgrPatch.m_scheduleApi == null)
            //{
            //    MyLog.LogMessage("SetSlotAllDel"
            //    , "스케줄 관리 접속 한번 필요"
            //    );
            //    return;
            //}

            if (configEntryUtill["SetFacilityAllMaid"])
                MyLog.LogMessage(
                "SetFacilityAllMaid1"
                );
            //List<KeyValuePair<int, ScheduleCSVData.Work>> works = ScheduleCSVData.WorkData.ToList();

            // 스케줄의 슬롯 정보
            // public Maid GetScheduleSlot(int slotNo)
            // if (string.IsNullOrEmpty(this.scheduleSlot[slotNo].maid_guid))
            ScheduleData[] scheduleDatas = GameMain.Instance.CharacterMgr.status.scheduleSlot;

            // 사용 가능한 목록
            List<int> slots = new();
            for (int i = 0; i < scheduleDatas.Length; i++)
            {
                if (scheduleDatas[i].maid_guid == string.Empty)
                    continue;
                slots.Add(i);
            }

            //var facilitys = GameMain.Instance.FacilityMgr.GetFacilityArray().Where(x=>x).ToList();
            var facilitys = GameMain.Instance.FacilityMgr.GetFacilityArray().ToList();
            if (configEntryUtill["SetFacilityAllMaid"])
                MyLog.LogMessage(
                "SetFacilityAllMaid3"
                , scheduleDatas.Length
                , slots.Count
                , facilitys.Count
                );

            // 구현부
            Facility facility;
            Maid maid;
            FacilityDataTable.FacilityDefaultData defaultData;
            ScheduleCSVData.Work workData;

            while (facilitys.Count > 2)
            {
                int n2 = UnityEngine.Random.Range(2, facilitys.Count);
                if (facilitys[n2] == null)
                {
                    if (configEntryUtill["SetFacilityAllMaid"])
                        MyLog.LogMessage(
                        "SetFacilityAllMaid null"
                        , n2
                        );
                }
                else
                {
                    facility = facilitys[n2];
                    defaultData = facility.defaultData;
                    workData = defaultData.workData;

                    if (facility.minMaidCount <= slots.Count && workData.id != 0)
                    {
                        for (int k = 0; k < facility.minMaidCount; k++)
                        {
                            int n1 = UnityEngine.Random.Range(0, slots.Count);
                            try
                            {
                                maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[n1]);
                                if (configEntryUtill["SetFacilityAllMaid"])
                                    MyLog.LogMessage(
                                    "SetFacilityAllMaid4"
                                    , n2
                                    , n1
                                    , MyUtill.GetMaidFullName(maid)
                                    , facility.defaultName

                                    );

                                if (ScheduleMgrPatch.m_scheduleApi == null)
                                    ScheduleMgrPatch.m_scheduleApi.SetNoonWorkSlot_Safe(scheduleTime, slots[n1], workData.id);

                                facility.AllocationMaid(maid, scheduleTime);
                            }
                            catch (Exception e)
                            {
                                MyLog.LogWarning(
                                "SetFacilityAllMaid4"
                                , n2
                                , e.ToString()
                                );
                            }
                            slots.RemoveAt(n1);
                        }
                        if (slots.Count == 0)
                        {
                            if (!DailyMgr.IsLegacy)
                            {
                                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
                            }
                            ScheduleAPI.MaidWorkIdErrorCheck(true);
                            return;
                        }
                    }

                }
                facilitys.RemoveAt(n2);
            }

            facility = facilitys[1];
            defaultData = facility.defaultData;
            workData = defaultData.workData;
            while (slots.Count > 0)
            {
                int n1 = 0;
                maid = GameMain.Instance.CharacterMgr.status.GetScheduleSlot(slots[n1]);

                if (ScheduleMgrPatch.m_scheduleApi == null)
                    ScheduleMgrPatch.m_scheduleApi.SetNoonWorkSlot_Safe(scheduleTime, slots[n1], workData.id);

                facility.AllocationMaid(maid, scheduleTime);

                slots.RemoveAt(n1);
            }

            if (!DailyMgr.IsLegacy)
            {
                GameMain.Instance.FacilityMgr.UpdateFacilityAssignedMaidData();
            }
            ScheduleAPI.MaidWorkIdErrorCheck(true);



            //ScheduleAPI.AddTrainingFacility(maid, workId, scheduleTime);





        }

        */
    }
}
/*
 *                     MyLog.LogMessage(
                        item.Key  // work id
                        , training.facilityId
                        , training.trainingType
                        , training.categoryID
                        , training.id
                        , training.name
                        , training.information
                        , training.mode
                        , training.type
                        , training.IsCommon
                        , training.isCommu
                        , training.IsLegacy
                        );
[Message:     Lilly] 3000 , 100 , Basic , 52 , 3000 , 施設強化 ,  , COM3D , Work , False , False , False
[Message:     Lilly] 3001 , 100 , Basic , 52 , 3001 , トレーニングルーム ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3002 , 100 , Basic , 52 , 3002 , オープンカフェ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3003 , 100 , Basic , 52 , 3003 , レストラン ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3004 , 100 , Basic , 52 , 3004 , 劇場 ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3005 , 100 , Basic , 52 , 3005 , バーラウンジ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3006 , 100 , Basic , 52 , 3006 , カジノ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3007 , 100 , Basic , 52 , 3007 , ソープ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3008 , 100 , Basic , 52 , 3008 , SMクラブ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3009 , 100 , Basic , 52 , 3009 , 宿泊部屋 ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3010 , 100 , Basic , 52 , 3010 , メイドリフレ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3011 , 100 , Basic , 52 , 3011 , 高級オープンカフェ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3012 , 100 , Basic , 52 , 3012 , 高級レストラン ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3013 , 100 , Basic , 52 , 3013 , 高級劇場 ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3014 , 100 , Basic , 52 , 3014 , 高級バーラウンジ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3015 , 100 , Basic , 52 , 3015 , 高級ソープ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3016 , 100 , Basic , 52 , 3016 , 高級SMクラブ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3017 , 100 , Basic , 52 , 3017 , 高級宿泊部屋 ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3018 , 100 , Basic , 52 , 3018 , 高級メイドリフレ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3019 , 100 , Basic , 52 , 3019 , 高級カジノ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3100 , 100 , Basic , 52 , 3100 , シーカフェ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3120 , 100 , Basic , 52 , 3120 , 神社 ,  , COM3D , Work , False , True , False
[Message:     Lilly] 4000 , 100 , Basic , 52 , 4000 , ポールダンス ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3260 , 100 , Basic , 52 , 3260 , ゲレンデ ,  , COM3D , Work , False , True , False
[Message:     Lilly] 3300 , 100 , Basic , 52 , 3300 , 春の庭園 ,  , COM3D , Work , False , True , False
 */