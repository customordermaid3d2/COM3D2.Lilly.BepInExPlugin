using BepInEx.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Utill
{
    /// <summary>
    ///     public static ConfigEntryUtill configEntryUtill = ConfigEntryUtill.Create(
    /// "CameraMainPatch"
    /// , "FadeIn"
    /// , "FadeOut"
    /// , "FadeInNoUI"
    /// , "FadeOutNoUI"
    /// );
    ///         if(configEntryUtill["FadeIn"])
    /// </summary>
    public class ConfigEntryUtill : IEnumerator, IEnumerable
    {

        public static ConfigFile customFile;

        public static Dictionary<string, ConfigEntryUtill> SectionList { get; } = new Dictionary<string, ConfigEntryUtill>();

        public Dictionary<string, ConfigEntry<bool>> KeyList { get; } = new Dictionary<string, ConfigEntry<bool>>();

        private static readonly string sectionMain = "ConfigEntryUtill";
        private readonly string section;

        private ConfigEntryUtill() : base()
        {
            //MyLog.LogDebug("ConfigEntryUtill.ctor", sectionMain);
            SectionList.Add(sectionMain, this);
        }

        private ConfigEntryUtill(string section) : base()
        {
            //MyLog.LogDebug("ConfigEntryUtill.ctor", section);
            this.section = section;
            SectionList.Add(section, this);
        }

        private ConfigEntryUtill(string section, params string[] keys) : base()
        {
            //MyLog.LogDebug("ConfigEntryUtill.ctor", section, keys.Length);
            this.section = section;
            SectionList.Add(section, this);
            Add(keys.ToList());
        }

        public static ConfigEntryUtill Create()
        {
            if (!SectionList.ContainsKey(sectionMain))
                return new ConfigEntryUtill();
            else
                return SectionList[sectionMain];
        }

        public static ConfigEntryUtill Create(string section)
        {
            //MyLog.LogDebug("ConfigEntryUtill.Create", section);
            if (!SectionList.ContainsKey(section))
                return new ConfigEntryUtill(section);
            else
                return SectionList[section];
        }
        public static ConfigEntryUtill Create(string section, params string[] keys)
        {
            //MyLog.LogDebug("ConfigEntryUtill.Create", section, keys.Length);
            if (!SectionList.ContainsKey(section))
                return new ConfigEntryUtill(section, keys);
            else
            {
                SectionList[section].Add(keys.ToList());
                return SectionList[section];
            }
        }

        public bool this[string section, string key, bool defaultValue = true] {
            get => Create(section)[key, defaultValue];
            set => Create(section)[key, defaultValue] = value;
        }

        internal static void init()
        {
            ConfigEntryUtill.customFile = Lilly.customFile;
        }

        internal static void init(ConfigFile customFile)
        {
            ConfigEntryUtill.customFile = customFile;
        }

        public bool this[string key, bool defaultValue = true] {
            get
            {
                if (!KeyList.ContainsKey(key))
                    Add(key, defaultValue);
                return KeyList[key].Value;
            }
            set
            {
                if (!KeyList.ContainsKey(key))
                    Add(key, defaultValue);
                KeyList[key].Value = value;
            }
        }

        public bool this[int key] {
            get => KeyList.ElementAt(key).Value.Value;
            set => KeyList.ElementAt(key).Value.Value = value;
        }

        public void Add(List<string> kyes, bool defaultValue = true)
        {
            //MyLog.LogMessage("ConfigEntryUtill.Awake", section, kyes.Count);
            foreach (var item in kyes)
            {
                //MyLog.LogDebug("ConfigEntryUtill.Awake", section, item);
                if (!KeyList.ContainsKey(item))
                {
                    Add(item, defaultValue);
                }
            }
            //MyLog.LogMessage("ConfigEntryUtill.Awake", section, KeyList.Count);
        }

        public void Add(string key, bool defaultValue = true, string description = null)
        {
            KeyList.Add(
                key,
                customFile.Bind(
                    section,
                    key
                    , defaultValue
                )
            );
        }

        int position = -1;

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        //IEnumerator
        public bool MoveNext()
        {
            position++;
            return (position < KeyList.Count);
        }

        //IEnumerable
        public void Reset()
        {
            position = 0;
        }

        //IEnumerable
        object IEnumerator.Current => KeyList.ElementAt(position);


    }



    public class ConfigEntryUtill<T> : IEnumerator, IEnumerable
    {
        public static ConfigFile customFile;
        public static Dictionary<string, ConfigEntryUtill<T>> listAll = new Dictionary<string, ConfigEntryUtill<T>>();
        public Dictionary<string, ConfigEntry<T>> list = new Dictionary<string, ConfigEntry<T>>();
        public string section;
        public T defult;

        public List<string> kyes = new List<string>();


        public ConfigEntryUtill(string section, T defult) : base()
        {
            //MyLog.LogDebug("ConfigEntryUtill.ctor", section);

            this.section = section;
            this.defult = defult;

            listAll.Add(section, this);
        }

        public ConfigEntryUtill(string section, T defult, params string[] keys) : base()
        {
            //MyLog.LogDebug("ConfigEntryUtill.ctor", section, keys.Length);

            this.section = section;
            this.defult = defult;

            listAll.Add(section, this);
            //foreach (var key in keys)
            //{
            //    Add(key, true);
            //}
            kyes.AddRange(keys);//나중에 처리하자            
            //Lilly.actionsAwake += Awake;
            Awake();
        }

        public static ConfigEntryUtill<T> Create(string section, T defult, params string[] keys)
        {
            //MyLog.LogDebug("ConfigEntryUtill.Create", section, keys.Length);
            return new ConfigEntryUtill<T>(section, defult, keys);
        }

        public void Awake()
        {
            //MyLog.LogMessage("ConfigEntryUtill.Awake", section, kyes.Count);
            foreach (var item in kyes)
            {
                //MyLog.LogDebug("ConfigEntryUtill.Awake", section, item);
                if (!list.ContainsKey(item))
                {
                    Add(item, defult);
                }
            }
            //MyLog.LogMessage("ConfigEntryUtill.Awake", section, list.Count);
        }

        public T this[string key] {
            get
            {
                if (!list.ContainsKey(key))
                {
                    Add(key, defult);
                }
                return list[key].Value;
            }
            set
            {
                if (!list.ContainsKey(key))
                {
                    Add(key, defult);
                }
                list[key].Value = value;
            }
        }

        public T this[int key] {
            get
            {
                return list.ElementAt(key).Value.Value;
            }
            set
            {
                list.ElementAt(key).Value.Value = value;
            }
        }

        public void Add(string key, T defaultValue, string description = null)
        {
            list.Add(
                key,
                customFile.Bind(
                    section,
                    key,
                    defaultValue
                )
            );
        }

        int position = -1;

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        //IEnumerator
        public bool MoveNext()
        {
            position++;
            return (position < list.Count);
        }

        //IEnumerable
        public void Reset()
        {
            position = 0;
        }

        //IEnumerable
        object IEnumerator.Current => list.ElementAt(position);
    }
}
