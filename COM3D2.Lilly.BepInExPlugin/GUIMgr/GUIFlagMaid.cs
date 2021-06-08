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
#if FlagMaid

    public class GUIFlagMaid : GUIMgr
    {
        static GUILayoutOptionUtill guio = GUILayoutOptionUtill.Instance;

        private static readonly string[] typesAll = new string[] { "new", "old" };
        private static readonly string[] typesone = new string[] { "new" };
        private static string[] types = new string[] { "new", "old" };

        private static string flagName = string.Empty;
        private static string flagValueS = string.Empty;

        private static int flagValue = 1;
        private static int type = 0;

        private static int selectedFlag;

        private static event Action action = SetBodyFlag;

        private static Maid maid;
        //private static int selectedFlagold;

        /// <summary>
        /// Key : maid.flags.key + maid.flags.value
        /// value : maid.flags.key
        /// </summary>
        public static Dictionary<string, string> flags;
        //public static Dictionary<string, string> flagsOld = new Dictionary<string, string>();

        public static string[] flagsStats = new string[] { };
        //public static string[] flagsOldStats;

        public static string[] flagsKey = new string[] { };
        //public static string[] flagsOldKey;


        public override void SetBody()
        {
            // base.SetBody();
            GUI.enabled = Lilly.scene.name == "SceneMaidManagement";
            if (!GUI.enabled)
            {
                GUILayout.Label("메이드 관리에서 사용 SceneMaidManagement");
                return;
            }

            GUILayout.Label(MyUtill.GetMaidFullName(maid));

            type = GUILayout.SelectionGrid(type, types, 2);

            if (GUI.changed)
            {
                GUIFlagMaid.SetingFlag(maid);
            }

            action();

            GUI.enabled = true;
        }

        private static void SetBodyFlag()
        {
            GUILayout.Label("플레그 추가");

            GUILayout.BeginHorizontal();
            flagName = GUILayout.TextField(flagName);
            flagValueS = GUILayout.TextField(flagValue.ToString("D"));
            if (GUI.changed)
            {
                int.TryParse(flagValueS, out flagValue);
            }
            if (GUILayout.Button("Set", guio[GUILayoutOptionUtill.Type.Width, 40]))
            {
                if (!string.IsNullOrEmpty(flagName))
                {
                    maid.status.SetFlag(flagName, flagValue);
                    GUIFlagMaid.SetingFlag(maid);
                }
            }
            GUILayout.EndHorizontal();



            GUILayout.Label("edit seleted flag ");

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("add"))//, guio[GUILayoutOptionUtill.Type.Width, 20]
            {
                maid.status.AddFlag(flagsKey[selectedFlag], 1);
                GUIFlagMaid.SetingFlag(maid);
            }
            if (GUILayout.Button("set 0"))//, guio[GUILayoutOptionUtill.Type.Width, 20]
            {
                maid.status.SetFlag(flagsKey[selectedFlag], 0);
                GUIFlagMaid.SetingFlag(maid);
            }
            if (GUILayout.Button("del"))//, guio[GUILayoutOptionUtill.Type.Width, 20]
            {
                maid.status.RemoveFlag(flagsKey[selectedFlag]);
                GUIFlagMaid.SetingFlag(maid);
            }

            GUILayout.EndHorizontal();



            GUILayout.Label("보유한 플레그 목록 " + flags.Count);

            selectedFlag = GUILayout.SelectionGrid(selectedFlag, flagsStats, 1);

            GUILayout.Label("경고! 모든 플레그 삭제");
            GUILayout.BeginHorizontal();
            GUILayout.Label("경고! 모든 플레그 삭제=>");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("del", guio[GUILayoutOptionUtill.Type.Width, 40]))
            {
                maid.status.RemoveFlagAll();
                GUIFlagMaid.SetingFlag(maid);
            }
            GUILayout.EndHorizontal();
        }


        private static void SetBodyFlagOld()
        {
            GUILayout.Label("플레그 추가");
            GUILayout.BeginHorizontal();
            flagName = GUILayout.TextField(flagName);
            flagValueS = GUILayout.TextField(flagValue.ToString());
            if (GUI.changed)
            {
                if (!int.TryParse(flagValueS, out flagValue))
                    flagValue = 1;
            }
            if (GUILayout.Button("Set", guio[GUILayoutOptionUtill.Type.Width, 40]))
            {
                if (string.IsNullOrEmpty(flagName))
                {
                    maid.status.OldStatus.SetFlag(flagName, flagValue);
                    GUIFlagMaid.SetingFlag(maid);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("edit seleted flag ");

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("add"))//, guio[GUILayoutOptionUtill.Type.Width, 20]
            {
                maid.status.OldStatus.AddFlag(flagsKey[selectedFlag], 1);
                GUIFlagMaid.SetingFlag(maid);
            }
            if (GUILayout.Button("set 0"))//, guio[GUILayoutOptionUtill.Type.Width, 20]
            {
                maid.status.OldStatus.SetFlag(flagsKey[selectedFlag], 0);
                GUIFlagMaid.SetingFlag(maid);
            }
            if (GUILayout.Button("del"))//, guio[GUILayoutOptionUtill.Type.Width, 20]
            {
                maid.status.OldStatus.RemoveFlag(flagsKey[selectedFlag]);
                GUIFlagMaid.SetingFlag(maid);
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("보유한 플레그 목록");

            selectedFlag = GUILayout.SelectionGrid(selectedFlag, flagsStats, 1);

            GUILayout.Label("경고! 모든 플레그 삭제");
            GUILayout.BeginHorizontal();
            GUILayout.Label("경고! 모든 플레그 삭제=>");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("del", guio[GUILayoutOptionUtill.Type.Width, 40]))
            {
                foreach (var item in flags)
                {
                    maid.status.OldStatus.RemoveFlag(item.Value);
                }
                GUIFlagMaid.SetingFlag(maid);
            }
            GUILayout.EndHorizontal();

        }


        public static void SetingFlag(Maid maid)
        {
            GUIFlagMaid.maid = maid;
            if (maid.status.OldStatus == null)
            {
                types = typesone;
                type = 0;
            }
            else
            {
                types = typesAll;
            }

            switch (type)
            {
                case 0:
                    action = SetBodyFlag;
                    flags = maid.status.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                    break;
                case 1:
                    action = SetBodyFlagOld;
                    flags = maid.status.OldStatus.flags.ToDictionary(x => x.Key + " , " + x.Value, x => x.Key);
                    break;
                default:
                    break;
            }
            flagsKey = flags.Values.ToArray();
            flagsStats = flags.Keys.ToArray();

        }
    }


#endif
}
