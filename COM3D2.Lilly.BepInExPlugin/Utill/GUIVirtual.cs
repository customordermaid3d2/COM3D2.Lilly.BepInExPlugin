using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin
{
    public class GUIVirtual
    {
        private int WindowId = new System.Random().Next();
        private Rect windowRect = new Rect(20f, 20f, 260f, 265f);
        // static 안됨. GUIStyle 같이 GUI 는 OnGui안에서만 쓸수 있다 함
        //private GUIStyle windowStyle = new GUIStyle(GUI.skin.box);
        private GUIStyle? windowStyle;

        public bool isGuiOn = false;

        public string name;

        public GUIVirtual()
        {
            SetName();
        }

        public GUIVirtual(string name)
        {
            this.name = name;
        }

        public virtual void SetName()
        {            
            this.name = this.GetType().Name;
        }

        public virtual void SetName(string name)
        {
            this.name = name;
        }

        public virtual void SetGuiOnOff()
        {
            isGuiOn = !isGuiOn;
            MyLog.LogMessage(name,"SetGuiOnOff", isGuiOn);
        }

        public virtual void OnGui()
        {
            if (!isGuiOn)
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
