using BepInEx;
using BepInEx.Logging;
using COM3D2.LillyUtill;
using MaidStatus;
using System;
using UnityEngine;


namespace DebugLilly
{
    class MyAttribute
    {
        public const string PLAGIN_NAME = "DebugLilly";
        public const string PLAGIN_VERSION = "21.11.21.00";
        public const string PLAGIN_FULL_NAME = "COM3D2.DebugLilly.Plugin";
    }


   [BepInPlugin(MyAttribute.PLAGIN_FULL_NAME, MyAttribute.PLAGIN_NAME, MyAttribute.PLAGIN_VERSION)]
    public class DebugLilly : BaseUnityPlugin
    {
        public static MyLog log;

        public void Awake()
        {
            log = new MyLog(Logger, Config);

            log.LogMessage("=== DebugLilly ===");
            log.LogDarkBlue("=== GetGameInfo st ===");


            log.LogMessage("Application.installerName : " + Application.installerName);
            log.LogMessage("Application.version : " + Application.version);
            log.LogMessage("Application.unityVersion : " + Application.unityVersion);
            log.LogMessage("Application.companyName : " + Application.companyName);
            log.LogMessage("Application.dataPath : " + Application.dataPath);

            log.LogMessage("Environment.CurrentDirectory : " + Environment.CurrentDirectory);
            log.LogMessage("Environment.SystemDirectory : " + Environment.SystemDirectory);
            log.LogMessage("Environment.ApplicationData : " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            log.LogMessage("Environment.CommonApplicationData : " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            log.LogMessage("Environment.LocalApplicationData : " + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            log.LogMessage("Environment.Personal : " + Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            log.LogMessage("Environment.History : " + Environment.GetFolderPath(Environment.SpecialFolder.History));
            log.LogMessage("Environment.Desktop : " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            log.LogMessage("Environment.Programs : " + Environment.GetFolderPath(Environment.SpecialFolder.Programs));

            log.LogMessage("UTY.gameProjectPath : " + UTY.gameProjectPath);
            log.LogMessage("UTY.gameDataPath : " + UTY.gameDataPath);

            log.LogMessage("GameUty.IsEnabledCompatibilityMode : " + GameUty.IsEnabledCompatibilityMode);

            log.LogMessage("GameUty.GetGameVersionText GameVersion : " + GameUty.GetGameVersionText());
            log.LogMessage("GameUty.GetBuildVersionText BuildVersion : " + GameUty.GetBuildVersionText());





            
            try
            {
            }
            catch (Exception e)
            {
                log.LogWarning("Awake:" + e.ToString());
            }

            try
            {
                log.LogMessage("Product.windowTitel : " + Product.windowTitel);

                log.LogMessage("Product.enabeldAdditionalRelation : " + Product.enabeldAdditionalRelation);
                log.LogMessage("Product.enabledSpecialRelation : " + Product.enabledSpecialRelation);

                log.LogMessage("Product.isEnglish : " + Product.isEnglish);
                log.LogMessage("Product.isJapan : " + Product.isJapan);
                log.LogMessage("Product.isPublic : " + Product.isPublic);

                log.LogMessage("Product.lockDLCSiteLink : " + Product.lockDLCSiteLink);

                log.LogMessage("Product.defaultLanguage : " + Product.defaultLanguage);
                log.LogMessage("Product.supportMultiLanguage : " + Product.supportMultiLanguage);
                log.LogMessage("Product.systemLanguage : " + Product.systemLanguage);

                log.LogMessage("Product.type : " + Product.type);

            }
            catch (Exception e)
            {
                log.LogWarning("Product:" + e.ToString());
            }

            try
            {
                Type type = typeof(Misc);
                foreach (var item in type.GetFields())
                {
                    log.LogMessage(type.Name, item.Name, item.GetValue(null));
                }
            }
            catch (Exception e)
            {
                log.LogWarning("Misc:" + e.ToString());
            }





            /*
             * 추출 안됨
            log.LogInfo("GUI.skin.customStyles");

            if (GUI.skin?.customStyles != null)
            {
                foreach (var item in GUI.skin.customStyles)
                {
                    log.LogMessage(
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
                log.LogInfo("GUI.skin null");
            }
            */
            log.LogMessage("");

            LogFolder(UTY.gameProjectPath);
            LogFolder(UTY.gameProjectPath + @"\lilly");
            LogFolder(UTY.gameProjectPath + @"\BepInEx\plugins");
            LogFolder(UTY.gameProjectPath + @"\Sybaris");
            LogFolder(UTY.gameProjectPath + @"\Sybaris\UnityInjector");
            LogFolder(UTY.gameProjectPath + @"\scripts");


            log.LogDarkBlue("=== GetGameInfo ed ===");
        }

        public void Start()
        {
            log.LogMessage("Start");
            log.LogMessage("=== DebugLilly ===");
            log.LogDarkBlue("=== GetGameInfo st ===");

            log.LogMessage("StoreDirectoryPath : " + GameMain.Instance.SerializeStorageManager.StoreDirectoryPath);

            if (!string.IsNullOrEmpty(GameMain.Instance.CMSystem.CM3D2Path))
            {

                log.LogMessage("GameMain.Instance.CMSystem.CM3D2Path : " + GameMain.Instance.CMSystem.CM3D2Path);
            }

            try
            {
                log.LogMessage("GameUty.GetLegacyGameVersionText カスタムメイド3D 2 GameVersion : " + GameUty.GetLegacyGameVersionText());                
            }
            catch (Exception e)
            {
                log.LogWarning("Start:" + e.ToString());
            }

            log.LogMessage("---PathList---");
            foreach (var item in GameUty.PathList)
            {
                log.LogMessage(item);
            }

            log.LogMessage("---ExistCsvPathList---");
            foreach (var item in GameUty.ExistCsvPathList)
            {
                log.LogMessage(item);
            }

            log.LogMessage("---PathListOld---");
            foreach (var item in GameUty.PathList)
            {
                log.LogMessage(item);
            }

            log.LogMessage("---ExistCsvPathListOld---");
            foreach (var item in GameUty.ExistCsvPathListOld)
            {
                log.LogMessage(item);
            }

            try
            {

                log.LogMessage("GameMain.Instance.CMSystem.CM3D2Path : " + GameMain.Instance.CMSystem.CM3D2Path);

              // log.LogInfo("GameUty.IsEnabledCompatibilityMode : " + GameUty.IsEnabledCompatibilityMode);

            }
            catch (Exception e)
            {
                log.LogWarning("Start:" + e.ToString());
            }


            try
            {
                var l = Personal.GetAllDatas(false);
                log.LogMessage("성격 전체",l.Count);
                foreach (var item in l)
                {
                    log.LogMessage("Personal:", item.id, item.replaceText, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                log.LogWarning("Personal:" + e.ToString());
            }

            try
            {
                var l = Personal.GetAllDatas(true);
                log.LogMessage("성격 가능",l.Count);
                foreach (var item in l)
                {
                    log.LogMessage("Personal:", item.id, item.replaceText, item.uniqueName, item.drawName, item.termName);//
                }
            }
            catch (Exception e)
            {
                log.LogWarning("Personal:" + e.ToString());
            }

            log.LogDarkBlue("=== GetGameInfo ed ===");
        }

        private static void LogFolder(string storeDirectoryPath)
        {
            log.LogDarkBlue("=== DirectoryInfo st === " + storeDirectoryPath);
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(storeDirectoryPath);
            if (di.Exists)
                foreach (System.IO.FileInfo File in di.GetFiles())
                {
                    log.LogMessage(File.Name);
                }
            log.LogDarkBlue("=== DirectoryInfo ed ===");
        }
    }
}
