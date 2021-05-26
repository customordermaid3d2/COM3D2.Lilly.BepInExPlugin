using COM3D2.Lilly.Plugin.PatchInfo;
using COM3D2.Lilly.Plugin.ToolPatch;
using COM3D2.Lilly.Plugin.Utill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using wf;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
    public class GUIFlagPlayer : GUIMgr
    {
        static GUILayoutOptionUtill guio = GUILayoutOptionUtill.Instance;
        public static string flag = string.Empty;
        public static string flagVs = string.Empty;

        public static int flagV = 1;



        public static Maid maid;

        public override void SetBody()
        {
            //if (GameMain.Instance?.CharacterMgr?.status?.flags==null)
            //    return;

            // base.SetBody();
            GUI.enabled = true;
            if (GUILayout.Button("reflash"))
            {
                StatusPatch.flagsPlayer = GameMain.Instance.CharacterMgr.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);                
            }
            GUILayout.Label("플레그 추가");
            GUILayout.BeginHorizontal();
            flag = GUILayout.TextField(flag);
            flagVs = GUILayout.TextField(flagV.ToString());
            if (GUI.changed)
            {
                if (!int.TryParse(flagVs, out flagV))
                    flagV = 1;
            }
            if (GUILayout.Button("add", guio[GUILayoutOptionUtill.Type.Width, 40]))
            {
                if (!string.IsNullOrEmpty(flag))
                {
                    GameMain.Instance.CharacterMgr.status.AddFlag(flag, flagV);
                    StatusPatch.flagsPlayer = GameMain.Instance.CharacterMgr.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("보유한 플레그 목록");
            foreach (var item in StatusPatch.flagsPlayer)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(item.Key);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("a", guio[GUILayoutOptionUtill.Type.Width, 20]))
                {
                    MyLog.LogMessage("add flag", item.Key);
                    GameMain.Instance.CharacterMgr.status.AddFlag(item.Value, 1);
                    StatusPatch.flagsPlayer = GameMain.Instance.CharacterMgr.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                }
                if (GUILayout.Button("0", guio[GUILayoutOptionUtill.Type.Width, 20]))
                {
                    MyLog.LogMessage("Set flag", item.Key);
                    GameMain.Instance.CharacterMgr.status.SetFlag(item.Value, 0);
                    StatusPatch.flagsPlayer = GameMain.Instance.CharacterMgr.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                }
                if (GUILayout.Button("d", guio[GUILayoutOptionUtill.Type.Width, 20]))
                {
                    MyLog.LogMessage("del flag", item.Key);
                    GameMain.Instance.CharacterMgr.status.RemoveFlag(item.Value);
                    StatusPatch.flagsPlayer = GameMain.Instance.CharacterMgr.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.Label("경고! 모든 플레그 삭제");
            GUILayout.BeginHorizontal();
            GUILayout.Label("경고! 모든 플레그 삭제=>");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("del", guio[GUILayoutOptionUtill.Type.Width, 40]))
            {
                foreach (var item in StatusPatch.flagsPlayer)
                {
                    MyLog.LogMessage("del flag", item.Key);
                    GameMain.Instance.CharacterMgr.status.RemoveFlag(item.Value);
                    StatusPatch.flagsPlayer = GameMain.Instance.CharacterMgr.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                }
            }
            GUILayout.EndHorizontal();

            GUI.enabled = true;
        }


    }
}
