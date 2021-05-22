using UnityEngine;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
    public class GUIPreset : GUIMgr
    {
        public GUIPreset()
        {
            nameGUI = "PresetGUI";
        }

        public override void SetBody()
        {
            GUILayout.Label("now scene.name : " + Lilly.scene.name);

            PresetUtill.SetButtonList();

            GUI.enabled = true;
            //if (GUILayout.Button("mod reflash")) modreflash();
        }
    }
}
