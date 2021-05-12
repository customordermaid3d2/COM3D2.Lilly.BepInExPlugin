using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Utill
{
    
    public class AwakeUtill 
    {        
        public static ConfigFile customFile;

        public AwakeUtill()
        {            
            MyLog.LogDebug("AwakeUtill", "ctor", GetType().Name);
            Lilly.actionsAwake += Awake;
            Lilly.actionsInit += init;
        }

        public virtual void init()
        {
            MyLog.LogDebug("AwakeUtill", "init", GetType().Name);
        }

        public virtual void Awake()
        {
            MyLog.LogDebug("AwakeUtill", "Awake", GetType().Name);
        }
    }
    
}
