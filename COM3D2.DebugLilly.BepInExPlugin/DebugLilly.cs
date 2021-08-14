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
        public const string PLAGIN_VERSION = "21.08.14.20";
        public const string PLAGIN_FULL_NAME = "COM3D2.DebugLilly.Plugin";
    }


   [BepInPlugin(MyAttribute.PLAGIN_FULL_NAME, MyAttribute.PLAGIN_NAME, MyAttribute.PLAGIN_VERSION)]
    public class DebugLilly : BaseUnityPlugin
    {
        public static MyLog log;

        public void Awake()
        {
            log = new MyLog(Logger);

            log.LogInfo("=== DebugLilly ===", MyUtill.GetBuildDateTime(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version));
            log.LogDarkBlue("=== GetGameInfo st ===");


            log.LogInfo("Application.installerName : " + Application.installerName);
            log.LogInfo("Application.version : " + Application.version);
            log.LogInfo("Application.unityVersion : " + Application.unityVersion);
            log.LogInfo("Application.companyName : " + Application.companyName);
            log.LogInfo("Application.dataPath : " + Application.dataPath);

            log.LogInfo("Environment.CurrentDirectory : " + Environment.CurrentDirectory);
            log.LogInfo("Environment.SystemDirectory : " + Environment.SystemDirectory);
            log.LogInfo("Environment.ApplicationData : " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            log.LogInfo("Environment.CommonApplicationData : " + Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            log.LogInfo("Environment.LocalApplicationData : " + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            log.LogInfo("Environment.Personal : " + Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            log.LogInfo("Environment.History : " + Environment.GetFolderPath(Environment.SpecialFolder.History));
            log.LogInfo("Environment.Desktop : " + Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            log.LogInfo("Environment.Programs : " + Environment.GetFolderPath(Environment.SpecialFolder.Programs));

            log.LogInfo("UTY.gameProjectPath : " + UTY.gameProjectPath);
            log.LogInfo("UTY.gameDataPath : " + UTY.gameDataPath);

            log.LogInfo("GameUty.IsEnabledCompatibilityMode : " + GameUty.IsEnabledCompatibilityMode);
            
            try
            {
            }
            catch (Exception e)
            {
                log.LogWarning("Awake:" + e.ToString());
            }

            try
            {

                log.LogInfo("Product.windowTitel : " + Product.windowTitel);

                log.LogInfo("Product.enabeldAdditionalRelation : " + Product.enabeldAdditionalRelation);
                log.LogInfo("Product.enabledSpecialRelation : " + Product.enabledSpecialRelation);

                log.LogInfo("Product.isEnglish : " + Product.isEnglish);
                log.LogInfo("Product.isJapan : " + Product.isJapan);
                log.LogInfo("Product.isPublic : " + Product.isPublic);

                log.LogInfo("Product.lockDLCSiteLink : " + Product.lockDLCSiteLink);

                log.LogInfo("Product.defaultLanguage : " + Product.defaultLanguage);
                log.LogInfo("Product.supportMultiLanguage : " + Product.supportMultiLanguage);
                log.LogInfo("Product.systemLanguage : " + Product.systemLanguage);

                log.LogInfo("Product.type : " + Product.type);

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
                    log.LogInfo(type.Name, item.Name, item.GetValue(null));
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
            log.LogInfo("");

            LogFolder(UTY.gameProjectPath);
            LogFolder(UTY.gameProjectPath + @"\lilly");
            LogFolder(UTY.gameProjectPath + @"\BepInEx\plugins");
            LogFolder(UTY.gameProjectPath + @"\Sybaris");
            LogFolder(UTY.gameProjectPath + @"\Sybaris\UnityInjector");


            log.LogDarkBlue("=== GetGameInfo ed ===");
        }

        public void Start()
        {
            log.LogMessage("Start");
            log.LogInfo("=== DebugLilly ===");
            log.LogDarkBlue("=== GetGameInfo st ===");

            log.LogInfo("StoreDirectoryPath : " + GameMain.Instance.SerializeStorageManager.StoreDirectoryPath);


            try
            {

                log.LogInfo("GameMain.Instance.CMSystem.CM3D2Path : " + GameMain.Instance.CMSystem.CM3D2Path);

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
                    log.LogInfo(File.Name);
                }
            log.LogDarkBlue("=== DirectoryInfo ed ===");
        }
    }
}
