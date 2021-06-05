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
    public class ConfigEntryUtill : IEnumerator<KeyValuePair<string, ConfigEntry<bool>>>, IEnumerable<KeyValuePair<string, ConfigEntry<bool>>>
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

        #region IEnumerator

        int position = -1;

        public KeyValuePair<string, ConfigEntry<bool>> Current => KeyList.ElementAt(position);

        object IEnumerator.Current => KeyList.ElementAt(position);

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            position++;
            return (position < KeyList.Count);
        }

        public void Reset()
        {
            position = -1;
        }


        #endregion

        #region IEnumerable

        public IEnumerator<KeyValuePair<string, ConfigEntry<bool>>> GetEnumerator()
        {
            for (int i = 0; i < KeyList.Count; i++)
            {
                yield return KeyList.ElementAt(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < KeyList.Count; i++)
            {
                yield return KeyList.ElementAt(i);
            }
        }

        #endregion
    }

    public class ConfigEntryUtill<T> : IEnumerator<KeyValuePair<string, ConfigEntry<T>>>, IEnumerable<KeyValuePair<string, ConfigEntry<T>>>
    {

        public static ConfigFile customFile;

        public static Dictionary<string, ConfigEntryUtill<T>> SectionList { get; } = new Dictionary<string, ConfigEntryUtill<T>>();

        public Dictionary<string, ConfigEntry<T>> KeyList { get; } = new Dictionary<string, ConfigEntry<T>>();

        private static readonly string sectionMain = "ConfigEntryUtill";
        private readonly string section;
        private T defaultValue;
        internal static void init(ConfigFile customFile)
        {
            ConfigEntryUtill<T>.customFile = customFile;
        }

        private ConfigEntryUtill() : base()
        {
            //MyLog.LogDebug("ConfigEntryUtill.ctor", sectionMain);
            SectionList.Add(sectionMain, this);
        }

        private ConfigEntryUtill(string section, T defaultValue) : base()
        {
            //MyLog.LogDebug("ConfigEntryUtill.ctor", section);
            this.section = section;
            this.defaultValue = defaultValue;
            SectionList.Add(section, this);
        }

        private ConfigEntryUtill(string section, T defaultValue, params string[] keys) : base()
        {
            //MyLog.LogDebug("ConfigEntryUtill.ctor", section, keys.Length);
            this.section = section;
            this.defaultValue = defaultValue;
            SectionList.Add(section, this);
            Add(keys.ToList(), defaultValue);
        }

        public static ConfigEntryUtill<T> Create()
        {
            if (!SectionList.ContainsKey(sectionMain))
                return new ConfigEntryUtill<T>();
            else
                return SectionList[sectionMain];
        }

        public static ConfigEntryUtill<T> Create(string section, T defaultValue)
        {
            //MyLog.LogDebug("ConfigEntryUtill.Create", section);
            if (!SectionList.ContainsKey(section))
                return new ConfigEntryUtill<T>(section, defaultValue);
            else
                return SectionList[section];
        }
        public static ConfigEntryUtill<T> Create(string section, T defaultValue, params string[] keys)
        {
            //MyLog.LogDebug("ConfigEntryUtill.Create", section, keys.Length);
            if (!SectionList.ContainsKey(section))
                return new ConfigEntryUtill<T>(section, defaultValue, keys);
            else
            {
                SectionList[section].Add(keys.ToList(), defaultValue);
                return SectionList[section];
            }
        }

        public T this[string section, string key, T defaultValue] {
            get => Create(section, defaultValue)[key, defaultValue];
            set => Create(section, defaultValue)[key, defaultValue] = value;
        }

        public T this[string section, string key] {
            get => Create(section, defaultValue)[key];
            set => Create(section, defaultValue)[key] = value;
        }

        public T this[string key, T defaultValue] {
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


        public T this[string key] {
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

        public T this[int key] {
            get => KeyList.ElementAt(key).Value.Value;
            set => KeyList.ElementAt(key).Value.Value = value;
        }

        public void Add(List<string> kyes, T defaultValue)
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

        public void Add(string key, T defaultValue, string description = null)
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

        #region IEnumerator

        int position = -1;

        public KeyValuePair<string, ConfigEntry<T>> Current => KeyList.ElementAt(position);

        object IEnumerator.Current => KeyList.ElementAt(position);

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            position++;
            return (position < KeyList.Count);
        }

        public void Reset()
        {
            position = -1;
        }


        #endregion

        #region IEnumerable

        public IEnumerator<KeyValuePair<string, ConfigEntry<T>>> GetEnumerator()
        {
            for (int i = 0; i < KeyList.Count; i++)
            {
                yield return KeyList.ElementAt(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < KeyList.Count; i++)
            {
                yield return KeyList.ElementAt(i);
            }
        }

        #endregion
    }


}
