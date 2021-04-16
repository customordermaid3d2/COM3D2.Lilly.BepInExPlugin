using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    //[MyHarmony(MyHarmonyType.Base)]
    public class GameUtyPatch
    {
        public static FileSystemWindows m_aryModOnlysMenuFiles;

        //[HarmonyPatch(typeof(GameUty), MethodType.Constructor), HarmonyPostfix]
        public static void GameUtyCtor(FileSystemWindows ___m_aryModOnlysMenuFiles) 
        {
            m_aryModOnlysMenuFiles = ___m_aryModOnlysMenuFiles;
        }
    }
}
