using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

namespace COM3D2.Lilly.Plugin.Utill
{
    public interface interfaceUnity
    {
        public void Awake();

        /// <summary>
        /// SceneManager.sceneLoaded += this.OnSceneLoaded;
        /// </summary>
        public void OnEnable();

        public void Start();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode);

        public void Update();

        public void FixedUpdate();

        public void LateUpdate();

        /// <summary>
        /// private Rect windowRect = new Rect(windowSpace, windowSpace, 400f, 400f);
        /// private int windowId = new System.Random().Next();
        /// private const float windowSpace = 40.0f;
        /// windowRect.x = Mathf.Clamp(windowRect.x, -windowRect.width + windowSpace, Screen.width - windowSpace);
        /// windowRect.y = Mathf.Clamp(windowRect.y, -windowRect.height + windowSpace, Screen.height - windowSpace);
        /// windowRect = GUILayout.Window(windowId, windowRect, WindowFunction, "My Window "+ windowId);
        /// </summary>
        public void OnGUI();

        /// <summary>
        /// private Vector2 scrollPosition;
        /// GUI.enabled = true;
        /// scrollPosition=GUILayout.BeginScrollView(scrollPosition);
        /// GUILayout.BeginHorizontal();
        /// GUILayout.BeginVertical();
        /// if (GUILayout.Button(text))
        /// GUILayout.EndVertical();
        /// GUILayout.EndHorizontal();
        /// GUILayout.EndScrollView();
        /// GUI.DragWindow();
        /// GUI.enabled = true;
        /// </summary>
        /// <param name="id"></param>
        public void WindowFunction(int id);

        /// <summary>
        /// SceneManager.sceneLoaded -= this.OnSceneLoaded;
        /// </summary>
        public void OnDisable();

        public void Pause();

        public void Resume();
    }
}
