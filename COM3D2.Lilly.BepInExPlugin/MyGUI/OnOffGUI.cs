using BepInEx.Configuration;
using COM3D2.Lilly.Plugin.Utill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.MyGUI
{
    public class OnOffGUI : GUIVirtual
    {
        public OnOffGUI() : base("OnOffGUI")
        {
        }

        public override void SetButtonList()
        {
            GUILayout.Label("ConfigEntryUtill : "+ ConfigEntryUtill.listAll.Count);
            // ConfigEntryUtill
            foreach (var kp in ConfigEntryUtill.listAll)
            {
                GUILayout.Label(kp.Key + " : " + kp.Value.list.Count);
                foreach (var item in kp.Value.list)
                {
                    if (GUILayout.Button(item.Key+":"+ item.Value.Value)) item.Value.Value=!item.Value.Value;
                }
            }
        }
    }
}
