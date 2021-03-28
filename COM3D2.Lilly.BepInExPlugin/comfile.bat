rmdir /S /q obj
"C:\Windows\Microsoft.NET\Framework\v3.5\csc" /out:COM3D2.Lilly.plugin.dll ^
/t:library /lib:"F:\COM3D2\COM3D2x64_Data\Managed","F:\COM3D2\BepInEx\core","F:\COM3D2\BepInEx\plugins","F:\COM3D2\Sybaris" ^
/r:0Harmony.dll ^
/r:Assembly-CSharp.dll ^
/r:Assembly-CSharp-firstpass.dll ^
/r:BepInEx.dll ^
/r:COM3D2.API.dll ^
/r:UnityEngine.dll ^
/r:ExIni.dll ^
/r:COM3D2.ExternalSaveData.Managed.dll ^
/r:System.Threading.dll ^
/recurse:*.cs
rem pause 