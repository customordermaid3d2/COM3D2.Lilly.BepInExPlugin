using System;
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
    public class GUIVirtualMgr// : AwakeUtill
    {
        private int WindowId = new System.Random().Next();
        private static Rect windowRect = new Rect(40f, 40f, 300f, 600f);
        // static 안됨. GUIStyle 같이 GUI 는 OnGui안에서만 쓸수 있다 함
        //private GUIStyle windowStyle = new GUIStyle(GUI.skin.box);
        //private static GUIStyle windowStyle;

        private static GUILayoutOptionUtill guio = GUILayoutOptionUtill.Instance;

        private bool isGuiOn = false;
        internal static ConfigFile customFile;

        public static event Action isGuiOff;
        public static event Action actionsOnGui;
        public static event Action actionsStart;
        public static event Action actionsSetButtonList;

        public string name = "GUIVirtual";

        public static Dictionary<int ,GUIVirtualMgr> guis = new Dictionary<int, GUIVirtualMgr>() ;
        public static int pageCount = 0;
        public static int pageNow = 0;
        public int pageNum = 0;
        public static bool open = true;

        public bool IsGuiOn {
            get => isGuiOn;
            set
            {
                pageNow = pageNum;
                isGuiOff();
                isGuiOn = value;
            }
        }

        public GUIVirtualMgr() : base()
        {
            MyLog.LogDebug("GUIVirtual()");

            SetName();
            Seting();

        }

        public GUIVirtualMgr(string name) : base()
        {
            MyLog.LogDebug("GUIVirtual()", name);
            this.name = name;
            Seting();
        }

        private void Seting()
        {            
            guis.Add(pageNum = pageCount++, this);
            isGuiOff += SetGuiOff;
            actionsOnGui += OnGui;
            actionsStart += Start;
            actionsSetButtonList += SetButtonList;
            SystemShortcutAPI.AddButton(name, new Action(SetGuiOnOff), name, GearMenu.png);
        }

        public virtual void SetName()
        {
            name = GetType().Name;
        }

        public virtual void SetName(string name)
        {
            this.name = name;
        }


        #region GUI On OFF

        public static void SetGuiOffAll()
        {
            isGuiOff();
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

        public static void GoPage(int p)
        {
            MyLog.LogDebug("GUIVirtual.GoPage", p);
            pageNow = (p + pageCount) % pageCount;
            guis[pageNow].IsGuiOn = true;
        }

        #endregion



        public void init()
        {
            MyLog.LogDebug("GUIVirtual.init", name);
            //configEntryUtill = new ConfigEntryUtill(
            //"GUIVirtual_" + name
            //, "OnSceneLoaded"
            //);
        }

        public void Awake()
        {
            MyLog.LogDebug("GUIVirtual.Awake", name);

        }

        public static void ActionsStart()
        {

            actionsStart();
        }

        public virtual void Start()
        {
            MyLog.LogDebug("Start", name);
        }

        #region OnGui

        public static void ActionsOnGui()
        {
            actionsOnGui();
        }

        public virtual void OnGui()
        {
            if (!IsGuiOn)
            {
                return;
            }

            //if (windowStyle == null)
            //{
            //    windowStyle = new GUIStyle(GUI.skin.box);
            //}
            // Assign the currently skin to be Unity's default.
            GUI.skin = null;

            // 화면 밖으로 안나가게 조정
            windowRect.x = Mathf.Clamp(windowRect.x, -windowRect.width + 20, Screen.width - 20);
            windowRect.y = Mathf.Clamp(windowRect.y, -windowRect.height + 20, Screen.height - 20);

            windowRect = GUILayout.Window(WindowId, windowRect, GuiFunc, Lilly.Instance.name);
        } 

        protected Vector2 scrollPosition;

       

        /// <summary>
        /// GUI는 이걸 오버라이드 해서 작성하기
        /// </summary>
        /// <param name="windowId"></param>
        public virtual void GuiFunc(int windowId)
        {
            GUI.enabled = true;
            //GUI.DragWindow();//이건 마지막이여야 의도대로 작동 하는듯?
            GUILayout.BeginVertical();

            #region title

            GUILayout.BeginHorizontal();

            GUILayout.Label(name + " : "+ windowId);
            GUILayout.FlexibleSpace();
            GUILayout.Label(pageNow + " / " + pageNum + " / " + pageCount+" , "+ open);
            if (GUILayout.Button("<", guio[GUILayoutOptionUtill.Type.Height, 20]))
            {
                MyLog.LogDebug("GUIVirtual.GuiFunc", pageNow);
                GoPage(pageNow+1);
            }
            if (GUILayout.Button(">", guio[GUILayoutOptionUtill.Type.Height, 20]))
            {
                MyLog.LogDebug("GUIVirtual.GuiFunc", pageNow);
                GoPage(pageNow - 1);
            }
            if (GUILayout.Button("-", guio[GUILayoutOptionUtill.Type.Height, 20])) { open = !open; }

            GUILayout.EndHorizontal();

            #endregion

            if (open && pageNow == pageNum)
            {

                #region body

                scrollPosition = GUILayout.BeginScrollView(scrollPosition);

                SetButtonList();

                GUILayout.EndScrollView();

                #endregion

            }

            GUILayout.EndVertical();

            GUI.DragWindow();
            GUI.enabled = true;
        }

        public virtual void SetButtonList()
        {
            MyLog.LogWarning("SetButtonList", name);
        }

        #endregion

    }
}
