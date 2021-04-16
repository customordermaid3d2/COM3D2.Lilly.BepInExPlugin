using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyHarmony : Attribute
    {
        public MyHarmonyType type;
        public static List<Type> infoList = new List<Type>();
        public static List<Type> baseList = new List<Type>();
        public static List<Type> toolList = new List<Type>();

        public MyHarmony(MyHarmonyType type)
        {
            this.type = type;
            switch (type)
            {
                case MyHarmonyType.Base:
                    break;
                case MyHarmonyType.Info:
                    break;
                case MyHarmonyType.Tool:
                    break;
                default:
                    break;
            }
        }
    }

    public enum MyHarmonyType
    {
        Base
        ,Info
        ,Tool
    }
}
