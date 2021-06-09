using HarmonyLib;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class ScheduleTaskViewerPatch
    {
        // ScheduleTaskViewer

        public static ScheduleTaskViewer instance;

        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleTaskViewer), MethodType.Constructor)]
        public static void ScheduleTaskCtrlCtor(ScheduleTaskViewer __instance)
        {
            MyLog.LogMessage(
            "ScheduleTaskCtrl.Ctor"
            );
            instance = __instance;
        }

        
        [HarmonyPostfix, HarmonyPatch(typeof(ScheduleTaskViewer), "Call")]
        public static void Call(Maid maid, ScheduleMgr.ScheduleTime scheduleTime, Dictionary<ScheduleTaskCtrl.TaskType, List<ScheduleTaskViewer.ViewData>> viewDic, Dictionary<ScheduleMgr.ScheduleTime, ScheduleCSVData.ScheduleBase> currentSetWorkDic,ScheduleTaskViewer __instance)
        {
            MyLog.LogMessage(
                "ScheduleTaskViewer.Call"
                , maid.status.fullNameEnStyle
                , scheduleTime
                );
            foreach (var item in viewDic)
            {
                MyLog.LogMessage(
                "ScheduleTaskViewer.Call.viewDic"
                , item.Key                
                );
                foreach (var item2 in  item.Value)
                {
                    MyLog.LogMessage(
                    "ScheduleTaskViewer.Call.viewDic"
                    , item2.is_enabled
                    , item2.schedule.id
                    , item2.schedule.type                    
                    , item2.schedule.name
                    );
                }
            }
            foreach (var item in currentSetWorkDic)
            {
                MyLog.LogMessage(
                "ScheduleTaskViewer.Call.currentSetWorkDic"
                , item.Key
                , item.Value.id
                , item.Value.type
                , item.Value.name
                );
            }
        }



#if false
        public void Call(Maid maid, ScheduleMgr.ScheduleTime scheduleTime, Dictionary<ScheduleTaskCtrl.TaskType, List<ScheduleTaskViewer.ViewData>> viewDic, Dictionary<ScheduleMgr.ScheduleTime, ScheduleCSVData.ScheduleBase> currentSetWorkDic)
        {
            if (this.category_button_dic_ == null)
            {
                this.Awake();
            }
            ScheduleTaskViewer.scheduleTime = scheduleTime;
            HashSet<int> hashSet = new HashSet<int>();
            int maxValue = int.MaxValue;
            UIWFTabButton uiwftabButton = null;
            UIWFTabButton uiwftabButton2 = null;
            foreach (KeyValuePair<ScheduleTaskCtrl.TaskType, List<ScheduleTaskViewer.ViewData>> keyValuePair in viewDic)
            {
                this.CallEnableButton(maid, keyValuePair.Key, keyValuePair.Value, ref hashSet, ref maxValue);
            }
            this.currentWorkDic = currentSetWorkDic;
            int num = maxValue;
            ScheduleCSVData.ScheduleBase work_data = null;
            foreach (KeyValuePair<int, UIButton> keyValuePair2 in this.category_button_dic_)
            {
                keyValuePair2.Value.onClick.Clear();
                bool flag = hashSet.Contains(keyValuePair2.Key);
                keyValuePair2.Value.transform.parent.gameObject.SetActive(flag);
                if (flag)
                {
                    EventDelegate.Add(keyValuePair2.Value.onClick, new EventDelegate.Callback(this.OnClickFromMainCategoryButton));
                    if (uiwftabButton2 == null && num == keyValuePair2.Key)
                    {
                        uiwftabButton2 = keyValuePair2.Value.gameObject.GetComponent<UIWFTabButton>();
                    }
                    ScheduleCSVData.ScheduleBase scheduleBase = currentSetWorkDic[scheduleTime];
                    if (uiwftabButton == null && scheduleBase != null && scheduleBase.categoryID == keyValuePair2.Key)
                    {
                        work_data = scheduleBase;
                        uiwftabButton = keyValuePair2.Value.gameObject.GetComponent<UIWFTabButton>();
                    }
                }
            }
            if (uiwftabButton == null)
            {
                uiwftabButton = uiwftabButton2;
            }
            Utility.ResetNGUI(this.CategoryGrid);
            Utility.ResetNGUI(this.CategoryScrollView);
            UIWFTabPanel componentInChildren = this.CategoryGrid.GetComponentInChildren<UIWFTabPanel>();
            componentInChildren.ResetSelect();
            componentInChildren.UpdateChildren();
            if (uiwftabButton != null)
            {
                componentInChildren.Select(uiwftabButton);
            }
            if (this.descDic == null)
            {
                DescScheduleYotogi component = UTY.GetChildObject(base.gameObject, "DescScheduleYotogi", false).GetComponent<DescScheduleYotogi>();
                component.Init(this.taskCtrl);
                DescScheduleTraining component2 = UTY.GetChildObject(base.gameObject, "DescScheduleTraining", false).GetComponent<DescScheduleTraining>();
                component2.Init(this.taskCtrl);
                DescScheduleWork component3 = UTY.GetChildObject(base.gameObject, "DescScheduleWork", false).GetComponent<DescScheduleWork>();
                component3.Init(this.taskCtrl);
                this.descDic = new Dictionary<ScheduleTaskCtrl.TaskType, DescScheduleBase>();
                this.descDic.Add(ScheduleTaskCtrl.TaskType.Yotogi, component);
                this.descDic.Add(ScheduleTaskCtrl.TaskType.Training, component2);
                this.descDic.Add(ScheduleTaskCtrl.TaskType.Work, component3);
            }
            this.UpdateForDescriptionViewer(work_data);
        }

#endif

    }
}
