using BepInEx.Configuration;
using COM3D2.Lilly.Plugin.Utill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.MyGUI
{
    public class OnOffGUI : GUIVirtualMgr
    {
        public OnOffGUI() : base("OnOffGUI")
        {
        }

        public override void SetButtonList()
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
