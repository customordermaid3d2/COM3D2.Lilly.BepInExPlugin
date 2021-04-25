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

        public static void SetButtonList()
        {
            GUILayout.Label("Random Preset");
            if (GUILayout.Button("Random Preset Wear " + listWear.Count)) { RandPreset(ListType.Wear); }
            if (GUILayout.Button("Random Preset Body " + listBody.Count)) { RandPreset(ListType.Body); }
            if (GUILayout.Button("Random Preset Wear/Body " + listAll.Count)) { RandPreset(ListType.All); }
            if (GUILayout.Button("Random Preset All " + lists.Count)) { RandPreset(ListType.All); }
            if (GUILayout.Button("Random Preset All. set Wear " + lists.Count)) { RandPreset(ListType.All, PresetType.Wear); }
            if (GUILayout.Button("Random Preset All. set Body " + lists.Count)) { RandPreset(ListType.All, PresetType.Body); }
            if (GUILayout.Button("Random Preset All. set All " + lists.Count)) { RandPreset(ListType.All, PresetType.All); }
            if (GUILayout.Button("Random list load")) { LoadList(); }

        }

        internal static void RandPreset(ListType listType = ListType.All, PresetType presetType = PresetType.none, Maid m_maid = null)
        {
            List<string> list = lists;
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



            UnityEngine.Debug.Log("RandPreset");
            if (m_maid == null)
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
            switch (presetType )
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



    }
}
