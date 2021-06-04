using COM3D2.Lilly.Plugin.PatchBase;
using COM3D2.Lilly.Plugin.PatchInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
    public class GUIScript : GUIMgr
    {
        public static string scenario_str = string.Empty;
        public static string label_name = string.Empty;
        public static string loadScene = string.Empty;

        public override void ActionsStart()
        {

        }

        public override void SetBody()
        {
            GUILayout.Label("=== 장면 이동 실행 ===");

            loadScene = GUILayout.TextField(loadScene);
            if (GUILayout.Button("LoadScene"))
            {
                if (!string.IsNullOrEmpty(loadScene))
                {
                    GameMain.Instance.LoadScene(loadScene);
                }
            }
            if (GUILayout.Button("SceneToTitle")) GameMain.Instance.LoadScene("SceneToTitle");
            //if (GUILayout.Button("SceneDaily")) KagPatch.SceneToTitle();

            GUILayout.Label("=== 세이브 로드창 ===");
            if (GUILayout.Button("OpenLoadPanel")) BasePanelMgrPatch.OpenLoadPanel();
            if (GUILayout.Button("OpenSavePanel")) BasePanelMgrPatch.OpenSavePanel();

            GUILayout.Label("=== 스크립트 실행기 ===");

            GUILayout.Label("scenario_str ");
            scenario_str = GUILayout.TextField(scenario_str);
            GUILayout.Label("label_name ");
            label_name = GUILayout.TextField(label_name);
            if (GUILayout.Button("LoadAdvScenarioScript"))
            {
                if (!string.IsNullOrEmpty(scenario_str)  && !string.IsNullOrEmpty(label_name)  )
                    KagPatch.LoadAdvScenarioScript(scenario_str, label_name);
            }
            if (GUILayout.Button("LoadScenarioStringGoToLabel"))
            {
                if (!string.IsNullOrEmpty(scenario_str) && !string.IsNullOrEmpty(label_name))
                    KagPatch.LoadScenarioStringGoToLabel(scenario_str, label_name);
            }


            GUI.enabled = true;
        }


    }
}
