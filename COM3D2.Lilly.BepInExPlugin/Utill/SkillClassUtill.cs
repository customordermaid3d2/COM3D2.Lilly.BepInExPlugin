using MaidStatus;
using MaidStatus.CsvData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yotogis;

namespace COM3D2.Lilly.Plugin.Utill
{
    class SkillClassUtill
    {
        public static void SetMaidYotogiClassAll()
        {
            MyLog.LogDarkBlue("SetMaidYotogiClassAll. start");

            foreach (var maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
            {
                if (maid.status.heroineType == HeroineType.Sub)
                    continue;
                SetMaidYotogiClass(maid);
            }
        }

        public static void SetMaidYotogiClass(Maid maid)
        {

            #region YotogiClass

            YotogiClassSystem yotogiClassSystem = maid.status.yotogiClass;
            List<YotogiClass.Data> learnPossibleYotogiClassDatas = yotogiClassSystem.GetLearnPossibleClassDatas(true, AbstractClassData.ClassType.Share | AbstractClassData.ClassType.New | AbstractClassData.ClassType.Old);

            MyLog.LogMessage("SetMaidStatus.YotogiClass learn", MyUtill.GetMaidFullName(maid), learnPossibleYotogiClassDatas.Count);
            foreach (YotogiClass.Data data in learnPossibleYotogiClassDatas)
                maid.status.yotogiClass.Add(data, true, true);

            var yotogiClassSystems = yotogiClassSystem.GetAllDatas().Values;
            MyLog.LogMessage("SetMaidStatus.YotogiClass expSystem", MyUtill.GetMaidFullName(maid), yotogiClassSystems.Count);
            SetExpMax(yotogiClassSystems.Select(x => x.expSystem));

            #endregion
        }

        public static void SetMaidJobClassAll()
        {
            MyLog.LogDarkBlue("SetMaidJobClassAll. start");


            foreach (var maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
            {
                if (maid.status.heroineType == HeroineType.Sub)
                    continue;
                SetMaidJobClass(maid);
            }
        }

        public static void SetMaidJobClass(Maid maid)
        {

            #region JobClass

            JobClassSystem jobClassSystem = maid.status.jobClass;
            List<JobClass.Data> learnPossibleClassDatas = jobClassSystem.GetLearnPossibleClassDatas(true, AbstractClassData.ClassType.Share | AbstractClassData.ClassType.New | AbstractClassData.ClassType.Old);

            MyLog.LogMessage("SetMaidStatus.JobClass.learn: " + MyUtill.GetMaidFullName(maid), learnPossibleClassDatas.Count);
            foreach (JobClass.Data data in learnPossibleClassDatas)
                jobClassSystem.Add(data, true, true);

            var jobClassSystems = jobClassSystem.GetAllDatas().Values;// old 데이터 포함
            MyLog.LogMessage("SetMaidStatus.JobClass.expSystem: " + MyUtill.GetMaidFullName(maid), jobClassSystems.Count);
            SetExpMax(jobClassSystems.Select(x => x.expSystem));

            #endregion
        }

        public static void SetMaidSkillAll()
        {
            MyLog.LogDarkBlue("SetMaidSkillAll. start");

            foreach (var maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
            {
                if (maid.status.heroineType == HeroineType.Sub)
                    continue;

                SetMaidSkill(maid);
            }
        }

        public static void SetMaidSkill(Maid maid)
        {
            #region 스킬 영역

            List<Skill.Data> learnPossibleSkills = Skill.GetLearnPossibleSkills(maid.status);
            MyLog.LogMessage("SetMaidStatus.Skill learn : " + MyUtill.GetMaidFullName(maid), learnPossibleSkills.Count);
            foreach (Skill.Data data in learnPossibleSkills)
                maid.status.yotogiSkill.Add(data);

            MyLog.LogMessage("SetMaidStatus.Skill expSystem : " + MyUtill.GetMaidFullName(maid), maid.status.yotogiSkill.datas.Count);
            SetExpMax(maid.status.yotogiSkill.datas.GetValueArray().Select(x => x.expSystem));


            List<Skill.Old.Data> learnPossibleOldSkills = Skill.Old.GetLearnPossibleSkills(maid.status);
            MyLog.LogMessage("SetMaidStatus.Old.Skill learn : " + MyUtill.GetMaidFullName(maid), learnPossibleOldSkills.Count);
            foreach (Skill.Old.Data data in learnPossibleOldSkills)
                maid.status.yotogiSkill.Add(data);

            MyLog.LogMessage("SetMaidStatus.Old.Skill expSystem : " + MyUtill.GetMaidFullName(maid), maid.status.yotogiSkill.oldDatas.Count);
            SetExpMax(maid.status.yotogiSkill.oldDatas.GetValueArray().Select(x => x.expSystem));

            #endregion
        }

        public static void SetExpMax(IEnumerable<SimpleExperienceSystem> expSystems)
        {
            //int c = 0;
            foreach (var expSystem in expSystems)
            {
                expSystem.SetLevel(expSystem.GetMaxLevel());
                //   c++;
            }
            //MyLog.LogMessage("SetExpMax : " + c);
        }


    }
}
