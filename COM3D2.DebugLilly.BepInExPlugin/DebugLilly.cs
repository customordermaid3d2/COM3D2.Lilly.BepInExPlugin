using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using COM3D2.LillyUtill;
using MaidStatus;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;


namespace COM3D2.DebugLilly.Plugin
{
    class MyAttribute
    {
        public const string PLAGIN_NAME = "DebugLilly";
        public const string PLAGIN_VERSION = "22.02.16.02";
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

            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "BepInEx\\LillyPack.dat ")))
                log.LogMessage($"LillyPack version { File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "BepInEx\\LillyPack.dat "))}");
            else
                log.LogMessage("no LillyPack?");

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

            log.LogMessage("GameUty.GetGameVersionText GameVersion : " + GameUty.GetGameVersionText());
            log.LogMessage("GameUty.GetBuildVersionText BuildVersion : " + GameUty.GetBuildVersionText());


            /*
            */
            #region MyRegion

            log.LogMessage("GameUty.IsEnabledCompatibilityMode : " + GameUty.IsEnabledCompatibilityMode);

            log.LogDarkBlue("=== PluginInfos ===");
            try
            {
                foreach (var item in Chainloader.PluginInfos)
                {
                    log.LogMessage(item.Key + " , " + item.Value.Metadata.GUID + " , " + item.Value.Metadata.Name + " , " + item.Value.Metadata.Version);
                }
            }
            catch (Exception e)
            {
                log.LogWarning("Awake:" + e.ToString());
            }

            log.LogDarkBlue("=== SybarisLoader ===");

            try
            {
                foreach (string text in Directory.GetFiles(BepInEx.SybarisLoader.Patcher.Util.Utils.SybarisDir.Value, "*.Patcher.dll"))
                {
                    Assembly assembly;
                    try
                    {
                        assembly = Assembly.LoadFile(text);
                        AssemblyName assemName = assembly.GetName();
                        Version ver = assemName.Version;
                        log.LogMessage(assemName.Name + " , " + ver.ToString());
                    }
                    catch (Exception ex)
                    {
                        log.LogError(string.Format("Failed to load {0}: {1}", text, ex.Message));
                        if (ex.InnerException != null)
                        {
                            log.LogError(string.Format("Inner: {0}", ex.Message));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWarning("SybarisLoader:" + e.ToString());
            }

            log.LogDarkBlue("=== UnityInjector ===");

            try
            {
                foreach (string text in Directory.GetFiles(Path.Combine(BepInEx.SybarisLoader.Patcher.Util.Utils.SybarisDir.Value, "UnityInjector"), "*.dll"))
                {
                    Assembly assembly;
                    try
                    {
                        assembly = Assembly.LoadFile(text);
                        AssemblyName assemName = assembly.GetName();
                        Version ver = assemName.Version;

                        log.LogMessage(assemName.Name + " , " + assemName.Version);

                        foreach (Type type in assembly.GetTypes())
                        {
                            foreach (var item in type.GetCustomAttributes(typeof(UnityInjector.Attributes.PluginVersionAttribute), false))
                            {
                                log.LogMessage(assemName.Name + " , " + type.Name + " , " + ((UnityInjector.Attributes.PluginVersionAttribute)item).Version);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.LogError(string.Format("Failed to load {0}: {1}", text, ex.Message));
                        if (ex.InnerException != null)
                        {
                            log.LogError(string.Format("Inner: {0}", ex.Message));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.LogWarning("UnityInjector:" + e.ToString());
            }

            #endregion
            /*
            */
            log.LogDarkBlue("===  ===");


            try
            {
               // 오류 영역?

                //log.LogMessage("Product.windowTitel : " + Product.windowTitel);





                #region UI번역영향 없음

                log.LogMessage("Product.lockDLCSiteLink : " + Product.lockDLCSiteLink);
                log.LogMessage("Product.enabeldAdditionalRelation : " + Product.enabeldAdditionalRelation);
                log.LogMessage("Product.enabledSpecialRelation : " + Product.enabledSpecialRelation);


                log.LogMessage("Product.type : " + Product.type);

                log.LogMessage("Product.isEnglish : " + Product.isEnglish);
                log.LogMessage("Product.isJapan : " + Product.isJapan);
                log.LogMessage("Product.isPublic : " + Product.isPublic);

                log.LogMessage("Product.defaultLanguage : " + Product.defaultLanguage);
                log.LogMessage("Product.supportMultiLanguage : " + Product.supportMultiLanguage);
                log.LogMessage("Product.systemLanguage : " + Product.systemLanguage);

                #endregion



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
        */



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

            LogFolder(UTY.gameProjectPath);
            LogFolder(UTY.gameProjectPath + @"\lilly");
            LogFolder(UTY.gameProjectPath + @"\BepInEx\plugins");
            LogFolder(UTY.gameProjectPath + @"\Sybaris");
            LogFolder(UTY.gameProjectPath + @"\Sybaris\UnityInjector");
            LogFolder(UTY.gameProjectPath + @"\scripts");




            log.LogDarkBlue("=== GetGameInfo ed ===");
            /*
     */
        }

        /// <summary>
        /// UI 번역 영향 없는듯
        /// </summary>
        public void Start()
        {
            log.LogMessage("Start");
            log.LogMessage("=== DebugLilly ===");
            log.LogDarkBlue("=== GetGameInfo st ===");
            // GameMain.Instance.SerializeStorageManager.StoreDirectoryPath 는 Awake에서 못씀
            log.LogMessage("StoreDirectoryPath : " + GameMain.Instance.SerializeStorageManager.StoreDirectoryPath);// Awake에서 못씀
            log.LogMessage("");
            if (!string.IsNullOrEmpty(GameMain.Instance.CMSystem.CM3D2Path))
            {
                log.LogMessage("GameMain.Instance.CMSystem.CM3D2Path : " + GameMain.Instance.CMSystem.CM3D2Path);
                LogFolder(GameMain.Instance.CMSystem.CM3D2Path);
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
                log.LogMessage("성격 전체", l.Count);
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
                log.LogMessage("성격 가능", l.Count);
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
        /*
    */
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
