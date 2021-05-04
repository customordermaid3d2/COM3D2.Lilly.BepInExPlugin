using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.ToolPatch
{

    /// <summary>
    /// 작동 시점에 문제가 있음.
    /// 시바리스 로딩후
    /// 유니티인젝트 로딩후에 해야되는데
    /// BepInEx.UnityInjectorLoader.UnityInjectorLoader.Init() 에서 dll 파일 읽어온 후에 패치를 작동 시켜야함
    /// 근데 Init 작동중에 NPRShader 플러그인이 터짐
    /// UnityInjectorLoader.Logger.LogInfo("UnityInjector: No plugins found!"); 직후에 
    /// </summary>
    class NPRShaderPatch
    {
        // NPRShader

        [HarmonyPatch("COM3D2.NPRShader.Plugin.Util", "LoadALLShaders")]
        [HarmonyPrefix]
        public static bool LoadALLShaders(string ___SHADER_DIR,Dictionary<string, Shader> ___shaderList, List<AssetBundle> ___ALLShaders)
        {
            string[] files = Directory.GetFiles(___SHADER_DIR);
            foreach (string text in files)
            {
                try
                {
                    AssetBundle assetBundle = AssetBundle.LoadFromFile(text);
                    foreach (string name in assetBundle.GetAllAssetNames())
                    {
                        Material material = assetBundle.LoadAsset(name, typeof(Material)) as Material;
                        ___shaderList[material.shader.name] = material.shader;
                    }
                    ___ALLShaders.Add(assetBundle);
                }
                catch
                {
                    MyLog.LogFatal("NPRShader.LoadALLShaders warning : " + text);
                }
            }
            return false;
        }

        /*
		internal static void LoadALLShaders()
		{
			string[] files = Directory.GetFiles(Util.SHADER_DIR);
			foreach (string text in files)
			{
				try
				{
					AssetBundle assetBundle = Util.LoadALLAssetBundle(text);
					foreach (string name in assetBundle.GetAllAssetNames())
					{
						Material material = assetBundle.LoadAsset(name, typeof(Material)) as Material;
						Util.shaderList[material.shader.name] = material.shader;
					}
					Util.ALLShaders.Add(assetBundle);
				}
				catch
				{
					Console.WriteLine("NPRShader.LoadALLShaders warning : " + text);
				}
			}
		}
        */
    }
}
