using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoSingleton<SoundManager>
{
    public const float BgmVolume = 0.5f;

    public AudioSource audioSrcBG;
    public AudioSource audioSrcShort;

    private Dictionary<string, AudioClip> audioClipList = new Dictionary<string, AudioClip>();

    protected override void AwakeHandle()
    {
        audioSrcBG.pitch = 1f;
        audioSrcShort.pitch = 1f;
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="strName"></param>
    /// <param name="fVolume"></param>
    /// <param name="bLoop"></param>
    public void PlayBGM(string strName, float fVolume = 0.5f, bool bLoop = true)
    {
        AudioClip audioClip = GetAudioClip(strName);
        if (audioClip == null) return ;

        audioSrcBG.clip = audioClip;
        audioSrcBG.Stop();

        audioSrcBG.loop = bLoop;
        audioSrcBG.volume = fVolume;
        audioSrcBG.Play();
    }
    /// <summary>
    /// 播放一些时间比较短的音乐(特效)
    /// </summary>
    /// <param name="strName"></param>
    /// <param name="fVolume"></param>
    public void PlayAudioEffect(string strName, float fVolume = 1f)
    {
        AudioClip audioClip = GetAudioClip(strName);
        if (audioClip == null) return;

        audioSrcShort.clip = audioClip;
        audioSrcShort.volume = fVolume;
        audioSrcShort.Play();
    }
    /// <summary>
    /// 停止播放音效
    /// </summary>
    public void StopAudioEffect()
    {
        audioSrcBG.Stop();
        audioSrcShort.Stop();
    }

    private AudioClip GetAudioClip(string strName)
    {
        AudioClip audioClip;
        if (audioClipList.ContainsKey(strName))
        {
            audioClip = audioClipList[strName];
        }
        else
        {
            audioClip = ResourceManager.Instance.RequestLoadMusic(strName);
            if (audioClip == null) return null;

            audioClipList.Add(strName, audioClip);
        }
        return audioClip;
    }
}
