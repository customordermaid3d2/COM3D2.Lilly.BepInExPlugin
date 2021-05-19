using COM3D2.Lilly.Plugin.Utill;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    class PresetUtill 
    {
        public static System.Random rand = new System.Random();

        public static List<string> listWear = new List<string>();
        public static List<string> listBody = new List<string>();
        public static List<string> listAll = new List<string>();
        public static List<string> lists = new List<string>();

        //public static ListType listType = ListType.All;
        //public static PresetType presetType = PresetType.All;
        //public static ModType modType = ModType.OneMaid;
        private static int selGridMod= (int)ModType.AllMaid_RandomPreset;
        private static int selGridList = (int)ListType.All;
        private static int selGridPreset = (int)PresetType.All;
        private static int selGridmaid = 0;

        public static string[] namesMod;
        public static string[] namesList;
        public static string[] namesPreset;

        public enum ListType
        {
            Wear,
            Body,
            WearAndBody,
            All
        }

        public enum PresetType
        {
            none,
            Wear,
            Body,
            All
        }
        
        public enum ModType
        {
            OneMaid,
            AllMaid_OnePreset,
            AllMaid_RandomPreset
        }

        public static void init()
        {
            MyLog.LogDebug("PresetUtill", "init");
            namesMod = Enum.GetNames(typeof(ModType));
            namesPreset = Enum.GetNames(typeof(PresetType));
            namesList  = Enum.GetNames(typeof(ListType));
        }

        public static void SetButtonList()
        {
            GUILayout.Label("Preset Random CharacterMgrPatch 필요");
            GUILayout.Label("Preset Wear " + listWear.Count);
            GUILayout.Label("Preset Body " + listBody.Count);
            GUILayout.Label("Preset Wear/Body " + listAll.Count);
            GUILayout.Label("Preset All " + lists.Count);
            //if (GUILayout.Button("Random Preset Wear " + listWear.Count)) { RandPreset(ListType.Wear); }
            //if (GUILayout.Button("Random Preset Body " + listBody.Count)) { RandPreset(ListType.Body); }
            //if (GUILayout.Button("Random Preset Wear/Body " + listAll.Count)) { RandPreset(ListType.All); }
            //if (GUILayout.Button("Random Preset All " + lists.Count)) { RandPreset(ListType.All); }
            //if (GUILayout.Button("Random Preset All. set Wear " + lists.Count)) { RandPreset(ListType.All, PresetType.Wear); }
            //if (GUILayout.Button("Random Preset All. set Body " + lists.Count)) { RandPreset(ListType.All, PresetType.Body); }
            //if (GUILayout.Button("Random Preset All. set All " + lists.Count)) { RandPreset(ListType.All, PresetType.All); }
            if (GUILayout.Button("Random Preset Run")) { RandPresetRun(); }
            if (GUILayout.Button("Random list load")) { LoadList(); }
            GUILayout.Label("PresetType "+ selGridPreset);
            selGridPreset = GUILayout.Toolbar(selGridPreset, namesPreset);
            GUILayout.Label("ListType "+ selGridList);
            selGridList = GUILayout.SelectionGrid(selGridList, namesList,2);
            GUILayout.Label("ModType "+ selGridMod);
            selGridMod = GUILayout.SelectionGrid(selGridMod, namesMod, 1);
            if ((ModType)selGridMod == ModType.OneMaid)
            {
            GUILayout.Label("Maid List "+ selGridmaid);
            //GUI.enabled = modType == ModType.OneMaid;
            selGridmaid = GUILayout.SelectionGrid(selGridmaid, CharacterMgrPatch.namesMaid, 2);
            }
            GUI.enabled = true;
        }

        private static void RandPresetRun()
        {
            List<string> list = lists;
            list = GetList((ListType)selGridList, list);

            if (list.Count == 0)
            {
                UnityEngine.Debug.LogWarning("RandPreset No list");
                return;
            }

            Maid m_maid;
            string file;
            switch ((ModType)selGridMod)
            {
                case ModType.OneMaid:
                    m_maid=CharacterMgrPatch.m_gcActiveMaid[selGridmaid];
                    file = list[rand.Next(list.Count)];
                    SetMaidPreset((PresetType)selGridPreset, m_maid, file);
                    break;
                case ModType.AllMaid_OnePreset:
                    file = list[rand.Next(list.Count)];
                    foreach (var item in CharacterMgrPatch.m_gcActiveMaid)
                    {
                        SetMaidPreset((PresetType)selGridPreset, item, file);
                    }
                    break;
                case ModType.AllMaid_RandomPreset:
                    foreach (var item in CharacterMgrPatch.m_gcActiveMaid)
                    {
                        file = list[rand.Next(list.Count)];
                        SetMaidPreset((PresetType)selGridPreset, item, file);
                    }
                    break;
                default:
                    break;
            }
        }

        internal static void RandPreset( Maid m_maid = null,ListType listType = ListType.All, PresetType presetType = PresetType.none)
        {
            UnityEngine.Debug.Log("RandPreset");
            if (m_maid == null)
            {
                m_maid = GameMain.Instance.CharacterMgr.GetMaid(0);
                if (m_maid == null)
                {
                    MyLog.LogWarning("RandPreset maid null");
                    return;
                }
            }
            MyLog.LogMessage("RandPreset", MyUtill.GetMaidFullName(m_maid));

            List<string> list = lists;
            list = GetList(listType, list);

            if (list.Count == 0)
            {
                UnityEngine.Debug.LogWarning("RandPreset No list");
                return;
            }

            string file = list[rand.Next(list.Count)];

            SetMaidPreset(presetType, m_maid, file);
        }

        private static List<string> GetList(ListType listType, List<string> list)
        {
            switch (listType)
            {
                case ListType.Wear:
                    list = listWear;
                    break;
                case ListType.Body:
                    list = listBody;
                    break;
                case ListType.WearAndBody:
                    list = listAll;
                    break;
                case ListType.All:
                    list = lists;
                    break;
                default:
                    break;
            }

            if (list.Count == 0)
            {
                LoadList();
            }

            return list;
        }

        private static void SetMaidPreset(PresetType presetType, Maid m_maid, string file)
        {
            if (m_maid == null)
            {
                MyLog.LogWarning("SetMaidPreset maid null");
                return;
            }
            if (m_maid.IsBusy)
            {
                UnityEngine.Debug.Log("RandPreset Maid Is Busy");
                return;
            }

            UnityEngine.Debug.Log("SetMaidPreset select :" + file);
            CharacterMgr.Preset preset = GameMain.Instance.CharacterMgr.PresetLoad(file);
            switch (presetType)
            {
                case PresetType.Wear:
                    preset.ePreType = CharacterMgr.PresetType.Wear;
                    break;
                case PresetType.Body:
                    preset.ePreType = CharacterMgr.PresetType.Body;
                    break;
                case PresetType.All:
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
                UnityEngine.Debug.Log("SetMaidPreset preset null ");
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
            MyLog.LogMessage("LoadList Preset", Environment.CurrentDirectory);

            listWear.Clear();
            listBody.Clear();
            listAll.Clear();
            lists.Clear();

            // 하위경로포함
            foreach (string f_strFileName in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Preset"), "*.preset", SearchOption.AllDirectories))
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



    }
}
