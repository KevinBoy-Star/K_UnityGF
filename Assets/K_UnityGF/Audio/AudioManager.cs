using System;
using System.Collections;
using UnityEngine;

namespace K_UnityGF
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        private AudioSource bgAudio;            //背景音乐播放器
        private AudioSource hintAudio;          //语音提示播放器

        private void Awake()
        {
            if(Instance==null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
            bgAudio = transform.GetChild(0).GetComponent<AudioSource>();
            hintAudio = transform.GetChild(1).GetComponent<AudioSource>();
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="_bgName"></param>
        public void PlayByMusic(string _bgName)
        {
            bgAudio.clip = GetClipByName(_bgName);
            bgAudio.Play();
        }

        /// <summary>
        /// 播放语音提示
        /// </summary>
        /// <param name="_clipName"></param>
        public void PlayHintClip(string _clipName)
        {
            hintAudio.clip = GetClipByName(_clipName);
            hintAudio.Play();
        }

        /// <summary>
        /// 播放语音提示 带回调参数
        /// </summary>
        /// <param name="_clipName"></param>
        /// <param name="callBack"></param>
        public void PlayHintClip(string _clipName,Action finishCallBack)
        {
            hintAudio.clip = GetClipByName(_clipName);
            hintAudio.Play();
            StartCoroutine(ClipPlayFinish(hintAudio.clip.length, finishCallBack));
        }

        /// <summary>
        /// 语音片段完成播放
        /// </summary>
        /// <returns></returns>
        IEnumerator ClipPlayFinish(float clipLengh,Action finishCallBack)
        {
            yield return new WaitForSeconds(clipLengh);
            finishCallBack();
        }

        /// <summary>
        /// 停止背景音乐
        /// </summary>
        public void StopBgMusic() { bgAudio.Stop(); }

        /// <summary>
        /// 背景音乐静音状态
        /// </summary>
        /// <param name="_state"></param>
        public void BgMusicMuteState(bool _state) { bgAudio.mute = _state; }

        /// <summary>
        /// 提示音乐静音状态
        /// </summary>
        /// <param name="_state"></param>
        public void HintAudioMuteState(bool _state) { hintAudio.mute = _state; }

        /// <summary>
        /// 停止语音提示
        /// </summary>
        public void StopHintAuido() { hintAudio.Stop(); }

        /// <summary>
        /// 根据名称获取声音片段
        /// </summary>
        /// <param name="_clipName"></param>
        /// <returns></returns>
        private AudioClip GetClipByName(string _clipName)
        {
            return Resources.Load<AudioClip>(Paths.AudioFolder + _clipName);
        }
    }
}
