using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.MyGUI
{
    public class PresetGUI : GUIVirtual
    {
        public PresetGUI()
        {
            name = "EasyUtill";
        }

        public override void SetButtonList()
        {
            GUILayout.Label("now scene.name : " + Lilly.scene.name);

            PresetUtill.SetButtonList();


            GUI.enabled = true;
            //if (GUILayout.Button("mod reflash")) modreflash();
        }


    }
}
