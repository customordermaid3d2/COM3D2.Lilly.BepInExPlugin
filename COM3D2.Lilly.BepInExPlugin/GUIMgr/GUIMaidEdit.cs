﻿using BepInEx.Configuration;
using COM3D2.Lilly.Plugin.GUIMgr;
using COM3D2.Lilly.Plugin.ToolPatch;
using COM3D2.Lilly.Plugin.Utill;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
    public class GUIMaidEdit : GUIMgr
    {
        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "GUIMaidEdit"
        );


        public static ConfigEntry<bool> _GP01FBFaceEyeRandomOnOff;
        public static ConfigEntry<bool> newMaid;
        public static ConfigEntry<bool> movMaid;
        public static ConfigEntry<bool> _SetMaidStatusOnOff;

        public GUIMaidEdit()
        {
            nameGUI = "MaidEditGui";

            _GP01FBFaceEyeRandomOnOff = customFile.Bind(
              nameGUI,
              "_GP01FBFaceEyeRandomOnOff",
              true
              );

            newMaid = customFile.Bind(
              nameGUI,
              "newMaid",
              true
              );

            movMaid = customFile.Bind(
              nameGUI,
              "movMaid",
              true
              );

            _SetMaidStatusOnOff = customFile.Bind(
            "EasyUtill",
            "_SetMaidStatusOnOff",
            true
            );
        }

        /*
            [Message: Lilly] Personal: , 10 , C , Pure , 純真で健気な妹系 , MaidStatus / 性格タイプ / Pure                  이적 가능
            [Message: Lilly] Personal: , 20 , B , Cool , クールで寡黙 , MaidStatus / 性格タイプ / Cool                      이적 가능
            [Message: Lilly] Personal: , 30 , A , Pride , プライドが高く負けず嫌い , MaidStatus / 性格タイプ / Pride        이적 가능
            [Message: Lilly] Personal: , 40 , D , Yandere , 病的な程一途な大和撫子 , MaidStatus / 性格タイプ / Yandere      이적 가능
            [Message: Lilly] Personal: , 50 , E , Anesan , 母性的なお姉ちゃん , MaidStatus / 性格タイプ / Anesan            이적 가능
            [Message: Lilly] Personal: , 60 , F , Genki , 健康的でスポーティなボクっ娘 , MaidStatus / 性格タイプ / Genki    이적 가능
            [Message: Lilly] Personal: , 70 , G , Sadist , Ｍ心を刺激するドＳ女王様 , MaidStatus / 性格タイプ / Sadist      이적 가능
            [Message: Lilly] Personal: , 80 , A1 , Muku , 無垢 , MaidStatus / 性格タイプ / Muku
            [Message: Lilly] Personal: , 90 , B1 , Majime , 真面目 , MaidStatus / 性格タイプ / Majime
            [Message: Lilly] Personal: , 100 , C1 , Rindere , 凜デレ , MaidStatus / 性格タイプ / Rindere
            [Message: Lilly] Personal: , 110 , D1 , Silent , 文学少女 , MaidStatus / 性格タイプ / Silent                 이적 가능
            [Message: Lilly] Personal: , 120 , E1 , Devilish , 小悪魔 , MaidStatus / 性格タイプ / Devilish               이적 가능
            [Message: Lilly] Personal: , 130 , F1 , Ladylike , おしとやか , MaidStatus / 性格タイプ / Ladylike           이적 가능
            [Message: Lilly] Personal: , 140 , G1 , Secretary , メイド秘書 , MaidStatus / 性格タイプ / Secretary         이적 가능
            [Message: Lilly] Personal: , 150 , H1 , Sister , ふわふわ妹 , MaidStatus / 性格タイプ / Sister               이적 가능
            [Message: Lilly] Personal: , 160 , J1 , Curtness , 無愛想 , MaidStatus / 性格タイプ / Curtness               이적 가능
            [Message: Lilly] Personal: , 170 , K1 , Missy , お嬢様 , MaidStatus / 性格タイプ / Missy                     이적 가능
            [Message: Lilly] Personal: , 180 , L1 , Childhood , 幼馴染 , MaidStatus / 性格タイプ / Childhood             이적 가능
            [Message: Lilly] Personal: , 190 , M1 , Masochist , ド変態ドＭ , MaidStatus / 性格タイプ / Masochist
            [Message: Lilly] Personal: , 200 , N1 , Crafty , 腹黒 , MaidStatus / 性格タイプ / Crafty
         */

        public bool rndPersonal = true;
        public bool rndContract = true;
        public int selGridPersonal = 0;
        public int selGridContract = 0;

        public string[] PersonalNames;//= new string[] { "radio1", "radio2", "radio3" };
        public string[] ContractNames;//= new string[] { "radio1", "radio2", "radio3" };

        //public bool rndHeroine = true;
        //public int selGridHeroine = 0;
        //public string[] HeroineNames;//= new string[] { "radio1", "radio2", "radio3" };

        public override void ActionsStart()
        {
            MyLog.LogMessage("GUIMaidEdit.ActionsStart");
            //base.Start();
            PersonalNames = PersonalUtill.GetPersonalData().Select((x) => x.uniqueName).ToArray();
            ContractNames = new string[] { "Random","Exclusive", "Free" };
           // HeroineNames = new string[] { "Random", "Original", "Transfer" };
            // Contract.Exclusive., Contract.Trainee 
            
            // Original = 0,
            // Sub = 1,
            // Transfer = 2

        }

        public override void SetBody()
        {
            GUILayout.Label("now scene.name : " + Lilly.scene.name);
            //GUILayout.Label("------------");
            
            if (GUILayout.Button("Maid add")) AddStockMaid();
            if (GUILayout.Button("Maid add * 10")) 
                for (int i = 0; i < 10; i++)
                {
                    AddStockMaid() ;
                }  
            if (GUILayout.Button("Maid add * 50")) 
                for (int i = 0; i < 50; i++)
                {
                    AddStockMaid() ;
                }  
            
            if (GUILayout.Button("Personal Rand " + rndPersonal + " " + PersonalNames[selGridPersonal])) rndPersonal=!rndPersonal;
            if (!rndPersonal)
            {
                selGridPersonal = GUILayout.SelectionGrid(selGridPersonal, PersonalNames, 1);
            }
            GUILayout.Label("Contract");
            //if (GUILayout.Button("Contract Rand " + rndContract + " " + ContractNames[selGridContract])) rndContract = !rndContract;
            //if (!rndContract)
            {
                selGridContract = GUILayout.SelectionGrid(selGridContract, ContractNames, 1);
            }
            /*
            GUI.enabled = !string.IsNullOrEmpty(GameMain.Instance.CMSystem.CM3D2Path);
            GUILayout.Label("Heroine " + ( GUI.enabled ? "" : "호환모드 아님"));
            //if (GUILayout.Button("rndHeroine Rand " + rndHeroine + " " + HeroineNames[selGridHeroine])) rndHeroine = !rndHeroine;
            //if (!rndHeroine)
            {
                selGridHeroine = GUILayout.SelectionGrid(selGridHeroine, HeroineNames, 1);
            }
            */
            GUI.enabled = true;

            //GUILayout.Label("------------");
            if (GUILayout.Button("성격 오류 보정")) HeroineCare();
            GUILayout.Label("메이드 에딧 진입시 자동 적용  ");
            //GUI.enabled = HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMain));
            // GUILayout.Label("MaidManagementMain Harmony 필요 : "+ HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMainPatch)));
            if (GUILayout.Button("Maid cheat " + _SetMaidStatusOnOff.Value)) _SetMaidStatusOnOff.Value = !_SetMaidStatusOnOff.Value;

            GUI.enabled = true;
            if (GUILayout.Button("New Maid " + newMaid.Value)) OnOffNewMaid();
            GUI.enabled = !newMaid.Value;
            if (GUILayout.Button("Mov Maid " + movMaid.Value)) OnOffMovMaid();

            GUI.enabled = true;
            if (GUILayout.Button("GP01FBFaceEyeRandomOnOff " + _GP01FBFaceEyeRandomOnOff.Value)) _GP01FBFaceEyeRandomOnOff.Value = !_GP01FBFaceEyeRandomOnOff.Value;

            GUILayout.Label("SceneEdit");
            GUI.enabled = Lilly.scene.name == "SceneEdit";
            if (GUILayout.Button("GP-01FB Face Eye Random")) GP01FBFaceEyeRandom(1);
            if (GUILayout.Button("GP-01FB Face Eye Random UP")) GP01FBFaceEyeRandom(2);
            if (GUILayout.Button("GP-01FB Face Eye Random DOWN")) GP01FBFaceEyeRandom(3);

            
            GUI.enabled = true;
        }


        public static void OnOffNewMaid()
        {
            newMaid.Value = !newMaid.Value;
        }

        public static void OnOffMovMaid()
        {
            movMaid.Value = !movMaid.Value;
        }


        private void HeroineCare()
        {
            List<Maid> maids = GameMain.Instance.CharacterMgr.GetStockMaidList();
            foreach (var maid in maids)
            {
                PersonalUtill.SetPersonalCare(maid);
            }
        }

        private void AddStockMaid()
        {
            if(configEntryUtill["AddStockMaid"])
            MyLog.LogMessage("GUIMaidEdit.AddStockMaid");

            Maid maid = GameMain.Instance.CharacterMgr.AddStockMaid();

            if (rndPersonal)
            {
                selGridPersonal=PersonalUtill.SetPersonalRandom(maid);
            }
            else
            {
                PersonalUtill.SetPersonal(maid, selGridPersonal);
            }

            switch (selGridContract)
            {
                case 1:
                    maid.status.contract = Contract.Exclusive;
                    break;
                case 2:
                    maid.status.contract = Contract.Free;
                    break;
                default:
                    maid.status.contract = MyUtill.RandomEnum(Contract.Trainee);
                    break;
            }
           
            if (_SetMaidStatusOnOff.Value)
                CheatUtill.SetMaidAll(maid);

#if PresetUtill
            PresetUtill.RandPreset(maid);
#endif

            if (configEntryUtill["AddStockMaid"])
                MyLog.LogMessage("GUIMaidEdit.AddStockMaid", MyUtill.GetMaidFullName(maid));
        }

        public static void GP01FBFaceEyeRandom(int v, Maid m_maid = null)
        {
            if (m_maid == null)
            {
                m_maid = GameMain.Instance.CharacterMgr.GetMaid(0);
            }
            MyLog.LogMessage("GP01FBFaceEyeRandom", MyUtill.GetMaidFullName(m_maid));
            if (v == 1 || v == 2)
                GP01FBFaceEyeRandomUp(m_maid);
            if (v == 1 || v == 3)
                GP01FBFaceEyeRandomDown(m_maid);

            m_maid.AllProcProp();
            SceneEdit.Instance.UpdateSliders();
        }

        public static void GP01FBFaceEyeRandomDown(Maid m_maid)
        {
            SetMaidPropRandom(m_maid, MPN.MabutaLowIn);
            SetMaidPropRandom(m_maid, MPN.MabutaLowUpMiddle);
            SetMaidPropRandom(m_maid, MPN.MabutaLowUpOut);
        }

        public static void GP01FBFaceEyeRandomUp(Maid m_maid)
        {
            SetMaidPropRandom(m_maid, MPN.MabutaUpIn);
            SetMaidPropRandom(m_maid, MPN.MabutaUpIn2);
            SetMaidPropRandom(m_maid, MPN.MabutaUpMiddle);
            SetMaidPropRandom(m_maid, MPN.MabutaUpOut);
            SetMaidPropRandom(m_maid, MPN.MabutaUpOut2);
        }

        private static void SetMaidPropRandom(Maid m_maid, MPN tag)
        {
            try
            {
                MaidProp maidProp = m_maid.GetProp(tag);
                m_maid.SetProp(tag, UnityEngine.Random.Range(maidProp.min, maidProp.max));
                MyLog.LogMessage("SetMaidPropRandom", tag, maidProp.min, maidProp.max);
            }
            catch (Exception e)
            {
                MyLog.LogError("SetMaidPropRandom", e.ToString());
            }
        }


    }
}
