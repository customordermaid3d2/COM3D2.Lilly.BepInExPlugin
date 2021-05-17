using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace COM3D2.Lilly.Plugin.Utill
{
    public class AudioManagerbak
    {
        private static AudioSourceMgr audioMgr;
        public static bool isLoaded = false;
        public static int danceVolumeLegacy;

        /// <summary>
        /// 楽曲読込
        /// </summary>
        public static bool LoadAndPlayAudioClip(string fileAndPath, bool loop = false)
        {
            Stop();

            var ext = Path.GetExtension(fileAndPath);
            if (string.IsNullOrEmpty(fileAndPath) || !File.Exists(fileAndPath) ||
                (ext != ".ogg" && ext != ".wav"))
            {
                if (!string.IsNullOrEmpty(fileAndPath))
                {
                    Debug.Log(string.Format("{0}または{1}ファイルを指定してください。{2}", ".ogg", ".wav", fileAndPath));
                }
                return false;
            }

            isLoaded = false;
            try
            {
                using (var www = new WWW(@"file:///" + fileAndPath))
                {
                    var timer = 0;
                    while (!www.isDone)
                    {
                        Thread.Sleep(100);
                        timer += 100;
                        Debug.Log("LoadAndPlayAudioClip " + timer);
                        if (10000 < timer)
                        {
                            Debug.LogWarning("音声読込タイムアウトのため処理を中止します。");
                            return false;
                        }
                    }
                    var audioClip = www.GetAudioClip();
                    if (audioClip.loadState == AudioDataLoadState.Loaded)
                    {
                        if (audioMgr == null)
                        {
                            AudioSourceMgr[] componentsInChildren = GameMain.Instance.MainCamera.gameObject.GetComponentsInChildren<AudioSourceMgr>();
                            audioMgr = componentsInChildren.FirstOrDefault(a => a.SoundType == AudioSourceMgr.Type.Bgm);
                        }
                        if (audioMgr != null)
                        {
                            audioMgr.audiosource.clip = audioClip;
                            audioMgr.audiosource.loop = loop;
                            danceVolumeLegacy = GameMain.Instance.SoundMgr.GetVolumeDance();
                            SetVolume(danceVolumeLegacy);
                            Debug.Log("GetLength " + audioMgr.audiosource.clip.length);
                            isLoaded = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.ToString());
            }
            return isLoaded;
        }

        /// <summary>
        /// 楽曲タイム設定
        /// </summary>
        public static void SetTime(float bgmTime)
        {
            audioMgr.audiosource.time = bgmTime;
        }

        /// <summary>
        /// ダンスBGMボリューム設定
        /// </summary>
        public static void SetVolume(int volume)
        {
            Debug.Log("SetVolume " + volume);
            var mgr = GameMain.Instance.SoundMgr;
            audioMgr.audiosource.outputAudioMixerGroup = mgr.mix_mgr[AudioMixerMgr.Group.Dance];
            audioMgr.audiosource.volume = volume;
            audioMgr.audiosource.mute = false;
            mgr.SetVolumeDance(volume);
            mgr.Apply();
        }

        /// <summary>
        /// 楽曲タイム取得
        /// </summary>
        public static float GetTime()
        {
            Debug.Log("GetTime " + audioMgr.audiosource.time);
            return audioMgr.audiosource.time;
        }

        /// <summary>
        /// 楽曲総タイム取得
        /// </summary>
        public static float GetLength()
        {
            Debug.Log("GetLength " + audioMgr.audiosource.clip.length);
            return audioMgr.audiosource.clip.length;
        }

        /// <summary>
        /// 楽曲再生
        /// </summary>
        public static void Play()
        {
            Debug.Log("Play ");
            if (audioMgr != null)
            {
                Debug.Log("Play ");
                GetTime();
                GameMain.Instance.SoundMgr.StopBGM(0f);
                danceVolumeLegacy = GameMain.Instance.SoundMgr.GetVolumeDance();
                audioMgr.audiosource.Play();
                SetVolume(danceVolumeLegacy);
            }
        }

        /// <summary>
        /// 楽曲停止
        /// </summary>
        public static void Stop()
        {
            if (audioMgr != null)
            {
                Debug.Log("Stop ");
                GetTime();
                audioMgr.audiosource.Stop();
            }
        }

        /// <summary>
        /// 楽曲一時停止
        /// </summary>
        public static void Pause()
        {
            if (audioMgr != null)
            {
                Debug.Log("Pause ");
                GetTime();
                audioMgr.audiosource.Pause();

            }
        }

        /// <summary>
        /// 楽曲一時停止解除
        /// </summary>
        public static void UnPause()
        {
            if (audioMgr != null)
            {
                Debug.Log("UnPause ");
                GetTime();
                audioMgr.audiosource.UnPause();
            }
        }

        public static bool isPlay()
        {
            if (audioMgr != null)
            {
                return audioMgr.audiosource.isPlaying;
            }
            return false;
        }

        public static void SetLoop(bool loop = true)
        {
            if (audioMgr != null)
            {
                audioMgr.audiosource.loop = loop;
            }
        }
    }

}
