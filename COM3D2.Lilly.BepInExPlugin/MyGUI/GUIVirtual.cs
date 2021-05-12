﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BepInEx.Configuration;
using COM3D2API;
using COM3D2.Lilly.Plugin.Utill;

namespace COM3D2.Lilly.Plugin.MyGUI
{
    /// <summary>
    /// 메뉴 화면 표준화
    /// 상속 받은후 SetButtonList 에다가 버튼 목록만 작성하고 있음
    /// </summary>
    public class GUIVirtual// : AwakeUtill
    {
        private static int WindowId = new System.Random().Next();
        private static Rect windowRect = new Rect(40f, 40f, 300f, 600f);
        // static 안됨. GUIStyle 같이 GUI 는 OnGui안에서만 쓸수 있다 함
        //private GUIStyle windowStyle = new GUIStyle(GUI.skin.box);
        private static GUIStyle windowStyle;

        private bool isGuiOn = false;
        internal static ConfigFile customFile;

        public static event Action isGuiOff;
        public static event Action actionsOnGui;
        public static event Action actionsStart;

        public string name = "GUIVirtual";

        public bool IsGuiOn {
            get => isGuiOn;
            set
            {
                isGuiOff();
                isGuiOn = value;
            }
        }

        public GUIVirtual() : base()
        {
            MyLog.LogDebug("GUIVirtual()");

            SetName();
            NewMethod();

        }

        public GUIVirtual(string name) : base()
        {
            MyLog.LogDebug("GUIVirtual()", name);
            this.name = name;
            NewMethod();
        }

        private void NewMethod()
        {            
            isGuiOff += SetGuiOff;
            actionsOnGui += OnGui;
            actionsStart += Start;
            SystemShortcutAPI.AddButton(name, new Action(SetGuiOnOff), name, GearMenu.png);
        }

        public static void SetGuiOffAll()
        {
            isGuiOff();
        }

        public static void ActionsOnGui()
        {
            actionsOnGui();
        }


        

        public static void ActionsStart()
        {

            actionsStart();
        }

        public virtual void SetName()
        {
            name = GetType().Name;
        }

        public virtual void SetName(string name)
        {
            this.name = name;
        }

        public virtual void SetGuiOff()
        {
            isGuiOn = false;
            //MyLog.LogDebug("SetGuiOff", name, IsGuiOn);
        }

        public virtual void SetGuiOnOff()
        {
            IsGuiOn = !IsGuiOn;
            //MyLog.LogDebug("SetGuiOnOff", name, IsGuiOn);
        }

        public  void init()
        {
            MyLog.LogDebug("GUIVirtual.init", name);
            //configEntryUtill = new ConfigEntryUtill(
            //"GUIVirtual_" + name
            //, "OnSceneLoaded"
            //);
        }
        public  void Awake()
        {
            MyLog.LogDebug("GUIVirtual.Awake", name);

        }

        public virtual void Start()
        {
            MyLog.LogDebug("Start", name);
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

        protected Vector2 scrollPosition;

        public virtual void GuiFunc(int windowId)
        {

            GUILayout.BeginVertical();

            GUILayout.Label(name + " List");

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            //try
            //{
            SetButtonList();
            //}
            //catch (Exception e)
            //{
            //    MyLog.LogFatal("SetButtonList", e.ToString());
            //}

            GUILayout.EndScrollView();

            GUILayout.FlexibleSpace();

            GUILayout.EndVertical();

            GUI.enabled = true;
            GUI.DragWindow();
        }

        public virtual void SetButtonList()
        {
            MyLog.LogWarning("SetButtonList", name);
        }

    }
}