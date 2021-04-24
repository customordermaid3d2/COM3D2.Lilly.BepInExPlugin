using Schedule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using BepInEx.Configuration;
using BepInEx;
using MaidStatus;
using System.Reflection;

namespace COM3D2.Lilly.Plugin
{
    public class EasyUtill : GUIVirtual
    {
        //public static Scene scene;

        public EasyUtill()
        {
            name = "EasyUtill";
           
        }


        internal void Awake()
        {
            _GP01FBFaceEyeRandomOnOff = customFile.Bind(
              "EasyUtill",
              "_GP01FBFaceEyeRandomOnOff",
              true
              );
            _SetMaidStatusOnOff = customFile.Bind(
                "EasyUtill",
                "_SetMaidStatusOnOff",
                true
                );
        }

        public void Start()
        {               
        }

        public void SetScene()
        {
            //scene=SceneManager.GetSceneByName("SceneDaily");

        }

        public static ConfigFile customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "COM3D2.Lilly.Plugin.EasyUtill.cfg"), true);

        public static ConfigEntry<bool> _GP01FBFaceEyeRandomOnOff;
        public static ConfigEntry<bool> _SetMaidStatusOnOff;

        public static System.Random rand = new System.Random();

        public static List<string> listWear = new List<string>();
        public static List<string> listBody = new List<string>();
        public static List<string> listAll = new List<string>();
        public static List<string> lists = new List<string>();


        public override void SetButtonList()
        {
            if (GUILayout.Button("mod reflash2")) modreflash2();
            if (GUILayout.Button("Maid add")) AddStockMaid();

            GUILayout.Label("Random Preset");
            if (GUILayout.Button("Random Preset Wear " + listWear.Count)) { RandPreset(listWear); }
            if (GUILayout.Button("Random Preset Body " + listBody.Count)) { RandPreset(listBody); }
            if (GUILayout.Button("Random Preset Wear/Body " + listAll.Count)) { RandPreset(listAll); }
            if (GUILayout.Button("Random Preset All " + lists.Count)) { RandPreset(lists); }
            if (GUILayout.Button("Random Preset All. set Wear " + lists.Count)) { RandPreset(lists, 1); }
            if (GUILayout.Button("Random Preset All. set Body " + lists.Count)) { RandPreset(lists, 2); }
            if (GUILayout.Button("Random Preset All. set All " + lists.Count)) { RandPreset(lists, 3); }
            if (GUILayout.Button("Random list load")) { LoadList(); }

            GUILayout.Label("MaidManagementMain Harmony 필요 : "+ HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMain)));
            GUILayout.Label("메이드 에딧 진입시 자동 적용  ");
            //GUI.enabled = HarmonyUtill.GetHarmonyPatchCheck(typeof(MaidManagementMain));
            if (GUILayout.Button("GP01FBFaceEyeRandomOnOff " + _GP01FBFaceEyeRandomOnOff.Value)) _GP01FBFaceEyeRandomOnOff.Value = !_GP01FBFaceEyeRandomOnOff.Value;
            if (GUILayout.Button("SetMaidStatus " + _SetMaidStatusOnOff.Value)) _SetMaidStatusOnOff.Value = !_SetMaidStatusOnOff.Value;

            GUILayout.Label("now scene.name : " + Lilly.scene.name);
            
            GUILayout.Label("SceneDaily");
            GUI.enabled = Lilly.scene.name == "SceneDaily";
            if (GUILayout.Button("DeleteMaidStatusAll")) DeleteMaidStatusAll();
            
            GUILayout.Label("SceneEdit");
            GUI.enabled = Lilly.scene.name == "SceneEdit";
            if (GUILayout.Button("GP-01FB Face Eye Random")) GP01FBFaceEyeRandom(1);
            if (GUILayout.Button("GP-01FB Face Eye Random UP")) GP01FBFaceEyeRandom(2);
            if (GUILayout.Button("GP-01FB Face Eye Random DOWN")) GP01FBFaceEyeRandom(3);
            

            GUI.enabled = true;
            //if (GUILayout.Button("mod reflash")) modreflash();
        }


        internal static void RandPreset(List<string> list, int t = 0, Maid m_maid = null)
        {
            UnityEngine.Debug.Log("RandPreset");
            if (m_maid==null)
            {
                m_maid = GameMain.Instance.CharacterMgr.GetMaid(0);
            }
            MyLog.LogMessage("RandPreset", MyUtill.GetMaidFullName(m_maid));
            if (m_maid.IsBusy)
            {
                UnityEngine.Debug.Log("RandPreset Maid Is Busy");
                return;
            }
            UnityEngine.Debug.Log("RandomPreset maid: " + m_maid.status.fullNameJpStyle);
            if (list.Count == 0)
            {
                LoadList();
                if (list.Count == 0)
                {
                    UnityEngine.Debug.Log("RandPreset No list");
                    return;
                }
            }
            string file = list[rand.Next(list.Count)];
            UnityEngine.Debug.Log("RandPreset select :" + file);
            CharacterMgr.Preset preset = GameMain.Instance.CharacterMgr.PresetLoad(file);
            switch (t)
            {
                case 1:
                    preset.ePreType = CharacterMgr.PresetType.Wear;
                    break;
                case 2:
                    preset.ePreType = CharacterMgr.PresetType.Body;
                    break;
                case 3:
                    preset.ePreType = CharacterMgr.PresetType.All;
                    break;
                default:
                    break;
            }
            //Main.CustomPresetDirectory = Path.GetDirectoryName(file);
            //UnityEngine.Debug.Log("RandPreset preset path "+ GameMain.Instance.CharacterMgr.PresetDirectory);
            //preset.strFileName = file;
            if (preset == null)
            {
                UnityEngine.Debug.Log("RandPreset preset null ");
                return;
            }
            GameMain.Instance.CharacterMgr.PresetSet(m_maid, preset);
            if (Product.isPublic)
                SceneEdit.AllProcPropSeqStart(m_maid);
        }

        /// <summary>
        /// 정상 처리된듯?
        /// </summary>
        internal static void LoadList()
        {
            MyLog.LogMessage("LoadList Preset");

            listWear.Clear();
            listBody.Clear();
            listAll.Clear();
            lists.Clear();

            // 하위경로포함
            foreach (string f_strFileName in Directory.GetFiles(Path.Combine(GameMain.Instance.SerializeStorageManager.StoreDirectoryPath, "Preset"), "*.preset", SearchOption.AllDirectories))
            {
                //jUnityEngine.Debug.Log("RandPreset load : " + f_strFileName);

                FileStream fileStream = new FileStream(f_strFileName, FileMode.Open);
                if (fileStream == null)
                {
                    continue;
                }
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                fileStream.Close();
                fileStream.Dispose();
                BinaryReader binaryReader = new BinaryReader(new MemoryStream(buffer));

                string a = binaryReader.ReadString();
                if (a != "CM3D2_PRESET")
                {
                    binaryReader.Close();
                    continue;
                }
                binaryReader.ReadInt32();
                switch ((CharacterMgr.PresetType)binaryReader.ReadInt32())
                {
                    case CharacterMgr.PresetType.Wear:
                        listWear.Add(f_strFileName);
                        break;
                    case CharacterMgr.PresetType.Body:
                        listBody.Add(f_strFileName);
                        break;
                    case CharacterMgr.PresetType.All:
                        listAll.Add(f_strFileName);
                        break;
                    default:
                        break;
                }
                binaryReader.Close();
            }

            lists.AddRange(listWear);
            lists.AddRange(listBody);
            lists.AddRange(listAll);
        }


        private void AddStockMaid()
        {
            MyLog.LogMessage("EasyUtill.AddStockMaid");

            Maid maid = GameMain.Instance.CharacterMgr.AddStockMaid();

            maid.status.SetPersonal(PersonalUtill.GetPersonalData(true)[UnityEngine.Random.Range(0, PersonalUtill.GetPersonalData(true).Count)]);

            if (EasyUtill._SetMaidStatusOnOff.Value)
                CheatUtill.SetMaidStatus(maid);

            //GP01FBFaceEyeRandom(1, maid);
            RandPreset(lists, 3, maid);

            MyLog.LogMessage("EasyUtill.AddStockMaid",MyUtill.GetMaidFullName(maid));
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


        public static void GP01FBFaceEyeRandom(int v, Maid m_maid=null)
        {
            if (m_maid==null)
            {
                m_maid = GameMain.Instance.CharacterMgr.GetMaid(0);
            }
            if (v == 1 || v == 2)
                GP01FBFaceEyeRandomUp(m_maid);
            if (v == 1 || v == 3)
                GP01FBFaceEyeRandomDown(m_maid);
            m_maid.AllProcProp();
            SceneEdit.Instance.UpdateSliders();
        }

        public static void GP01FBFaceEyeRandomDown(Maid m_maid)
        {
            m_maid.body0.VertexMorph_FromProcItem("mabutalowin"         , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutalowupmiddle"   , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutalowupout"      , UnityEngine.Random.Range(0f, 1f));
        }

        public static void GP01FBFaceEyeRandomUp(Maid m_maid)
        {
            m_maid.body0.VertexMorph_FromProcItem("mabutaupin"      , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutaupin2"     , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutaupmiddle"  , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutaupout"     , UnityEngine.Random.Range(0f, 1f));
            m_maid.body0.VertexMorph_FromProcItem("mabutaupout2"    , UnityEngine.Random.Range(0f, 1f));
        }

        private void modreflash2()
        {
            if (isRunModreflash)
                return;

            isRunModreflash = true;
            Task.Factory.StartNew(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                MyLog.LogDarkBlue("modreflash2. start ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));

                bool flag = Directory.Exists(UTY.gameProjectPath + "\\Mod\\");
                if (flag)
                {
                    GameUty.UpdateFileSystemPath();
                    GameUty.UpdateFileSystemPathOld();
                }
                bool flag3 = GameUty.FileSystemMod != null;
                if (flag3)
                {
                    typeof(GameUty).GetField("m_aryModOnlysMenuFiles").SetValue(null, Array.FindAll<string>(GameUty.FileSystemMod.GetList(string.Empty, AFileSystemBase.ListType.AllFile), (string i) => new Regex(".*\\.menu$").IsMatch(i)));
                }

                MyLog.LogDarkBlue("modreflash2. end ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));
                isRunModreflash = false;
            }
            );

        }

        static bool isRunModreflash = false;

        private void modreflash()
        {
            if (isRunModreflash)
                return;

            isRunModreflash = true;
            Task.Factory.StartNew(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                MyLog.LogDarkBlue("modreflash. start ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));

                FileSystemWindows m_ModFileSystem = null;
                string text = UTY.gameProjectPath + "\\";
                if (Directory.Exists(text + "Mod"))
                {
                    m_ModFileSystem = new FileSystemWindows();
                    m_ModFileSystem.SetBaseDirectory(text);
                    m_ModFileSystem.AddFolder("Mod");

                    string[] list2 = m_ModFileSystem.GetList(string.Empty, AFileSystemBase.ListType.AllFolder);
                    foreach (string text31 in list2)
                    {
                        if (!m_ModFileSystem.AddAutoPath(text31))
                        {
                            UnityEngine.Debug.Log("m_ModFileSystemのAddAutoPathには既に " + text31 + " がありました。");
                        }
                    }
                }
                //typeof(GameUty).GetField("m_ModFileSystem").SetValue(null, m_ModFileSystem);
                // 메이드 에딧에서 목록만 갱신함
                if (m_ModFileSystem != null)
                {
                    string[] list3 = m_ModFileSystem.GetList(string.Empty, AFileSystemBase.ListType.AllFile);
                    //GameUty.m_aryModOnlysMenuFiles = Array.FindAll<string>(list3, (string i) => new Regex(".*\\.menu$").IsMatch(i));
                    string[] list4 = Array.FindAll<string>(list3, (string i) => new Regex(".*\\.menu$").IsMatch(i));
                    typeof(GameUty).GetField("m_aryModOnlysMenuFiles").SetValue(null, list4);

                }
                if (m_ModFileSystem != null)
                {
                    m_ModFileSystem.Dispose();
                    m_ModFileSystem = null;
                }

                MyLog.LogDarkBlue("modreflash. end ", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));
                isRunModreflash = false;
            }
            );
        }   /*
        */
        private void DeleteMaidStatusAll()
        {
            for (int i = 0; i < 40; i++)//SetDataForViewer 에서 하드 코딩됨
            {
                ScheduleCtrlPatch.DeleteMaidAndReDraw("slot_" + i + "_MaidStatus");
            }
        }

        internal static void RandomPreset()
        {
            Maid m_maid = GameMain.Instance.CharacterMgr.GetStockMaid(0);
            if (m_maid.IsBusy)
            {
                return;
            }
            // Path.Combine(GameMain.Instance.SerializeStorageManager.StoreDirectoryPath, "Preset");
            string[] filepath = Directory.GetFiles(Path.Combine(GameMain.Instance.SerializeStorageManager.StoreDirectoryPath, "Preset"), "*.preset", SearchOption.AllDirectories);
            if (filepath.Length == 0 || filepath is null)
            {
                return;
            }
            CharacterMgr.Preset preset = GameMain.Instance.CharacterMgr.PresetLoad(filepath[Lilly.rand.Next(filepath.Length)]);
            GameMain.Instance.CharacterMgr.PresetSet(m_maid, preset, false);
        }

        /*
        public void ClickMaidStatus()
        {
            string name = UIButton.current.name;
            if (UICamera.currentTouchID == -1)
            {
                if (this.CurrentActiveButton == name)
                {
                    return;
                }
                Debug.Log(string.Format("{0}ボタンがクリックされました。", name));
                this.m_MaidStatusListCtrl.CreateTaskViewer(name);
                this.CurrentActiveButton = name;
            }
            else if (UICamera.currentTouchID == -2)
            {
                Debug.Log(string.Format("{0}ボタンが右クリックされました。", name));
                if (this.m_Ctrl.CanDeleteData(name))
                {
                    this.m_Ctrl.DeleteMaidStatus(this.m_scheduleApi, name);
                }
            }
        }
        */
    }
}
