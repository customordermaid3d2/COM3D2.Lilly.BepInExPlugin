using BepInEx;
using COM3D2.Lilly.Plugin.Utill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin.UtillGUI
{
    /// <summary>
    /// 유니티 앤진 동작 분석용
    /// </summary>
    class GameObjectMgr : MonoBehaviour
    {
        public static GameObjectMgr instance;

        public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
            "GameObjectMgr"
        );

        public int UpdateCount=0;
        public int LateUpdateCount = 0;
        public int FixedUpdateCount = 0;
        public int OnGUICount = 0;
        public int CoroutineCount = 0;
        public float time = 0;

        public static GameObjectMgr Install(GameObject container)
        {
            MyLog.LogMessage("GameObjectMgr.Install");
                instance = container.GetComponent<GameObjectMgr>();
            if (instance == null)
            {
                instance = container.AddComponent<GameObjectMgr>();
                MyLog.LogMessage("GameObjectMgr.Install", instance.name);                
            }
            return instance;
        }

        public void Awake()
        {
            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime dateTime = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            MyLog.LogMessage("GameObjectMgr.Awake", dateTime.ToString("u"));
        }

        public void OnEnable()
        {
            MyLog.LogMessage("GameObjectMgr.OnEnable");
            SceneManager.sceneLoaded += this.OnSceneLoaded;
        }

        public void Start()
        {
            MyLog.LogMessage("GameObjectMgr.Start");
            //StartCoroutine("MyCoroutine");
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "COM3D2.Lilly.Plugin."+instance.name));
        }

        public void Update()
        {
            if (configEntryUtill["Update", false])
                MyLog.LogMessage("GameObjectMgr.Update", ++UpdateCount);
        }

        public void FixedUpdate()
        {
            if (configEntryUtill["FixedUpdate", false])
                MyLog.LogMessage("GameObjectMgr.FixedUpdate", ++FixedUpdateCount);
        }

        public void LateUpdate()
        {
            if (configEntryUtill["LateUpdate",false])
                MyLog.LogMessage("GameObjectMgr.LateUpdate", ++LateUpdateCount, time=+Time.deltaTime, Time.deltaTime);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (configEntryUtill["OnSceneLoaded", false])
                MyLog.LogMessage("GameObjectMgr.OnSceneLoaded"
                , scene.buildIndex
                , scene.rootCount
                , scene.name
                , scene.isLoaded
                , scene.isDirty
                , scene.IsValid()
                , scene.path
                , mode
                );
        }


        private const float windowSpace = 40.0f;
        private Rect windowRect = new Rect(windowSpace, windowSpace, 400f, 400f);
        private int windowId = new System.Random().Next();
        private float sliderValue = 1.0f;
        private float maxSliderValue = 10.0f;
        private float mySlider = 1.0f;
        private bool isCoroutine = false;

        public void OnGUI()
        {
            if (configEntryUtill["OnGUI", false])
                MyLog.LogMessage("GameObjectMgr.OnGUI", ++OnGUICount);

            if (!configEntryUtill["OnGUI.GUI", false])
                return;

            // Assign the currently skin to be Unity's default.
            GUI.skin = null;

            windowRect.x = Mathf.Clamp(windowRect.x, -windowRect.width + windowSpace, Screen.width - windowSpace);
            windowRect.y = Mathf.Clamp(windowRect.y, -windowRect.height + windowSpace, Screen.height - windowSpace);
            //windowRect = GUI.Window(windowId, windowRect, WindowFunction, "My Window "+ windowId);
            windowRect = GUILayout.Window(windowId, windowRect, WindowFunction, "My Window "+ windowId);

            if (GUI.changed)
            {
                if (configEntryUtill["OnGUI"])
                    MyLog.LogMessage("GameObjectMgr.OnGUI.changed", GUI.changed);
            }
        }

        private void WindowFunction(int id)
        {
            GUI.enabled = true;
            //GUI.DragWindow(); 여기 있으면 창 전체가 드레그만 됨

            // Wrap everything in the designated GUI Area
            // GUILayout.BeginArea(new Rect(0, 0, 400f, 300f));//Window 안에서 쓰면 작동 안됨

            // Begin the singular Horizontal Group
            GUILayout.BeginHorizontal();

                    // Place a Button normally
                    if (GUILayout.RepeatButton("Increase max\nSlider Value"))
                    {
                        maxSliderValue += 3.0f * Time.deltaTime;
                    }

                    // Arrange two more Controls vertically beside the Button
                    GUILayout.BeginVertical();
                    GUILayout.Box("Slider Value: " + Mathf.Round(sliderValue));
                    sliderValue = GUILayout.HorizontalSlider(sliderValue, 0.0f, maxSliderValue);

                    // End the Groups and Area
                    GUILayout.EndVertical();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                    mySlider = LabelSlider(new Rect(10, 100, 100, 20), mySlider, 5.0f, "Label text here");

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();

                    if (GUILayout.RepeatButton("StartCoroutine add"))
                    {
                        StartCoroutine("MyCoroutine");
                    }
                    if (GUILayout.RepeatButton("StartCoroutine end all"))
                    {
                isCoroutine = false;
            }

                GUILayout.EndHorizontal();

           // GUILayout.EndArea();
            GUI.DragWindow();
        }

        float LabelSlider(Rect screenRect, float sliderValue, float sliderMaxValue, string labelText)
        {
            GUI.Label(screenRect, labelText);

            // <- Push the Slider to the end of the Label
            screenRect.x += screenRect.width;

            sliderValue = GUI.HorizontalSlider(screenRect, sliderValue, 0.0f, sliderMaxValue);
            return sliderValue;
        }

        public void OnDisable()
        {
            if (configEntryUtill["OnDisable"])
                MyLog.LogMessage("GameObjectMgr.OnDisable");
            SceneManager.sceneLoaded -= this.OnSceneLoaded;

        }

        public IEnumerator MyCoroutine()
        {
            isCoroutine = true;
            while (isCoroutine)
            {
                if (configEntryUtill["MyCoroutine"])
                    MyLog.LogMessage("GameObjectMgr.MyCoroutine", ++CoroutineCount);
                //yield return null;
                yield return new WaitForSeconds(1f);
            }
        }

        public void Pause()
        {
           // Time.timeScale = 0;
        }

        public void Resume()
        {
           // Time.timeScale = 1;
        }
    }
}
