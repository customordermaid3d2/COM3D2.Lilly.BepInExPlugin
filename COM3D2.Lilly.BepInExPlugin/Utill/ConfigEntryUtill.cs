using BepInEx.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Utill
{

    public class ConfigEntryUtill :  IEnumerator, IEnumerable
    {
        public static ConfigFile customFile;
        public static Dictionary<string, ConfigEntryUtill> listAll = new Dictionary<string, ConfigEntryUtill>();
        public Dictionary<string, ConfigEntry<bool>> list = new Dictionary<string, ConfigEntry<bool>>();
        public string section;

        public List<string> kyes=new List<string>();


        public ConfigEntryUtill(string section):base()
        {
            MyLog.LogDebug("ConfigEntryUtill.ctor", section);
            this.section = section;
            listAll.Add(section, this);
        }
        
        public ConfigEntryUtill(string section,params string[] keys) : base()
        {
            MyLog.LogDebug("ConfigEntryUtill.ctor", section, keys.Length);
            this.section = section;
            listAll.Add(section, this);
            //foreach (var key in keys)
            //{
            //    Add(key, true);
            //}
            kyes.AddRange(keys);//나중에 처리하자            
            //Lilly.actionsAwake += Awake;
            Awake();
        }

        public static ConfigEntryUtill Create(string section, params string[] keys)
        {
            MyLog.LogDebug("ConfigEntryUtill.Create", section, keys.Length);
            return new ConfigEntryUtill(section, keys);
        }

        public static bool Get(string section, string key)
        {
            if (!listAll.ContainsKey(section))
            {
                listAll.Add(section, new ConfigEntryUtill(section));
            }
            return listAll[section][key];
        }

        public  void Awake()
        {
            MyLog.LogMessage("ConfigEntryUtill.Awake", section, kyes.Count);
            foreach (var item in kyes)
            {
                MyLog.LogDebug("ConfigEntryUtill.Awake", section, item);
                if (!list.ContainsKey(item))
                {
                    Add(item, true);
                }
            }
            MyLog.LogMessage("ConfigEntryUtill.Awake", section, list.Count);
        }
        /*
        public static bool this[string section,string key] {
            get
            {
                if (!listAll.ContainsKey(section))
                {
                    listAll.Add(section, new ConfigEntryUtill(section));
                }                
                if (!list.ContainsKey(key))
                {
                    Add(key, true);
                }
                return list[key].Value;
            }
            set
            {

                if (!list.ContainsKey(key))
                {
                    Add(key, true);
                }
                list[key].Value = value;
            }
        }
        */
        public bool this[string key] {
            get
            {
                if (!list.ContainsKey(key))
                {
                    Add(key, true);
                }
                return list[key].Value;
            }
            set
            {
                if (!list.ContainsKey(key))
                {
                    Add(key, true);
                }
                list[key].Value = value;
            }
        }

        public bool this[int key] {
            get
            {
                return list.ElementAt(key).Value.Value;
            }
            set
            {
                list.ElementAt(key).Value.Value = value;
            }
        }

        public void Add(string key, bool defaultValue, string description = null)
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
        object IEnumerator.Current =>   list.ElementAt(position);
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
            MyLog.LogDebug("ConfigEntryUtill.ctor", section);

            this.section = section;
            this.defult = defult;

            listAll.Add(section, this);
        }

        public ConfigEntryUtill(string section, T defult,params string[] keys) : base()
        {
            MyLog.LogDebug("ConfigEntryUtill.ctor", section, keys.Length);

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
            MyLog.LogDebug("ConfigEntryUtill.Create", section, keys.Length);
            return new ConfigEntryUtill<T>(section,defult, keys);
        }
        /*
        public static T Get(string section, string key)
        {
            if (!listAll.ContainsKey(section))
            {
                listAll.Add(section, new ConfigEntryUtill<T>(section, null));
            }
            return listAll[section][key];
        }
        */

        public void Awake()
        {
            MyLog.LogMessage("ConfigEntryUtill.Awake", section, kyes.Count);
            foreach (var item in kyes)
            {
                MyLog.LogDebug("ConfigEntryUtill.Awake", section, item);
                if (!list.ContainsKey(item))
                {
                    Add(item, defult);
                }
            }
            MyLog.LogMessage("ConfigEntryUtill.Awake", section, list.Count);
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
