using UnityEngine;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
#if PresetUtill

    public class GUIRndPreset : GUIMgr
    {
        public GUIRndPreset()
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

#endif

}
