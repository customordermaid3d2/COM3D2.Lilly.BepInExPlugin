using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    /// <summary>
    /// 메뉴 화면 표준화
    /// 상속 받은후 SetButtonList 에다가 버튼 목록만 작성하고 있음
    /// </summary>
    public class GUIVirtual
    {
        private static int WindowId = new System.Random().Next();
        private static Rect windowRect = new Rect(40f, 40f, 300f, 300f);
        // static 안됨. GUIStyle 같이 GUI 는 OnGui안에서만 쓸수 있다 함
        //private GUIStyle windowStyle = new GUIStyle(GUI.skin.box);
        private static GUIStyle? windowStyle;

        private bool isGuiOn = false;

        public static event Action isGuiOff;

        public string name= "GUIVirtual";

        public bool IsGuiOn { get => isGuiOn; 
            set
                {
                    isGuiOff();
                    isGuiOn = value;
                }
            }

        public GUIVirtual()
        {
            SetName();
            isGuiOff += SetGuiOff;
        }

        public GUIVirtual(string name)
        {
            this.name = name;
            isGuiOff += SetGuiOff;
        }

        public virtual void SetName()
        {            
            this.name = this.GetType().Name;
        }

        public virtual void SetName(string name)
        {
            this.name = name;
        }

        public virtual void SetGuiOff()
        {
            isGuiOn = false;
            MyLog.LogMessage("SetGuiOff", name, IsGuiOn);
        }

        public virtual void SetGuiOnOff()
        {
            IsGuiOn = !IsGuiOn;
            MyLog.LogMessage("SetGuiOnOff", name, IsGuiOn);
        }

        public virtual void OnGui()
        {
            if (!IsGuiOn)
            {
                return;
            }

            if (windowStyle == null)
            {
                windowStyle = new GUIStyle(GUI.skin.box);
            }

            windowRect.x = Mathf.Clamp(windowRect.x, -windowRect.width + 20, Screen.width - 20);
            windowRect.y = Mathf.Clamp(windowRect.y, -windowRect.height + 20, Screen.height - 20);

            windowRect = GUILayout.Window(WindowId, windowRect, GuiFunc, string.Empty, windowStyle);
        }

        public virtual void GuiFunc(int windowId)
        {

            GUILayout.BeginVertical();

            GUILayout.Label(name+" List");

            SetButtonList();

            GUILayout.FlexibleSpace();

            GUILayout.EndVertical();

            GUI.enabled = true;
            GUI.DragWindow();
        }

        public virtual void SetButtonList()
        {
            MyLog.LogWarning("SetButtonList",name);
        }
    }
}
