using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BepInEx.Configuration;
using COM3D2API;
using COM3D2.Lilly.Plugin.Utill;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
    /// <summary>
    /// 메뉴 화면 표준화
    /// 상속 받은후 SetButtonList 에다가 버튼 목록만 작성하고 있음
    /// </summary>
    public class GUIMgr : MonoBehaviour// : AwakeUtill
    {
        public string nameGUI = "GUIVirtual";

        private ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "GUIVirtualMgr"
        );
        internal static ConfigFile customFile;
        private static ConfigEntry<BepInEx.Configuration.KeyboardShortcut> ShowCounter;

        public const int openW = 50;
        private static Rect windowRect = new Rect(40f, 40f, 300f, 600f);
        private const float windowSpace = 40.0f;

        public static GUIMgr instance;

        private static GUILayoutOptionUtill guio = GUILayoutOptionUtill.Instance;


        public static event Action actionsStart = delegate { };

        //private static Dictionary<int, Action> actionsBody = new Dictionary<int, Action>();
        private static Dictionary<int, GUIMgr> guis = new Dictionary<int, GUIMgr>();

        public static int pageCount = 0; // 퐁 페이지 수
        private static ConfigEntry<int> PageNow;
        public static int pageNow {
            set
            {
                PageNow.Value = (value + pageCount) % pageCount;
            }
            get
            {
                return PageNow.Value;// (PageNow.Value + pageCount) % pageCount;
            }
        }  // 현제 페이지
        public int pageNum = 0; // 해당 클래스 페이지 번호
        private static bool isGuiOnMain = true;
        private static bool open = true;
        public static bool Open {
            get => open;
            set
            {
                if (open != value)
                    if (open = value)
                    {
                        windowRect.height = 600f;
                        windowRect.width = 300f;
                        windowRect.x -= openW;
                    }
                    else
                    {
                        windowRect.height = 40f;
                        windowRect.width = 300f - openW;
                        windowRect.x += openW;
                    }
            }
        }

        public static GUIHarmony harmonyUtill;
        public static GUIInfo infoUtill;
        public static GUICheat cheatUtill;
        public static GUIEasy easyUtill;
        public static GUIMaidEdit maidEditGui;
        //public static GUIRndPreset presetGUI;
        public static GUIOnOff OnOffGUI;
        public static GUIPlugin pluginUtill;
        public static GUIFlagMaid GUIFlag;
        public static GUIFlagPlayer GUIFlagPlayer;
        public static GUIScript GUIScript;

        public GUIMgr() : base()
        {

            SetName();
            //name = "GUIVirtual";
            Seting();

            MyLog.LogDebug("GUIVirtual()", nameGUI);
        }

        public GUIMgr(string name) : base()
        {
            this.nameGUI = name;
            Seting();
            MyLog.LogDebug("GUIVirtual()", name);
        }

        internal static void init()
        {
            customFile = Lilly.customFile;
            ShowCounter = customFile.Bind("GUIVirtualMgr", "GUI ON OFF KeyboardShortcut", new BepInEx.Configuration.KeyboardShortcut(KeyCode.Alpha0, KeyCode.LeftControl));
        }

        public static GUIMgr Install(GameObject container)
        {
            MyLog.LogMessage("GameObjectMgr.Install");
            instance = container.GetComponent<GUIMgr>();
            if (instance == null)
            {
                PageNow = customFile.Bind("GUIMgr", "PageNow", 0);

                instance = container.AddComponent<GUIMgr>();

                harmonyUtill = new GUIHarmony();
                infoUtill = new GUIInfo();
                cheatUtill = new GUICheat();
                easyUtill = new GUIEasy();
                maidEditGui = new GUIMaidEdit();
                //presetGUI = new GUIRndPreset();
                GUIScript = new GUIScript();
                GUIFlag = new GUIFlagMaid();
                GUIFlagPlayer = new GUIFlagPlayer();
                OnOffGUI = new GUIOnOff();
                pluginUtill = new GUIPlugin();

                PageNow.Value = (PageNow.Value + pageCount) % pageCount;

                MyLog.LogMessage("GameObjectMgr.Install", instance.name);
            }
            return instance;
        }

        private void Seting()
        {
            guis.Add(pageNum = pageCount++, this);
            //actionsBody.Add(pageNum , SetBody);
            actionsStart += ActionsStart;
        }

        public virtual void SetName()
        {
            nameGUI = GetType().Name;
        }

        public virtual void SetName(string name)
        {
            this.nameGUI = name;
        }


        #region GUI On OFF

        public virtual void SetGuiOnOff()
        {
            isGuiOnMain = !isGuiOnMain;
        }

        #endregion

        public virtual void ActionsStart()
        {
            MyLog.LogDebug("GUIVirtual.ActionsStart", pageNow, nameGUI);
        }

        private void Start()
        {
            windowRect.x = Screen.width - windowRect.width - 20;
            MyLog.LogDebug("Start", nameGUI, Screen.width, windowRect.width, windowRect.x);
            SystemShortcutAPI.AddButton("Lilly Plugin", new Action(SetGuiOnOff), "Lilly Plugin", GearMenu.png);
            actionsStart();
        }


        private void Update()
        {
            //if (!configEntryUtill["Update"])
            //    return;
            //if (ShowCounter.Value.IsDown())
            //{
            //    MyLog.LogMessage("IsDown", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            //}
            //if (ShowCounter.Value.IsPressed())
            //{
            //    MyLog.LogMessage("IsPressed", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
            //}
            if (ShowCounter.Value.IsUp())
            {
                if (!configEntryUtill["Update"])
                    MyLog.LogMessage("IsUp", ShowCounter.Value.Modifiers, ShowCounter.Value.MainKey);
                isGuiOnMain = !isGuiOnMain;
            }
        }


        #region OnGui

        private void OnGUI()
        {
            if (!isGuiOnMain)
            {
                return;
            }

            GUI.skin = null;

            // 화면 밖으로 안나가게 조정
            //windowRect.x = Mathf.Clamp(windowRect.x, -windowRect.width + 20, Screen.width - 20);
            //windowRect.y = Mathf.Clamp(windowRect.y, -windowRect.height + 20, Screen.height - 20);
            windowRect.x = Mathf.Clamp(windowRect.x, -windowRect.width + windowSpace, Screen.width - windowSpace);
            windowRect.y = Mathf.Clamp(windowRect.y, -windowRect.height + windowSpace, Screen.height - windowSpace);

            windowRect = GUILayout.Window(pageNow, windowRect, GuiFunc, Lilly.Instance.name);
        }

        private Vector2 scrollPosition;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowId"></param>
        private void GuiFunc(int windowId)
        {
            GUI.enabled = true;
            //GUI.DragWindow();//이건 마지막이여야 의도대로 작동 하는듯?
            GUILayout.BeginVertical();

            #region title

            GUILayout.BeginHorizontal();

            GUILayout.Label(guis[pageNow].nameGUI, guio[100, 20]);
            GUILayout.FlexibleSpace();
            if (Open)
                GUILayout.Label((pageNow + 1) + " / " + pageCount, guio[40, 20]);
            if (GUILayout.Button("M", guio[20, 20])) pageNow = 0;
            if (GUILayout.Button("<", guio[20, 20]))
            {
                if (configEntryUtill["GuiFunc", false])
                    MyLog.LogDebug("GUIVirtual.GuiFunc", pageNow);
                pageNow--;
            }
            if (GUILayout.Button(">", guio[20, 20]))
            {
                if (configEntryUtill["GuiFunc", false])
                    MyLog.LogDebug("GUIVirtual.GuiFunc", pageNow);
                pageNow++;
            }
            if (GUILayout.Button("-", guio[20, 20])) { Open = !Open; }
            if (GUILayout.Button("x", guio[20, 20])) { isGuiOnMain = false; }

            GUILayout.EndHorizontal();

            #endregion

            if (Open)
            {

                #region body

                scrollPosition = GUILayout.BeginScrollView(scrollPosition);

                //actionsBody[pageNow]();
                guis[pageNow].SetBody();//이거나 저거나 같은데......

                GUILayout.EndScrollView();

                #endregion

            }

            GUILayout.EndVertical();

            GUI.enabled = true;
            GUI.DragWindow();
        }

        /// <summary>
        /// 구현체
        /// </summary>
        public virtual void SetBody()
        {
            if (GUILayout.Button("All LogOnOff")) Lilly.SetLogOnOff();
            GUILayout.Label("page list");
            pageNow = GUILayout.SelectionGrid(pageNow, guis.Select(x => x.Value.nameGUI).ToArray(), 1);
            //MyLog.LogWarning("SetBody", nameGUI);
            //foreach (var item in guis)
            //{
            //
            //}
        }

        #endregion

    }
}
