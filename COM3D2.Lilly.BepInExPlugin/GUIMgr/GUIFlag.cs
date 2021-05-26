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
    public class GUIFlag : GUIMgr
    {
        static GUILayoutOptionUtill guio = GUILayoutOptionUtill.Instance;
        public static string flag = string.Empty;
        public static string flagVs = string.Empty;
        public static string[] typesAll = new string[] { "new", "old" };
        public static string[] typesone = new string[] { "new" };
        public static string[] types = new string[] { "new", "old" };
        public static int flagV = 1;
        public static int type = 0;
        public static event Action a = bodyFlag;

        public static Maid maid;

        public override void SetBody()
        {
            // base.SetBody();
            GUI.enabled = Lilly.scene.name == "SceneMaidManagement";
            if (!GUI.enabled)
            {
                GUILayout.Label("메이드 관리에서 사용 SceneMaidManagement");
                return;
            }
            if (maid!= MaidManagementMainPatch.select_maid)
            {
                maid = MaidManagementMainPatch.select_maid;
                if (maid.status.OldStatus == null)
                {
                    a = bodyFlag;
                    types = typesone;
                    type = 0;
                }
                else
                {
                    types = typesAll;
                }

            }

            GUILayout.Label(MyUtill.GetMaidFullName(maid));

            type = GUILayout.SelectionGrid(type, types, 2);

            if (GUI.changed)
            {
                switch (type)
                {
                    case 0:
                        a = bodyFlag;
                        break;
                    case 1:
                        a = bodyFlagOld;
                        break;
                    default:
                        break;
                }
            }

            a();

            GUI.enabled = true;
        }

        private static void bodyFlag()
        {
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
                    maid.status.AddFlag(flag, flagV);
                    MaidManagementMainPatch.flags = maid.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("보유한 플레그 목록");
            foreach (var item in MaidManagementMainPatch.flags)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(item.Key);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("del", guio[GUILayoutOptionUtill.Type.Width, 40]))
                {
                    MyLog.LogMessage("del flag" , item.Key);
                    maid.status.RemoveFlag(item.Value);
                    MaidManagementMainPatch.flags = maid.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.Label("경고! 모든 플레그 삭제");
            GUILayout.BeginHorizontal();
            GUILayout.Label("경고! 모든 플레그 삭제");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("del", guio[GUILayoutOptionUtill.Type.Width, 40]))
            {
                maid.status.RemoveFlagAll();
                MaidManagementMainPatch.flags = maid.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
            }
            GUILayout.EndHorizontal();
        }


        private static void bodyFlagOld()
        {
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
                if (string.IsNullOrEmpty(flag))
                {
                    maid.status.OldStatus.AddFlag(flag, flagV);
                    MaidManagementMainPatch.flagsOld = maid.status.OldStatus.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("보유한 플레그 목록");
            foreach (var item in MaidManagementMainPatch.flagsOld)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(item.Key);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("del", guio[GUILayoutOptionUtill.Type.Width, 40]))
                {
                    maid.status.OldStatus.RemoveFlag(item.Value);
                    MaidManagementMainPatch.flagsOld = maid.status.OldStatus.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.Label("경고! 모든 플레그 삭제");
            GUILayout.BeginHorizontal();
            GUILayout.Label("경고! 모든 플레그 삭제");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("del", guio[GUILayoutOptionUtill.Type.Width, 40]))
            {
                foreach (var item in MaidManagementMainPatch.flagsOld)
                {
                    maid.status.OldStatus.RemoveFlag(item.Value);
                }
                MaidManagementMainPatch.flagsOld = maid.status.OldStatus.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
            }
            GUILayout.EndHorizontal();

        }
    }
}
