using COM3D2.Lilly.Plugin.GUIMgr;
using COM3D2.Lilly.Plugin.ToolPatch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.Utill
{
    class FlagUtill
    {
        public static Dictionary<string, int> oldflags=new Dictionary<string, int>();
        public static Dictionary<string, int> flags = new Dictionary<string, int>();
        public static Dictionary<int, bool> eventEndFlags = new Dictionary<int, bool>();

        public FlagUtill()
        {
            //ReadCSVAll();
        }

        private static void ReadCSVAll()
        {
            ReadCSV("oldflags", ref oldflags);
            ReadCSV("flags", ref flags);
            ReadCSV("eventEndFlags", ref eventEndFlags);
        }

        public static void ReadCSV(string file,ref Dictionary<string, int> list)
        {
            string filePath = Path.GetDirectoryName(Lilly.customFile.ConfigFilePath) + "/" + file+".csv";
            if (!File.Exists(filePath))
            {
                MyLog.LogMessage("ReadCSV make : " + filePath);
                
                using StreamWriter sw = File.CreateText(filePath);
                sw.WriteLine("# flag name,int value");
                sw.WriteLine("// キャラクターパックEXメイド,0");
                return;
            }
            list.Clear();
            using StreamReader sr = new StreamReader(filePath);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                MyLog.LogDebug("ReadCSV read : " + line);
                if (line.StartsWith("//") || line.StartsWith("#"))
                {
                    continue;
                }
                var values = line.Split(',');
                if (int.TryParse(values[1], out int j))
                    //if (list.ContainsKey(values[0]))
                    //{
                    //    list[values[0]] = j;
                    //}
                    //else
                    {
                        list.Add(values[0], j);
                    }
                else
                    MyLog.LogWarning("ReadCSV error : " + line);
            }
        }
        
        public static void ReadCSV(string file,ref Dictionary<int, bool> list)
        {
            string filePath = Path.GetDirectoryName(Lilly.customFile.ConfigFilePath) + "/" + file + ".csv";
            if (!File.Exists(filePath))
            {
                MyLog.LogMessage("ReadCSV make : " + filePath);
                
                using StreamWriter sw = File.CreateText(filePath);
                sw.WriteLine("# flag id,bool value");
                sw.WriteLine("// flag id,bool value");
                sw.WriteLine("// 1,"+false);
                sw.WriteLine("// 2,"+true);
                return;
            }
            list.Clear();
            using StreamReader sr = new StreamReader(filePath);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                MyLog.LogDebug("ReadCSV read : " + line);
                if (line.StartsWith("//") || line.StartsWith("#"))
                {
                    continue;
                }
                var values = line.Split(',');
                if (int.TryParse(values[0], out int j)&&bool.TryParse(values[1], out bool k))
                    //if (list.ContainsKey(j))
                    //{
                    //    list[j] = k;
                    //}
                    //else
                    {
                        list.Add(j, k);
                    }

                else
                    MyLog.LogWarning("ReadCSV error : " + line);
            }
        }

        public static void RemoveErrFlagAll()
        {
            ReadCSVAll();
            foreach (var maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
            {
                foreach (var item in eventEndFlags)
                {
                    maid.status.RemoveEventEndFlag(item.Key);
                }
                foreach (var item in flags)
                {
                    maid.status.RemoveFlag(item.Key);
                }
                foreach (var item in oldflags)
                {
                    maid.status.OldStatus.RemoveFlag(item.Key);
                }
            }
            
        }


        public static void RemoveEventEndFlagAll()
        {
            foreach (var maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
            {
                MyLog.LogMessage("RemoveEventEndFlagAll:" + MyUtill.GetMaidFullName(maid)); ;
                maid.status.RemoveEventEndFlagAll();
            }
        }

        public static void RemoveFlagAll()
        {
            foreach (var maid in GameMain.Instance.CharacterMgr.GetStockMaidList())
            {
                MyLog.LogMessage("RemoveEventEndFlagAll:" + MyUtill.GetMaidFullName(maid)); ;
                maid.status.RemoveFlagAll();                
            }
        }

        public static void RemoveEventEndFlag(bool logon = false)
        {
            /*
            if (Lilly.scene.name != "SceneMaidManagement")
            {
                MyLog.LogDarkBlue("메이드 관리에서 사용하세요"); ;
                return;
            }
            */
            if (GUIHarmony.GetHarmonyPatchCheck(typeof(MaidManagementMainPatch)))
                RemoveEventEndFlag(MaidManagementMainPatch.___select_maid_);
            else
                MyLog.LogDarkBlue("HarmonyUtill에서 MaidManagementMainPatch 를 켜주시고 메이드를 선택하세요");
            //RemoveEventEndFlag(SceneEdit.Instance.maid);
        }

        public static void RemoveEventEndFlag(Maid maid)
        {
            if (maid != null)
            {
                MyLog.LogDarkBlue("RemoveEventEndFlag" + MyUtill.GetMaidFullName(maid));
                maid.status.RemoveEventEndFlagAll();
            }

        }

    }
}
