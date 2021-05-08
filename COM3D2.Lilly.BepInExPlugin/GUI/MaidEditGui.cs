using BepInEx.Configuration;
using COM3D2.Lilly.Plugin.ToolPatch;
using COM3D2.Lilly.Plugin.Utill;
using MaidStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    public class MaidEditGui : GUIVirtual
    {
        public static ConfigEntry<bool> _GP01FBFaceEyeRandomOnOff;
        public static ConfigEntry<bool> newMaid;
        public static ConfigEntry<bool> movMaid;
        public static ConfigEntry<bool> _SetMaidStatusOnOff;

        public MaidEditGui()
        {
            name = "MaidEditGui";

            _GP01FBFaceEyeRandomOnOff = customFile.Bind(
              name,
              "_GP01FBFaceEyeRandomOnOff",
              true
              );

            newMaid = customFile.Bind(
              name,
              "newMaid",
              true
              );

            movMaid = customFile.Bind(
              name,
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
MyLog.LogMessage("Personal:", item.id, item.replaceText, item.uniqueName, item.drawName, item.termName);//
         */

        public bool rnd = true;
        public int selGridInt = 0;
        public string[] PersonalNames;//= new string[] { "radio1", "radio2", "radio3" };

        public override void Start()
        {
            base.Start();
            PersonalNames = PersonalUtill.GetPersonalData().Select((x) => x.uniqueName).ToArray();            
        }

        public override void SetButtonList()
        {
            GUILayout.Label("now scene.name : " + Lilly.scene.name);
            //GUILayout.Label("------------");
            if (GUILayout.Button("SetRandomCommu")) { ScheduleAPIPatch.SetRandomCommu(true); ScheduleAPIPatch.SetRandomCommu(false); };
            if (GUILayout.Button("Maid add")) AddStockMaid();
            if (GUILayout.Button("Personal Rand " + rnd + " " + PersonalNames[selGridInt])) rnd=!rnd;
            if (!rnd)
            {
                selGridInt = GUILayout.SelectionGrid(selGridInt, PersonalNames, 1);
            }
            //GUILayout.Label("------------");

            GUILayout.Label("메이드 에딧 진입시 자동 적용  ");
            //GUI.enabled = HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMain));
            // GUILayout.Label("MaidManagementMain Harmony 필요 : "+ HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMainPatch)));
            if (GUILayout.Button("SetMaidStatus " + _SetMaidStatusOnOff.Value)) _SetMaidStatusOnOff.Value = !_SetMaidStatusOnOff.Value;

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

        private void AddStockMaid()
        {
            MyLog.LogMessage("EasyUtill.AddStockMaid");

            Maid maid = GameMain.Instance.CharacterMgr.AddStockMaid();

            if (rnd)
            {
                selGridInt=PersonalUtill.SetPersonalRandom(maid);
            }
            else
            {
                PersonalUtill.SetPersonal(maid, selGridInt);
            }

            maid.status.contract = MyUtill.RandomEnum(Contract.Trainee);
            maid.status.heroineType = MyUtill.RandomEnum(HeroineType.Sub);

            if (_SetMaidStatusOnOff.Value)
                CheatUtill.SetMaidAll(maid);


            PresetUtill.RandPreset(PresetUtill.ListType.All, PresetUtill.PresetType.All, maid);

            MyLog.LogMessage("EasyUtill.AddStockMaid", MyUtill.GetMaidFullName(maid));
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
            }
            catch (Exception e)
            {
                MyLog.LogError("SetMaidPropRandom", e.ToString());
            }
        }


    }
}
