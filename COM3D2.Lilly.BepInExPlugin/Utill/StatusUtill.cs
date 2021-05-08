using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Utill
{
    class StatusUtill
    {

        public static void SetMaidStatusAll()
        {
            MyLog.LogDarkBlue("MaidStatusUtill.SetMaidStatusAll. start");

            foreach (var maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
            {
                if (maid.status.heroineType != HeroineType.Sub)
                    SetMaidStatus(maid);

            }

            MyLog.LogDarkBlue("MaidStatusUtill.SetMaidStatusAll. end");
        }

        public static void SetMaidStatus(Maid maid)
        {
            if (maid == null)
            {
                MyLog.LogFatal("MaidStatusUtill.SetMaidStatus:null");
                return;
            }
            MyLog.LogMessage("SetMaidStatus : " + MyUtill.GetMaidFullName(maid));

            maid.status.employmentDay = 1;// 고용기간

            maid.status.baseAppealPoint = 9999;
            maid.status.baseCare = 9999;
            maid.status.baseCharm = 9999;
            maid.status.baseCooking = 9999;
            maid.status.baseDance = 9999;
            maid.status.baseElegance = 9999;
            maid.status.baseHentai = 9999;
            maid.status.baseHousi = 9999;
            maid.status.baseInyoku = 9999;
            maid.status.baseLovely = 9999;
            maid.status.baseMaxHp = 9999;
            maid.status.baseMaxMind = 9999;
            maid.status.baseMaxReason = 9999;
            maid.status.baseMvalue = 9999;
            maid.status.baseReception = 9999;
            maid.status.baseTeachRate = 9999;
            maid.status.baseVocal = 9999;

            maid.status.studyRate = 0;   // 습득율
            maid.status.likability = 999;// 호감도

            if (maid.boNPC)
            {

            }

            //if (true)
            //{
            //    //maid.status.contract = Contract.;// 적용 방식 고민 필요
            //}

            //maid.status.specialRelation = SpecialRelation.Married;// 되는건가?
            maid.status.additionalRelation = AdditionalRelation.Slave;// 되는건가?

            //maid.status.heroineType = HeroineType.Original;// 기본, 엑스트라 , 이전 // 사용 금지.일반 메이드를 엑스트라로 하면 꼬인다. 반대도 마찬가지
            maid.status.relation = Relation.Lover;// 호감도
            maid.status.seikeiken = Seikeiken.Yes_Yes;// 


            // 특징
            MyLog.LogMessage("SetMaidStatus.AddFeature: " + MyUtill.GetMaidFullName(maid));
            foreach (Feature.Data data in Feature.GetAllDatas(true))
                maid.status.AddFeature(data);


            // 성벽
            MyLog.LogMessage("SetMaidStatus.AddPropensity: " + MyUtill.GetMaidFullName(maid));
            foreach (Propensity.Data data in Propensity.GetAllDatas(true))
                maid.status.AddPropensity(data);


            MyLog.LogMessage("SetMaidStatus.WorkData max : " + MyUtill.GetMaidFullName(maid), maid.status.workDatas.Count);
            foreach (WorkData workData in maid.status.workDatas.GetValueArray())
            {
                workData.level = 10;
                workData.playCount = 9999U;
            }


        }

    }
}
