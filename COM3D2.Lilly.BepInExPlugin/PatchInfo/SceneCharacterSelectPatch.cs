using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class SceneCharacterSelectPatch
    {
        // SceneCharacterSelect

        [HarmonyPatch(typeof(SceneCharacterSelect), "CreateYotogiCharaList")]// , new Type[] { typeof(BinaryReader), typeof(string) }
        [HarmonyPostfix]
        public static void CreateYotogiCharaList()
        {
            MyLog.LogMessage("CreateYotogiCharaList() "
            );
        }
        
        [HarmonyPatch(typeof(SceneCharacterSelect), "CreateVipCharaList")]// , new Type[] { typeof(BinaryReader), typeof(string) }
        [HarmonyPostfix]
        public static void CreateVipCharaList()
        {
            MyLog.LogMessage("CreateVipCharaList() "
            );
        }
        
        [HarmonyPatch(typeof(SceneCharacterSelect), "CreateNewYotogiHaremPairCharaList")]// , new Type[] { typeof(BinaryReader), typeof(string) }
        [HarmonyPostfix]
        public static void CreateNewYotogiHaremPairCharaList()
        {
            MyLog.LogMessage("CreateNewYotogiHaremPairCharaList() "
            );
        }
        
        [HarmonyPatch(typeof(SceneCharacterSelect), "CreateNewYotogiCharaList")]// , new Type[] { typeof(BinaryReader), typeof(string) }
        [HarmonyPostfix]
        public static void CreateNewYotogiCharaList()
        {
            MyLog.LogMessage("CreateNewYotogiCharaList() "
            );
        }
        
        [HarmonyPatch(typeof(SceneCharacterSelect), "CreateLifeModeCharaList")]// , new Type[] { typeof(BinaryReader), typeof(string) }
        [HarmonyPostfix]
        public static void CreateLifeModeCharaList()
        {
            MyLog.LogMessage("CreateLifeModeCharaList() "
            );
        }
        
        
        [HarmonyPatch(typeof(SceneCharacterSelect), "CreateFacilityCharaList")]// , new Type[] { typeof(BinaryReader), typeof(string) }
        [HarmonyPostfix]
        public static void CreateFacilityCharaList()
        {
            MyLog.LogMessage("CreateFacilityCharaList() "
            );
        }

        private static List<int> randomMaidList = new List<int>();
        private static bool oneMaidIsSelected = false;
        private static int CurrentLevel => Lilly.scene.buildIndex;


        public static void SelectRandomMaid()
        {
            // Yotogi & Karaoke
            if ("SceneCharacterSelect" == Lilly.scene.name || CurrentLevel == 36)
            //if (CurrentLevel == 1 || CurrentLevel == 36)
            {
                GameObject maidSkillUnit = GameObject.Find("UI Root/Parent/CharacterSelectPanel/Contents/MaidSkillUnitParent");
                UIWFTabButton[] buttonsList = maidSkillUnit.GetComponentsInChildren<UIWFTabButton>();

                int randomButtonIndex = UnityEngine.Random.Range(0, buttonsList.Length);
                MyLog.LogInfo("Number selected:" + randomButtonIndex);

                AccessTools.Method(typeof(UIWFTabButton), "OnClick").Invoke(buttonsList[randomButtonIndex], null);
            }
            // Dance
            else if (CurrentLevel == 21)
            {
                bool isSingleMaidDance = true;
                GameObject maidSkillUnit = GameObject.Find("UI Root/Parent/CharaSelect/CharacterSelectPanel/Contents/MaidSkillUnitParent");
                UIWFTabButton[] buttonsList = maidSkillUnit.GetComponentsInChildren<UIWFTabButton>();

                // If multiple maids to select the type of button changes.
                if (buttonsList.Length == 0)
                {
                    isSingleMaidDance = false;
                }

                if (isSingleMaidDance)
                {
                    MyLog.LogInfo("Number of maids found:" + buttonsList.Length);
                    int randomButton = UnityEngine.Random.Range(0, buttonsList.Length);
                    MyLog.LogInfo("Maid number " + +randomButton + " selected");

                    AccessTools.Method(typeof(UIWFTabButton), "OnClick").Invoke(buttonsList[randomButton], null);
                }

                if (!isSingleMaidDance)
                {
                    MyLog.LogInfo("Multiple Maids Dance");
                    UIWFSelectButton[] SelectList = maidSkillUnit.GetComponentsInChildren<UIWFSelectButton>();
                    MyLog.LogInfo("Number of maids found:" + SelectList.Length);

                    // Unselect already selected Maids
                    // Determines if the list has, at least, one selected maid first, to avoid conflict if the user cancels or after another dance.
                    foreach (UIWFSelectButton buttonInList in SelectList)
                    {
                        if (buttonInList.isSelected)
                        {
                            oneMaidIsSelected = true;
                            break;
                        }
                    }

                    if (randomMaidList.Count != 0)
                    {
                        if (oneMaidIsSelected)
                        {
                            foreach (int value in randomMaidList)
                            {
                                MyLog.LogInfo("Unselecting Maid: " + value);
                                UIWFSelectButton.selected = false;
                                AccessTools.Method(typeof(UIWFSelectButton), "OnClick").Invoke(SelectList[value], null);
                            }
                            oneMaidIsSelected = false;
                        }
                        randomMaidList.Clear();
                    }

                    // Check if enough maids to avoid having an infinite while loop
                    if (SelectList.Length < 3)
                    {
                        MyLog.LogInfo("Not enough Maids");
                        return;
                    }

                    // Select 3 maids 
                    while (randomMaidList.Count < 3)
                    {
                        int randomButton = UnityEngine.Random.Range(0, SelectList.Length);
                        if (!randomMaidList.Contains(randomButton))
                        {
                            randomMaidList.Add(randomButton);
                            MyLog.LogInfo("Selecting Maid: " + randomButton);
                        }
                    }

                    foreach (int value in randomMaidList)
                    {
                        AccessTools.Method(typeof(UIWFSelectButton), "OnClick").Invoke(SelectList[value], null);
                    }
                }
            }
        }

    }
}
