using BepInEx;
using COM3D2.Lilly.Plugin.UtillGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.GUIMgr
{
    public class GUIPlugin : GUIMgr
    {
        List<BaseUnityPlugin> baseUnityPlugins = new List<BaseUnityPlugin>();

        public override void ActionsStart()
        {
            MyLog.LogMessage("GUIMaidEdit.AddStockMaid");
            baseUnityPlugins = UnityEngine.Object.FindObjectsOfType<BaseUnityPlugin>().ToList();

            //baseUnityPlugins.Remove(UnityEngine.Object.FindObjectOfType<Lilly>());
            baseUnityPlugins.Remove(Lilly.Instance);
        }

        public override void SetBody()
        {
            if (GUILayout.Button("GameObjectMgr.enabled" + ":" + GameObjectMgr.instance.enabled)) GameObjectMgr.instance.enabled = !GameObjectMgr.instance.enabled;
            //GUILayout.Label("test2");            
            //foreach (var item in baseUnityPlugins)
            //{
            //    if (GUILayout.Button(item.Info.Metadata.Name + ":" + item.Info.Instance.enabled)) item.Info.Instance.enabled = !item.Info.Instance.enabled;
            //}
            GUILayout.Label("BepInEx 플러그인 On Off 안되는것도 있음");
            foreach (var item in baseUnityPlugins)
            {
                if (GUILayout.Button(item.Info.Metadata.Name + ":" + item.enabled)) item.enabled = !item.enabled;
            }
        }


    }
}
