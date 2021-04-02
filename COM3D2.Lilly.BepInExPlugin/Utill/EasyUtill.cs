using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin
{
    public class EasyUtill : GUIVirtual
    {
        public static Scene scene;

        public EasyUtill()
        {
            name = "EasyUtill";            
        }

        public static void SetScene()
        {
            scene=SceneManager.GetSceneByName("SceneDaily");
        }

        public override void SetButtonList()
        {

            if (Lilly.scene.name == scene.name)
            if (GUILayout.Button("DeleteMaidStatusAll")) DeleteMaidStatusAll();
        }

        private void DeleteMaidStatusAll()
        {
            for (int i = 0; i < 40; i++)//SetDataForViewer 에서 하드 코딩됨
            {
                ScheduleCtrlPatch.DeleteMaidAndReDraw("slot_"+i+"_MaidStatus");
            }
        }

        /*
        public void ClickMaidStatus()
        {
            string name = UIButton.current.name;
            if (UICamera.currentTouchID == -1)
            {
                if (this.CurrentActiveButton == name)
                {
                    return;
                }
                Debug.Log(string.Format("{0}ボタンがクリックされました。", name));
                this.m_MaidStatusListCtrl.CreateTaskViewer(name);
                this.CurrentActiveButton = name;
            }
            else if (UICamera.currentTouchID == -2)
            {
                Debug.Log(string.Format("{0}ボタンが右クリックされました。", name));
                if (this.m_Ctrl.CanDeleteData(name))
                {
                    this.m_Ctrl.DeleteMaidStatus(this.m_scheduleApi, name);
                }
            }
        }
        */
    }
}
