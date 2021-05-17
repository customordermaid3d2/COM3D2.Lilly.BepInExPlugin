using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.Utill
{
    public class SoundManager
    {
        private readonly SoundMgr soundMgr;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SoundManager()
        {
            soundMgr = GameMain.Instance.SoundMgr;
        }

        /// <summary>
        /// BGMタイム取得
        /// </summary>
        public float GetBgmTime()
        {
            return soundMgr.GetAudioSourceBgm().time;
        }

        /// <summary>
        /// BGMタイム設定
        /// </summary>
        public void SetBgmTime(float bgmTime)
        {
            soundMgr.GetAudioSourceBgm().time = bgmTime;
        }

        /// <summary>
        /// BGMミュート判定
        /// </summary>
        public bool IsMuteBgm()
        {
            return soundMgr.GetAudioSourceBgm().mute;
        }

        /// <summary>
        /// BGMミュート
        /// </summary>
        public void MuteBgm(bool isMute)
        {
            soundMgr.GetAudioSourceBgm().mute = isMute;
        }

        /// <summary>
        /// BGM停止
        /// </summary>
        public void StopBgm(float time)
        {
            soundMgr.StopBGM(time);
        }

        /// <summary>
        /// BGM一時停止
        /// </summary>
        public void PauseBgm()
        {
            soundMgr.GetAudioSourceBgm().Pause();
        }

        /// <summary>
        /// BGM一時停止解除
        /// </summary>
        public void UnPauseBgm()
        {
            soundMgr.GetAudioSourceBgm().UnPause();
        }

        /// <summary>
        /// BGM名取得
        /// </summary>
        public string GetClipName()
        {
            return soundMgr.GetAudioSourceBgm().clip.name;
        }

        /// <summary>
        /// BGM総タイム取得
        /// </summary>
        public float GetClipLength()
        {
            return soundMgr.GetAudioSourceBgm().clip.length;
        }

        /// <summary>
        /// BGMボリューム取得
        /// </summary>
        public int GetVolumeDance()
        {
            return soundMgr.GetVolumeDance();
        }

        /// <summary>
        /// ダンスボリューム設定
        /// </summary>
        public void SetVolumeDance(int volume, bool isDance)
        {
            var bgm = soundMgr.GetAudioSourceBgm();
            var group = isDance ? AudioMixerMgr.Group.Dance : AudioMixerMgr.Group.BGM;
            bgm.outputAudioMixerGroup = soundMgr.mix_mgr[group];
            //mgr.SetVolume(AudioSourceMgr.Type.Bgm, volume);
            soundMgr.SetVolumeDance(volume);
            soundMgr.Apply();
        }

        /// <summary>
        /// ダンスBGM再生
        /// </summary>
        public void PlayDanceBGM(string bgmName)
        {
            soundMgr.PlayDanceBGM(bgmName, 1f, false);
        }

        /// <summary>
        /// ダンスBGM再生（CM3D2）
        /// </summary>
        public void PlayBGMCM3D2Dance(string bgmName)
        {
            soundMgr.PlayBGMLegacy(bgmName, 0f, false);
        }

        /// <summary>
        /// BGM再生
        /// </summary>
        public void PlayBGM(string bgmName)
        {
            soundMgr.PlayBGM(bgmName, 1f, true);
        }

        /// <summary>
        /// SE再生
        /// </summary>
        public void PlaySe(string seName)
        {
            soundMgr.PlaySe(seName, false);
        }

        /// <summary>
        /// ランダムBGM再生
        /// </summary>
        public string GetRandomBgmName()
        {
            var bgm = COM3D2_RANDOM_BGM_INDEX;
            var index = new System.Random().Next(0, bgm.GetLength(0));
            return string.Format(FILE_BGM, bgm[index]);
        }

        public readonly static int[] COM3D2_RANDOM_BGM_INDEX =
    { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };

        public const string FILE_BGM = "BGM{0:000}.ogg";
    }

}
