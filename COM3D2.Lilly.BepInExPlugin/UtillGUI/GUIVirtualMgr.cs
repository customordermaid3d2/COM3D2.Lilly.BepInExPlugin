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
    public class GUIVirtualMgr : MonoBehaviour// : AwakeUtill
    {

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
        "GUIVirtualMgr"
        );

        //private int WindowId = new System.Random().Next();// 제대로 랜덤 생성 안됨
        //private int WindowId = UnityEngine.Random.;// 제대로 랜덤 생성 안됨
        //private static Rect windowRectMax = new Rect(40f, 40f, 300f, 600f);
        //private static Rect windowRectMin = new Rect(40f, 40f, 300f, 45);
        private static Rect windowRect = new Rect(40f, 40f, 300f, 600f);

        // static 안됨. GUIStyle 같이 GUI 는 OnGui안에서만 쓸수 있다 함
        //private GUIStyle windowStyle = new GUIStyle(GUI.skin.box);
        //private static GUIStyle windowStyle;

        private static GUILayoutOptionUtill guio = GUILayoutOptionUtill.Instance;

        private static bool isGuiOnMain = true;
        private bool isGuiOn = false;
        internal static ConfigFile customFile;

        public static event Action isGuiOff;
        public static event Action actionsOnGui;
        public static event Action actionsStart;
        public static event Action actionsSetButtonList;

        public string nameGUI = "GUIVirtual";

        public static Dictionary<int ,GUIVirtualMgr> guis = new Dictionary<int, GUIVirtualMgr>() ;
        public static int pageCount = 0;
        public static int pageNow = 0;
        public int pageNum = 0;
        private static bool open = true;
        public static bool Open { get => open; 
            set  {
                if (open != value)
                if (open = value)
                {
                    windowRect.height= 600f;
                    windowRect.width= 300f;
                        windowRect.x -= 100;
                }
                else
                {
                    windowRect.height= 40f;
                    windowRect.width= 200f;
                        windowRect.x += 100;
                    }
            } }

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
            //name = "GUIVirtual";
            Seting();

        }

        public GUIVirtualMgr(string name) : base()
        {
            MyLog.LogDebug("GUIVirtual()", name);
            this.nameGUI = name;
            Seting();
        }

        private void Seting()
        {            
            guis.Add(pageNum = pageCount++, this);
            isGuiOff += SetGuiOff;
            actionsOnGui += OnGui;
            actionsStart += Start;
            actionsSetButtonList += SetButtonList;
            SystemShortcutAPI.AddButton(nameGUI, new Action(SetGuiOnOff), nameGUI, GearMenu.png);
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
            if (configEntryUtill["GoPage",false])
                MyLog.LogDebug("GUIVirtual.GoPage", p);
            pageNow = (p + pageCount) % pageCount;
            guis[pageNow].IsGuiOn = true;
        }

        #endregion



        public void init()
        {
            MyLog.LogDebug("GUIVirtual.init", nameGUI);
            //configEntryUtill = new ConfigEntryUtill(
            //"GUIVirtual_" + name
            //, "OnSceneLoaded"
            //);
        }

        public void Awake()
        {
            MyLog.LogDebug("GUIVirtual.Awake", nameGUI);

        }

        public static void ActionsStart()
        {

            actionsStart();
        }

        public virtual void Start()
        {
            windowRect.x = Screen.width - windowRect.width  - 20;
            MyLog.LogDebug("Start", nameGUI, Screen.width, windowRect.width, windowRect.x);
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

            GUI.skin = null;

            // 화면 밖으로 안나가게 조정
            windowRect.x = Mathf.Clamp(windowRect.x, -windowRect.width + 20, Screen.width - 20);
            windowRect.y = Mathf.Clamp(windowRect.y, -windowRect.height + 20, Screen.height - 20);

            windowRect = GUILayout.Window(pageNow, windowRect, GuiFunc, Lilly.Instance.name);
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

            GUILayout.Label(nameGUI );
            GUILayout.FlexibleSpace();
            if (Open)
                GUILayout.Label((pageNum+1 )+ " / " + pageCount);
            if (GUILayout.Button("<", guio[GUILayoutOptionUtill.Type.Height, 20]))
            {
                if(configEntryUtill["GuiFunc",false])
                MyLog.LogDebug("GUIVirtual.GuiFunc", pageNow);
                GoPage(pageNow - 1);
            }
            if (GUILayout.Button(">", guio[GUILayoutOptionUtill.Type.Height, 20]))
            {
                if (configEntryUtill["GuiFunc",false])
                    MyLog.LogDebug("GUIVirtual.GuiFunc", pageNow);
                GoPage(pageNow + 1);
            }
            if (GUILayout.Button("-", guio[GUILayoutOptionUtill.Type.Height, 20])) { Open = !Open; }
            if (GUILayout.Button("x", guio[GUILayoutOptionUtill.Type.Height, 20])) { isGuiOff(); }

            GUILayout.EndHorizontal();

            #endregion

            if (Open && pageNow == pageNum)
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
            MyLog.LogWarning("SetButtonList", nameGUI);
        }

        #endregion

    }
}
