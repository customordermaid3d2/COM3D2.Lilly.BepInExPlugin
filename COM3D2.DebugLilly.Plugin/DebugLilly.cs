using COM3D2.Lilly.Plugin;
using MaidStatus;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;

namespace DebugLilly
{
    [PluginFilter("COM3D2x64")]
    [PluginName("COM3D2.DebugLilly.Plugin")]
    [PluginVersion("21.08.14.20")]
    public class DebugLilly : PluginBase
    {
        //public static Stopwatch stopwatch = new Stopwatch(); //객체 선언

        public DebugLilly()
        {
            //stopwatch.Start(); // 시간측정 시작
        }

        public void Awake()
        {
            //MyLog.LogMessage("Awake", string.Format("{0:0.000} ", stopwatch.Elapsed.ToString()));

            MyLog.LogInfo("=== DebugLilly ===");
            MyLog.LogDarkBlue("=== GetGameInfo st ===");

            MyLog.LogInfo("Application.installerName : " + Application.installerName);
            MyLog.LogInfo("Application.version : " + Application.version);
            MyLog.LogInfo("Application.unityVersion : " + Application.unityVersion);
            MyLog.LogInfo("Application.companyName : " + Application.companyName);
            MyLog.LogInfo("Application.dataPath : " + Application.dataPath);

            MyLog.LogInfo("Environment.CurrentDirectory : " + Environment.CurrentDirectory);
            MyLog.LogInfo("Environment.SystemDirectory : " + Environment.SystemDirectory);
            MyLog.LogInfo("Environment.ApplicationData : " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            MyLog.LogInfo("Environment.CommonApplicationData : " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            MyLog.LogInfo("Environment.LocalApplicationData : " + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MyLog.LogInfo("Environment.Personal : " + Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            MyLog.LogInfo("Environment.History : " + Environment.GetFolderPath(Environment.SpecialFolder.History));
            MyLog.LogInfo("Environment.Desktop : " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            MyLog.LogInfo("Environment.Programs : " + Environment.GetFolderPath(Environment.SpecialFolder.Programs));

            MyLog.LogInfo("StoreDirectoryPath : " + GameMain.Instance.SerializeStorageManager.StoreDirectoryPath);

            MyLog.LogInfo("UTY.gameProjectPath : " + UTY.gameProjectPath);
            MyLog.LogInfo("UTY.gameDataPath : " + UTY.gameDataPath);


            MyLog.LogInfo("GameMain.Instance.CMSystem.CM3D2Path : " + GameMain.Instance.CMSystem.CM3D2Path);

            MyLog.LogInfo("GameUty.IsEnabledCompatibilityMode : " + GameUty.IsEnabledCompatibilityMode);

            MyLog.LogInfo("Product.windowTitel : " + Product.windowTitel);

            MyLog.LogInfo("Product.enabeldAdditionalRelation : " + Product.enabeldAdditionalRelation);
            MyLog.LogInfo("Product.enabledSpecialRelation : " + Product.enabledSpecialRelation);

            MyLog.LogInfo("Product.isEnglish : " + Product.isEnglish);
            MyLog.LogInfo("Product.isJapan : " + Product.isJapan);
            MyLog.LogInfo("Product.isPublic : " + Product.isPublic);

            MyLog.LogInfo("Product.lockDLCSiteLink : " + Product.lockDLCSiteLink);

            MyLog.LogInfo("Product.defaultLanguage : " + Product.defaultLanguage);
            MyLog.LogInfo("Product.supportMultiLanguage : " + Product.supportMultiLanguage);
            MyLog.LogInfo("Product.systemLanguage : " + Product.systemLanguage);

            MyLog.LogInfo("Product.type : " + Product.type);

            Type type = typeof(Misc);
            foreach (var item in type.GetFields())
            {
                MyLog.LogInfo(type.Name, item.Name, item.GetValue(null));
            }
            /*
             * 추출 안됨
            MyLog.LogInfo("GUI.skin.customStyles");

            if (GUI.skin?.customStyles != null)
            {
                foreach (var item in GUI.skin.customStyles)
                {
                    MyLog.LogMessage(
                        item.name
                        , item.fixedWidth
                        , item.fixedHeight
                        , item.stretchWidth
                        , item.stretchHeight
                        , item.font.name
                        , item.fontSize
                        , item.fontStyle
                        );
                }
            }
            else
            {
                MyLog.LogInfo("GUI.skin null");
            }
            */
            MyLog.LogInfo();



            try
            {
                var l = Personal.GetAllDatas(false);
                MyLog.LogMessage("성격 전체", l.Count);
                foreach (var item in l)
                {
                    MyLog.LogMessage("Personal:", item.id, item.replaceText, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogWarning("Personal:" + e.ToString());
            }

            try
            {
                var l = Personal.GetAllDatas(true);
                MyLog.LogMessage("성격 가능", l.Count);
                foreach (var item in l)
                {
                    MyLog.LogMessage("Personal:", item.id, item.replaceText, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                MyLog.LogWarning("Personal:" + e.ToString());
            }


            MyLog.LogDarkBlue("=== GetGameInfo ed ===");
            LogFolder(UTY.gameProjectPath);
            LogFolder(UTY.gameProjectPath + @"\lilly");
            LogFolder(UTY.gameProjectPath + @"\BepInEx\plugins");
            LogFolder(UTY.gameProjectPath + @"\Sybaris");
            LogFolder(UTY.gameProjectPath + @"\Sybaris\UnityInjector");
        }

        private static void LogFolder(string storeDirectoryPath)
        {
            MyLog.LogDarkBlue("=== DirectoryInfo st === " + storeDirectoryPath);
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(storeDirectoryPath);
            if (di.Exists)
                foreach (System.IO.FileInfo File in di.GetFiles())
                {
                    MyLog.LogInfo(File.Name);
                }
            MyLog.LogDarkBlue("=== DirectoryInfo ed ===");
        }
    }
}
