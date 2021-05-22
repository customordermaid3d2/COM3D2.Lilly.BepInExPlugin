using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    [BepInPlugin("COM3D2.LullySub.Plugin", "COM3D2.LullySub.Plugin", "21.5.22")]// 버전 규칙 잇음. 반드시 2~4개의 숫자구성으로 해야함. 미준수시 못읽어들임
    [BepInProcess("COM3D2x64.exe")]
    class LullySub : BaseUnityPlugin
    {
        public void Awake()
        {
            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime dateTime = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            MyLog.LogMessage("LullySub.Awake", dateTime.ToString("u"));
            MyLog.LogMessage("LullySub", string.Format("{0:0.000} ", Lilly.stopwatch.Elapsed.ToString()));

        }
    }
}
