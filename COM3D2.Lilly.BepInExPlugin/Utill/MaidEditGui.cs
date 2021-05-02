using BepInEx.Configuration;
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
        public static ConfigEntry<bool>? newMaid;
        public static ConfigEntry<bool>? movMaid;

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
        }

        public override void SetButtonList()
        {
            GUILayout.Label("now scene.name : " + Lilly.scene.name);
            if (GUILayout.Button("Maid add")) AddStockMaid();

            GUILayout.Label("메이드 에딧 진입시 자동 적용  ");
            //GUI.enabled = HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMain));
            if (GUILayout.Button("GP01FBFaceEyeRandomOnOff " + _GP01FBFaceEyeRandomOnOff.Value)) _GP01FBFaceEyeRandomOnOff.Value = !_GP01FBFaceEyeRandomOnOff.Value;

            GUILayout.Label("SceneEdit");
            GUI.enabled = Lilly.scene.name == "SceneEdit";
            if (GUILayout.Button("GP-01FB Face Eye Random")) GP01FBFaceEyeRandom(1);
            if (GUILayout.Button("GP-01FB Face Eye Random UP")) GP01FBFaceEyeRandom(2);
            if (GUILayout.Button("GP-01FB Face Eye Random DOWN")) GP01FBFaceEyeRandom(3);

            GUI.enabled = true;
            if (GUILayout.Button("New Maid "+ newMaid.Value)) OnOffNewMaid();
            GUI.enabled = !newMaid.Value;
            if (GUILayout.Button("Mov Maid "+ movMaid.Value)) OnOffMovMaid();
            
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

            PersonalUtill.SetPersonalRandom(maid);

            if (EasyUtill._SetMaidStatusOnOff.Value)
                CheatGUI.SetMaidStatus(maid);

            //GP01FBFaceEyeRandom(1, maid);
            PresetUtill.RandPreset(PresetUtill.ListType.All, PresetUtill.PresetType.All, maid);

            MyLog.LogMessage("EasyUtill.AddStockMaid", MyUtill.GetMaidFullName(maid));
            //private void OnEndDlgOk()
            {
                //BaseMgr<ProfileMgr>.Instance.UpdateProfileData(true);
                ////GameMain.Instance.SysDlg.Close();
                ////UICamera.InputEnable = false;
                //maid.ThumShotCamMove();
                //maid.body0.trsLookTarget = GameMain.Instance.ThumCamera.transform;
                //maid.boMabataki = false;
                ////if (this.modeType == SceneEdit.ModeType.CostumeEdit)
                //{
                //    for (int i = 81; i <= 80; i++)
                //    {
                //        maid.ResetProp((MPN)i, true);
                //    }
                //}
                //Lilly.StartCoroutine(this.CoWaitPutCloth());
            }
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
