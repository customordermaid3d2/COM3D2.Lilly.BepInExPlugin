using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class AbstractFreeModeItemPatch //: AbstractFreeModeItem
    {
        // AbstractFreeModeItem
        /*
        public override ItemType type { get { return ItemType.Normal; } }
        public override string play_file_name { get { return null; } }
       
        public override string[] condition_text_terms { get { return null; } }
        public override string[] condition_texts { get { return null; } }
        public override string textTerm { get { return null; } }
        public override string text { get { return null; } }
        public override string titleTerm { get { return null; } }
        public override string title { get { return null; } }
        public override int item_id { get { return 0; } }
        protected override bool is_enabled { get { return false; } }

        public static HashSet<int> GetEnabledIdList_()
        {
            return GetEnabledIdList();
        }
        public static HashSet<int> GetEnabledOldIdList_()
        {
            return GetEnabledOldIdList();
        }
        */
        /// <summary>
        /// FreeModeItemEveryday
        /// FreeModeItemLifeMode
        /// FreeModeItemVip
        /// 가상화에서 통합적으론 안되는듯?
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="___is_enabled"></param>
        //[HarmonyPatch(typeof(AbstractFreeModeItem), "is_enabled", MethodType.Getter)]
        //[HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void get_is_enabled(AbstractFreeModeItem __instance, bool __result)
        {
            __result = true;
            OutMsg("AbstractFreeModeItem.get_is_enabled");
        }

        public static void OutMsg(string s)
        {
                MyLog.LogMessage("OutMsg:"+s);
                return;
            /*
            MyLog.LogMessage("OutMsg:" + s
            , __instance.item_id
            , __instance.is_enabled
            , __instance.play_file_name
            , __instance.title
            , __instance.text
            , __instance.type
            , MyUtill.Join(" | ", GetEnabledIdList())
            , MyUtill.Join(" | ", GetEnabledOldIdList())
            , MyUtill.Join(" | ", __instance.condition_texts)
            , __instance.titleTerm
            , __instance.textTerm
            , MyUtill.Join(" | ", __instance.condition_text_terms)
            );
            */
        }






    }
}
