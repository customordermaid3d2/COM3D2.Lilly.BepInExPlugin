using Kasizuki;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using wf;

namespace COM3D2.Lilly.Plugin
{
    class InfoUtill
    {
        public static void GetTbodyInfo()
        {

            MyLog.LogDarkBlue("GetTbodyInfo");

            Maid maid = GameMain.Instance.CharacterMgr.GetMaid(0);
            if (maid == null)
            {
                return;
            }

            foreach (string item in TBody.m_strDefSlotNameCRC)
            {
                MyLog.LogInfo("m_strDefSlotNameCRC"
                , item
                );
            }

            foreach (string item in TBody.m_strDefSlotName)
            {
                MyLog.LogInfo("m_strDefSlotName"
                , item
                );
            }

            TBody body = maid.body0;

            try
            {
                BoneMorph_ bonemorph = body.bonemorph;
                foreach (BoneMorphLocal item in bonemorph.bones)
                {
                    Transform t = item.linkT;
                    MyLog.LogInfo("bonemorph.bones.linkT"
                    , t.name
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("bonemorph.bones:"
                    + e.ToString()
                    );
            }

            try
            {
                Dictionary<string, float> m_MorphBlendValues = body.m_MorphBlendValues;
                foreach (KeyValuePair<string, float> item in m_MorphBlendValues)
                {
                    MyLog.LogInfo("m_MorphBlendValues"
                    , item.Key
                    , item.Value
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("m_MorphBlendValues:"
                    + e.ToString()
                    );
            }

            try
            {
                Dictionary<string, Transform> m_dicBonesMR = body.m_dicBonesMR;
                foreach (KeyValuePair<string, Transform> item in m_dicBonesMR)
                {
                    MyLog.LogInfo("m_dicBonesMR"
                    , item.Key
                    , item.Value.name
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("m_dicBonesMR:"
                    + e.ToString()
                    );
            }

            try
            {
                Dictionary<string, Transform> m_dicTrans = body.m_dicTrans;
                foreach (KeyValuePair<string, Transform> item in m_dicTrans)
                {
                    MyLog.LogInfo("m_dicTrans"
                    , item.Key
                    , item.Value.name
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("m_dicTrans:"
                    + e.ToString()
                    );
            }

            try
            {
                Dictionary<string, Transform> m_dicBones = body.m_dicBones;
                foreach (KeyValuePair<string, Transform> item in m_dicBones)
                {
                    MyLog.LogInfo("m_dicBones"
                    , item.Key
                    , item.Value.name
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("m_dicBones:"
                    + e.ToString()
                    );
            }

            try
            {
                TBody.Slot goSlot = body.goSlot;

                for (int i = 0; i < goSlot.Count; i++)
                {
                    for (int j = 0; j < goSlot.CountChildren(i); j++)
                    //for (int j = 0; j < goSlot[i].Count; j++)
                    {
                        TBodySkin bodySkin = goSlot[i, j];
                        MyLog.LogInfo("TBody.Slot"
                        , bodySkin.Category
                        , bodySkin.SlotId
                        , bodySkin.m_subNo
                        , bodySkin.m_AttachName
                        , bodySkin.m_AttachSlotIdx
                        , bodySkin.m_AttachSlotSubIdx
                        , bodySkin.m_attachType
                        , bodySkin.m_BackAttachName
                        , bodySkin.m_BackAttachSlotIdx
                        , bodySkin.m_BackAttachSlotSubIdx
                        );
                    }
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("TBody.Slot:"
                    + e.ToString()
                    );
            }

        }

        internal static void GetMaidStatus()
        {

            MyLog.LogDarkBlue("ScenarioDataUtill.GetMaidStatus. start");

            Maid maid_ = GameMain.Instance.CharacterMgr.GetStockMaid(0);

            MyLog.LogMessage("Maid: " + MyUtill.GetMaidFullNale(maid_));

            ReadOnlyDictionary<int, bool> eventEndFlags = maid_.status.eventEndFlags;
            foreach (var item in eventEndFlags)
            {
                MyLog.LogMessage("eventEndFlags: " + item.Key, item.Value);
            }

            ReadOnlyDictionary<string, int> flags = maid_.status.flags;
            foreach (var item in flags)
            {
                MyLog.LogMessage("flags: " + item.Key, item.Value);
            }

            ReadOnlyDictionary<int, WorkData> workDatas = maid_.status.workDatas;
            foreach (var item in workDatas)
            {
                MyLog.LogMessage("workDatas: " + item.Key, item.Value.id, item.Value.level);
            }

            MyLog.LogDarkBlue("ScenarioDataUtill.GetMaidStatus. start");
        }

        public static List<AbstractFreeModeItem> scnearioFree = new List<AbstractFreeModeItem>();

        internal static void GetMaidInfo()
        {

            MyLog.LogDarkBlue("=== GetGameInfo st ===");

            try
            {
                foreach (var item in Feature.GetAllDatas(false))
                {
                    MyLog.LogMessage("Feature:", item.id, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("Feature:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var item in Personal.GetAllDatas(false))
                {
                    MyLog.LogMessage("Personal:", item.id, item.replaceText, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("Personal:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var item in Propensity.GetAllDatas(false))
                {
                    MyLog.LogMessage("Propensity:", item.id, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("Propensity:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var data in JobClass.GetAllDatas(false))
                {
                    MyLog.LogMessage("JobClass", data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termExplanatoryText);//, data.termName
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("JobClass:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var data in YotogiClass.GetAllDatas(false))
                {
                    MyLog.LogMessage("YotogiClass", data.id, data.classType, data.drawName, data.uniqueName, data.explanatoryText, data.termExplanatoryText);//, data.termName
                }
            }
            catch (Exception e)
            {

                MyLog.LogMessage("YotogiClass:" + e.ToString());
            }
            MyLog.LogInfo();


            try
            {
                // 시나리오. 일상 등등?
                SetscnearioFreeList();
                foreach (var data in scnearioFree)
                {
                    MyLog.LogMessage("scneario"
                        , data.item_id
                        //, data.is_enabled
                        , data.play_file_name
                        , data.title
                        , data.text
                        , data.type
                        , MyUtill.Join(" / ", data.condition_texts)
                        , data.titleTerm
                        , data.textTerm
                        , MyUtill.Join(" , ", data.condition_text_terms)
                        );
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("scneario:" + e.ToString());
            }
            MyLog.LogInfo();

            MyLog.LogDarkBlue("=== GetGameInfo ed ===");
        }

        private static void SetscnearioFreeList()
        {
            if (scnearioFree.Count == 0)
            {
                // var newArray = Array.ConvertAll(array, item => (NewType)item);
                // AbstractFreeModeItem  / protected static HashSet<int> GetEnabledIdList() / 에서 처리하자
                try
                {
                }
                catch (Exception)
                {
                    throw;
                }
                scnearioFree.AddRange(FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Story, null).ConvertAll(item => (AbstractFreeModeItem)item));
                scnearioFree.AddRange(FreeModeItemEveryday.CreateItemEverydayList(FreeModeItemEveryday.ScnearioType.Nitijyou, null).ConvertAll(item => (AbstractFreeModeItem)item));
                scnearioFree.AddRange(FreeModeItemLifeMode.CreateItemList(true).ConvertAll(item => (AbstractFreeModeItem)item));
                scnearioFree.AddRange(FreeModeItemVip.CreateItemVipList(null).ConvertAll(item => (AbstractFreeModeItem)item));
                // 이걸 가져올 방법이 없어서 이렇게 씀
                //scneario.AddRange(FreeModeItemLifeMode.CreateItemList(true));
                //scneario.AddRange(FreeModeItemVip.CreateItemVipList(null));
            }
        }

        internal static void GetPlayerInfo()
        {

            MyLog.LogDarkBlue("=== GetGameInfo st ===");

            MyLog.LogInfo("Application.installerName : " + Application.installerName);
            MyLog.LogInfo("Application.version : " + Application.version);
            MyLog.LogInfo("Application.unityVersion : " + Application.unityVersion);
            MyLog.LogInfo("Application.companyName : " + Application.companyName);

            MyLog.LogInfo();

            MyLog.LogInfo("CharacterMgr.MaidStockMax : " + CharacterMgr.MaidStockMax);
            MyLog.LogInfo("CharacterMgr.ActiveMaidSlotCount : " + CharacterMgr.ActiveMaidSlotCount);
            MyLog.LogInfo("CharacterMgr.NpcMaidCreateCount : " + CharacterMgr.NpcMaidCreateCount);
            MyLog.LogInfo("CharacterMgr.ActiveManSloatCount : " + CharacterMgr.ActiveManSloatCount);

            MyLog.LogInfo();


            try
            {
                foreach (var data in PlayData.GetAllDatas(false))
                {
                    MyLog.LogMessage("PlayData"
                        , data.ID
                        , data.drawName
                        , data.drawNameTerm
                        );
                }
            }
            catch (Exception e)
            {

                MyLog.LogMessage("PlayData:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var data in RoomData.GetAllDatas(false))
                {
                    MyLog.LogMessage("RoomData"
                        , data.ID
                        , data.upwardRoomID
                        , data.uniqueName
                        , data.isEnableNTR
                        , data.isOnlyNTR
                        , data.drawName
                        , data.drawNameTerm
                        , data.explanatoryTextTerm
                        , data.facilityTypeID
                        , data.facilityDefaultName
                        );
                }
            }
            catch (Exception e)
            {

                MyLog.LogMessage("RoomData:" + e.ToString());
            }
            MyLog.LogInfo();


            try
            {
                foreach (var data in YotogiStage.GetAllDatas(false))
                {
                    MyLog.LogMessage("YotogiStage"
                        , data.id
                        , data.uniqueName
                        , data.drawName
                        );
                }
            }
            catch (Exception e)
            {

                MyLog.LogMessage("YotogiStage:" + e.ToString());
            }
            MyLog.LogInfo();


            try
            {
                foreach (var data in GameMain.Instance.CharacterMgr.status.flags)
                {
                    MyLog.LogMessage("Playerflags"
                        , data.Key
                        , data.Value
                        );
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("Playerflags:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var data in GameMain.Instance.CharacterMgr.status.haveItems)
                {
                    MyLog.LogMessage("haveItems"
                        , data.Key
                        , data.Value
                        );
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("haveItems:" + e.ToString());
            }
            MyLog.LogInfo();

            try
            {
                foreach (var data in GameMain.Instance.CharacterMgr.status.havePartsItems)
                {
                    MyLog.LogMessage("havePartsItems"
                        , data.Key
                        , data.Value
                        );
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("havePartsItems:" + e.ToString());
            }
            MyLog.LogInfo();

            MyLog.LogDarkBlue("=== GetGameInfo ed ===");
        }
    }
}
