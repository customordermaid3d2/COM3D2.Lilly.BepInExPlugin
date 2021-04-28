using Kasizuki;
using MaidStatus;
using PlayerStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using wf;
using Yotogis;


namespace COM3D2.Lilly.Plugin
{
    public class InfoUtill : GUIVirtual
    {
        public override void SetButtonList()
        {
            if (GUILayout.Button("게임 정보 얻기 관련")) InfoUtill.GetGameInfo();
            if (GUILayout.Button("Scene 얻기 관련")) InfoUtill.GetSceneInfo();
            if (GUILayout.Button("정보 얻기 바디 관련")) InfoUtill.GetTbodyInfo();
            if (GUILayout.Button("정보 얻기 메이드 플레그 관련")) InfoUtill.GetMaidFlag();
            if (GUILayout.Button("정보 얻기 플레이어 관련")) InfoUtill.GetPlayerInfo();
            if (GUILayout.Button("정보 얻기 메이드 관련")) InfoUtill.GetMaidInfo();
            #if COM3D2_157
            if (GUILayout.Button("GetStrIKCtrlPairInfo")) FullBodyIKMgrPatch.GetStrIKCtrlPairInfo();
            #endif
        }

        private static void GetGameInfo()
        {
            MyLog.LogDarkBlue("=== GetGameInfo st ===");

            MyLog.LogInfo("Application.installerName : " + Application.installerName);
            MyLog.LogInfo("Application.version : " + Application.version);
            MyLog.LogInfo("Application.unityVersion : " + Application.unityVersion);
            MyLog.LogInfo("Application.companyName : " + Application.companyName);

            MyLog.LogInfo("StoreDirectoryPath : " + GameMain.Instance.SerializeStorageManager.StoreDirectoryPath);
            //MyLog.LogInfo("StoreDirectoryPath : " + GameMain.Instance.SerializeStorageManager.);

            MyLog.LogInfo();

            MyLog.LogDarkBlue("=== GetGameInfo ed ===");
        }

        public static void GetSceneInfo()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                MyLog.LogMessage("GetSceneInfo"
                    , scene.buildIndex
                    , scene.rootCount
                    , scene.name
                    , scene.isLoaded
                    , scene.isDirty
                    , scene.IsValid()
                    , scene.path
                    );
            }
        }

        public static void GetTbodyInfo()
        {

            MyLog.LogDarkBlue("GetTbodyInfo");

            Maid maid = GameMain.Instance.CharacterMgr.GetMaid(0);
            if (maid == null)
            {
                return;
            }

#if COM3D2_157

            foreach (string item in TBody.m_strDefSlotNameCRC)
            {
                MyLog.LogInfo("m_strDefSlotNameCRC"
                , item
                );
            }

#endif

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

#if COM3D2_157

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

#endif

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

#if COM3D2_157

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

#endif

            try
            {
#if COM3D2_157
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
#elif COM3D2_155
                List<TBodySkin> goSlot = body.goSlot;

                for (int i = 0; i < goSlot.Count; i++)
                {
                    {
                        TBodySkin bodySkin = goSlot[i];
                        MyLog.LogInfo("TBody.Slot"
                        , bodySkin.Category
                        , bodySkin.SlotId
                        //, bodySkin.m_subNo
                        , bodySkin.AttachName
                        , bodySkin.AttachSlotIdx
                        );
                    }
                }
#endif
            }
            catch (Exception e)
            {
                MyLog.LogError("TBody.Slot:"
                    + e.ToString()
                    );
            }

        }

        internal static void GetMaidFlag()
        {

            MyLog.LogDarkBlue("ScenarioDataUtill.GetMaidStatus. start");

            Maid maid_ = GameMain.Instance.CharacterMgr.GetStockMaid(0);

            MyLog.LogMessage("Maid: " + MyUtill.GetMaidFullName(maid_));

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

            MyLog.LogDarkBlue("ScenarioDataUtill.GetMaidStatus. end");
        }

        public static List<AbstractFreeModeItem> scnearioFree = new List<AbstractFreeModeItem>();

        internal static void GetMaidInfo()
        {

            MyLog.LogDarkBlue("=== GetGameInfo st ===");



            MyLog.LogMessage("성격");
            try
            {
                foreach (var item in PersonalUtill.GetPersonalData(false))
                {
                    MyLog.LogMessage("Personal:", item.id, item.replaceText, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogMessage("Personal:" + e.ToString());
            }
            /*
            [Message: Lilly] Personal: , 10 , C , Pure , 純真で健気な妹系 , MaidStatus / 性格タイプ / Pure
            [Message: Lilly] Personal: , 20 , B , Cool , クールで寡黙 , MaidStatus / 性格タイプ / Cool
            [Message: Lilly] Personal: , 30 , A , Pride , プライドが高く負けず嫌い , MaidStatus / 性格タイプ / Pride
            [Message: Lilly] Personal: , 40 , D , Yandere , 病的な程一途な大和撫子 , MaidStatus / 性格タイプ / Yandere
            [Message: Lilly] Personal: , 50 , E , Anesan , 母性的なお姉ちゃん , MaidStatus / 性格タイプ / Anesan
            [Message: Lilly] Personal: , 60 , F , Genki , 健康的でスポーティなボクっ娘 , MaidStatus / 性格タイプ / Genki
            [Message: Lilly] Personal: , 70 , G , Sadist , Ｍ心を刺激するドＳ女王様 , MaidStatus / 性格タイプ / Sadist
            [Message: Lilly] Personal: , 80 , A1 , Muku , 無垢 , MaidStatus / 性格タイプ / Muku
            [Message: Lilly] Personal: , 90 , B1 , Majime , 真面目 , MaidStatus / 性格タイプ / Majime
            [Message: Lilly] Personal: , 100 , C1 , Rindere , 凜デレ , MaidStatus / 性格タイプ / Rindere
            [Message: Lilly] Personal: , 110 , D1 , Silent , 文学少女 , MaidStatus / 性格タイプ / Silent
            [Message: Lilly] Personal: , 120 , E1 , Devilish , 小悪魔 , MaidStatus / 性格タイプ / Devilish
            [Message: Lilly] Personal: , 130 , F1 , Ladylike , おしとやか , MaidStatus / 性格タイプ / Ladylike
            [Message: Lilly] Personal: , 140 , G1 , Secretary , メイド秘書 , MaidStatus / 性格タイプ / Secretary
            [Message: Lilly] Personal: , 150 , H1 , Sister , ふわふわ妹 , MaidStatus / 性格タイプ / Sister
            [Message: Lilly] Personal: , 160 , J1 , Curtness , 無愛想 , MaidStatus / 性格タイプ / Curtness
            [Message: Lilly] Personal: , 170 , K1 , Missy , お嬢様 , MaidStatus / 性格タイプ / Missy
            [Message: Lilly] Personal: , 180 , L1 , Childhood , 幼馴染 , MaidStatus / 性格タイプ / Childhood
            [Message: Lilly] Personal: , 190 , M1 , Masochist , ド変態ドＭ , MaidStatus / 性格タイプ / Masochist
            [Message: Lilly] Personal: , 200 , N1 , Crafty , 腹黒 , MaidStatus / 性格タイプ / Crafty
            */

            MyLog.LogMessage("특징");
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

            MyLog.LogMessage("성벽");
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

            MyLog.LogMessage("메이드 관리 - 클래스");
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

            MyLog.LogMessage(" 메이드 관리 - 밤시중 정보");
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

            // 밤시증 스킬 및 커맨드 정보
            {
                SortedDictionary<int, Skill.Data> sd;
                Skill.Data skilld;
                Yotogis.Skill.Data.Command command;
                MyLog.LogMessage("skill_data_list", Skill.skill_data_list.Length);
                for (int i = 0; i < Skill.skill_data_list.Length; i++)
                {
                    sd =Skill.skill_data_list[i];
                    foreach (var item in sd)
                    {
                        skilld = item.Value;
                        MyLog.LogMessage("skill_data_list"
                            , skilld.id 
                            , skilld.name
                            , skilld.player_num
                            , skilld.category                        
                            );
                        command = skilld.command;
                        foreach (var item1 in command.data)
                        {                        
                            MyLog.LogMessage("skill_data_list.command"
                            , item1.basic.id
                            , item1.basic.name
                            , item1.basic.group_name
                            , item1.basic.command_type
                            );
                        }
                    }
                }
            }


            {
                SortedDictionary<int, Skill.Old.Data> sd;
                Skill.Old.Data skilld;
                Yotogis.Skill.Old.Data.Command command;
                MyLog.LogMessage("skill_data_list", Skill.Old.skill_data_list.Length);
                for (int i = 0; i < Skill.skill_data_list.Length; i++)
                {
                    sd = Skill.Old.skill_data_list[i];
                    foreach (var item in sd)
                    {
                        skilld = item.Value;
                        MyLog.LogMessage("skill_data_list"
                            , skilld.id
                            , skilld.name
                            , skilld.player_num
                            , skilld.category
                            );
                        command = skilld.command;
                        foreach (var item1 in command.data)
                        {
                            MyLog.LogMessage("skill_data_list.command"
                            , item1.basic.id
                            , item1.basic.name
                            , item1.basic.group_name
                            , item1.basic.command_type
                            );
                        }
                    }
                }
            }


            MyLog.LogMessage();
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
            MyLog.LogMessage();

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

            MyLog.LogInfo("CharacterMgr.MaidStockMax : " + CharacterMgr.MaidStockMax);
            MyLog.LogInfo("CharacterMgr.ActiveMaidSlotCount : " + CharacterMgr.ActiveMaidSlotCount);
            MyLog.LogInfo("CharacterMgr.NpcMaidCreateCount : " + CharacterMgr.NpcMaidCreateCount);
            MyLog.LogInfo("CharacterMgr.ActiveManSloatCount : " + CharacterMgr.ActiveManSloatCount);

            MyLog.LogInfo();

            try
            {
                foreach (var item in Trophy.GetAllDatas(false))
                {
                    MyLog.LogMessage("Trophy"
                    , item.id
                    , item.name
                    , item.type
                    , item.rarity
                    , item.maidPoint
                    , item.infoText
                    , item.bonusText
                    );
                }
            }
            catch (Exception e)
            {
                MyLog.LogError("Trophy:" + e.ToString());
            }

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
            MyLog.LogMessage();

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
            MyLog.LogMessage();

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
            MyLog.LogMessage();

            NightWorkState nightWorkState;
            foreach (var data in GameMain.Instance.CharacterMgr.status.night_works_state_dic)
            {
                nightWorkState = data.Value;
                MyLog.LogMessage("havePartsItems"
                    , data.Key
                    , nightWorkState.workId
                    );
            }

            MyLog.LogDarkBlue("=== GetGameInfo ed ===");
        }
    }
}

/*

[Message: Lilly] Feature: , 10 , 元気な笑顔 , 元気な笑顔 , MaidStatus / 特徴タイプ / 元気な笑顔
[Message: Lilly] Feature: , 20 , 朗らかな魅力 , 朗らかな魅力 , MaidStatus / 特徴タイプ / 朗らかな魅力
[Message: Lilly] Feature: , 30 , 優美 , 優美 , MaidStatus / 特徴タイプ / 優美
[Message: Lilly] Feature: , 40 , 純真無垢 , 純真無垢 , MaidStatus / 特徴タイプ / 純真無垢
[Message: Lilly] Feature: , 50 , 天使 , 天使 , MaidStatus / 特徴タイプ / 天使
[Message: Lilly] Feature: , 60 , 魅了 , 魅了 , MaidStatus / 特徴タイプ / 魅了
[Message: Lilly] Feature: , 70 , 大人びた魅力 , 大人びた魅力 , MaidStatus / 特徴タイプ / 大人びた魅力
[Message: Lilly] Feature: , 80 , 魔性の魅力 , 魔性の魅力 , MaidStatus / 特徴タイプ / 魔性の魅力
[Message: Lilly] Feature: , 90 , 凜とした , 凜とした , MaidStatus / 特徴タイプ / 凜とした
[Message: Lilly] Feature: , 100 , 高貴 , 高貴 , MaidStatus / 特徴タイプ / 高貴
[Message: Lilly] Feature: , 110 , 恥ずかしがり屋 , 恥ずかしがり屋 , MaidStatus / 特徴タイプ / 恥ずかしがり屋
[Message: Lilly] Feature: , 120 , 素直 , 素直 , MaidStatus / 特徴タイプ / 素直
[Message: Lilly] Feature: , 130 , 妖艶な魅力 , 妖艶な魅力 , MaidStatus / 特徴タイプ / 妖艶な魅力
[Message: Lilly] Feature: , 140 , 上品 , 上品 , MaidStatus / 特徴タイプ / 上品
[Message: Lilly] Feature: , 150 , 優雅 , 優雅 , MaidStatus / 特徴タイプ / 優雅
[Message: Lilly] Feature: , 160 , とことん尽くす , とことん尽くす , MaidStatus / 特徴タイプ / とことん尽くす
[Message: Lilly] Feature: , 170 , 傅く , 傅く , MaidStatus / 特徴タイプ / 傅く
[Message: Lilly] Feature: , 180 , 過去の秘密 , 過去の秘密 , MaidStatus / 特徴タイプ / 過去の秘密
[Message: Lilly] Feature: , 190 , 疲労 , 疲労 , MaidStatus / 特徴タイプ / 疲労
[Message: Lilly] Feature: , 200 , 過去の過ち , 過去の過ち , MaidStatus / 特徴タイプ / 過去の過ち
[Message: Lilly] Feature: , 210 , 雅 , 雅 , MaidStatus / 特徴タイプ / 雅
[Message: Lilly] Feature: , 220 , 撫子 , 撫子 , MaidStatus / 特徴タイプ / 撫子
[Message: Lilly] Feature: , 230 , 一途 , 一途 , MaidStatus / 特徴タイプ / 一途
[Message: Lilly] Feature: , 240 , 盲目的 , 盲目的 , MaidStatus / 特徴タイプ / 盲目的
[Message: Lilly] Feature: , 250 , 風雅 , 風雅 , MaidStatus / 特徴タイプ / 風雅
[Message: Lilly] Feature: , 260 , 母性的 , 母性的 , MaidStatus / 特徴タイプ / 母性的
[Message: Lilly] Feature: , 270 , 女神 , 女神 , MaidStatus / 特徴タイプ / 女神
[Message: Lilly] Feature: , 280 , 無防備 , 無防備 , MaidStatus / 特徴タイプ / 無防備
[Message: Lilly] Feature: , 290 , 小悪魔 , 小悪魔 , MaidStatus / 特徴タイプ / 小悪魔
[Message: Lilly] Feature: , 300 , 落ち着きがある , 落ち着きがある , MaidStatus / 特徴タイプ / 落ち着きがある
[Message: Lilly] Feature: , 310 , 礼儀正しい , 礼儀正しい , MaidStatus / 特徴タイプ / 礼儀正しい
[Message: Lilly] Feature: , 320 , 妹系 , 妹系 , MaidStatus / 特徴タイプ / 妹系
[Message: Lilly] Feature: , 330 , 元気 , 元気 , MaidStatus / 特徴タイプ / 元気
[Message: Lilly] Feature: , 340 , ハニートラップ , ハニートラップ , MaidStatus / 特徴タイプ / ハニートラップ
[Message: Lilly] Feature: , 350 , 傾国の美女 , 傾国の美女 , MaidStatus / 特徴タイプ / 傾国の美女
[Message: Lilly] Feature: , 360 , 甘えん坊 , 甘えん坊 , MaidStatus / 特徴タイプ / 甘えん坊
[Message: Lilly] Feature: , 370 , お姫様の素質 , お姫様の素質 , MaidStatus / 特徴タイプ / お姫様の素質
[Message: Lilly] Feature: , 380 , 母性 , 母性 , MaidStatus / 特徴タイプ / 母性
[Message: Lilly] Feature: , 390 , 聖母 , 聖母 , MaidStatus / 特徴タイプ / 聖母
[Message: Lilly] Feature: , 400 , 仔犬系 , 仔犬系 , MaidStatus / 特徴タイプ / 仔犬系
[Message: Lilly] Feature: , 410 , ピュアガール , ピュアガール , MaidStatus / 特徴タイプ / ピュアガール
[Message: Lilly] Feature: , 420 , 料理小町 , 料理小町 , MaidStatus / 特徴タイプ / 料理小町
[Message: Lilly] Feature: , 430 , 料理職人 , 料理職人 , MaidStatus / 特徴タイプ / 料理職人
[Message: Lilly] Feature: , 440 , のど自慢 , のど自慢 , MaidStatus / 特徴タイプ / のど自慢
[Message: Lilly] Feature: , 450 , エンジェルボイス , エンジェルボイス , MaidStatus / 特徴タイプ / エンジェルボイス
[Message: Lilly] Feature: , 460 , 天性のリズム感 , 天性のリズム感 , MaidStatus / 特徴タイプ / 天性のリズム感
[Message: Lilly] Feature: , 470 , フェアリー , フェアリー , MaidStatus / 特徴タイプ / フェアリー
[Message: Lilly] Feature: , 480 , むっつり , むっつり , MaidStatus / 特徴タイプ / むっつり
[Message: Lilly] Feature: , 490 , エッチな願望 , エッチな願望 , MaidStatus / 特徴タイプ / エッチな願望
[Message: Lilly] Feature: , 500 , 委員長 , 委員長 , MaidStatus / 特徴タイプ / 委員長
[Message: Lilly] Feature: , 510 , 誠実 , 誠実 , MaidStatus / 特徴タイプ / 誠実
[Message: Lilly] Feature: , 520 , 愛が重い , 愛が重い , MaidStatus / 特徴タイプ / 愛が重い
[Message: Lilly] Feature: , 530 , 純愛 , 純愛 , MaidStatus / 特徴タイプ / 純愛
[Message: Lilly] Feature: , 540 , 精緻なシェフ , 精緻なシェフ , MaidStatus / 特徴タイプ / 精緻なシェフ
[Message: Lilly] Feature: , 550 , 美食シェフ , 美食シェフ , MaidStatus / 特徴タイプ / 美食シェフ
[Message: Lilly] Feature: , 560 , 癒しの音色 , 癒しの音色 , MaidStatus / 特徴タイプ / 癒しの音色
[Message: Lilly] Feature: , 570 , クリアボイス , クリアボイス , MaidStatus / 特徴タイプ / クリアボイス
[Message: Lilly] Feature: , 580 , フルコンボマニア , フルコンボマニア , MaidStatus / 特徴タイプ / フルコンボマニア
[Message: Lilly] Feature: , 590 , 舞姫 , 舞姫 , MaidStatus / 特徴タイプ / 舞姫
[Message: Lilly] Feature: , 600 , お姉さん , お姉さん , MaidStatus / 特徴タイプ / お姉さん
[Message: Lilly] Feature: , 610 , アダルティ , アダルティ , MaidStatus / 特徴タイプ / アダルティ
[Message: Lilly] Feature: , 620 , 洗練 , 洗練 , MaidStatus / 特徴タイプ / 洗練
[Message: Lilly] Feature: , 630 , 凜然 , 凜然 , MaidStatus / 特徴タイプ / 凜然
[Message: Lilly] Feature: , 640 , 乙女な一面 , 乙女な一面 , MaidStatus / 特徴タイプ / 乙女な一面
[Message: Lilly] Feature: , 650 , 乙女の夢 , 乙女の夢 , MaidStatus / 特徴タイプ / 乙女の夢
[Message: Lilly] Feature: , 660 , シェフ , シェフ , MaidStatus / 特徴タイプ / シェフ
[Message: Lilly] Feature: , 670 , ロイヤルシェフ , ロイヤルシェフ , MaidStatus / 特徴タイプ / ロイヤルシェフ
[Message: Lilly] Feature: , 680 , シンガー , シンガー , MaidStatus / 特徴タイプ / シンガー
[Message: Lilly] Feature: , 690 , ディーヴァ , ディーヴァ , MaidStatus / 特徴タイプ / ディーヴァ
[Message: Lilly] Feature: , 700 , ビートセンス , ビートセンス , MaidStatus / 特徴タイプ / ビートセンス
[Message: Lilly] Feature: , 710 , エンパイアダンサー , エンパイアダンサー , MaidStatus / 特徴タイプ / エンパイアダンサー
[Message: Lilly] Feature: , 720 , 母の味 , 母の味 , MaidStatus / 特徴タイプ / 母の味
[Message: Lilly] Feature: , 730 , 料亭の味 , 料亭の味 , MaidStatus / 特徴タイプ / 料亭の味
[Message: Lilly] Feature: , 740 , 優しい声 , 優しい声 , MaidStatus / 特徴タイプ / 優しい声
[Message: Lilly] Feature: , 750 , 歌姫 , 歌姫 , MaidStatus / 特徴タイプ / 歌姫
[Message: Lilly] Feature: , 760 , 踊り子 , 踊り子 , MaidStatus / 特徴タイプ / 踊り子
[Message: Lilly] Feature: , 770 , 神楽舞 , 神楽舞 , MaidStatus / 特徴タイプ / 神楽舞
[Message: Lilly] Feature: , 780 , 専属シェフ , 専属シェフ , MaidStatus / 特徴タイプ / 専属シェフ
[Message: Lilly] Feature: , 790 , 三ツ星シェフ , 三ツ星シェフ , MaidStatus / 特徴タイプ / 三ツ星シェフ
[Message: Lilly] Feature: , 800 , スイートボイス , スイートボイス , MaidStatus / 特徴タイプ / スイートボイス
[Message: Lilly] Feature: , 810 , エンパイアボイス , エンパイアボイス , MaidStatus / 特徴タイプ / エンパイアボイス
[Message: Lilly] Feature: , 820 , ヒートダンサー , ヒートダンサー , MaidStatus / 特徴タイプ / ヒートダンサー
[Message: Lilly] Feature: , 830 , クイーンダンサー , クイーンダンサー , MaidStatus / 特徴タイプ / クイーンダンサー
[Message: Lilly] Feature: , 840 , 美食好き , 美食好き , MaidStatus / 特徴タイプ / 美食好き
[Message: Lilly] Feature: , 850 , 料理研究家 , 料理研究家 , MaidStatus / 特徴タイプ / 料理研究家
[Message: Lilly] Feature: , 860 , 魅惑の歌声 , 魅惑の歌声 , MaidStatus / 特徴タイプ / 魅惑の歌声
[Message: Lilly] Feature: , 870 , セイレーン , セイレーン , MaidStatus / 特徴タイプ / セイレーン
[Message: Lilly] Feature: , 880 , クールステップ , クールステップ , MaidStatus / 特徴タイプ / クールステップ
[Message: Lilly] Feature: , 890 , ミューズ , ミューズ , MaidStatus / 特徴タイプ / ミューズ
[Message: Lilly] Feature: , 900 , 好みを把握 , 好みを把握 , MaidStatus / 特徴タイプ / 好みを把握
[Message: Lilly] Feature: , 910 , 撫子料理 , 撫子料理 , MaidStatus / 特徴タイプ / 撫子料理
[Message: Lilly] Feature: , 920 , 大和魂 , 大和魂 , MaidStatus / 特徴タイプ / 大和魂
[Message: Lilly] Feature: , 930 , 月光の歌声 , 月光の歌声 , MaidStatus / 特徴タイプ / 月光の歌声
[Message: Lilly] Feature: , 940 , 和のステップ , 和のステップ , MaidStatus / 特徴タイプ / 和のステップ
[Message: Lilly] Feature: , 950 , 冥土の踊り子 , 冥土の踊り子 , MaidStatus / 特徴タイプ / 冥土の踊り子
[Message: Lilly] Feature: , 960 , 田舎料理 , 田舎料理 , MaidStatus / 特徴タイプ / 田舎料理
[Message: Lilly] Feature: , 970 , 愛情たっぷり , 愛情たっぷり , MaidStatus / 特徴タイプ / 愛情たっぷり
[Message: Lilly] Feature: , 980 , 癒しの歌声 , 癒しの歌声 , MaidStatus / 特徴タイプ / 癒しの歌声
[Message: Lilly] Feature: , 990 , 聖母の安らぎ , 聖母の安らぎ , MaidStatus / 特徴タイプ / 聖母の安らぎ
[Message: Lilly] Feature: , 1000 , セクシーステップ , セクシーステップ , MaidStatus / 特徴タイプ / セクシーステップ
[Message: Lilly] Feature: , 1010 , エロチックダンス , エロチックダンス , MaidStatus / 特徴タイプ / エロチックダンス
[Message: Lilly] Feature: , 1020 , 無防備乙女 , 無防備乙女 , MaidStatus / 特徴タイプ / 無防備乙女
[Message: Lilly] Feature: , 1030 , 窓辺の少女 , 窓辺の少女 , MaidStatus / 特徴タイプ / 窓辺の少女
[Message: Lilly] Feature: , 1040 , 司書さん , 司書さん , MaidStatus / 特徴タイプ / 司書さん
[Message: Lilly] Feature: , 1050 , 深窓の麗人 , 深窓の麗人 , MaidStatus / 特徴タイプ / 深窓の麗人
[Message: Lilly] Feature: , 1060 , ダイヤの原石 , ダイヤの原石 , MaidStatus / 特徴タイプ / ダイヤの原石
[Message: Lilly] Feature: , 1070 , 読書小町 , 読書小町 , MaidStatus / 特徴タイプ / 読書小町
[Message: Lilly] Feature: , 1080 , 歩く献立表 , 歩く献立表 , MaidStatus / 特徴タイプ / 歩く献立表
[Message: Lilly] Feature: , 1090 , 握り寿司の名人 , 握り寿司の名人 , MaidStatus / 特徴タイプ / 握り寿司の名人
[Message: Lilly] Feature: , 1100 , お風呂場歌手 , お風呂場歌手 , MaidStatus / 特徴タイプ / お風呂場歌手
[Message: Lilly] Feature: , 1110 , 歌行燈 , 歌行燈 , MaidStatus / 特徴タイプ / 歌行燈
[Message: Lilly] Feature: , 1120 , 創作ダンス趣味 , 創作ダンス趣味 , MaidStatus / 特徴タイプ / 創作ダンス趣味
[Message: Lilly] Feature: , 1130 , 鹿踊りのはじまり , 鹿踊りのはじまり , MaidStatus / 特徴タイプ / 鹿踊りのはじまり
[Message: Lilly] Feature: , 1140 , サドシェフ , サドシェフ , MaidStatus / 特徴タイプ / サドシェフ
[Message: Lilly] Feature: , 1150 , ド・サドシェフ , ド・サドシェフ , MaidStatus / 特徴タイプ / ド・サドシェフ
[Message: Lilly] Feature: , 1160 , 月天の歌姫 , 月天の歌姫 , MaidStatus / 特徴タイプ / 月天の歌姫
[Message: Lilly] Feature: , 1170 , 鮮烈天使 , 鮮烈天使 , MaidStatus / 特徴タイプ / 鮮烈天使
[Message: Lilly] Feature: , 1180 , 桜華の踊り子 , 桜華の踊り子 , MaidStatus / 特徴タイプ / 桜華の踊り子
[Message: Lilly] Feature: , 1190 , 花鳥風月 , 花鳥風月 , MaidStatus / 特徴タイプ / 花鳥風月
[Message: Lilly] Feature: , 1200 , 天性の小悪魔 , 天性の小悪魔 , MaidStatus / 特徴タイプ / 天性の小悪魔
[Message: Lilly] Feature: , 1210 , ニンフェット , ニンフェット , MaidStatus / 特徴タイプ / ニンフェット
[Message: Lilly] Feature: , 1220 , 玉の輿 , 玉の輿 , MaidStatus / 特徴タイプ / 玉の輿
[Message: Lilly] Feature: , 1230 , 社長令嬢 , 社長令嬢 , MaidStatus / 特徴タイプ / 社長令嬢
[Message: Lilly] Feature: , 1240 , 無邪気 , 無邪気 , MaidStatus / 特徴タイプ / 無邪気
[Message: Lilly] Feature: , 1250 , アイドル , アイドル , MaidStatus / 特徴タイプ / アイドル
[Message: Lilly] Feature: , 1260 , モテ料理 , モテ料理 , MaidStatus / 特徴タイプ / モテ料理
[Message: Lilly] Feature: , 1270 , 胃袋掴み , 胃袋掴み , MaidStatus / 特徴タイプ / 胃袋掴み
[Message: Lilly] Feature: , 1280 , 天使のささやき , 天使のささやき , MaidStatus / 特徴タイプ / 天使のささやき
[Message: Lilly] Feature: , 1290 , 悪魔のささやき , 悪魔のささやき , MaidStatus / 特徴タイプ / 悪魔のささやき
[Message: Lilly] Feature: , 1300 , ゆるふわダンサー , ゆるふわダンサー , MaidStatus / 特徴タイプ / ゆるふわダンサー
[Message: Lilly] Feature: , 1310 , セクシーダンサー , セクシーダンサー , MaidStatus / 特徴タイプ / セクシーダンサー
[Message: Lilly] Feature: , 1320 , アクティブ料理人 , アクティブ料理人 , MaidStatus / 特徴タイプ / アクティブ料理人
[Message: Lilly] Feature: , 1330 , アグレッシブ料理人 , アグレッシブ料理人 , MaidStatus / 特徴タイプ / アグレッシブ料理人
[Message: Lilly] Feature: , 1340 , カラオケっ子 , カラオケっ子 , MaidStatus / 特徴タイプ / カラオケっ子
[Message: Lilly] Feature: , 1350 , カラオケマスター , カラオケマスター , MaidStatus / 特徴タイプ / カラオケマスター
[Message: Lilly] Feature: , 1360 , エネルギッシュダンス , エネルギッシュダンス , MaidStatus / 特徴タイプ / エネルギッシュダンス
[Message: Lilly] Feature: , 1370 , ブレイクダンサー , ブレイクダンサー , MaidStatus / 特徴タイプ / ブレイクダンサー
[Message: Lilly] Feature: , 1380 , 大人のお姉さん , 大人のお姉さん , MaidStatus / 特徴タイプ / 大人のお姉さん
[Message: Lilly] Feature: , 1390 , 憧れの人 , 憧れの人 , MaidStatus / 特徴タイプ / 憧れの人
[Message: Lilly] Feature: , 1400 , 高貴なあの人 , 高貴なあの人 , MaidStatus / 特徴タイプ / 高貴なあの人
[Message: Lilly] Feature: , 1410 , 高嶺の花 , 高嶺の花 , MaidStatus / 特徴タイプ / 高嶺の花
[Message: Lilly] Feature: , 1420 , 可愛いお姉さん , 可愛いお姉さん , MaidStatus / 特徴タイプ / 可愛いお姉さん
[Message: Lilly] Feature: , 1430 , 天然乙女 , 天然乙女 , MaidStatus / 特徴タイプ / 天然乙女
[Message: Lilly] Feature: , 1440 , あの時の味 , あの時の味 , MaidStatus / 特徴タイプ / あの時の味
[Message: Lilly] Feature: , 1450 , 料理教室講師 , 料理教室講師 , MaidStatus / 特徴タイプ / 料理教室講師
[Message: Lilly] Feature: , 1460 , 合唱部OG , 合唱部OG , MaidStatus / 特徴タイプ / 合唱部OG
[Message: Lilly] Feature: , 1470 , ウィスパーボイス , ウィスパーボイス , MaidStatus / 特徴タイプ / ウィスパーボイス
[Message: Lilly] Feature: , 1480 , チアダンサー , チアダンサー , MaidStatus / 特徴タイプ / チアダンサー
[Message: Lilly] Feature: , 1490 , 社交界の女王 , 社交界の女王 , MaidStatus / 特徴タイプ / 社交界の女王
[Message: Lilly] Feature: , 1500 , 大人の色気 , 大人の色気 , MaidStatus / 特徴タイプ / 大人の色気
[Message: Lilly] Feature: , 1510 , 隙だらけ , 隙だらけ , MaidStatus / 特徴タイプ / 隙だらけ
[Message: Lilly] Feature: , 1520 , メイド長 , メイド長 , MaidStatus / 特徴タイプ / メイド長
[Message: Lilly] Feature: , 1530 , 理想のメイド秘書 , 理想のメイド秘書 , MaidStatus / 特徴タイプ / 理想のメイド秘書
[Message: Lilly] Feature: , 1540 , アナウンサー , アナウンサー , MaidStatus / 特徴タイプ / アナウンサー
[Message: Lilly] Feature: , 1550 , お祭り好き , お祭り好き , MaidStatus / 特徴タイプ / お祭り好き
[Message: Lilly] Feature: , 1560 , 小料理屋の女将 , 小料理屋の女将 , MaidStatus / 特徴タイプ / 小料理屋の女将
[Message: Lilly] Feature: , 1570 , 料亭の技術 , 料亭の技術 , MaidStatus / 特徴タイプ / 料亭の技術
[Message: Lilly] Feature: , 1580 , カラオケ常連 , カラオケ常連 , MaidStatus / 特徴タイプ / カラオケ常連
[Message: Lilly] Feature: , 1590 , ボイストレーナー , ボイストレーナー , MaidStatus / 特徴タイプ / ボイストレーナー
[Message: Lilly] Feature: , 1600 , 創作ダンサー , 創作ダンサー , MaidStatus / 特徴タイプ / 創作ダンサー
[Message: Lilly] Feature: , 1610 , 実はアイドル志望？ , 実はアイドル志望？ , MaidStatus / 特徴タイプ / 実はアイドル志望？
[Message: Lilly] Feature: , 1620 , 無意識な誘惑 , 無意識な誘惑 , MaidStatus / 特徴タイプ / 無意識な誘惑
[Message: Lilly] Feature: , 1630 , 意識的無防備 , 意識的無防備 , MaidStatus / 特徴タイプ / 意識的無防備
[Message: Lilly] Feature: , 1640 , 不安定な魅力 , 不安定な魅力 , MaidStatus / 特徴タイプ / 不安定な魅力
[Message: Lilly] Feature: , 1650 , 箱入りお嬢様 , 箱入りお嬢様 , MaidStatus / 特徴タイプ / 箱入りお嬢様
[Message: Lilly] Feature: , 1660 , 庇護欲促進 , 庇護欲促進 , MaidStatus / 特徴タイプ / 庇護欲促進
[Message: Lilly] Feature: , 1670 , 小動物系女子 , 小動物系女子 , MaidStatus / 特徴タイプ / 小動物系女子
[Message: Lilly] Feature: , 1680 , 見よう見まねのママの味 , 見よう見まねのママの味 , MaidStatus / 特徴タイプ / 見よう見まねのママの味
[Message: Lilly] Feature: , 1690 , 愛妹弁当 , 愛妹弁当 , MaidStatus / 特徴タイプ / 愛妹弁当
[Message: Lilly] Feature: , 1700 , 家族カラオケ , 家族カラオケ , MaidStatus / 特徴タイプ / 家族カラオケ
[Message: Lilly] Feature: , 1710 , 妹はアイドル志望 , 妹はアイドル志望 , MaidStatus / 特徴タイプ / 妹はアイドル志望
[Message: Lilly] Feature: , 1720 , スポーティうさぎ , スポーティうさぎ , MaidStatus / 特徴タイプ / スポーティうさぎ
[Message: Lilly] Feature: , 1730 , テーマパークダンサー , テーマパークダンサー , MaidStatus / 特徴タイプ / テーマパークダンサー
[Message: Lilly] Feature: , 1740 , 無自覚な魅力 , 無自覚な魅力 , MaidStatus / 特徴タイプ / 無自覚な魅力
[Message: Lilly] Feature: , 1750 , 潔癖症 , 潔癖症 , MaidStatus / 特徴タイプ / 潔癖症
[Message: Lilly] Feature: , 1760 , 風紀委員長 , 風紀委員長 , MaidStatus / 特徴タイプ / 風紀委員長
[Message: Lilly] Feature: , 1770 , 規律の鬼 , 規律の鬼 , MaidStatus / 特徴タイプ / 規律の鬼
[Message: Lilly] Feature: , 1780 , 束縛 , 束縛 , MaidStatus / 特徴タイプ / 束縛
[Message: Lilly] Feature: , 1790 , 強い独占欲 , 強い独占欲 , MaidStatus / 特徴タイプ / 強い独占欲
[Message: Lilly] Feature: , 1800 , 豊富な知識 , 豊富な知識 , MaidStatus / 特徴タイプ / 豊富な知識
[Message: Lilly] Feature: , 1810 , 完璧な計量 , 完璧な計量 , MaidStatus / 特徴タイプ / 完璧な計量
[Message: Lilly] Feature: , 1820 , 器用貧乏 , 器用貧乏 , MaidStatus / 特徴タイプ / 器用貧乏
[Message: Lilly] Feature: , 1830 , 努力の天才 , 努力の天才 , MaidStatus / 特徴タイプ / 努力の天才
[Message: Lilly] Feature: , 1840 , 暗記重視 , 暗記重視 , MaidStatus / 特徴タイプ / 暗記重視
[Message: Lilly] Feature: , 1850 , 完璧主義 , 完璧主義 , MaidStatus / 特徴タイプ / 完璧主義
[Message: Lilly] Feature: , 1860 , 無邪気な令嬢 , 無邪気な令嬢 , MaidStatus / 特徴タイプ / 無邪気な令嬢
[Message: Lilly] Feature: , 1870 , 天真爛漫 , 天真爛漫 , MaidStatus / 特徴タイプ / 天真爛漫
[Message: Lilly] Feature: , 1880 , 貴婦人 , 貴婦人 , MaidStatus / 特徴タイプ / 貴婦人
[Message: Lilly] Feature: , 1890 , 優雅無比 , 優雅無比 , MaidStatus / 特徴タイプ / 優雅無比
[Message: Lilly] Feature: , 1900 , 華々しい , 華々しい , MaidStatus / 特徴タイプ / 華々しい
[Message: Lilly] Feature: , 1910 , 咲き誇る笑顔 , 咲き誇る笑顔 , MaidStatus / 特徴タイプ / 咲き誇る笑顔
[Message: Lilly] Feature: , 1920 , 美食を嗜む , 美食を嗜む , MaidStatus / 特徴タイプ / 美食を嗜む
[Message: Lilly] Feature: , 1930 , 醍醐味を知る , 醍醐味を知る , MaidStatus / 特徴タイプ / 醍醐味を知る
[Message: Lilly] Feature: , 1940 , 麗しい歌声 , 麗しい歌声 , MaidStatus / 特徴タイプ / 麗しい歌声
[Message: Lilly] Feature: , 1950 , プリマドンナ , プリマドンナ , MaidStatus / 特徴タイプ / プリマドンナ
[Message: Lilly] Feature: , 1960 , 社交ダンスの姫君 , 社交ダンスの姫君 , MaidStatus / 特徴タイプ / 社交ダンスの姫君
[Message: Lilly] Feature: , 1970 , マスカレードの女王 , マスカレードの女王 , MaidStatus / 特徴タイプ / マスカレードの女王
[Message: Lilly] Feature: , 1980 , 密かな人気者 , 密かな人気者 , MaidStatus / 特徴タイプ / 密かな人気者
[Message: Lilly] Feature: , 1990 , 芸能スカウトの的 , 芸能スカウトの的 , MaidStatus / 特徴タイプ / 芸能スカウトの的
[Message: Lilly] Feature: , 2000 , 典型的良い子 , 典型的良い子 , MaidStatus / 特徴タイプ / 典型的良い子
[Message: Lilly] Feature: , 2010 , クラス委員長 , クラス委員長 , MaidStatus / 特徴タイプ / クラス委員長
[Message: Lilly] Feature: , 2020 , 仔犬 , 仔犬 , MaidStatus / 特徴タイプ / 仔犬
[Message: Lilly] Feature: , 2030 , みんなのアイドル , みんなのアイドル , MaidStatus / 特徴タイプ / みんなのアイドル
[Message: Lilly] Feature: , 2040 , 家庭の味 , 家庭の味 , MaidStatus / 特徴タイプ / 家庭の味
[Message: Lilly] Feature: , 2050 , メイドのたしなみ , メイドのたしなみ , MaidStatus / 特徴タイプ / メイドのたしなみ
[Message: Lilly] Feature: , 2060 , カラオケボックスのヌシ , カラオケボックスのヌシ , MaidStatus / 特徴タイプ / カラオケボックスのヌシ
[Message: Lilly] Feature: , 2070 , 歌ってみたの女神 , 歌ってみたの女神 , MaidStatus / 特徴タイプ / 歌ってみたの女神
[Message: Lilly] Feature: , 2080 , 優れた体幹 , 優れた体幹 , MaidStatus / 特徴タイプ / 優れた体幹
[Message: Lilly] Feature: , 2090 , 芸者ガール , 芸者ガール , MaidStatus / 特徴タイプ / 芸者ガール
[Message: Lilly] Feature: , 2100 , 蜜の罠 , 蜜の罠 , MaidStatus / 特徴タイプ / 蜜の罠
[Message: Lilly] Feature: , 2110 , 痴女 , 痴女 , MaidStatus / 特徴タイプ / 痴女
[Message: Lilly] Feature: , 2120 , 自由人 , 自由人 , MaidStatus / 特徴タイプ / 自由人
[Message: Lilly] Feature: , 2130 , マゾヒスト , マゾヒスト , MaidStatus / 特徴タイプ / マゾヒスト
[Message: Lilly] Feature: , 2140 , 日陰の華 , 日陰の華 , MaidStatus / 特徴タイプ / 日陰の華
[Message: Lilly] Feature: , 2150 , 保護対象 , 保護対象 , MaidStatus / 特徴タイプ / 保護対象
[Message: Lilly] Feature: , 2160 , ドMシェフ , ドMシェフ , MaidStatus / 特徴タイプ / ドMシェフ
[Message: Lilly] Feature: , 2170 , 未来のパティシエ , 未来のパティシエ , MaidStatus / 特徴タイプ / 未来のパティシエ
[Message: Lilly] Feature: , 2180 , エロシンガー , エロシンガー , MaidStatus / 特徴タイプ / エロシンガー
[Message: Lilly] Feature: , 2190 , 姿無き歌い手 , 姿無き歌い手 , MaidStatus / 特徴タイプ / 姿無き歌い手
[Message: Lilly] Feature: , 2200 , 情熱な踊り , 情熱な踊り , MaidStatus / 特徴タイプ / 情熱な踊り
[Message: Lilly] Feature: , 2210 , エロダンサー志望 , エロダンサー志望 , MaidStatus / 特徴タイプ / エロダンサー志望
[Message: Lilly] Feature: , 2220 , おじさんキラー , おじさんキラー , MaidStatus / 特徴タイプ / おじさんキラー
[Message: Lilly] Feature: , 2230 , 計算ずくの距離感 , 計算ずくの距離感 , MaidStatus / 特徴タイプ / 計算ずくの距離感
[Message: Lilly] Feature: , 2240 , 家事手伝いマスター , 家事手伝いマスター , MaidStatus / 特徴タイプ / 家事手伝いマスター
[Message: Lilly] Feature: , 2250 , 上流階級への憧れ , 上流階級への憧れ , MaidStatus / 特徴タイプ / 上流階級への憧れ
[Message: Lilly] Feature: , 2260 , 褒められ耐性ゼロ , 褒められ耐性ゼロ , MaidStatus / 特徴タイプ / 褒められ耐性ゼロ
[Message: Lilly] Feature: , 2270 , アイドル級 , アイドル級 , MaidStatus / 特徴タイプ / アイドル級
[Message: Lilly] Feature: , 2280 , お茶汲み係 , お茶汲み係 , MaidStatus / 特徴タイプ / お茶汲み係
[Message: Lilly] Feature: , 2290 , 代理主婦 , 代理主婦 , MaidStatus / 特徴タイプ / 代理主婦
[Message: Lilly] Feature: , 2300 , 暇つぶしの歌姫 , 暇つぶしの歌姫 , MaidStatus / 特徴タイプ / 暇つぶしの歌姫
[Message: Lilly] Feature: , 2310 , 無観席シンガー , 無観席シンガー , MaidStatus / 特徴タイプ / 無観席シンガー
[Message: Lilly] Feature: , 2320 , 独学ダンサー , 独学ダンサー , MaidStatus / 特徴タイプ / 独学ダンサー
[Message: Lilly] Feature: , 2330 , まねっこアイドル , まねっこアイドル , MaidStatus / 特徴タイプ / まねっこアイドル
[Info: Lilly]
[Message: Lilly] Personal: , 10 , C , Pure , 純真で健気な妹系 , MaidStatus / 性格タイプ / Pure
[Message: Lilly] Personal: , 20 , B , Cool , クールで寡黙 , MaidStatus / 性格タイプ / Cool
[Message: Lilly] Personal: , 30 , A , Pride , プライドが高く負けず嫌い , MaidStatus / 性格タイプ / Pride
[Message: Lilly] Personal: , 40 , D , Yandere , 病的な程一途な大和撫子 , MaidStatus / 性格タイプ / Yandere
[Message: Lilly] Personal: , 50 , E , Anesan , 母性的なお姉ちゃん , MaidStatus / 性格タイプ / Anesan
[Message: Lilly] Personal: , 60 , F , Genki , 健康的でスポーティなボクっ娘 , MaidStatus / 性格タイプ / Genki
[Message: Lilly] Personal: , 70 , G , Sadist , Ｍ心を刺激するドＳ女王様 , MaidStatus / 性格タイプ / Sadist
[Message: Lilly] Personal: , 80 , A1 , Muku , 無垢 , MaidStatus / 性格タイプ / Muku
[Message: Lilly] Personal: , 90 , B1 , Majime , 真面目 , MaidStatus / 性格タイプ / Majime
[Message: Lilly] Personal: , 100 , C1 , Rindere , 凜デレ , MaidStatus / 性格タイプ / Rindere
[Message: Lilly] Personal: , 110 , D1 , Silent , 文学少女 , MaidStatus / 性格タイプ / Silent
[Message: Lilly] Personal: , 120 , E1 , Devilish , 小悪魔 , MaidStatus / 性格タイプ / Devilish
[Message: Lilly] Personal: , 130 , F1 , Ladylike , おしとやか , MaidStatus / 性格タイプ / Ladylike
[Message: Lilly] Personal: , 140 , G1 , Secretary , メイド秘書 , MaidStatus / 性格タイプ / Secretary
[Message: Lilly] Personal: , 150 , H1 , Sister , ふわふわ妹 , MaidStatus / 性格タイプ / Sister
[Message: Lilly] Personal: , 160 , J1 , Curtness , 無愛想 , MaidStatus / 性格タイプ / Curtness
[Message: Lilly] Personal: , 170 , K1 , Missy , お嬢様 , MaidStatus / 性格タイプ / Missy
[Message: Lilly] Personal: , 180 , L1 , Childhood , 幼馴染 , MaidStatus / 性格タイプ / Childhood
[Message: Lilly] Personal: , 190 , M1 , Masochist , ド変態ドＭ , MaidStatus / 性格タイプ / Masochist
[Message: Lilly] Personal: , 200 , N1 , Crafty , 腹黒 , MaidStatus / 性格タイプ / Crafty
[Info: Lilly]
[Message: Lilly] Propensity: , 0 , 淫乱 , 淫乱 , MaidStatus / 性癖タイプ / 淫乱
[Message: Lilly] Propensity: , 1 , 変態 , 変態 , MaidStatus / 性癖タイプ / 変態
[Message: Lilly] Propensity: , 2 , 奉仕好き , 奉仕好き , MaidStatus / 性癖タイプ / 奉仕好き
[Message: Lilly] Propensity: , 3 , M女 , M女 , MaidStatus / 性癖タイプ / M女
[Message: Lilly] Propensity: , 4 , 尻穴好き , 尻穴好き , MaidStatus / 性癖タイプ / 尻穴好き
[Message: Lilly] Propensity: , 5 , 二穴好き , 二穴好き , MaidStatus / 性癖タイプ / 二穴好き
[Message: Lilly] Propensity: , 6 , 中出し好き , 中出し好き , MaidStatus / 性癖タイプ / 中出し好き
[Message: Lilly] Propensity: , 7 , 飲精好き , 飲精好き , MaidStatus / 性癖タイプ / 飲精好き
[Message: Lilly] Propensity: , 10 , 敏感体質 , 敏感体質 , MaidStatus / 性癖タイプ / 敏感体質
[Message: Lilly] Propensity: , 20 , 敏感なクリトリス , 敏感なクリトリス , MaidStatus / 性癖タイプ / 敏感なクリトリス
[Message: Lilly] Propensity: , 30 , お漏らし癖 , お漏らし癖 , MaidStatus / 性癖タイプ / お漏らし癖
[Message: Lilly] Propensity: , 40 , 中出し大好き , 中出し大好き , MaidStatus / 性癖タイプ / 中出し大好き
[Message: Lilly] Propensity: , 50 , 甘えん坊 , 甘えん坊 , MaidStatus / 性癖タイプ / 甘えん坊
[Message: Lilly] Propensity: , 60 , いじられ好き , いじられ好き , MaidStatus / 性癖タイプ / いじられ好き
[Message: Lilly] Propensity: , 70 , お尻中出し大好き , お尻中出し大好き , MaidStatus / 性癖タイプ / お尻中出し大好き
[Message: Lilly] Propensity: , 80 , 変態の素質 , 変態の素質 , MaidStatus / 性癖タイプ / 変態の素質
[Message: Lilly] Propensity: , 90 , ごっくん大好き , ごっくん大好き , MaidStatus / 性癖タイプ / ごっくん大好き
[Message: Lilly] Propensity: , 100 , 桃色泡姫 , 桃色泡姫 , MaidStatus / 性癖タイプ / 桃色泡姫
[Message: Lilly] Propensity: , 110 , ドMの素質 , ドMの素質 , MaidStatus / 性癖タイプ / ドMの素質
[Message: Lilly] Propensity: , 120 , ムチがご褒美 , ムチがご褒美 , MaidStatus / 性癖タイプ / ムチがご褒美
[Message: Lilly] Propensity: , 130 , ロウがご褒美 , ロウがご褒美 , MaidStatus / 性癖タイプ / ロウがご褒美
[Message: Lilly] Propensity: , 140 , 罵倒がご褒美 , 罵倒がご褒美 , MaidStatus / 性癖タイプ / 罵倒がご褒美
[Message: Lilly] Propensity: , 150 , ドS女王様 , ドS女王様 , MaidStatus / 性癖タイプ / ドS女王様
[Info: Lilly]   메이드 관리 - 클래스
[Message: Lilly] JobClass , 0 , Old , ノービスメイド , Novice , 駆け出しのメイドにぴったりなメイドクラス。最初はここから始まります。メイドの作法をしっかり覚えていきましょう。 , MaidStatus / ジョブクラス / 説明 / Novice
[Message: Lilly] JobClass , 1 , Old , ラブリーメイド , Lovely , 可憐なメイドにぴったりなメイドクラス。守ってあげたくなるような可愛さを滲ませます。 , MaidStatus / ジョブクラス / 説明 / Lovely
[Message: Lilly] JobClass , 2 , Old , エレガンスメイド , Elegance , 優雅な振る舞いがとても似合うメイドためのメイドクラス。普段から優雅に振る舞っていると、ついつい紅茶を淹れたくなるようです。 , MaidStatus / ジョブクラス / 説明 / Elegance
[Message: Lilly] JobClass , 3 , Old , セクシーメイド , Sexy , セクシーな魅力で周りを惹きつけるメイドのためのメイドクラス。刺さる視線がもっとメイドをセクシーにしていきます。 , MaidStatus / ジョブクラス / 説明 / Sexy
[Message: Lilly] JobClass , 4 , Old , イノセントメイド , Innocent , 無垢な心を忘れないメイドのためのメイドクラス。小さな気遣いや思いやりが、周りの人々を幸せにしていきます。 , MaidStatus / ジョブクラス / 説明 / Innocent
[Message: Lilly] JobClass , 5 , Old , チャームメイド , Charm , ステキな魅力を振りまくメイドのためのメイドクラス。内面から溢れ出す不思議な魅力に、虜になってしまいそうです。 , MaidStatus / ジョブクラス / 説明 / Charm
[Message: Lilly] JobClass , 6 , Old , レディーメイド , Ready , 落ち着いたレディのためのメイドクラス。品のあるレディを目指すメイドにも。最高のレディを目指していきましょう。 , MaidStatus / ジョブクラス / 説明 / Ready
[Message: Lilly] JobClass , 10 , New , ノービスメイド , Novice2 , メイドとしての初歩を歩み始めた事を表すジョブメイドクラス。まずはメイドとしての知識と経験を。 , MaidStatus / ジョブクラス / 説明 / Novice2
[Message: Lilly] JobClass , 20 , New , コンシェルジュメイド , Concierge , ベッドメイクやドア係など、ホテルで働く際に必要な技術の習熟を表すジョブメイドクラス。ホテルを建設する事で習得可能。特別な記念日のフォローなども行います。 , MaidStatus / ジョブクラス / 説明 / Concierge
[Message: Lilly] JobClass , 30 , New , ウェイトレスメイド , Waitress , コーヒーの淹れ方、お菓子作りなど、オープンカフェで働く際に必要な技術の習熟を表すジョブメイドクラス。オープンカフェを建設する事で習得可能。ほっと一息の安らぎの時間を提供します。 , MaidStatus / ジョブクラス / 説明 / Waitress
[Message: Lilly] JobClass , 40 , New , メートルメイド , Maitre , 料理やサーブなど、レストランで働く際に必要な技術の習熟を表すジョブメイドクラス。レストランを建設する事で習得可能。皿洗いからメニュー考案など、その業務は多岐に渡ります。 , MaidStatus / ジョブクラス / 説明 / Maitre
[Message: Lilly] JobClass , 50 , New , バーテンダーメイド , Bartender , カクテル作りやトークなど、バーラウンジで働く際に必要な技術の習熟を表すジョブメイドクラス。バーラウンジを建設する事で習得可能。優雅な夜をお客様に提供する為、メイドは奮闘します。 , MaidStatus / ジョブクラス / 説明 / Bartender
[Message: Lilly] JobClass , 60 , New , セラピストメイド , Therapist , 施術全般や健康に関わる事など、リフレで働く際に必要な技術の習熟を表すジョブメイドクラス。リフレを建設する事で習得可能。お客様の身も心も癒します。 , MaidStatus / ジョブクラス / 説明 / Therapist
[Message: Lilly] JobClass , 70 , New , エンパイアメイド , Empire , 接客、上流階級の知識など、劇場で働く際に必要な技術の習熟を表すジョブメイドクラス。劇場を建設する事で習得可能。最も伝統的なジョブメイドクラスです。 , MaidStatus / ジョブクラス / 説明 / Empire
[Message: Lilly] JobClass , 80 , New , ディーラーメイド , Dealer , ルーレット操作やトランプテクニックなど、カジノで働く際に必要な技術の習熟を表すジョブメイドクラス。カジノを建設する事で習得可能。賭けのスリルに興じる夜に華を添えます。 , MaidStatus / ジョブクラス / 説明 / Dealer
[Message: Lilly] JobClass , 90 , New , ヒーリングメイド , Healing , お風呂やベッドの上での奉仕など、ソープランドで働く際に必要な技術の習熟を表すジョブメイドクラス。ソープランドを建設する事で習得可能。溺れるような快楽の夜をお客様に。 , MaidStatus / ジョブクラス / 説明 / Healing
[Message: Lilly] JobClass , 100 , New , ナイトメイド , Night , 女王様やM嬢など、SMクラブで働く際に必要な技術の習熟を表すジョブメイドクラス。SMクラブを建設する事で習得可能。背徳的でアブノーマルな奉仕をお客様に。 , MaidStatus / ジョブクラス / 説明 / Night
[Info: Lilly]  메이드 관리 - 밤시중 정보
[Message: Lilly] YotogiClass , 0 , Old , デビューメイド , Debut , 夜伽経験の浅い未熟なメイドにぴったりな夜伽クラス。夜伽の道は茨の道。じっくり技術を磨いていきましょう。 , MaidStatus / 夜伽クラス / 説明 / Debut
[Message: Lilly] YotogiClass , 1 , Old , ルードネスメイド , Rudeness , 夜伽に対して積極的なメイドにぴったりな夜伽クラス。美しくも淫らで、抜け出せない沼へようこそ。 , MaidStatus / 夜伽クラス / 説明 / Rudeness
[Message: Lilly] YotogiClass , 2 , Old , スレイブメイド , Slave , 被虐的なプレイも嬉々として受け入れてしまうメイドにぴったりな夜伽クラス。気絶には気を付けましょう。 , MaidStatus / 夜伽クラス / 説明 / Slave
[Message: Lilly] YotogiClass , 3 , Old , アブノーマルメイド , Abnormal , ちょっぴり変態的なプレイがクセになりそうなメイドにぴったりな夜伽クラス。だんだんそれもクセになる？ , MaidStatus / 夜伽クラス / 説明 / Abnormal
[Message: Lilly] YotogiClass , 4 , Share , 詰られサービスメイド , Service , ご主人様を詰る夜伽を習得する夜伽クラス。さっさとそこに跪いてブヒブヒ鳴きなさい。 , MaidStatus / 夜伽クラス / 説明 / Service
[Message: Lilly] YotogiClass , 5 , Old , エスコートメイド , Escort , 夜伽を自分からリードしていきたいと思うメイドにぴったりな夜伽クラス。甘くて淫靡な快楽の虜です。 , MaidStatus / 夜伽クラス / 説明 / Escort
[Message: Lilly] YotogiClass , 6 , Share , ソープベーシックメイド , Soap , 基礎的なソープランドで行う夜伽を習得する夜伽クラス。魅惑と癒しの一時をあなたに。 , MaidStatus / 夜伽クラス / 説明 / Soap
[Message: Lilly] YotogiClass , 7 , Old , スウィートメイド , Sweet , ご主人様と恋人以上な関係のメイドにぴったりな夜伽クラス。止まらないラブなハートが甘くホットに燃えあがる。 , MaidStatus / 夜伽クラス / 説明 / Sweet
[Message: Lilly] YotogiClass , 8 , Old , パーティメイド , Party , たくさんの男達とのプレイにも抵抗がないメイドにぴったりな夜伽クラス。くんずほぐれつ酒池肉林！今夜は皆で朝までパーリナイッ！ , MaidStatus / 夜伽クラス / 説明 / Party
[Message: Lilly] YotogiClass , 9 , Old , セックススレイブメイド , SexSlave , まるで性奴隷のような扱いを望むメイドにぴったりな夜伽クラス。メイドを苛める道具、多数取り揃えております。 , MaidStatus / 夜伽クラス / 説明 / SexSlave
[Message: Lilly] YotogiClass , 10 , Old , フェスティバルメイド , Festival , お祭りの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。メイドをしっかりと受け止めて上げましょう。 , MaidStatus / 夜伽クラス / 説明 / Festival
[Message: Lilly] YotogiClass , 11 , Share , ハーレムメイド , Harlem , 複数人のメイドでご主人様の相手をする夜伽を習得する夜伽クラス。ご主人様だけでも恥ずかしいのに…… , MaidStatus / 夜伽クラス / 説明 / Harlem
[Message: Lilly] YotogiClass , 12 , Old , ロイヤルソープメイド , Royalsoap , 泡やマットを使ったプレイが上手なメイドにぴったりな夜伽クラス。魅惑の泡に溺れてしまいそう。 , MaidStatus / 夜伽クラス / 説明 / Royalsoap
[Message: Lilly] YotogiClass , 13 , Old , プレイメイド , Toilet , トイレで変態的なエッチをしてしまうメイドにぴったりな夜伽クラス。誰かに見られないように注意です。 , MaidStatus / 夜伽クラス / 説明 / Toilet
[Message: Lilly] YotogiClass , 14 , Old , インモラルメイド , Immoral , 電車で変態的なエッチをしてしまうメイドにぴったりな夜伽クラス。背徳的な快楽を楽しみましょう。 , MaidStatus/夜伽クラス/説明/Immoral
[Message:     Lilly] YotogiClass , 15 , Old , ブライドメイド , Bride , まるで花嫁にするかようなエッチが大好きなメイドにぴったりな夜伽クラス。色々な体位で二人の愛を育みましょう。 , MaidStatus/夜伽クラス/説明/Bride
[Message:     Lilly] YotogiClass , 16 , Old , サキュバスメイド , Succubus , 淫魔のように精を搾り取りたいメイドにぴったりな夜伽クラス。ご主人様以外の男性にも可愛がってもらいましょう！ , MaidStatus/夜伽クラス/説明/Succubus
[Message:     Lilly] YotogiClass , 17 , Old , クイーンメイド , Queen , ご主人様を豚として扱いたいメイドにぴったりな夜伽クラス。主従逆転の快楽をメイド様から教えて頂きましょう！ , MaidStatus/夜伽クラス/説明/Queen
[Message:     Lilly] YotogiClass , 18 , Old , ダークネスメイド , Darkness , ダークでハードな夜伽を望むメイドにぴったりな夜伽クラス。その痛みはご主人様とメイドの為のもの。 , MaidStatus/夜伽クラス/説明/Darkness
[Message:     Lilly] YotogiClass , 19 , Old , スクールメイド , School , 学園での夜伽が大好きなメイドにぴったりな夜伽クラス。ご主人様という呼び方もここでは変わります。 , MaidStatus/夜伽クラス/説明/School
[Message:     Lilly] YotogiClass , 20 , Old , NTRメイド , NTR , お客様に心まで寝取られてしまったメイドにぴったりな夜伽クラス。お客様と乱れるメイドをしっかりと見てあげましょう。 , MaidStatus/夜伽クラス/説明/NTR
[Message:     Lilly] YotogiClass , 21 , Old , マリッジメイド , Marriage , 愛するご主人様との夜伽が大好きなメイドにぴったりな夜伽クラス。大好きなご主人様といつまでも。 , MaidStatus/夜伽クラス/説明/Marriage
[Message:     Lilly] YotogiClass , 22 , Old , リリィメイド , Lily , 女の子同士での夜伽が大好きなメイドにぴったりな夜伽クラス。メイドは禁断の夜伽に溺れます。 , MaidStatus/夜伽クラス/説明/Lily
[Message:     Lilly] YotogiClass , 23 , Old , カーニバルメイド , Carnival , お祭りの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。今度は謝肉祭だ！ , MaidStatus/夜伽クラス/説明/Carnival
[Message:     Lilly] YotogiClass , 24 , Old , チェリッシュメイド , Cherish , ご主人様だけの大切なメイドにぴったりな夜伽クラス。イチャイチャ、甘々。 , MaidStatus/夜伽クラス/説明/Cherish
[Message:     Lilly] YotogiClass , 25 , Old , バインドメイド , Bind , 逃げられぬ愛の拘束を求めるメイドにぴったりな夜伽クラス。ただ一人だけ、ご主人様の支配を受けたいから。 , MaidStatus/夜伽クラス/説明/Bind
[Message:     Lilly] YotogiClass , 26 , Old , エロティックメイド , Erotic , ド変態なエッチが大好きなメイドにぴったりな夜伽クラス。しっかりとメイドの痴態を目に焼き付けましょう。 , MaidStatus/夜伽クラス/説明/Erotic
[Message:     Lilly] YotogiClass , 27 , Old , セレブレイトメイド , Celebrate , お祭りの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。祝福されたメイドとエッチしましょう。 , MaidStatus/夜伽クラス/説明/Celebrate
[Message:     Lilly] YotogiClass , 28 , Old , ディスペアメイド , Despair , 群がる男達にムリヤリメイドが犯されてしまう夜伽クラス。メイドの顔が絶望に歪みます。 , MaidStatus/夜伽クラス/説明/Despair
[Message:     Lilly] YotogiClass , 29 , Old , エンプレスメイド , Empress , ご主人様をオモチャにしたいメイドにぴったりな夜伽クラス。それは愛の執着でもあります。 , MaidStatus/夜伽クラス/説明/Empress
[Message:     Lilly] YotogiClass , 30 , Old , イチャラブメイド , Ityalove , どれだけ幸せになっても、まだまだ足りないと感じるメイドにぴったりな夜伽クラス。いつまでもイチャラブ。 , MaidStatus/夜伽クラス/説明/Ityalove
[Message:     Lilly] YotogiClass , 31 , Old , ライブメイド , Live , ライブの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。ちょっと変態なエッチもご主人様となら。 , MaidStatus/夜伽クラス/説明/Live
[Message:     Lilly] YotogiClass , 32 , Old , アビスメイド , abyss , 群がる男達に無理矢理メイドがさらにハードに犯されてしまう夜伽クラス。それは失意の混沌。 , MaidStatus/夜伽クラス/説明/abyss
[Message:     Lilly] YotogiClass , 33 , Old , ピッグブリーダーメイド , Breeding , M豚を調教する事に特化した夜伽クラス。跪いて泣いて頼むのなら、相手してあげてもいいわ。 , MaidStatus/夜伽クラス/説明/Breeding
[Message:     Lilly] YotogiClass , 34 , Old , ラブバインドメイド , Victim , 拘束され、欲望のままに好き勝手に犯されてしまう夜伽クラス。メイドはご主人様の欲望の被害者なのか、それとも…… , MaidStatus/夜伽クラス/説明/Victim
[Message:     Lilly] YotogiClass , 35 , Old , アニバーサリーメイド , Anniversary , お祭りの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。これまでもこれからもよろしく！ , MaidStatus/夜伽クラス/説明/Anniversary
[Message:     Lilly] YotogiClass , 36 , Old , エクスタシーメイド , Ecstasy , 貪欲に快楽を求めるメイドにぴったりな夜伽クラス。メイドに感じた事の無い絶頂を何度でもプレゼントしましょう。 , MaidStatus/夜伽クラス/説明/Ecstasy
[Message:     Lilly] YotogiClass , 37 , Old , バグバグメイド , Bugbug , 長く続く名誉ある雑誌の為に用意された夜伽クラス。快楽と背徳の狭間をメイドと楽しみましょう。 , MaidStatus/夜伽クラス/説明/Bugbug
[Message:     Lilly] YotogiClass , 38 , Old , クライシスメイド , Crisis , 台の上で、ソファの上で、男たちに弄ばれる夜伽クラス。抵抗できないメイドの行く末は、まさに危機一髪。 , MaidStatus/夜伽クラス/説明/Crisis
[Message:     Lilly] YotogiClass , 39 , Old , ヒーリングメイド , Healing , メイドから癒やしと快楽を与えられたいときの夜伽クラス。時には優しく、時には厳しいご奉仕を貴方へ。 , MaidStatus/夜伽クラス/説明/Healing
[Message:     Lilly] YotogiClass , 40 , Old , オビディエントメイド , Obedient , 嗜虐的な欲望、その全てをメイドに与えたいときの夜伽クラス。もっと拘束して、もっと激しく苛め倒す。 , MaidStatus/夜伽クラス/説明/Obedient
[Message:     Lilly] YotogiClass , 41 , Share , スノーメイド , Snow , 冬のお祭りの雰囲気にあてられて、開放的になったメイドにぴったりな夜伽クラス。降りしきる雪の中、貴方と二人で…… , MaidStatus/夜伽クラス/説明/Snow
[Message:     Lilly] YotogiClass , 42 , New , 変態辱めセックスメイド , Hentaihazukasime , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/説明/Hentaihazukasime
[Message:     Lilly] YotogiClass , 43 , New , ソープご奉仕セックスメイド , Sorpgohousi , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/説明/Sorpgohousi
[Message:     Lilly] YotogiClass , 44 , Share , 変態辱めセックスメイド , Hentaihazukasime_old , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して……おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/説明/Hentaihazukasime_old
[Message:     Lilly] YotogiClass , 45 , Share , ソープご奉仕セックスメイド , Sorpgohousi_old , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ……？ , MaidStatus/夜伽クラス/説明/Sorpgohousi_old
[Message:     Lilly] YotogiClass , 46 , Share , アブノーマル卑猥セックスメイド , HiwaiSex , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex
[Message:     Lilly] YotogiClass , 47 , Share , 癒しラブラブ奉仕メイド , LoveLoveHoushi , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi
[Message:     Lilly] YotogiClass , 48 , New , アブノーマル卑猥セックスメイド , HiwaiSex_old , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex_old
[Message:     Lilly] YotogiClass , 49 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_old , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_old
[Message:     Lilly] YotogiClass , 50 , New , アブノーマル卑猥セックスメイド , HiwaiSex_ane , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex_ane
[Message:     Lilly] YotogiClass , 51 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_ane , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_ane
[Message:     Lilly] YotogiClass , 52 , New , 変態辱めセックスメイド , Hentaihazukasime_sil , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/説明/Hentaihazukasime_sil
[Message:     Lilly] YotogiClass , 53 , New , ソープご奉仕セックスメイド , Sorpgohousi_sil , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/説明/Sorpgohousi_sil
[Message:     Lilly] YotogiClass , 54 , New , アブノーマル卑猥セックスメイド , HiwaiSex_sil , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex_sil
[Message:     Lilly] YotogiClass , 55 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_sil , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_sil
[Message:     Lilly] YotogiClass , 56 , New , らぶらぶメイド , lovelove , ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 , MaidStatus/夜伽クラス/説明/lovelove
[Message:     Lilly] YotogiClass , 57 , New , 欲情メイド , yokujyou , ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… , MaidStatus/夜伽クラス/説明/yokujyou
[Message:     Lilly] YotogiClass , 58 , Share , らぶらぶメイド , lovelove_old , ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 , MaidStatus/夜伽クラス/説明/lovelove_old
[Message:     Lilly] YotogiClass , 59 , Share , 欲情メイド , yokujyou_old , ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… , MaidStatus/夜伽クラス/説明/yokujyou_old
[Message:     Lilly] YotogiClass , 60 , New , 変態辱めセックスメイド , Hentaihazukasime_dvl , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/説明/Hentaihazukasime_dvl
[Message:     Lilly] YotogiClass , 61 , New , ソープご奉仕セックスメイド , Sorpgohousi_dvl , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/説明/Sorpgohousi_dvl
[Message:     Lilly] YotogiClass , 62 , New , アブノーマル卑猥セックスメイド , HiwaiSex_dvl , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex_dvl
[Message:     Lilly] YotogiClass , 63 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_dvl , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_dvl
[Message:     Lilly] YotogiClass , 64 , New , 拘束変態責めメイド , Kousokuhentaiseme , メイドを拘束し、ハードに責める夜伽クラス。拘束し、絶対に逃げられない状態でメイドを苛烈に責めてあげましょう , MaidStatus/夜伽クラス/説明/Kousokuhentaiseme
[Message:     Lilly] YotogiClass , 65 , New , ハードセックスメイド , Hardsex , 愛しのメイドに対し、もっとたくさんセックスをして愛し合いたい時の目夜伽クラス。終わらない夜をメイドと。 , MaidStatus/夜伽クラス/説明/Hardsex
[Message:     Lilly] YotogiClass , 66 , Share , 拘束変態責めメイド , Kousokuhentaiseme_old , メイドを拘束し、ハードに責める夜伽クラス。拘束し、絶対に逃げられない状態でメイドを苛烈に責めてあげましょう , MaidStatus/夜伽クラス/説明/Kousokuhentaiseme_old
[Message:     Lilly] YotogiClass , 67 , Share , ハードセックスメイド , Hardsex_old , 愛しのメイドに対し、もっとたくさんセックスをして愛し合いたい時の目夜伽クラス。終わらない夜をメイドと。 , MaidStatus/夜伽クラス/説明/Hardsex_old
[Message:     Lilly] YotogiClass , 68 , New , 変態辱めセックスメイド , Hentaihazukasime_ldy , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/説明/Hentaihazukasime_ldy
[Message:     Lilly] YotogiClass , 69 , New , ソープご奉仕セックスメイド , Sorpgohousi_ldy , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/説明/Sorpgohousi_ldy
[Message:     Lilly] YotogiClass , 70 , New , アブノーマル卑猥セックスメイド , HiwaiSex_ldy , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex_ldy
[Message:     Lilly] YotogiClass , 71 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_ldy , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_ldy
[Message:     Lilly] YotogiClass , 72 , Share , 服従セックスメイド , Fukujyuusex_old , メイドを服従させ拘束し、ご主人様の意のままにする夜伽クラス。全てはご主人様の為に。 , MaidStatus/夜伽クラス/説明/Fukujyuusex_old
[Message:     Lilly] YotogiClass , 73 , Share , ハードスワップセックスメイド , Hardswapsex_old , メイドに対し別の男とセックスさせる夜伽クラス。他の男に弄ばれ嬲られるメイド。 , MaidStatus/夜伽クラス/説明/Hardswapsex_old
[Message:     Lilly] YotogiClass , 74 , Share , バグバグオーダーメイド , BUGBUGorder_old , 長く続く名誉ある雑誌の為に用意された夜伽クラス。詰られたり露出したり、ド変態なプレイを楽しみましょう！ , MaidStatus/夜伽クラス/説明/BUGBUGorder_old
[Message:     Lilly] YotogiClass , 75 , Share , マスタープレイメイド , Masterplay_old , メイドがご主人様となり、ご主人様を嬲り倒す夜伽クラス。『お前』と呼び方が変わり、詰られてしまいます。 , MaidStatus/夜伽クラス/説明/Masterplay_old
[Message:     Lilly] YotogiClass , 76 , Share , 変態恥辱セックスメイド , Hentaitijyokusex_old , メイドにポーズをとらせたり、メイドを乱暴に責めたりする夜伽クラス。変態恥辱責めに、メイドは淫らな本性を現し…… , MaidStatus/夜伽クラス/説明/Hentaitijyokusex_old
[Message:     Lilly] YotogiClass , 77 , Share , 甘やかしマザーメイド , Mamameido_old , 普段疲れているあなたを、ママが包まれるように甘やかしてくれる夜伽クラス。ママはあなたを優しく癒してくれます。 , MaidStatus/夜伽クラス/説明/Mamameido_old
[Message:     Lilly] YotogiClass , 78 , Share , ご奉仕マザーメイド , MamameidoB_old , 普段疲れているあなたへ、ママがご奉仕してくれる夜伽クラス。ママはあなたを優しくご奉仕してくれます。 , MaidStatus/夜伽クラス/説明/MamameidoB_old
[Message:     Lilly] YotogiClass , 80 , New , アクティブセックスメイド , Activesex , 凜デレタイプ限定の夜伽クラス。お姉さんらしくリードしてくれます。 , MaidStatus/夜伽クラス/説明/Activesex
[Message:     Lilly] YotogiClass , 81 , Share , マニアックプレイメイド , Maniacplay_old , かなり変態的な夜伽クラス。犬の真似をさせたり、お尻の穴を舐めたり……異常な性癖を楽しみましょう。 , MaidStatus/夜伽クラス/説明/Maniacplay_old
[Message:     Lilly] YotogiClass , 82 , Share , ハード拘束プレイメイド , Hardkousokuplay_old , 器具を用いてメイドを拘束する夜伽クラス。がちがちに拘束したメイドを、ご主人様のお好きなままに。 , MaidStatus/夜伽クラス/説明/Hardkousokuplay_old
[Message:     Lilly] YotogiClass , 83 , Share , アブノーマル卑猥メイド・病 , HiwaiSex_yan , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。※この夜伽クラスはヤンデレのみ習得致します※ , MaidStatus/夜伽クラス/説明/HiwaiSex_yan
[Message:     Lilly] YotogiClass , 84 , Share , 癒しラブラブ奉仕メイド・病 , LoveLoveHoushi_yan , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。※この夜伽クラスはヤンデレのみ習得致します※ , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_yan
[Message:     Lilly] YotogiClass , 90 , New , アブノーマルエクスタシーメイド , Abnormalecstasy , 普通のセックスでは行わないような、強い快楽をメイドに与える夜伽クラス。普通ではいられない…… , MaidStatus/夜伽クラス/説明/Abnormalecstasy
[Message:     Lilly] YotogiClass , 100 , New , アブノーマルセックスメイド , Abnormalsex , 普通のセックスでは行わないようなセックスをする夜伽を習得する夜伽クラス。快楽からは逃れられない。 , MaidStatus/夜伽クラス/説明/Abnormalsex
[Message:     Lilly] YotogiClass , 110 , New , エクスタシーベーシックメイド , Ecstasybasic , 基礎的なセックス夜伽を習得する夜伽クラス。何度も何度も愛し合いましょう。 , MaidStatus/夜伽クラス/説明/Ecstasybasic
[Message:     Lilly] YotogiClass , 120 , New , 献身セックスメイド , Kensinsex , 無垢タイプ限定の夜伽クラス。無垢なメイドがセックスで誠心誠意ご主人様を癒します。 , MaidStatus/夜伽クラス/説明/Kensinsex
[Message:     Lilly] YotogiClass , 130 , New , 専属専用ラブラブアナルセックスメイド , Senzokusenyouloveloveanalsex , 専属メイド限定の夜伽クラス。ご主人様だけに大好きを伝え、お尻を捧げます。 , MaidStatus/夜伽クラス/説明/Senzokusenyouloveloveanalsex
[Message:     Lilly] YotogiClass , 140 , New , 専属専用ラブラブセックスメイド , Senzokusenyoulovelovesex , 専属メイド限定の夜伽クラス。ご主人様だけに大好きを伝えます。 , MaidStatus/夜伽クラス/説明/Senzokusenyoulovelovesex
[Message:     Lilly] YotogiClass , 150 , New , 倒錯変態プレイメイド , hentaiplay , 真面目タイプ限定の夜伽クラス。通常ではありえない場所での倒錯的な夜伽を頑張ってくれます。 , MaidStatus/夜伽クラス/説明/hentaiplay
[Message:     Lilly] YotogiClass , 151 , New , 倒錯変態セックスメイド , hentaisex , 真面目タイプ限定の夜伽クラス。通常ではありえないシチュエーションでの倒錯的なセックスに溺れます。 , MaidStatus/夜伽クラス/説明/hentaisex
[Message:     Lilly] YotogiClass , 160 , New , オナニーベーシックメイド , Onaniebasic , 基礎的なオナニー夜伽を習得する夜伽クラス。ご主人様の前でオナニーを行います。 , MaidStatus/夜伽クラス/説明/Onaniebasic
[Message:     Lilly] YotogiClass , 170 , New , 変態エキスパートメイド , Hentaiexpert , 過激な変態夜伽を習得する夜伽クラス。羞恥は鮮烈な夜伽のスパイス。 , MaidStatus/夜伽クラス/説明/Hentaiexpert
[Message:     Lilly] YotogiClass , 180 , New , 変態ベーシックメイド , Hentaibasic , 基礎的な変態夜伽を習得する夜伽クラス。羞恥を強く感じるメイドを愛でましょう。 , MaidStatus/夜伽クラス/説明/Hentaibasic
[Message:     Lilly] YotogiClass , 190 , New , 露出ベーシックメイド , Rosyutubasic , 基礎的な露出夜伽を習得する夜伽クラス。エンパイアクラブから出て、更なる羞恥を感じましょう。 , MaidStatus/夜伽クラス/説明/Rosyutubasic
[Message:     Lilly] YotogiClass , 200 , New , ソープエキスパートメイド , Soapexpert , ソープランドで行う熟練したソープ夜伽を習得する夜伽クラス。培った癒しの技術でご主人様に尽くします。 , MaidStatus/夜伽クラス/説明/Soapexpert
[Message:     Lilly] YotogiClass , 210 , New , 奉仕エキスパートメイド , Houshiexpert , 発展的な奉仕夜伽を習得する夜伽クラス。より濃厚な奉仕をご主人様に。 , MaidStatus/夜伽クラス/説明/Houshiexpert
[Message:     Lilly] YotogiClass , 220 , New , 奉仕ベーシックメイド , HoushiBasic , 基礎的な奉仕夜伽を習得する夜伽クラス。エンパイアクラブのメイドとしての基礎の基礎といえるでしょう。 , MaidStatus/夜伽クラス/説明/HoushiBasic
[Message:     Lilly] YotogiClass , 230 , New , Ｍ女エキスパートメイド , Monnaexpert , 苛烈な被虐夜伽を習得する夜伽クラス。ご主人様となら、どんな事だって。 , MaidStatus/夜伽クラス/説明/Monnaexpert
[Message:     Lilly] YotogiClass , 240 , New , Ｍ女ベーシックメイド , MonnaBasic , 基礎的な被虐夜伽を習得する夜伽クラス。ご主人様から与えられる痛みなら…… , MaidStatus/夜伽クラス/説明/MonnaBasic
[Message:     Lilly] YotogiClass , 250 , New , スワッピングベーシックメイド , Swappingbasic , ご主人様以外の男性とスワッピングを行う夜伽を習得する夜伽クラス。ご主人様以外となんて…… , MaidStatus/夜伽クラス/説明/Swappingbasic
[Message:     Lilly] YotogiClass , 260 , New , 乱交愛撫ベーシックメイド , Rankoubasic , ご主人様以外の男性を加えて複数人で行う夜伽を夜伽を習得する夜伽クラス。 , MaidStatus/夜伽クラス/説明/Rankoubasic
[Message:     Lilly] YotogiClass , 270 , New , 酔いアナルベーシックメイド , Yoianalbasic , 酔った状態で行う夜伽を習得する夜伽クラス。開放的になったメイドのお尻の味を楽しみましょう。 , MaidStatus/夜伽クラス/説明/Yoianalbasic
[Message:     Lilly] YotogiClass , 280 , New , 酔いエキスパートメイド , Yoiexpart , 酔った状態で行う夜伽を習得する夜伽クラス。開放的になったメイドに、過激な夜伽を頼みましょう。 , MaidStatus/夜伽クラス/説明/Yoiexpart
[Message:     Lilly] YotogiClass , 290 , New , 酔いベーシックメイド , Yoibasic , 酔った状態で行う夜伽を習得する夜伽クラス。開放的になったメイドとの甘い夜伽を楽しみましょう。 , MaidStatus/夜伽クラス/説明/Yoibasic
[Message:     Lilly] YotogiClass , 300 , New , 媚薬プレイメイド , Biyakuplay , 媚薬を用いた夜伽を習得する夜伽クラス。塗布型の媚薬を塗って、普段と違う声色のメイドのメイドを楽しみましょう。 , MaidStatus/夜伽クラス/説明/Biyakuplay
[Message:     Lilly] YotogiClass , 310 , New , バージンメイド , Virginplay , 初々しい反応を見せる夜伽クラス。初めての相手は、あなた。※処女を喪失すると夜伽クラスは消失します。 , MaidStatus/夜伽クラス/説明/Virginplay
[Message:     Lilly] YotogiClass , 320 , New , 目隠しプレイメイド , Mekakusiplay , 目隠しを用いた夜伽を習得する夜伽クラス。見えないだけ敏感に…… , MaidStatus/夜伽クラス/説明/Mekakusiplay
[Message:     Lilly] YotogiClass , 330 , New , 告白プレイメイド , Kokuhakuplay , フリーメイド限定夜伽クラス。お客様との行為を詳細に、淫靡に告白します。 , MaidStatus/夜伽クラス/説明/Kokuhakuplay
[Message:     Lilly] YotogiClass , 340 , New , エクスタシーアナルベーシックメイド , Ecstasyanalbasic , 基礎的なお尻でのセックス夜伽を習得する夜伽クラス。お尻でもご主人様と…… , MaidStatus/夜伽クラス/説明/Ecstasyanalbasic
[Message:     Lilly] YotogiClass , 350 , New , 露出奉仕メイド , Rosyutuhousi , 無垢タイプ限定の夜伽クラス。優しい無垢なメイドがお外でも健気に奉仕します。 , MaidStatus/夜伽クラス/説明/Rosyutuhousi
[Message:     Lilly] YotogiClass , 360 , New , 変態ハードメイド , HentaiHard , 凜デレタイプ限定の夜伽クラス。より過激な変態行為をお姉さんと。 , MaidStatus/夜伽クラス/説明/HentaiHard
[Message:     Lilly] YotogiClass , 370 , New , 献身奉仕メイド , Kensinhoushi , 無垢タイプ限定の夜伽クラス。無垢なメイドが献身的に奉仕します。 , MaidStatus/夜伽クラス/説明/Kensinhoushi
[Message:     Lilly] YotogiClass , 380 , New , 変態奉仕メイド , Hentai , 真面目タイプ限定の夜伽クラス。むっつりな真面目タイプは、ご主人様を洗っているうちに…… , MaidStatus/夜伽クラス/説明/Hentai
[Message:     Lilly] YotogiClass , 390 , New , アクティブ奉仕メイド , Activehoushi , 凜デレタイプ限定の夜伽クラス。お姉さんらしく奉仕してくれます。 , MaidStatus/夜伽クラス/説明/Activehoushi
[Message:     Lilly] YotogiClass , 400 , New , 失禁セックスメイド , Sikkinsex , 真面目タイプ限定の夜伽クラス。吊るされ、犯されているうちに真面目タイプは…… , MaidStatus/夜伽クラス/説明/Sikkinsex
[Message:     Lilly] YotogiClass , 410 , New , M女ハードメイド , Monnahard , 凜デレタイプ限定の夜伽クラス。凜デレタイプのみに耐えられる痛烈にハードな夜伽を。 , MaidStatus/夜伽クラス/説明/Monnahard
[Message:     Lilly] YotogiClass , 420 , New , M女従順メイド , Monnajyuujyun , 無垢タイプ限定の夜伽クラス。従順なメイドを乱暴に犯します。 , MaidStatus/夜伽クラス/説明/Monnajyuujyun
[Message:     Lilly] YotogiClass , 430 , New , ハードスワッピングメイド , Hardrankou , 凜デレタイプ限定の夜伽クラス。ご主人様以外の男性に、激しく、過激に犯されます。 , MaidStatus/夜伽クラス/説明/Hardrankou
[Message:     Lilly] YotogiClass , 440 , New , 変態スワッピングメイド , Hentairankou , 真面目タイプ限定の夜伽クラス。真面目なメイドがご主人様以外の男性に、激しく、 , MaidStatus/夜伽クラス/説明/Hentairankou
[Message:     Lilly] YotogiClass , 450 , New , 嬲り乱交メイド , Naburirankou , 無垢タイプ限定の夜伽クラス。無垢なメイドに、複数の男のいやらしい手が伸びます。 , MaidStatus/夜伽クラス/説明/Naburirankou
[Message:     Lilly] YotogiClass , 460 , New , ノービスメイド , beginner , エンパイアクラブのメイドとしての基礎的な夜伽クラス。全てはここから。 , MaidStatus/夜伽クラス/説明/beginner
[Message:     Lilly] YotogiClass , 480 , New , ハンドクイーンメイド , HandQueen , 女王様になりたいメイドにぴったりな夜伽クラス。夜の女王様への第一歩。 , MaidStatus/夜伽クラス/説明/HandQueen
[Message:     Lilly] YotogiClass , 490 , New , 甘えプレイメイド , Amaeplay , 文学少女タイプ専用の夜伽クラス。甘えん坊になったあの子に、たっぷり甘えてもらいましょう。 , MaidStatus/夜伽クラス/説明/Amaeplay
[Message:     Lilly] YotogiClass , 500 , New , 言いなりプレイメイド , Iinariplay , 文学少女タイプ専用の夜伽クラス。ご主人様の言う事なら、何でも…… , MaidStatus/夜伽クラス/説明/Iinariplay
[Message:     Lilly] YotogiClass , 510 , New , 忠実奉仕メイド , TyujitsuHoushi , 文学少女タイプ専用の夜伽クラス。今まで勉強した奉仕を披露して、一生懸命奉仕してくれます。 , MaidStatus/夜伽クラス/説明/TyujitsuHoushi
[Message:     Lilly] YotogiClass , 520 , New , 語らせ責めメイド , Katarase , 文学少女タイプ専用の夜伽クラス。普段言わないようなあんな言葉やこんな言葉を、無理やり喋らせてみましょう。 , MaidStatus/夜伽クラス/説明/Katarase
[Message:     Lilly] YotogiClass , 530 , New , 語らせスワップメイド , KataraseSwap , 文学少女タイプ専用の夜伽クラス。お客様へ語りかけるあの子を見たいあなたへ。 , MaidStatus/夜伽クラス/説明/KataraseSwap
[Message:     Lilly] YotogiClass , 540 , New , 嬌声プレイメイド , Kyouseiplay , 文学少女タイプ専用の夜伽クラス。あの子にあられもない言葉を言わせたいあなたへ。 , MaidStatus/夜伽クラス/説明/Kyouseiplay
[Message:     Lilly] YotogiClass , 550 , New , 逆転プレイメイド , Gyakutenplay , 小悪魔タイプ専用の夜伽クラス。いつもからかってくるあの子に、やり返してあげましょう。 , MaidStatus/夜伽クラス/説明/Gyakutenplay
[Message:     Lilly] YotogiClass , 560 , New , ビッチメイド , Bitch , 小悪魔タイプ専用の夜伽クラス。ビッチになってしまったあの子が、あなたにどんな言葉を投げかけるでしょうか…… , MaidStatus/夜伽クラス/説明/Bitch
[Message:     Lilly] YotogiClass , 570 , New , 小悪魔メイド , Koakuma , 小悪魔タイプ専用の夜伽クラス。あなたに喜んで貰うため、いつも以上に小悪魔っぽく…… , MaidStatus/夜伽クラス/説明/Koakuma
[Message:     Lilly] YotogiClass , 580 , New , リードお姉さんメイド , Leadoneesan , おしとやかタイプ専用の夜伽クラス。おしとやかなお姉さんにリードされて果ててしまいましょう。 , MaidStatus/夜伽クラス/説明/Leadoneesan
[Message:     Lilly] YotogiClass , 590 , New , M豚調教メイド , Mbutaoneesan , おしとやかタイプ専用の夜伽クラス。M豚化してしまったお姉さん嬲りを楽しみましょう。 , MaidStatus/夜伽クラス/説明/Mbutaoneesan
[Message:     Lilly] YotogiClass , 600 , New , M豚目隠しメイド , Mbutamekakushi , おしとやかタイプ専用の夜伽クラス。M豚化してしまったお姉さんを目隠ししてイタズラしてしまいましょう。 , MaidStatus/夜伽クラス/説明/Mbutamekakushi
[Message:     Lilly] YotogiClass , 610 , New , 甘やかしお姉さんメイド , Amayakasioneesan , おしとやかタイプ専用の夜伽クラス。おしとやかなお姉さんに思い切り甘えましょう。 , MaidStatus/夜伽クラス/説明/Amayakasioneesan
[Message:     Lilly] YotogiClass , 620 , New , 見下し詰りメイド , Mikudasinaziri , おしとやかタイプ専用の夜伽クラス。おしとやかなお姉さんに見下されるのは……とても素敵な体験になるでしょう。 , MaidStatus/夜伽クラス/説明/Mikudasinaziri
[Message:     Lilly] YotogiClass , 630 , New , テリブルクイーンメイド , Terribleｑueen , ご主人様を危険なぐらい詰り倒したいメイドの為の夜伽クラス。全てはドMなご主人様の為に。 , MaidStatus/夜伽クラス/説明/Terribleｑueen
[Message:     Lilly] YotogiClass , 640 , New , 孕ませメイド , Haramase , メイド秘書タイプ専用の夜伽クラス。何としてでもご主人様との赤ちゃんを孕みたいと思っています。 , MaidStatus/夜伽クラス/説明/Haramase
[Message:     Lilly] YotogiClass , 650 , New , 従順犬メイド , Juujyun , メイド秘書タイプ専用の夜伽クラス。飼い主様の命令に絶対従う、従順な犬となってセックスをするメイドになります。 , MaidStatus/夜伽クラス/説明/Juujyun
[Message:     Lilly] YotogiClass , 660 , New , 熟練ソープメイド , Jukurensoap , メイド秘書タイプ専用の夜伽クラス。メイド秘書にしか体得できない、熟練したソープの技術を持っています。 , MaidStatus/夜伽クラス/説明/Jukurensoap
[Message:     Lilly] YotogiClass , 670 , New , 邪淫奉仕メイド , Jainhoushi , メイド秘書タイプ専用の夜伽クラス。背徳的なまでに淫らに、複数の飢えた男達に奉仕を行います。 , MaidStatus/夜伽クラス/説明/Jainhoushi
[Message:     Lilly] YotogiClass , 680 , New , 主従逆転メイド , Shyujyuugyakuten , メイド秘書タイプ専用の夜伽クラス。ご主人様との主従関係を逆転させ、苛烈にご主人様を責めます。 , MaidStatus/夜伽クラス/説明/Shyujyuugyakuten
[Message:     Lilly] YotogiClass , 700 , New , メスガキメイド , Mesugaki , 妹タイプ専用の夜伽クラス。 , MaidStatus/夜伽クラス/説明/Mesugaki
[Message:     Lilly] YotogiClass , 710 , New , わがままメイド , Wagamama , 妹タイプ専用の夜伽クラス。 , MaidStatus/夜伽クラス/説明/Wagamama
[Message:     Lilly] YotogiClass , 720 , New , 退行メイド , Taikou , 妹タイプ専用の夜伽クラス。 , MaidStatus/夜伽クラス/説明/Taikou
[Message:     Lilly] YotogiClass , 730 , New , ハードスワップメイド , Hardswap , 妹タイプ専用の夜伽クラス。 , MaidStatus/夜伽クラス/説明/Hardswap
[Message:     Lilly] YotogiClass , 740 , New , おもらしわんちゃんメイド , Omorashiwantyan , 妹タイプ専用の夜伽クラス。 , MaidStatus/夜伽クラス/説明/Omorashiwantyan
[Message:     Lilly] YotogiClass , 750 , New , 説教メイド , Sekkyou , 無愛想タイプ専用の夜伽クラス。ご主人様を徹底的に説教して、真人間に矯正させようとしています。 , MaidStatus/夜伽クラス/説明/Sekkyou
[Message:     Lilly] YotogiClass , 760 , New , 反論封印メイド , Hanlon_ng , 無愛想タイプ専用の夜伽クラス。反論を封印され、渋々従順な状態になっています。 , MaidStatus/夜伽クラス/説明/Hanlon_ng
[Message:     Lilly] YotogiClass , 770 , New , 欲望開放メイド , Yokuboukaihou , 無愛想タイプ専用の夜伽クラス。特殊なお酒を飲まされて、普段理性に押し込められている欲望が開放されています。 , MaidStatus/夜伽クラス/説明/Yokuboukaihou
[Message:     Lilly] YotogiClass , 1000 , New , 服従セックスメイド , Fukujyuusex , メイドを服従させ拘束し、ご主人様の意のままにする夜伽クラス。全てはご主人様の為に。 , MaidStatus/夜伽クラス/説明/Fukujyuusex
[Message:     Lilly] YotogiClass , 1010 , New , ハードスワップセックスメイド , Hardswapsex , メイドに対し別の男とセックスさせる夜伽クラス。他の男に弄ばれ嬲られるメイド。 , MaidStatus/夜伽クラス/説明/Hardswapsex
[Message:     Lilly] YotogiClass , 1020 , New , 変態辱めセックスメイド , Hentaihazukasime_sec , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/説明/Hentaihazukasime_sec
[Message:     Lilly] YotogiClass , 1030 , New , ソープご奉仕セックスメイド , Sorpgohousi_sec , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/説明/Sorpgohousi_sec
[Message:     Lilly] YotogiClass , 1040 , New , アブノーマル卑猥セックスメイド , HiwaiSex_sec , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex_sec
[Message:     Lilly] YotogiClass , 1050 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_sec , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_sec
[Message:     Lilly] YotogiClass , 1060 , New , 変態辱めセックスメイド , Hentaihazukasime_sis , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/説明/Hentaihazukasime_sis
[Message:     Lilly] YotogiClass , 1070 , New , ソープご奉仕セックスメイド , Sorpgohousi_sis , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/説明/Sorpgohousi_sis
[Message:     Lilly] YotogiClass , 1080 , New , アブノーマル卑猥セックスメイド , HiwaiSex_sis , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex_sis
[Message:     Lilly] YotogiClass , 1090 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_sis , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_sis
[Message:     Lilly] YotogiClass , 1100 , New , バグバグオーダーメイド , BUGBUGorder , 長く続く名誉ある雑誌の為に用意された夜伽クラス。詰られたり露出したり、ド変態なプレイを楽しみましょう！ , MaidStatus/夜伽クラス/説明/BUGBUGorder
[Message:     Lilly] YotogiClass , 1200 , New , マスタープレイメイド , Masterplay , メイドがご主人様となり、ご主人様を嬲り倒す夜伽クラス。『お前』と呼び方が変わり、詰られてしまいます。 , MaidStatus/夜伽クラス/説明/Masterplay
[Message:     Lilly] YotogiClass , 1210 , New , 変態恥辱セックスメイド , Hentaitijyokusex , メイドにポーズをとらせたり、メイドを乱暴に責めたりする夜伽クラス。変態恥辱責めに、メイドは淫らな本性を現し…… , MaidStatus/夜伽クラス/説明/Hentaitijyokusex
[Message:     Lilly] YotogiClass , 1300 , New , 変態トイレプレイメイド , Toiletplay , トイレで変態的なプレイを行う夜伽クラス。誰かが来てしまうのではというドキドキをあなたに。 , MaidStatus/夜伽クラス/説明/Toiletplay
[Message:     Lilly] YotogiClass , 1310 , New , ファイナルクイーンメイド , RoseWhipQueen , これ以上が無いぐらい苛烈な詰りを習得する夜伽クラス。お前はもう、これで終わりよ！ , MaidStatus/夜伽クラス/説明/RoseWhipQueen
[Message:     Lilly] YotogiClass , 1400 , New , 甘やかしマザーメイド , Mamameido , 普段疲れているあなたを、ママが包まれるように甘やかしてくれる夜伽クラス。ママはあなたを優しく癒してくれます。 , MaidStatus/夜伽クラス/説明/Mamameido
[Message:     Lilly] YotogiClass , 1410 , New , ご奉仕マザーメイド , MamameidoB , 普段疲れているあなたへ、ママがご奉仕してくれる夜伽クラス。ママはあなたを優しくご奉仕してくれます。 , MaidStatus/夜伽クラス/説明/MamameidoB
[Message:     Lilly] YotogiClass , 1450 , New , 淫語実況メイド , ingojikkyou , お嬢様タイプ専用の夜伽クラス。貞操観念の強いお嬢様に、卑猥な言葉を言わせ夜伽を実況させます。 , MaidStatus/夜伽クラス/説明/ingojikkyou
[Message:     Lilly] YotogiClass , 1460 , New , 王女様メイド , oujosama , お嬢様タイプ専用の夜伽クラス。お嬢様のワガママな部分を解放させて、王女のように振る舞いご主人様が尽くすようにします。 , MaidStatus/夜伽クラス/説明/oujosama
[Message:     Lilly] YotogiClass , 1470 , New , 隷属メイド , reizoku , お嬢様タイプ専用の夜伽クラス。普段はプライドの高いお嬢様を完全に屈服させて従順にします。 , MaidStatus/夜伽クラス/説明/reizoku
[Message:     Lilly] YotogiClass , 1480 , New , 抵抗メイド , teikou , お嬢様タイプ専用の夜伽クラス。プライドの高いお嬢様を他の男とさせて、抵抗する様子を楽しみます。 , MaidStatus/夜伽クラス/説明/teikou
[Message:     Lilly] YotogiClass , 1500 , New , 変態辱めセックスメイド , Hentaihazukasime_cur , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/説明/Hentaihazukasime_cur
[Message:     Lilly] YotogiClass , 1510 , New , ソープご奉仕セックスメイド , Sorpgohousi_cur , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/説明/Sorpgohousi_cur
[Message:     Lilly] YotogiClass , 1520 , New , アブノーマル卑猥セックスメイド , HiwaiSex_cur , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex_cur
[Message:     Lilly] YotogiClass , 1530 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_cur , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_cur
[Message:     Lilly] YotogiClass , 1600 , New , マニアックプレイメイド , Maniacplay , かなり変態的な夜伽クラス。犬の真似をさせたり、お尻の穴を舐めたり……異常な性癖を楽しみましょう。 , MaidStatus/夜伽クラス/説明/Maniacplay
[Message:     Lilly] YotogiClass , 1610 , New , ハード拘束プレイメイド , Hardkousokuplay , 器具を用いてメイドを拘束する夜伽クラス。がちがちに拘束したメイドを、ご主人様のお好きなままに。 , MaidStatus/夜伽クラス/説明/Hardkousokuplay
[Message:     Lilly] YotogiClass , 1700 , New , ラブバイブメイド , Lovevibe , ラブバイブを用いたプレイを行う夜伽クラス。かわい～いラブバイブを使ってメイドを可愛がりましょう！ , MaidStatus/夜伽クラス/説明/Lovevibe
[Message:     Lilly] YotogiClass , 1800 , New , 変態ハメ撮りメイド , Hamedori , カメラを用いた変態プレイを行う夜伽クラス。ご主人様のお気に入りオナニー動画をメイドと一緒に撮影しましょう！ , MaidStatus/夜伽クラス/説明/Hamedori
[Message:     Lilly] YotogiClass , 1900 , New , ソフトＳＭメイド , SoftSM , どちらかというとソフトなSMプレイを行う夜伽クラス。ご主人様と一緒に、SMプレイを楽しみましょう。 , MaidStatus/夜伽クラス/説明/SoftSM
[Message:     Lilly] YotogiClass , 1910 , New , ソフトＳＭメイド , SoftSM_add , どちらかというとソフトなSMプレイを行う夜伽クラス。ご主人様と一緒に、SMプレイを楽しみましょう。 , MaidStatus/夜伽クラス/説明/SoftSM_add
[Message:     Lilly] YotogiClass , 2000 , New , 変態辱めセックスメイド , Hentaihazukasime_mis , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/説明/Hentaihazukasime_mis
[Message:     Lilly] YotogiClass , 2010 , New , ソープご奉仕セックスメイド , Sorpgohousi_mis , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/説明/Sorpgohousi_mis
[Message:     Lilly] YotogiClass , 2020 , New , アブノーマル卑猥セックスメイド , HiwaiSex_mis , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex_mis
[Message:     Lilly] YotogiClass , 2030 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_mis , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_mis
[Message:     Lilly] YotogiClass , 2100 , New , 調教プレイメイド , Tyokyo , メイドに対し調教を行い、被虐的な快楽を教え込む夜伽クラス。愛するメイドと、背徳的な快楽を…… , MaidStatus/夜伽クラス/説明/Tyokyo
[Message:     Lilly] YotogiClass , 2110 , New , 調教プレイメイド , Tyokyo_add , メイドに対し調教を行い、被虐的な快楽を教え込む夜伽クラス。愛するメイドと、背徳的な快楽を…… , MaidStatus/夜伽クラス/説明/Tyokyo_add
[Message:     Lilly] YotogiClass , 2200 , New , いじめっ子メイド , Ijime , 幼馴染タイプ専用の夜伽クラス。普段は優しい幼馴染に、いじめっ子になりきってもらってご主人様を苛めてくれます , MaidStatus/夜伽クラス/説明/Ijime
[Message:     Lilly] YotogiClass , 2210 , New , 愛玩奴隷メイド , Aigandorei , 幼馴染タイプ専用の夜伽クラス。幼馴染がご主人様の奴隷になりきって、酷いことをして貰います。 , MaidStatus/夜伽クラス/説明/Aigandorei
[Message:     Lilly] YotogiClass , 2220 , New , 泥酔逆行メイド , Deisuigyakkou , 幼馴染タイプ専用の夜伽クラス。普段はしっかりしている幼馴染を酔わせて、子供っぽくしてエッチをします。 , MaidStatus/夜伽クラス/説明/Deisuigyakkou
[Message:     Lilly] YotogiClass , 2230 , New , 母性メイド , Bosei , 幼馴染タイプ専用の夜伽クラス。ご主人様の事が大好きな幼馴染が、母性を発揮して甘えさせてくれます。 , MaidStatus/夜伽クラス/説明/Bosei
[Message:     Lilly] YotogiClass , 2240 , New , 上級奉仕メイド , Jyokyuhousi , 幼馴染タイプ専用の夜伽クラス。ご奉仕が好きな幼馴染が、夜伽を重ねて上達したご奉仕を披露してくれます。 , MaidStatus/夜伽クラス/説明/Jyokyuhousi
[Message:     Lilly] YotogiClass , 2300 , New , ハードSMプレイメイド , HardSM , ハードなSMプレイを行う夜伽クラス。最早暴力ともいえる行為に、メイドは何を思うのか…… , MaidStatus/夜伽クラス/説明/HardSM
[Message:     Lilly] YotogiClass , 2310 , New , ハードSMプレイメイド , HardSM_add , ハードなSMプレイを行う夜伽クラス。最早暴力ともいえる行為に、メイドは何を思うのか…… , MaidStatus/夜伽クラス/説明/HardSM_add
[Message:     Lilly] YotogiClass , 2400 , New , 乱交プレイメイド , RankouPlay , ご主人様以外の男性と乱交する夜伽クラス。お慕いするご主人様の目の前で、ご主人様以外と…… , MaidStatus/夜伽クラス/説明/RankouPlay
[Message:     Lilly] YotogiClass , 2410 , New , 乱交プレイメイド , RankouPlay_add , ご主人様以外の男性と乱交する夜伽クラス。お慕いするご主人様の目の前で、ご主人様以外と…… , MaidStatus/夜伽クラス/説明/RankouPlay_add
[Message:     Lilly] YotogiClass , 2500 , New , ラブ奉仕メイド , LoveHoushi , ラブラブでいちゃいちゃなご奉仕を行う夜伽クラス。いつもお疲れのご主人様に、最大限の愛情奉仕を…… , MaidStatus/夜伽クラス/説明/LoveHoushi
[Message:     Lilly] YotogiClass , 2510 , New , ラブ奉仕メイド , LoveHoushi_add , ラブラブでいちゃいちゃなご奉仕を行う夜伽クラス。いつもお疲れのご主人様に、最大限の愛情奉仕を…… , MaidStatus/夜伽クラス/説明/LoveHoushi_add
[Message:     Lilly] YotogiClass , 2600 , New , マニアッククイーンメイド , Fukujyuunajirare , ご主人様を拘束したり詰り倒す夜伽クラス。それもこれも全てご主人様を愛しているからなのです。 , MaidStatus/夜伽クラス/説明/Fukujyuunajirare
[Message:     Lilly] YotogiClass , 2610 , New , マニアッククイーンメイド , Fukujyuunajirare_add , ご主人様を拘束したり詰り倒す夜伽クラス。それもこれも全てご主人様を愛しているからなのです。 , MaidStatus/夜伽クラス/説明/Fukujyuunajirare_add
[Message:     Lilly] YotogiClass , 2700 , New , 激甘ラブメイド , Gekiamalove , ただひたすらご主人様といちゃいちゃラブラブエッチをする夜伽クラス。ほかに望むものなど何もない！ , MaidStatus/夜伽クラス/説明/Gekiamalove
[Message:     Lilly] YotogiClass , 2800 , New , 逆転変態プレイメイド , GyakuAnalPlay , ついに一線を越え、メイドに犯されてしまう夜伽クラス。もう戻れない、背徳的なされる快楽を…… , MaidStatus/夜伽クラス/説明/GyakuAnalPlay
[Message:     Lilly] YotogiClass , 2810 , New , 逆転変態プレイメイド , GyakuAnalPlay_add , ついに一線を越え、メイドに犯されてしまう夜伽クラス。もう戻れない、背徳的なされる快楽を…… , MaidStatus/夜伽クラス/説明/GyakuAnalPlay_add
[Message:     Lilly] YotogiClass , 2900 , New , ＡＶ撮影プレイメイド , AVsatueiPlay , ＡＶ撮影プレイを行う夜伽クラス。ご主人様とのプレイをずっと残る動画にしちゃいましょう。 , MaidStatus/夜伽クラス/説明/AVsatueiPlay
[Message:     Lilly] YotogiClass , 3000 , New , エクスタシーメイド , SexSofaPlay , セックスソファーを用いてメイドとプレイを行う夜伽クラス。メイドとのイチャイチャラブラブプレイを楽しみましょう！ , MaidStatus/夜伽クラス/説明/SexSofaPlay
[Message:     Lilly] YotogiClass , 3010 , New , エクスタシーメイド , SexSofaPlay_add , セックスソファーを用いてメイドとプレイを行う夜伽クラス。メイドとのイチャイチャラブラブプレイを楽しみましょう！ , MaidStatus/夜伽クラス/説明/SexSofaPlay_add
[Message:     Lilly] YotogiClass , 3020 , New , エクスタシーメイド , SexSofaPlay_add2 , セックスソファーを用いてメイドとプレイを行う夜伽クラス。メイドとのイチャイチャラブラブプレイを楽しみましょう！ , MaidStatus/夜伽クラス/説明/SexSofaPlay_add2
[Message:     Lilly] YotogiClass , 3100 , New , クライシスメイド , DaiRankouPlay , ご主人様と多人数の男性とで乱交する夜伽クラス。快楽の宴に身を任せて…… , MaidStatus/夜伽クラス/説明/DaiRankouPlay
[Message:     Lilly] YotogiClass , 3110 , New , クライシスメイド , DaiRankouPlay_add , ご主人様と多人数の男性とで乱交する夜伽クラス。快楽の宴に身を任せて…… , MaidStatus/夜伽クラス/説明/DaiRankouPlay_add
[Message:     Lilly] YotogiClass , 3120 , New , クライシスメイド , DaiRankouPlay_add2 , ご主人様と多人数の男性とで乱交する夜伽クラス。快楽の宴に身を任せて…… , MaidStatus/夜伽クラス/説明/DaiRankouPlay_add2
[Message:     Lilly] YotogiClass , 3200 , New , 変態辱めセックスメイド , Hentaihazukasime_chi , 時には辱め、そして時には二人きりで愛し合いたいときの夜伽クラス。露出して、おしっこも出しちゃいます。 , MaidStatus/夜伽クラス/説明/Hentaihazukasime_chi
[Message:     Lilly] YotogiClass , 3210 , New , ソープご奉仕セックスメイド , Sorpgohousi_chi , 温かいお風呂でご主人様にご奉仕する夜伽クラス。ぬるぬるのするのはローションだけ？ , MaidStatus/夜伽クラス/説明/Sorpgohousi_chi
[Message:     Lilly] YotogiClass , 3220 , New , アブノーマル卑猥セックスメイド , HiwaiSex_chi , より変態なメイドを求めるときの夜伽クラス。カメラを用いたり、秘部奥を見せたり、よりアブノーマルな変態セックスを。 , MaidStatus/夜伽クラス/説明/HiwaiSex_chi
[Message:     Lilly] YotogiClass , 3230 , New , 癒しラブラブ奉仕メイド , LoveLoveHoushi_chi , メイドに癒やされ、可愛がられたいときの夜伽クラス。メイド主導による癒し系プレイで、優しく詰ってもらいましょう。 , MaidStatus/夜伽クラス/説明/LoveLoveHoushi_chi
[Message:     Lilly] YotogiClass , 3300 , New , ラブバインドメイド , LBPlay , ご主人様に拘束され、逃げられない状態でセックスをする夜伽クラス。メイドを独占し、愉しみましょう。 , MaidStatus/夜伽クラス/説明/LBPlay
[Message:     Lilly] YotogiClass , 3310 , New , ラブバインドメイド , LBPlay_add , ご主人様に拘束され、逃げられない状態でセックスをする夜伽クラス。メイドを独占し、愉しみましょう。 , MaidStatus/夜伽クラス/説明/LBPlay_add
[Message:     Lilly] YotogiClass , 3320 , New , ラブバインドメイド , LBPlay_add2 , ご主人様に拘束され、逃げられない状態でセックスをする夜伽クラス。メイドを独占し、愉しみましょう。 , MaidStatus/夜伽クラス/説明/LBPlay_add2
[Message:     Lilly] YotogiClass , 3400 , New , ラブイチャ卑猥メイド , LoveItyaHiwai , ほんのちょっと変態チックな体位でセックスする夜伽クラス。恥ずかしい恰好でもご主人様となら……♪ , MaidStatus/夜伽クラス/説明/LoveItyaHiwai
[Message:     Lilly] YotogiClass , 3410 , New , ラブイチャ卑猥メイド , LoveItyaHiwai_add , ほんのちょっと変態チックな体位でセックスする夜伽クラス。恥ずかしい恰好でもご主人様となら……♪ , MaidStatus/夜伽クラス/説明/LoveItyaHiwai_add
[Message:     Lilly] YotogiClass , 3420 , New , ラブイチャ卑猥メイド , LoveItyaHiwai_add2 , ほんのちょっと変態チックな体位でセックスする夜伽クラス。恥ずかしい恰好でもご主人様となら……♪ , MaidStatus/夜伽クラス/説明/LoveItyaHiwai_add2
[Message:     Lilly] YotogiClass , 3500 , New , らぶらぶ変態プレイメイド , LoveHentaiPlay , ご主人様とのらぶらぶセックス。ちょっとだけ変態なプレイは、大好きなご主人様との夜伽のスパイス。 , MaidStatus/夜伽クラス/説明/LoveHentaiPlay
[Message:     Lilly] YotogiClass , 3510 , New , らぶらぶ変態プレイメイド , LoveHentaiPlay_add , ご主人様とのらぶらぶセックス。ちょっとだけ変態なプレイは、大好きなご主人様との夜伽のスパイス。 , MaidStatus/夜伽クラス/説明/LoveHentaiPlay_add
[Message:     Lilly] YotogiClass , 3520 , New , らぶらぶ変態プレイメイド , LoveHentaiPlay_add2 , ご主人様とのらぶらぶセックス。ちょっとだけ変態なプレイは、大好きなご主人様との夜伽のスパイス。 , MaidStatus/夜伽クラス/説明/LoveHentaiPlay_add2
[Message:     Lilly] YotogiClass , 3600 , New , ハードエロティックメイド , HardEroticPlay , ご主人様とメイド、二人だけの特別なハードエロなプレイをお楽しみ頂く夜伽クラス。乱暴なのも下品なのも、全てご主人様だけに。 , MaidStatus/夜伽クラス/説明/HardEroticPlay
[Message:     Lilly] YotogiClass , 3700 , New , らぶらぶメイド , loveloveplus , ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 , MaidStatus/夜伽クラス/説明/loveloveplus
[Message:     Lilly] YotogiClass , 3710 , New , 欲情メイド , yokujyouplus , ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… , MaidStatus/夜伽クラス/説明/yokujyouplus
[Message:     Lilly] YotogiClass , 3720 , New , らぶらぶメイド , loveloveplus_add , ご主人様ととってもらぶらぶになりたいメイドの為の夜伽クラス。大好きな恋人とらぶらぶえっちしましょう。 , MaidStatus/夜伽クラス/説明/loveloveplus_add
[Message:     Lilly] YotogiClass , 3730 , New , 欲情メイド , yokujyouplus_add , ご主人様に欲情してちょっとアブノーマルになったメイドの為の夜伽クラス。恋人の為ならどんなことだって…… , MaidStatus/夜伽クラス/説明/yokujyouplus_add
[Message:     Lilly] YotogiClass , 3800 , New , 発情淫語メイド , Hatujyouingo , ご主人様の為に恥ずかしくて下品な言葉を言っちゃうようになっちゃった夜伽クラス。こんな言葉は、ご主人様だけ……♪ , MaidStatus/夜伽クラス/説明/Hatujyouingo
[Message:     Lilly] YotogiClass , 3900 , New , マスターソープセックスメイド , MasterSoapSex , ソープランドでのプレイをマスターしたメイドがセックスをする夜伽クラス。受け攻め自在、魅惑の時間をご主人様に。 , MaidStatus/夜伽クラス/説明/MasterSoapSex
[Message:     Lilly] YotogiClass , 3910 , New , マスターソープサービスメイド , MasterSoapService , ソープランドでのプレイをマスターメイドがご奉仕をする夜伽クラス。多種多様なテクニックでご主人様を快楽の虜に。 , MaidStatus/夜伽クラス/説明/MasterSoapService
[Message:     Lilly] YotogiClass , 3920 , New , マスターソープセックスメイド , MasterSoapSex_add , ソープランドでのプレイをマスターしたメイドがセックスをする夜伽クラス。受け攻め自在、魅惑の時間をご主人様に。 , MaidStatus/夜伽クラス/説明/MasterSoapSex_add
[Message:     Lilly] YotogiClass , 3930 , New , マスターソープサービスメイド , MasterSoapService_add , ソープランドでのプレイをマスターメイドがご奉仕をする夜伽クラス。多種多様なテクニックでご主人様を快楽の虜に。 , MaidStatus/夜伽クラス/説明/MasterSoapService_add
[Message:     Lilly] YotogiClass , 4000 , New , 激甘ラブメイド , Gekiamalove_add , ただひたすらご主人様といちゃいちゃラブラブエッチをする夜伽クラス。ほかに望むものなど何もない！ , MaidStatus/夜伽クラス/説明/Gekiamalove_add
[Info   :     Lilly] 

    */