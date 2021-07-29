using BepInEx.Configuration;
using COM3D2.Lilly.Plugin;
using HarmonyLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BepInPluginSample
{
    public class MyWindowRect
    {
        private float windowSpace;
        private Rect windowRect;
        private Size windowRectOpen;
        private Size windowRectClose;
        private Position position;
        private string jsonPath;

        private static Harmony harmony;
        private static event Action actionSave;

        private ConfigEntry<bool> isOpen;

        public bool IsOpen
        {
            get => isOpen.Value;
            set
            {
                if (isOpen.Value = value)
                {
                    windowRect.width = windowRectOpen.w;
                    windowRect.height = windowRectOpen.h;
                    windowRect.x -= windowRectOpen.w - windowRectClose.w;
                }
                else
                {
                    windowRect.width = windowRectClose.w;
                    windowRect.height = windowRectClose.h;
                    windowRect.x += windowRectOpen.w - windowRectClose.w;
                }
            }
        }

        private static event Action<int, int> actionScreen;


        private static int widthbak;
        private static int heightbak;


        struct Position
        {
            public float x;
            public float y;

            public Position(float x, float y) : this()
            {
                this.x = x;
                this.y = y;
            }
        }

        struct Size
        {
            public float w;
            public float h;

            public Size(float w, float h) : this()
            {
                this.w = w;
                this.h = h;
            }
        }

        public Rect WindowRect
        {
            get
            {
                // 윈도우 리사이즈시 밖으로 나가버리는거 방지
                windowRect.x = Mathf.Clamp(windowRect.x, -windowRect.width + windowSpace, Screen.width - windowSpace);
                windowRect.y = Mathf.Clamp(windowRect.y, -windowRect.height + windowSpace, Screen.height - windowSpace);
                return windowRect;
            }
            set => windowRect = value;
        }

        public float Height { get => windowRect.height; set => windowRect.height = value; }
        public float Width { get => windowRect.width; set => windowRect.width = value; }
        public float X { get => windowRect.x; set => windowRect.x = value; }
        public float Y { get => windowRect.y; set => windowRect.y = value; }

        public MyWindowRect(ConfigFile config, string fileName, float wc = 200f, float wo = 300f, float hc = 32f, float ho = 600f, float x = 32f, float y = 32f, float windowSpace = 32f)
        {
            jsonPath = Path.GetDirectoryName(config.ConfigFilePath) + $@"\{fileName}-rect.json";

            this.windowSpace = windowSpace;
            windowRect = new Rect(x, y, wo, ho);
            windowRectOpen = new Size(wo, ho);
            windowRectClose = new Size(wc, hc);
            isOpen = config.Bind("GUI", "isOpen", true);
            IsOpen = isOpen.Value;

            if (harmony == null)
            {
                harmony = Harmony.CreateAndPatchAll(typeof(MyWindowRect));
                widthbak = Screen.width;
                heightbak = Screen.height;
            }
            actionSave += save;
            actionScreen += ScreenChg;

        }

        public void load()
        {
            if (File.Exists(jsonPath))
            {
                position = JsonConvert.DeserializeObject<Position>(File.ReadAllText(jsonPath));
            }
            else
            {
                position = new Position(windowSpace, windowSpace);
                File.WriteAllText(jsonPath, JsonConvert.SerializeObject(position, Formatting.Indented)); // 자동 들여쓰기
            }
            windowRect.x = position.x;
            windowRect.y = position.y;
        }

        public void save()
        {
            position.x = windowRect.x;
            position.y = windowRect.y;
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(position, Formatting.Indented)); // 자동 들여쓰기
        }


        [HarmonyPatch(typeof(GameMain), "LoadScene")]
        [HarmonyPostfix]
        public static void LoadScene()
        {
            actionSave();
        }


        public void ScreenChg(int width, int height)
        {
            if (windowRect.x > widthbak / 2)
            {
                windowRect.x += width - widthbak;
            }
            if (windowRect.y > heightbak / 2)
            {
                windowRect.y += height - heightbak;
            }
            //MyLog.LogMessage("SetResolution3", widthbak, heightbak, Screen.fullScreen);
            //MyLog.LogMessage("SetResolution4", Screen.width, Screen.height, Screen.fullScreen);
            //MyLog.LogMessage("SetResolution5", windowRect.x, windowRect.y);
        }

        [HarmonyPatch(typeof(Screen), "SetResolution", typeof(int), typeof(int), typeof(bool))]
        [HarmonyPostfix]
        public static void SetResolutionPost(int width, int height, bool fullscreen)
        {
            actionScreen(width, height);
            widthbak = width;
            heightbak = height;
            //MyLog.LogMessage("SetResolution");
        }
    }
}
