using BepInEx.Configuration;
using COM3D2.Lilly.Plugin.Utill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
    public class GUIOnOff : GUIMgr
    {
        public GUIOnOff() : base("OnOffGUI")
        {
        }

        public override void SetBody()
        {
            GUILayout.Label("ConfigEntryUtill : "+ ConfigEntryUtill.SectionList.Count);
            // ConfigEntryUtill
            foreach (var kp in ConfigEntryUtill.SectionList)
            {
                GUILayout.Label(kp.Key + " : " + kp.Value.KeyList.Count);
                foreach (var item in kp.Value.KeyList)
                {
                    if (GUILayout.Button(item.Key+":"+ item.Value.Value)) item.Value.Value=!item.Value.Value;
                }
            }
        }
    }
}
